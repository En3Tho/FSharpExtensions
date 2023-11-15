namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

[<Struct; NoComparison; NoEquality>]
type StateMachineDataWithState<'TMethodBuilder, 'TStateCheck, 'TTask, 'TState, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask, 'TResult>
    and 'TStateCheck :> IStateCheck<'TState>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable State: 'TState

    [<DefaultValue(false)>]
    val mutable Result: 'TResult

    interface IGenericTaskBuilderStateMachineDataResult<StateMachineDataWithState<'TMethodBuilder, 'TStateCheck, 'TTask, 'TState, 'TResult>, 'TResult> with

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
        member this.SetResult(result) = this.Result <- result

    interface IGenericTaskStateMachineDataWithState<StateMachineDataWithState<'TMethodBuilder, 'TStateCheck, 'TTask, 'TState, 'TResult>, 'TState> with
        member this.State = this.State

type [<Struct>] DefaultStateMachineDataWithStateInitializer<'TMethodBuilder, 'TTask, 'TState, 'TStateCheck, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TStateCheck :> IStateCheck<'TState>> =

    interface IGenericTaskBuilderStateMachineDataInitializer<StateMachineDataWithState<'TMethodBuilder, 'TStateCheck, 'TTask, 'TState, 'TResult>, 'TState, 'TTask> with
        static member Initialize(sm, data, state) =
            data.State <- state
            data.MethodBuilder <- 'TMethodBuilder.Create()
            data.MethodBuilder.Start(&sm)
            data.MethodBuilder.Task