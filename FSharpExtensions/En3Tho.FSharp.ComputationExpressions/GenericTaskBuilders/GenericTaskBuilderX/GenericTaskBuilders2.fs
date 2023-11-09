namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder2

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

type GenericTaskBuilder2Core<'TState, 'TExtensions>(state: 'TState) =
    inherit GenericTaskBuilder2Core<'TExtensions>()
    
    member _.State = state
    
    member inline this.Bind(_: StateIntrinsic, [<InlineIfLambda>] continuation: 'TState -> ResumableCode<'TData, 'TResult>) =
        ResumableCode(fun sm ->
            (continuation this.State).Invoke(&sm)
        )

    static member inline RunDynamic<'TData, 'TResult, 'TBuilderResult
        when 'TData :> IGenericTaskStateMachineData<'TData, 'TState, 'TBuilderResult>>
        ([<InlineIfLambda>] code: ResumableCode<'TData, 'TResult>, state: 'TState) : 'TBuilderResult =

        let mutable sm = ResumableStateMachine<'TData>()
        let initialResumptionFunc = ResumptionFunc<'TData>(fun sm -> code.Invoke(&sm))
        let resumptionInfo =
            { new ResumptionDynamicInfo<'TData>(initialResumptionFunc) with
                member info.MoveNext(sm) =
                    let mutable savedExn = null
                    try
                        sm.ResumptionDynamicInfo.ResumptionData <- null
                        let step = info.ResumptionFunc.Invoke(&sm)

                        if step then
                            sm.Data.Finish(&sm)
                        else
                            let mutable awaiter = sm.ResumptionDynamicInfo.ResumptionData :?> ICriticalNotifyCompletion
                            assert not (isNull awaiter)
                            sm.Data.AwaitUnsafeOnCompleted(&awaiter, &sm)
                    with exn ->
                        savedExn <- exn
                    // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                    match savedExn with
                    | null -> ()
                    | exn -> sm.Data.SetException(exn)

                member _.SetStateMachine(sm, state) =
                    sm.Data.SetStateMachine(state)
            }

        sm.ResumptionDynamicInfo <- resumptionInfo
        'TData.Initialize(&sm, &sm.Data, state)

    member inline this.RunInternal<'TData, 'TResult, 'TBuilderResult
        when 'TData :> IGenericTaskStateMachineData<'TData, 'TState, 'TBuilderResult>>
        ([<InlineIfLambda>] code: ResumableCode<'TData, 'TResult>) : 'TBuilderResult =

        (if __useResumableCode then
            __stateMachine<'TData, 'TBuilderResult>
                (MoveNextMethodImpl<_>(fun sm ->
                    __resumeAt sm.ResumptionPoint
                    let mutable __stack_exn: Exception = null
                    try
                        let __stack_code_fin = code.Invoke(&sm)
                        if __stack_code_fin then
                            sm.Data.Finish(&sm)
                    with exn ->
                        __stack_exn <- exn
                    // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                    match __stack_exn with
                    | null -> ()
                    | exn -> sm.Data.SetException(exn)
                ))
                (SetStateMachineMethodImpl<_>(fun sm state -> sm.Data.SetStateMachine(state)))
                (AfterCode<_,_>(fun sm ->
                    'TData.Initialize(&sm, &sm.Data, this.State)))
        else
            GenericTaskBuilder2Core<'TState, 'TExtensions>.RunDynamic(code, this.State))

type GenericTaskBuilder2Base() =
    inherit GenericTaskBuilder2Core<unit, ReturnExtensions>()

type GenericTaskBuilder2WithStateBase<'TState>(state: 'TState) =
    inherit GenericTaskBuilder2Core<'TState, ReturnExtensions>(state)

type GenericTaskSeqBuilder2Base() =
    inherit GenericTaskBuilder2Core<unit, YieldExtensions>()

type GenericTaskSeqBuilder2WithStateBase<'TState>(state: 'TState) =
    inherit GenericTaskBuilder2Core<'TState, YieldExtensions>(state)