namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

type GenericUnitTaskBuilderWithState<'TExtensionsMarker, 'TState>(state: 'TState) =

    inherit GenericUnitTaskBuilderBase<'TExtensionsMarker>()

    member _.State = state
    
    member inline this.Bind(_: StateIntrinsic, [<InlineIfLambda>] continuation: 'TState -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>) =
        GenericUnitTaskCode(fun sm ->
            (continuation this.State).Invoke(&sm)
        )
    
    static member inline RunDynamic<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResultTask
        when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
        and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TState, 'TMethodBuilder>
        and 'TAwaiter :> ITaskAwaiter
        and 'TTask :> ITaskLike<'TAwaiter>
        and 'TTask :> ITaskLikeTask<'TResultTask>>(
        [<InlineIfLambda>] code: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>, state: 'TState)
        : 'TTask =
        let mutable sm = GenericUnitTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask>()
        let initialResumptionFunc = GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask>(fun sm -> code.Invoke(&sm))
        let resumptionInfo =
            { new GenericUnitTaskResumptionDynamicInfo<'TMethodBuilder, 'TAwaiter, 'TTask>(initialResumptionFunc) with
                member info.MoveNext(sm) =
                    let mutable savedExn = null
                    try
                        sm.ResumptionDynamicInfo.ResumptionData <- null
                        let step = info.ResumptionFunc.Invoke(&sm)
                        if step then
                            sm.Data.MethodBuilder.SetResult()
                        else
                            let mutable awaiter = sm.ResumptionDynamicInfo.ResumptionData :?> ICriticalNotifyCompletion
                            assert not (isNull awaiter)
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)

                    with exn ->
                        savedExn <- exn
                    // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                    match savedExn with
                    | null -> ()
                    | exn -> sm.Data.MethodBuilder.SetException exn

                member _.SetStateMachine(sm, state) =
                    sm.Data.MethodBuilder.SetStateMachine(state)
                }

        sm.ResumptionDynamicInfo <- resumptionInfo
        sm.Data.MethodBuilder <- 'TMethodBuilder.Create(state)
        sm.Data.MethodBuilder.Start(&sm)
        sm.Data.MethodBuilder.Task

    static member inline Run<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResultTask
        when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
        and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TState, 'TMethodBuilder>
        and 'TAwaiter :> ITaskAwaiter
        and 'TTask :> ITaskLike<'TAwaiter>
        and 'TTask :> ITaskLikeTask<'TResultTask>>(
        [<InlineIfLambda>] code: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>, state: 'TState) : 'TResultTask =
        (if __useResumableCode then
            __stateMachine<GenericUnitTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask>, 'TTask>
                (MoveNextMethodImpl<_>(fun sm ->
                    __resumeAt sm.ResumptionPoint
                    let mutable __stack_exn: Exception = null
                    try
                        let __stack_code_fin = code.Invoke(&sm)
                        if __stack_code_fin then
                            sm.Data.MethodBuilder.SetResult()
                    with exn ->
                        __stack_exn <- exn
                    // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                    match __stack_exn with
                    | null -> ()
                    | exn -> sm.Data.MethodBuilder.SetException exn
                ))
                (SetStateMachineMethodImpl<_>(fun sm state -> sm.Data.MethodBuilder.SetStateMachine(state)))
                (AfterCode<_,_>(fun sm ->
                    sm.Data.MethodBuilder <- 'TMethodBuilder.Create(state)
                    sm.Data.MethodBuilder.Start(&sm)
                    sm.Data.MethodBuilder.Task))
        else
            GenericUnitTaskBuilderWithState<'TExtensionsMarker, 'TState>.RunDynamic(code, state)).Task