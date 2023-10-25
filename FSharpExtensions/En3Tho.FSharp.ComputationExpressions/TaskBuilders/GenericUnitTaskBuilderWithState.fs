namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

type GenericUnitTaskBuilderWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResultTask, 'TInitialState, 'TExtensionsMarker
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TInitialState, 'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>
    and 'TTask :> ITaskLikeTask<'TResultTask>>(initialState: 'TInitialState) =

    inherit GenericUnitTaskBuilderBase<'TExtensionsMarker>()

    static member inline RunDynamic(
        [<InlineIfLambda>] code: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>, initialState: 'TInitialState)
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
        sm.Data.MethodBuilder <- 'TMethodBuilder.Create(initialState)
        sm.Data.MethodBuilder.Start(&sm)
        sm.Data.MethodBuilder.Task

    member _.InitialState = initialState

    member inline this.Run(
        [<InlineIfLambda>] code: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>) : 'TResultTask =
        (if __useResumableCode then
            __stateMachine<GenericUnitTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask>, 'TTask>
                (MoveNextMethodImpl<_>(fun sm ->
                    //-- RESUMABLE CODE START
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
                    //-- RESUMABLE CODE END
                ))
                (SetStateMachineMethodImpl<_>(fun sm state -> sm.Data.MethodBuilder.SetStateMachine(state)))
                (AfterCode<_,_>(fun sm ->
                    sm.Data.MethodBuilder <- 'TMethodBuilder.Create(this.InitialState)
                    sm.Data.MethodBuilder.Start(&sm)
                    sm.Data.MethodBuilder.Task))
        else
            GenericUnitTaskBuilderWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResultTask, 'TInitialState, 'TExtensionsMarker>.RunDynamic(code, this.InitialState)).Task