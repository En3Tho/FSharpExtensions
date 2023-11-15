namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2

open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

[<Struct; NoComparison; NoEquality>]
type StateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable Result: 'TResult

    interface IGenericTaskBuilderStateMachineDataResult<StateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>, 'TResult> with

        member this.CheckCanContinueOrThrow() = true
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.Finish(sm) = this.MethodBuilder.SetResult(this.Result)
        member this.SetException(``exception``: exn) = this.MethodBuilder.SetException(``exception``)
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)
        member this.SetResult(result) = this.Result <- result

type [<Struct>] DefaultStateMachineDataInitializer<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>> =

    interface IGenericTaskBuilderStateMachineDataInitializer<StateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>, unit, 'TBuilderResult> with
        static member Initialize(sm, data, _) =
            data.MethodBuilder <- 'TMethodBuilder.Create()
            data.MethodBuilder.Start(&sm)
            data.MethodBuilder.Task.Task

[<AbstractClass; NoComparison; NoEquality>]
type StateMachineRefDataBase<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>>() =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable Result: 'TResult

    abstract member MoveNext: unit -> unit

    interface IAsyncStateMachine with
        member this.MoveNext() = this.MoveNext()
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)

    interface IGenericTaskBuilderStateMachineDataResult<StateMachineRefDataBase<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>, 'TResult> with

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