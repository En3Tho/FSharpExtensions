namespace En3Tho.FSharp.ComputationExpressions.TaskBuilders.ExnResultValueTask

open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type [<Struct>] ExnResultValueTaskAwaiter<'a> =
    val mutable private awaiter: ValueTaskAwaiter<Result<'a, exn>>
    new(awaiter) = { awaiter = awaiter }

    member this.IsCompleted = this.awaiter.IsCompleted
    member this.GetResult() = this.awaiter.GetResult()
    member this.OnCompleted(continuation) = this.awaiter.OnCompleted(continuation)
    member this.UnsafeOnCompleted(continuation) = this.awaiter.UnsafeOnCompleted(continuation)

    interface ITaskAwaiter<Result<'a, exn>> with
        member this.IsCompleted = this.IsCompleted
        member this.GetResult() = this.GetResult()
        member this.OnCompleted(continuation) = this.OnCompleted(continuation)
        member this.UnsafeOnCompleted(continuation) = this.UnsafeOnCompleted(continuation)

and [<Struct>] ExnResultValueTaskMethodBuilder<'a> =
    val mutable private builder: AsyncValueTaskMethodBuilder<Result<'a, exn>>
    new(builder) = { builder = builder }

    static member Create() = ExnResultValueTaskMethodBuilder(AsyncValueTaskMethodBuilder<Result<'a, exn>>.Create())
    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetException(``exception``) = this.builder.SetResult(Error ``exception``)
    member this.SetResult(data) = this.builder.SetResult(data)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = ExnResultValueTask(this.builder.Task)

    interface IAsyncMethodBuilder<ExnResultValueTaskMethodBuilder<'a>, ExnResultValueTaskAwaiter<'a>, ExnResultValueTask<'a>, Result<'a, exn>> with
        static member Create() = ExnResultValueTaskMethodBuilder.Create()
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult(data) = this.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Struct>] ExnResultValueTask<'a> =
    val mutable private task: ValueTask<Result<'a, exn>>
    new(task) = { task = task }

    member this.GetAwaiter() = ExnResultValueTaskAwaiter(this.task.GetAwaiter())
    member this.Task = this.task

    interface ITaskLike<ExnResultValueTaskAwaiter<'a>, Result<'a, exn>> with
        member this.GetAwaiter() = ExnResultValueTaskAwaiter(this.task.GetAwaiter())

    interface ITaskLikeTask<ExnResultValueTask<'a>> with
        member this.Task = this

type [<Struct>] ExnResultValueTaskAwaiter =
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

and [<Struct>] ExnResultValueTaskMethodBuilder =
    val mutable private builder: AsyncValueTaskMethodBuilder
    new(builder) = { builder = builder }

    static member Create() = ExnResultValueTaskMethodBuilder(AsyncValueTaskMethodBuilder.Create())
    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetException(``exception``) = this.builder.SetException(``exception``)
    member this.SetResult() = this.builder.SetResult()
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = ExnResultValueTask(this.builder.Task)

    interface IAsyncMethodBuilder<ExnResultValueTaskMethodBuilder, ExnResultValueTaskAwaiter, ExnResultValueTask> with
        static member Create() = ExnResultValueTaskMethodBuilder(AsyncValueTaskMethodBuilder.Create())
        member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
            this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
            this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.builder.SetException(``exception``)
        member this.SetResult() = this.builder.SetResult()
        member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
        member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
        member this.Task = ExnResultValueTask(this.builder.Task)

and [<Struct>] ExnResultValueTask =
    val mutable private task: ValueTask
    new(task) = { task = task }

    member this.GetAwaiter() = ExnResultValueTaskAwaiter(this.task.GetAwaiter())
    member this.Task = this.task

    interface ITaskLike<ExnResultValueTaskAwaiter> with
        member this.GetAwaiter() = ExnResultValueTaskAwaiter(this.task.GetAwaiter())

    interface ITaskLikeTask<ValueTask> with
        member this.Task = this.task