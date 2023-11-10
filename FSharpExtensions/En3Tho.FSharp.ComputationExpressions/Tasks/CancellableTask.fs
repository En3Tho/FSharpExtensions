namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open System.Runtime.CompilerServices
open System.Threading
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

// TODO: what about loops? For/While?

type [<Struct>] CancellableTaskAwaiter<'a> =
    val mutable private awaiter: TaskAwaiter<'a>
    new(awaiter) = { awaiter = awaiter }

    member this.IsCompleted = this.awaiter.IsCompleted
    member this.GetResult() = this.awaiter.GetResult()
    member this.OnCompleted(continuation) = this.awaiter.OnCompleted(continuation)
    member this.UnsafeOnCompleted(continuation) = this.awaiter.UnsafeOnCompleted(continuation)

    interface ITaskAwaiter<'a> with
        member this.IsCompleted = this.IsCompleted
        member this.GetResult() = this.GetResult()
        member this.OnCompleted(continuation) = this.OnCompleted(continuation)
        member this.UnsafeOnCompleted(continuation) = this.UnsafeOnCompleted(continuation)

and [<Struct>] CancellableTaskMethodBuilder<'a> =
    val mutable private builder: AsyncTaskMethodBuilder<'a>
    val mutable private cancellationToken: CancellationToken

    new(builder, cancellationToken) = {
        builder = builder
        cancellationToken = cancellationToken
    }

    static member Create(activity) = CancellableTaskMethodBuilder(AsyncTaskMethodBuilder<'a>.Create(), activity)

    member this.SetException(exn: exn) =
        this.builder.SetException(exn)

    member this.SetResult(data) =
        this.builder.SetResult(data)

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = CancellableTask(this.builder.Task)

    interface IAsyncMethodBuilderCreator<CancellationToken, CancellableTaskMethodBuilder<'a>> with
        static member Create(state) = CancellableTaskMethodBuilder<'a>.Create(state)

    interface IAsyncMethodBuilder<CancellableTaskAwaiter<'a>, CancellableTask<'a>, 'a> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult(data) = this.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Struct>] CancellableTask<'a> =
    val mutable private task: Task<'a>
    new(task) = { task = task }

    member this.GetAwaiter() = CancellableTaskAwaiter(this.task.GetAwaiter())
    member this.Task = this.task

    interface ITaskLike<CancellableTaskAwaiter<'a>, 'a> with
        member this.GetAwaiter() = CancellableTaskAwaiter(this.task.GetAwaiter())

    interface ITaskLikeTask<CancellableTask<'a>> with
        member this.Task = this

type [<Struct>] CancellableTaskAwaiter =
    val mutable private awaiter: TaskAwaiter
    new(awaiter) = { awaiter = awaiter }

    member this.IsCompleted = this.awaiter.IsCompleted
    member this.GetResult() = this.awaiter.GetResult()
    member this.OnCompleted(continuation) = this.awaiter.OnCompleted(continuation)
    member this.UnsafeOnCompleted(continuation) = this.awaiter.UnsafeOnCompleted(continuation)

    interface ITaskAwaiter with
        member this.IsCompleted = this.IsCompleted
        member this.GetResult() = this.GetResult()
        member this.OnCompleted(continuation) = this.OnCompleted(continuation)
        member this.UnsafeOnCompleted(continuation) = this.UnsafeOnCompleted(continuation)

and [<Struct>] CancellableTaskMethodBuilder =
    val mutable private builder: AsyncTaskMethodBuilder
    val mutable private cancellationToken: CancellationToken
    new(builder, cancellationToken) = {
        builder = builder
        cancellationToken = cancellationToken
    }

    static member Create(activityName) = CancellableTaskMethodBuilder(AsyncTaskMethodBuilder.Create(), activityName)

    member this.SetException(exn: exn) =
        this.builder.SetException(exn)

    member this.SetResult() =
        this.builder.SetResult()

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = CancellableTask(this.builder.Task)

    interface IAsyncMethodBuilderCreator<CancellationToken, CancellableTaskMethodBuilder> with
        static member Create(state) = CancellableTaskMethodBuilder.Create(state)

    interface IAsyncMethodBuilder<CancellableTaskAwaiter, CancellableTask> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult() = this.builder.SetResult()
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Struct>] CancellableTask =
    val mutable private task: Task
    new(task) = { task = task }

    member this.GetAwaiter() = CancellableTaskAwaiter(this.task.GetAwaiter())

    interface ITaskLike<CancellableTaskAwaiter> with
        member this.GetAwaiter() = this.GetAwaiter()

    interface ITaskLikeTask<CancellableTask> with
        member this.Task = this