﻿namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

[<Struct; NoComparison; NoEquality>]
type StateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable Result: 'TResult

    interface IGenericTaskBuilderStateMachineDataInitializer<StateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>, unit, 'TBuilderResult> with
        member this.Initialize(stateMachine, _) =
            this.MethodBuilder <- 'TMethodBuilder.Create()
            this.MethodBuilder.Start(&stateMachine)
            this.MethodBuilder.Task.Task

    interface IGenericTaskStateMachineData<StateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>, unit> with

        member this.CheckCanContinueOrThrow() = true
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.Finish(sm) = this.MethodBuilder.SetResult(this.Result)
        member this.SetException(``exception``: exn) = this.MethodBuilder.SetException(``exception``)
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)

    interface IGenericTaskBuilderStateMachineDataResult<'TResult> with
        member this.SetResult(result) = this.Result <- result