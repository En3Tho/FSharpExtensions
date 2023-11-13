namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2

open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open UnsafeEx

[<Struct; NoComparison; NoEquality>]
type StateMachineDataWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TState, 'TStateCheck, 'TResult, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>
    and 'TStateCheck :> IStateCheck<'TState>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable State: 'TState

    [<DefaultValue(false)>]
    val mutable Result: 'TResult

    interface IGenericTaskStateMachineData<StateMachineDataWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TState, 'TStateCheck, 'TResult, 'TBuilderResult>> with

        member this.CheckCanContinueOrThrow() =
            if 'TStateCheck.CanCheckState then
                'TStateCheck.CheckState(&this.State)
            else
                true

        member this.Finish(sm) =
            if 'TStateCheck.CanProcessSuccess then
                'TStateCheck.ProcessSuccess(&this.State)
            this.MethodBuilder.SetResult(this.Result)

        member this.SetException(``exception``: exn) =
            if 'TStateCheck.CanProcessException then
                'TStateCheck.ProcessException(&this.State, ``exception``)
            this.MethodBuilder.SetException(``exception``)

        member this.AwaitOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)

    interface IGenericTaskStateMachineDataWithState<StateMachineDataWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TState, 'TStateCheck, 'TResult, 'TBuilderResult>, 'TState> with
        member this.State = this.State

    interface IGenericTaskBuilderStateMachineDataResult<'TResult> with
        member this.SetResult(result) = this.Result <- result

type [<Struct>] DefaultStateMachineDataWithStateInitializer<'TMethodBuilder, 'TAwaiter, 'TTask, 'TState, 'TStateCheck, 'TResult, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>
    and 'TStateCheck :> IStateCheck<'TState>> =

    interface IGenericTaskBuilderStateMachineDataInitializer<StateMachineDataWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TState, 'TStateCheck, 'TResult, 'TBuilderResult>, 'TState, 'TBuilderResult> with
        static member Initialize(sm, data, state) =
            data.State <- state
            data.MethodBuilder <- 'TMethodBuilder.Create()
            data.MethodBuilder.Start(&sm)
            data.MethodBuilder.Task.Task