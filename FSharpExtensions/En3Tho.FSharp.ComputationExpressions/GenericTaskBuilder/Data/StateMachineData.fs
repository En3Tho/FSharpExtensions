namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

[<Struct; NoComparison; NoEquality>]
type StateMachineData<'TMethodBuilder, 'TTask, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask, 'TResult>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable Result: 'TResult

    interface IGenericTaskStateMachineData<StateMachineData<'TMethodBuilder, 'TTask, 'TResult>, 'TResult> with

        member this.CheckCanContinueOrThrow() = true
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.Finish(sm) = this.MethodBuilder.SetResult(this.Result)
        member this.SetException(``exception``: exn) = this.MethodBuilder.SetException(``exception``)
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)
        member this.SetResult(result) = this.Result <- result

type [<Struct>] DefaultStateMachineDataInitializer<'TMethodBuilder, 'TTask, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>> =

    interface IGenericTaskStateMachineDataInitializer<StateMachineData<'TMethodBuilder, 'TTask, 'TResult>, unit, 'TTask> with
        static member Initialize(sm, data, _) =
            data.MethodBuilder <- 'TMethodBuilder.Create()
            data.MethodBuilder.Start(&sm)
            data.MethodBuilder.Task

// TODO: Do I even need this?
[<AbstractClass; NoComparison; NoEquality>]
type StateMachineRefDataBase<'TMethodBuilder, 'TTask, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask, 'TResult>>() =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable Result: 'TResult

    abstract member MoveNext: unit -> unit

    interface IAsyncStateMachine with
        member this.MoveNext() = this.MoveNext()
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)

    interface IGenericTaskStateMachineData<StateMachineRefDataBase<'TMethodBuilder, 'TTask, 'TResult>, 'TResult> with

        member this.CheckCanContinueOrThrow() = true
        member this.AwaitOnCompleted(awaiter, arg) =
            let mutable this = this
            this.MethodBuilder.AwaitOnCompleted(&awaiter, &this)
        member this.AwaitUnsafeOnCompleted(awaiter, arg) =
            let mutable this = this
            this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &this)
        member this.Finish(sm) =
            this.MethodBuilder.SetResult(this.Result)
        member this.SetException(``exception``: exn) = this.MethodBuilder.SetException(``exception``)
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)
        member this.SetResult(result) = this.Result <- result