namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System.Runtime.CompilerServices
open System.Threading.Tasks

type [<Struct>] ValueTaskWrapperAwaiter<'a> =
    val mutable private awaiter: ValueTaskAwaiter<'a>
    new(awaiter) = { awaiter = awaiter }

    member this.IsCompleted = this.awaiter.IsCompleted
    member this.GetResult() = this.awaiter.GetResult()
    member this.OnCompleted(continuation) = this.awaiter.OnCompleted(continuation)
    member this.UnsafeOnCompleted(continuation) = this.awaiter.UnsafeOnCompleted(continuation)

    interface ITaskAwaiter<'a> with
        member this.IsCompleted = this.awaiter.IsCompleted
        member this.GetResult() = this.awaiter.GetResult()
        member this.OnCompleted(continuation) = this.awaiter.OnCompleted(continuation)
        member this.UnsafeOnCompleted(continuation) = this.awaiter.UnsafeOnCompleted(continuation)

and [<Struct>] ValueTaskWrapperMethodBuilder<'a> =
    val mutable private builder: AsyncValueTaskMethodBuilder<'a>
    new(builder) = { builder = builder }

    static member Create() = ValueTaskWrapperMethodBuilder(AsyncValueTaskMethodBuilder<'a>.Create())
    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetException(``exception``) = this.builder.SetException(``exception``)
    member this.SetResult(data) = this.builder.SetResult(data)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = ValueTaskWrapper(this.builder.Task)

    interface IAsyncMethodBuilder<ValueTaskWrapperMethodBuilder<'a>, ValueTaskWrapperAwaiter<'a>, ValueTaskWrapper<'a>, 'a> with
        static member Create() = ValueTaskWrapperMethodBuilder(AsyncValueTaskMethodBuilder<'a>.Create())
        member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
            this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
            this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.builder.SetException(``exception``)
        member this.SetResult(data) = this.builder.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
        member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
        member this.Task = ValueTaskWrapper(this.builder.Task)

and [<Struct>] ValueTaskWrapper<'a> =
    val mutable private task: ValueTask<'a>
    new(task) = { task = task }

    member this.GetAwaiter() = ValueTaskWrapperAwaiter(this.task.GetAwaiter())
    member this.Task = this.task

    interface ITaskLike<ValueTaskWrapperAwaiter<'a>, 'a> with
        member this.GetAwaiter() = ValueTaskWrapperAwaiter(this.task.GetAwaiter())

    interface ITaskLikeTask<ValueTask<'a>> with
        member this.Task = this.task

type [<Struct>] ValueTaskWrapperAwaiter =
    val mutable private awaiter: ValueTaskAwaiter
    new(awaiter) = { awaiter = awaiter }

    member this.IsCompleted = this.awaiter.IsCompleted
    member this.GetResult() = this.awaiter.GetResult()
    member this.OnCompleted(continuation) = this.awaiter.OnCompleted(continuation)
    member this.UnsafeOnCompleted(continuation) = this.awaiter.UnsafeOnCompleted(continuation)

    interface ITaskAwaiter with
        member this.IsCompleted = this.awaiter.IsCompleted
        member this.GetResult() = this.awaiter.GetResult()
        member this.OnCompleted(continuation) = this.awaiter.OnCompleted(continuation)
        member this.UnsafeOnCompleted(continuation) = this.awaiter.UnsafeOnCompleted(continuation)

and [<Struct>] ValueTaskWrapperMethodBuilder =
    val mutable private builder: AsyncValueTaskMethodBuilder
    new(builder) = { builder = builder }

    static member Create() = ValueTaskWrapperMethodBuilder(AsyncValueTaskMethodBuilder.Create())
    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetException(``exception``) = this.builder.SetException(``exception``)
    member this.SetResult() = this.builder.SetResult()
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = ValueTaskWrapper(this.builder.Task)

    interface IAsyncMethodBuilder<ValueTaskWrapperMethodBuilder, ValueTaskWrapperAwaiter, ValueTaskWrapper> with
        static member Create() = ValueTaskWrapperMethodBuilder(AsyncValueTaskMethodBuilder.Create())
        member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
            this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
            this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.builder.SetException(``exception``)
        member this.SetResult() = this.builder.SetResult()
        member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
        member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
        member this.Task = ValueTaskWrapper(this.builder.Task)

and [<Struct>] ValueTaskWrapper =
    val mutable private task: ValueTask
    new(task) = { task = task }

    member this.GetAwaiter() = ValueTaskWrapperAwaiter(this.task.GetAwaiter())
    member this.Task = this.task

    interface ITaskLike<ValueTaskWrapperAwaiter> with
        member this.GetAwaiter() = ValueTaskWrapperAwaiter(this.task.GetAwaiter())

    interface ITaskLikeTask<ValueTask> with
        member this.Task = this.task