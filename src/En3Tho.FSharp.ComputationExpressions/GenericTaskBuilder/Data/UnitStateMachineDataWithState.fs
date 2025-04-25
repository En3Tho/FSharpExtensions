namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

[<Struct; NoComparison; NoEquality>]
type UnitStateMachineDataWithState<'TMethodBuilder, 'TStateCheck, 'TTask, 'TState
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask>
    and 'TStateCheck :> IStateCheck<'TState>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable State: 'TState

    interface IStateMachineData<UnitStateMachineDataWithState<'TMethodBuilder, 'TStateCheck, 'TTask, 'TState>, unit> with

        member this.CheckCanContinueOrThrow() =
            if 'TStateCheck.CanCheckState then
                'TStateCheck.CheckState(&this.State)
            else
                true

        member this.Finish(sm) =
            if 'TStateCheck.CanProcessSuccess then
                'TStateCheck.ProcessSuccess(&this.State)
            this.MethodBuilder.SetResult()

        member this.SetException(``exception``: exn) =
            if 'TStateCheck.CanProcessException then
                'TStateCheck.ProcessException(&this.State, ``exception``)
            this.MethodBuilder.SetException(``exception``)

        member this.AwaitOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)
        member this.SetResult(_) = ()

    interface IStateMachineDataWithState<UnitStateMachineDataWithState<'TMethodBuilder, 'TStateCheck, 'TTask, 'TState>, 'TState> with
        member this.State = this.State

type [<Struct>] DefaultUnitStateMachineDataWithStateInitializer<'TMethodBuilder, 'TTask, 'TState, 'TStateCheck
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TStateCheck :> IStateCheck<'TState>> =

    interface IStateMachineDataInitializer<UnitStateMachineDataWithState<'TMethodBuilder, 'TStateCheck, 'TTask, 'TState>, 'TState, 'TTask> with
        static member Initialize(sm, data, state) =
            data.State <- state
            data.MethodBuilder <- 'TMethodBuilder.Create()
            data.MethodBuilder.Start(&sm)
            data.MethodBuilder.Task