namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

type GenericTaskBuilderCore<'TState, 'TExtensions>(state: 'TState) =
    inherit GenericTaskBuilderCore<'TExtensions>()
    
    member _.State = state
    
    member inline this.Bind<'TData, 'TResult when 'TData :> IGenericTaskStateMachineDataWithState<'TData, 'TState>>(_: StateIntrinsic, [<InlineIfLambda>] continuation: 'TState -> ResumableCode<'TData, 'TResult>) =
        ResumableCode<'TData, 'TResult>(fun sm ->
            let state: 'TState = sm.Data.State
            (continuation state).Invoke(&sm)
        )

    static member inline RunDynamic<'TData, 'TResult, 'TBuilderResult, 'TInitializer
        when 'TData :> IGenericTaskStateMachineData<'TData>
        and 'TInitializer :> IGenericTaskBuilderStateMachineDataInitializer<'TData, 'TState, 'TBuilderResult>>
        ([<InlineIfLambda>] code: ResumableCode<'TData, 'TResult>, state: 'TState) : 'TBuilderResult =

        let mutable sm = ResumableStateMachine<'TData>()
        let initialResumptionFunc = ResumptionFunc<'TData>(fun sm -> code.Invoke(&sm))
        let resumptionInfo =
            { new ResumptionDynamicInfo<'TData>(initialResumptionFunc) with
                member info.MoveNext(sm) =
                    let mutable savedExn = null
                    if sm.Data.CheckCanContinueOrThrow() then
                        try
                            sm.ResumptionDynamicInfo.ResumptionData <- null
                            let step = info.ResumptionFunc.Invoke(&sm)

                            if step then
                                sm.ResumptionPoint <- -1
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
                        | exn ->
                            sm.ResumptionPoint <- -1
                            sm.Data.SetException(exn)
                    else
                        sm.ResumptionPoint <- -1
                        sm.Data.Finish(&sm)

                member _.SetStateMachine(sm, state) =
                    sm.Data.SetStateMachine(state)
            }

        sm.ResumptionDynamicInfo <- resumptionInfo
        'TInitializer.Initialize<_, 'TData, 'TState>(&sm, &sm.Data, state)

    member inline this.RunInternal<'TData, 'TResult, 'TBuilderResult, 'TInitializer
        when 'TData :> IGenericTaskStateMachineData<'TData>
        and 'TInitializer :> IGenericTaskBuilderStateMachineDataInitializer<'TData, 'TState, 'TBuilderResult>>
        ([<InlineIfLambda>] code: ResumableCode<'TData, 'TResult>) : 'TBuilderResult =

        (if __useResumableCode then
            __stateMachine<'TData, 'TBuilderResult>
                (MoveNextMethodImpl<_>(fun sm ->
                    __resumeAt sm.ResumptionPoint
                    let mutable __stack_exn: Exception = null
                    try
                        if sm.Data.CheckCanContinueOrThrow() then
                            let __stack_code_fin = code.Invoke(&sm)
                            if __stack_code_fin then
                                sm.ResumptionPoint <- -1
                                sm.Data.Finish(&sm)
                        else
                            sm.ResumptionPoint <- -1
                            sm.Data.Finish(&sm)
                    with exn ->
                        __stack_exn <- exn
                    // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                    match __stack_exn with
                    | null -> ()
                    | exn ->
                        sm.ResumptionPoint <- -1
                        sm.Data.SetException(exn)
                ))
                (SetStateMachineMethodImpl<_>(fun sm state -> sm.Data.SetStateMachine(state)))
                (AfterCode<_,_>(fun sm ->
                    'TInitializer.Initialize<_, 'TData, 'TState>(&sm, &sm.Data, this.State)))
        else
            GenericTaskBuilderCore<'TState, 'TExtensions>.RunDynamic(code, this.State))

type GenericTaskBuilderBase() =
    inherit GenericTaskBuilderCore<unit, ReturnExtensions>()

type GenericTaskBuilderWithStateBase<'TState>(state: 'TState) =
    inherit GenericTaskBuilderCore<'TState, ReturnExtensions>(state)

type GenericTaskSeqBuilderBase() =
    inherit GenericTaskBuilderCore<unit, YieldExtensions>()

// type GenericTaskSeqBuilderWithStateBase<'TState>(state: 'TState) =
//     inherit GenericTaskBuilderCore<'TState, YieldExtensions>(state)