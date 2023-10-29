namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open System.Diagnostics
open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type [<Struct>] ActivityTaskAwaiter<'a> =
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

and [<Struct>] ActivityTaskMethodBuilder<'a> =
    val mutable private builder: AsyncTaskMethodBuilder<'a>
    val mutable private activity: Activity

    new(builder, activity) = {
        builder = builder
        activity = activity
    }

    static member Create(activity) = ActivityTaskMethodBuilder(AsyncTaskMethodBuilder<'a>.Create(), activity)

    member this.SetException(exn: exn) =
        match this.activity with
        | null -> ()
        | activity ->
            activity.SetStatus(ActivityStatusCode.Error, exn.Message).Dispose()

        this.builder.SetException(exn)

    member this.SetResult(data) =
        match this.activity with
        | null -> ()
        | activity ->
            activity.SetStatus(ActivityStatusCode.Ok).Dispose()

        this.builder.SetResult(data)

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = ActivityTask(this.builder.Task)

    interface IAsyncMethodBuilderCreator<Activity, ActivityTaskMethodBuilder<'a>> with
        static member Create(state) = ActivityTaskMethodBuilder<'a>.Create(state)

    interface IAsyncMethodBuilder<ActivityTaskAwaiter<'a>, ActivityTask<'a>, 'a> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult(data) = this.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Struct>] ActivityTask<'a> =
    val mutable private task: Task<'a>
    new(task) = { task = task }

    member this.GetAwaiter() = ActivityTaskAwaiter(this.task.GetAwaiter())
    member this.Task = this.task

    interface ITaskLike<ActivityTaskAwaiter<'a>, 'a> with
        member this.GetAwaiter() = ActivityTaskAwaiter(this.task.GetAwaiter())

    interface ITaskLikeTask<ActivityTask<'a>> with
        member this.Task = this

type [<Struct>] ActivityTaskAwaiter =
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

and [<Struct>] ActivityTaskMethodBuilder =
    val mutable private builder: AsyncTaskMethodBuilder
    val mutable private activity: Activity
    new(builder, activity) = {
        builder = builder
        activity = activity
    }

    static member Create(activityName) = ActivityTaskMethodBuilder(AsyncTaskMethodBuilder.Create(), activityName)

    member this.SetException(exn: exn) =
        match this.activity with
        | null -> ()
        | activity ->
            activity.SetStatus(ActivityStatusCode.Error, exn.Message).Dispose()

        this.builder.SetException(exn)

    member this.SetResult() =
        match this.activity with
        | null -> ()
        | activity ->
            activity.Dispose()

        this.builder.SetResult()

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = ActivityTask(this.builder.Task)

    interface IAsyncMethodBuilderCreator<Activity, ActivityTaskMethodBuilder> with
        static member Create(state) = ActivityTaskMethodBuilder.Create(state)

    interface IAsyncMethodBuilder<ActivityTaskAwaiter, ActivityTask> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult() = this.builder.SetResult()
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Struct>] ActivityTask =
    val mutable private task: Task
    new(task) = { task = task }

    member this.GetAwaiter() = ActivityTaskAwaiter(this.task.GetAwaiter())

    interface ITaskLike<ActivityTaskAwaiter> with
        member this.GetAwaiter() = this.GetAwaiter()

    interface ITaskLikeTask<ActivityTask> with
        member this.Task = this