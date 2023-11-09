namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

[<Struct; NoComparison; NoEquality>]
type UnitStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    interface IGenericTaskBuilderStateMachineDataInitializer<UnitStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TBuilderResult>, unit, 'TBuilderResult> with
        member this.Initialize(stateMachine, _) =
            this.MethodBuilder <- 'TMethodBuilder.Create()
            this.MethodBuilder.Start(&stateMachine)
            this.MethodBuilder.Task.Task

    interface IGenericTaskStateMachineData<UnitStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TBuilderResult>, unit> with
        member this.CheckCanContinueOrThrow() = true
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.Finish(sm) = this.MethodBuilder.SetResult()
        member this.SetException(``exception``: exn) = this.MethodBuilder.SetException(``exception``)
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)