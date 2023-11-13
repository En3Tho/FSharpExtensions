namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2.Tasks

open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type [<Struct>] TaskAwaiterWrapper<'a> =
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

and [<Struct>] AsyncTaskMethodBuilderWrapper<'a> =
    val mutable private builder: AsyncTaskMethodBuilder<'a>
    new(builder) = { builder = builder }

    static member Create() = AsyncTaskMethodBuilderWrapper(AsyncTaskMethodBuilder<'a>.Create())
    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetException(``exception``) = this.builder.SetException(``exception``)
    member this.SetResult(data) = this.builder.SetResult(data)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = TaskWrapper(this.builder.Task)

    interface IAsyncMethodBuilderCreator<AsyncTaskMethodBuilderWrapper<'a>> with
        static member Create() = AsyncTaskMethodBuilderWrapper.Create()

    interface IAsyncMethodBuilder<TaskAwaiterWrapper<'a>, TaskWrapper<'a>, 'a> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult(data) = this.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Struct>] TaskWrapper<'a> =
    val mutable private task: Task<'a>
    new(task) = { task = task }

    member this.GetAwaiter() = TaskAwaiterWrapper(this.task.GetAwaiter())

    interface ITaskLike<TaskAwaiterWrapper<'a>, 'a> with
        member this.GetAwaiter() = this.GetAwaiter()

    interface ITaskLikeTask<Task<'a>> with
        member this.Task = this.task

type [<Struct>] TaskAwaiterWrapper(awaiter: TaskAwaiter) =

    member _.IsCompleted = awaiter.IsCompleted
    member _.GetResult() = awaiter.GetResult()
    member _.OnCompleted(continuation) = awaiter.OnCompleted(continuation)
    member _.UnsafeOnCompleted(continuation) = awaiter.UnsafeOnCompleted(continuation)

    interface ITaskAwaiter with
        member this.IsCompleted = this.IsCompleted
        member this.GetResult() = this.GetResult()
        member this.OnCompleted(continuation) = this.OnCompleted(continuation)
        member this.UnsafeOnCompleted(continuation) = this.UnsafeOnCompleted(continuation)

and [<Struct>] AsyncTaskMethodBuilderWrapper =
    val mutable private builder: AsyncTaskMethodBuilder
    new(builder) = { builder = builder }

    static member Create() = AsyncTaskMethodBuilderWrapper(AsyncTaskMethodBuilder.Create())
    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetException(``exception``) = this.builder.SetException(``exception``)
    member this.SetResult() = this.builder.SetResult()
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = TaskWrapper(this.builder.Task)

    interface IAsyncMethodBuilderCreator<AsyncTaskMethodBuilderWrapper> with
        static member Create() = AsyncTaskMethodBuilderWrapper.Create()

    interface IAsyncMethodBuilder<TaskAwaiterWrapper, TaskWrapper> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult() = this.SetResult()
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Struct>] TaskWrapper =
    val mutable private task: Task
    new(task) = { task = task }

    member this.GetAwaiter() = TaskAwaiterWrapper(this.task.GetAwaiter())

    interface ITaskLike<TaskAwaiterWrapper> with
        member this.GetAwaiter() = this.GetAwaiter()

    interface ITaskLikeTask<Task> with
        member this.Task = this.task