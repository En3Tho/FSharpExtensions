namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

[<Struct; NoComparison; NoEquality>]
type UnitStateMachineData<'TMethodBuilder, 'TTask
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    interface IStateMachineData<UnitStateMachineData<'TMethodBuilder, 'TTask>, unit> with
        member this.CheckCanContinueOrThrow() = true
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.Finish(sm) = this.MethodBuilder.SetResult()
        member this.SetException(``exception``: exn) = this.MethodBuilder.SetException(``exception``)
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)
        member this.SetResult(_) = ()

type [<Struct>] DefaultUnitStateMachineDataInitializer<'TMethodBuilder, 'TTask
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>> =
    interface IStateMachineDataInitializer<UnitStateMachineData<'TMethodBuilder, 'TTask>, unit, 'TTask> with
        static member Initialize(sm, data, _) =
            data.MethodBuilder <- 'TMethodBuilder.Create()
            data.MethodBuilder.Start(&sm)
            data.MethodBuilder.Task