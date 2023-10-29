namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

type GenericTaskBuilderWithState<'TExtensionsMarker, 'TState>(state: 'TState) =
    
    inherit GenericTaskBuilderBase<'TExtensionsMarker>()
    
    member _.State = state
    
    member inline this.Bind(_: StateIntrinsic, [<InlineIfLambda>] continuation: 'TState -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>) =
        GenericTaskCode(fun sm ->
            (continuation this.State).Invoke(&sm)
        )
    
    static member inline RunDynamic<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResultTask
        when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
        and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TState, 'TMethodBuilder>
        and 'TAwaiter :> ITaskAwaiter<'TOverall>
        and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>
        and 'TTask :> ITaskLikeTask<'TResultTask>>
        ([<InlineIfLambda>] code: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>, state: 'TState) : 'TTask =
        let mutable sm = GenericTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>()
        let initialResumptionFunc = GenericTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>(fun sm -> code.Invoke(&sm))
        let resumptionInfo =
            { new GenericTaskResumptionDynamicInfo<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>(initialResumptionFunc) with
                member info.MoveNext(sm) =
                    let mutable savedExn = null
                    try
                        sm.ResumptionDynamicInfo.ResumptionData <- null
                        let step = info.ResumptionFunc.Invoke(&sm)
                        if step then
                            sm.Data.MethodBuilder.SetResult(sm.Data.Result)
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

    static member inline Run<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResultTask
        when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
        and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TState, 'TMethodBuilder>
        and 'TAwaiter :> ITaskAwaiter<'TOverall>
        and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>
        and 'TTask :> ITaskLikeTask<'TResultTask>>
        ([<InlineIfLambda>] code: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>, state: 'TState) : 'TResultTask =
        (if __useResumableCode then
            __stateMachine<GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>, 'TTask>
                (MoveNextMethodImpl<_>(fun sm ->
                    //-- RESUMABLE CODE START
                    __resumeAt sm.ResumptionPoint
                    let mutable __stack_exn: Exception = null
                    try
                        let __stack_code_fin = code.Invoke(&sm)
                        if __stack_code_fin then
                            sm.Data.MethodBuilder.SetResult(sm.Data.Result)
                    with exn ->
                        __stack_exn <- exn
                    // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                    match __stack_exn with
                    | null -> ()
                    | exn -> sm.Data.MethodBuilder.SetException exn
                    //-- RESUMABLE CODE END
                ))
                (SetStateMachineMethodImpl<_>(fun sm state -> sm.Data.MethodBuilder.SetStateMachine(state)))
                (AfterCode<_,_>(fun sm ->
                    sm.Data.MethodBuilder <- 'TMethodBuilder.Create(state)
                    sm.Data.MethodBuilder.Start(&sm)
                    sm.Data.MethodBuilder.Task))
        else
            GenericTaskBuilderWithState<'TExtensionsMarker, 'TState>.RunDynamic(code, state)).Task