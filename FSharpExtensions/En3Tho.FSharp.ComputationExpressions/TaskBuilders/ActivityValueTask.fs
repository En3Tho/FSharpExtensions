namespace En3Tho.FSharp.ComputationExpressions.TaskBuilders.ActivityTask

open System.Diagnostics
open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type [<Struct>] ActivityValueTaskAwaiter<'a> =
    val mutable private awaiter: ValueTaskAwaiter<'a>
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

and [<Struct>] ActivityValueTaskMethodBuilder<'a> =
    val mutable private builder: AsyncValueTaskMethodBuilder<'a>
    val mutable private activity: Activity
    new(builder, activityName: string) = {
        builder = builder
        activity =
            match Activity.Current with
            | null -> null
            | activity ->
                activity.Source.StartActivity(activityName)
    }

    static member Create(activityName) = ActivityValueTaskMethodBuilder(AsyncValueTaskMethodBuilder<'a>.Create(), activityName)

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
            activity.Dispose()
        this.builder.SetResult(data)

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = ActivityValueTask(this.builder.Task)

    interface IAsyncMethodBuilderCreator<string, ActivityValueTaskMethodBuilder<'a>> with
        static member Create(initialState) = ActivityValueTaskMethodBuilder<'a>.Create(initialState)

    interface IAsyncMethodBuilder<ActivityValueTaskAwaiter<'a>, ActivityValueTask<'a>, 'a> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult(data) = this.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Struct>] ActivityValueTask<'a> =
    val mutable private task: ValueTask<'a>
    new(task) = { task = task }

    member this.GetAwaiter() = ActivityValueTaskAwaiter(this.task.GetAwaiter())
    member this.Task = this.task

    interface ITaskLike<ActivityValueTaskAwaiter<'a>, 'a> with
        member this.GetAwaiter() = ActivityValueTaskAwaiter(this.task.GetAwaiter())

    interface ITaskLikeTask<ActivityValueTask<'a>> with
        member this.Task = this

type [<Struct>] ActivityValueTaskAwaiter =
    val mutable private awaiter: ValueTaskAwaiter
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

and [<Struct>] ActivityValueTaskMethodBuilder =
    val mutable private builder: AsyncValueTaskMethodBuilder
    val mutable private activity: Activity
    new(builder, activityName: string) = {
        builder = builder
        activity =
            match Activity.Current with
            | null -> null
            | activity ->
                activity.Source.StartActivity(activityName)
    }

    static member Create(activityName) = ActivityValueTaskMethodBuilder(AsyncValueTaskMethodBuilder.Create(), activityName)

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
    member this.Task = ActivityValueTask(this.builder.Task)

    interface IAsyncMethodBuilderCreator<string, ActivityValueTaskMethodBuilder> with
        static member Create(initialState) = ActivityValueTaskMethodBuilder.Create(initialState)

    interface IAsyncMethodBuilder<ActivityValueTaskAwaiter, ActivityValueTask> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult() = this.builder.SetResult()
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Struct>] ActivityValueTask =
    val mutable private task: ValueTask
    new(task) = { task = task }

    member this.GetAwaiter() = ActivityValueTaskAwaiter(this.task.GetAwaiter())

    interface ITaskLike<ActivityValueTaskAwaiter> with
        member this.GetAwaiter() = this.GetAwaiter()

    interface ITaskLikeTask<ActivityValueTask> with
        member this.Task = this