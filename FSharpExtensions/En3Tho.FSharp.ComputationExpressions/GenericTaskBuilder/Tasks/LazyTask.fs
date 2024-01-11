namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.LazyTask

open System
open System.Runtime.CompilerServices
open System.Threading
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

type LazyTask(task: Task, moveNext: Action) =
    let mutable moveNext = moveNext
    member _.DangerousGetTask() = task
    member _.GetAwaiter() =
        if not (Object.ReferenceEquals(moveNext, null)) then
            match Interlocked.Exchange(&moveNext, null) with
            | null -> ()
            | moveNext -> moveNext.Invoke()

        task.GetAwaiter()

type LazyTask<'T>(task: Task<'T>, moveNext: Action) =
    let mutable moveNext = moveNext
    member _.DangerousGetTask() = task
    member _.GetAwaiter() =
        if not (Object.ReferenceEquals(moveNext, null)) then
            match Interlocked.Exchange(&moveNext, null) with
            | null -> ()
            | moveNext -> moveNext.Invoke()

        task.GetAwaiter()

type [<Struct>] AsyncTaskMethodBuilderWrapperForLazyTask<'TResult, 'TBehavior
    when 'TBehavior :> IAsyncTaskMethodBuilderBehavior<'TResult>> =

    val mutable private builder: AsyncTaskMethodBuilder<'TResult>
    val mutable private task: LazyTask<'TResult>
    new(builder) = { builder = builder; task = Unchecked.defaultof<_> }

    static member Create() = AsyncTaskMethodBuilderWrapperForLazyTask(AsyncTaskMethodBuilder<'TResult>.Create())
    member this.SetException(``exception``) = 'TBehavior.SetException(&this.builder, ``exception``)
    member this.SetResult(data) = 'TBehavior.SetResult(&this.builder, data)

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) =
        let mutable fakeAwaiter = FakeAwaiter()
        this.builder.AwaitOnCompleted(&fakeAwaiter, &stateMachine)
        this.task <- LazyTask<'TResult>(this.builder.Task, fakeAwaiter.Continuation)
    member this.Task = this.task

    interface IAsyncMethodBuilderCreator<AsyncTaskMethodBuilderWrapperForLazyTask<'TResult, 'TBehavior>> with
        static member Create() = AsyncTaskMethodBuilderWrapperForLazyTask<'TResult, 'TBehavior>.Create()

    interface IAsyncMethodBuilder<LazyTask<'TResult>, 'TResult> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult(data) = this.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.Start(&stateMachine)
        member this.Task = this.Task

type [<Struct>] AsyncTaskMethodBuilderWrapperForLazyTask<'TBehavior
    when 'TBehavior :> IAsyncTaskMethodBuilderBehavior> =

    val mutable private builder: AsyncTaskMethodBuilder
    val mutable private task: LazyTask
    new(builder) = { builder = builder; task = Unchecked.defaultof<_> }

    member this.SetException(``exception``) = 'TBehavior.SetException(&this.builder, ``exception``)
    member this.SetResult() = 'TBehavior.SetResult(&this.builder)

    static member Create() = AsyncTaskMethodBuilderWrapperForLazyTask<'TBehavior>(AsyncTaskMethodBuilder.Create())
    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) =
        let mutable fakeAwaiter = FakeAwaiter()
        this.builder.AwaitOnCompleted(&fakeAwaiter, &stateMachine)
        this.task <- LazyTask(this.builder.Task, fakeAwaiter.Continuation)
    member this.Task = this.task

    interface IAsyncMethodBuilderCreator<AsyncTaskMethodBuilderWrapperForLazyTask<'TBehavior>> with
        static member Create() = AsyncTaskMethodBuilderWrapperForLazyTask<'TBehavior>.Create()

    interface IAsyncMethodBuilder<LazyTask> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult() = this.SetResult()
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task

type LazyTaskBuilder() =
    inherit GenericTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapperForLazyTask<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

type LazyUnitTaskBuilder() =
    inherit GenericTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<UnitStateMachineData<AsyncTaskMethodBuilderWrapperForLazyTask<DefaultAsyncTaskMethodBuilderBehavior>,_>,_,_,DefaultUnitStateMachineDataInitializer<_,_>>(code)