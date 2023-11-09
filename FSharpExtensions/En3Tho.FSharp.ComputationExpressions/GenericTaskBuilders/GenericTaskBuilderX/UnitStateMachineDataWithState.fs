﻿namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

[<Struct; NoComparison; NoEquality>]
type UnitStateMachineDataWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TState, 'TStateCheck, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>
    and 'TStateCheck :> IStateCheck<'TState>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable State: 'TState

    interface IGenericTaskBuilderStateMachineDataInitializer<UnitStateMachineDataWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TState, 'TStateCheck, 'TBuilderResult>, 'TState, 'TBuilderResult> with
        member this.Initialize(stateMachine, state) =
            this.State <- state
            this.MethodBuilder <- 'TMethodBuilder.Create()
            this.MethodBuilder.Start(&stateMachine)
            this.MethodBuilder.Task.Task

    interface IGenericTaskStateMachineData<UnitStateMachineDataWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TState, 'TStateCheck, 'TBuilderResult>, 'TState> with

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