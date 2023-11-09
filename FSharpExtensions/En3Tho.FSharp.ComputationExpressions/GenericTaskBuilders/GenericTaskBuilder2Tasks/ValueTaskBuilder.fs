namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2.Tasks

open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder2
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2.StateMachineDataXX

type [<Struct>] ValueTaskAwaiterWrapper<'a> =
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

and [<Struct>] AsyncValueTaskMethodBuilderWrapper<'a> =
    val mutable private builder: AsyncValueTaskMethodBuilder<'a>
    new(builder) = { builder = builder }

    static member Create() = AsyncValueTaskMethodBuilderWrapper(AsyncValueTaskMethodBuilder<'a>.Create())
    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetException(``exception``) = this.builder.SetException(``exception``)
    member this.SetResult(data) = this.builder.SetResult(data)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = ValueTaskWrapper(this.builder.Task)

    interface IAsyncMethodBuilderCreator<AsyncValueTaskMethodBuilderWrapper<'a>> with
        static member Create() = AsyncValueTaskMethodBuilderWrapper.Create()

    interface IAsyncMethodBuilder<ValueTaskAwaiterWrapper<'a>, ValueTaskWrapper<'a>, 'a> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult(data) = this.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Struct>] ValueTaskWrapper<'a> =
    val mutable private task: ValueTask<'a>
    new(task) = { task = task }

    member this.GetAwaiter() = ValueTaskAwaiterWrapper(this.task.GetAwaiter())

    interface ITaskLike<ValueTaskAwaiterWrapper<'a>, 'a> with
        member this.GetAwaiter() = ValueTaskAwaiterWrapper(this.task.GetAwaiter())

    interface ITaskLikeTask<ValueTask<'a>> with
        member this.Task = this.task

//
// type ValueTaskBuilder2() =
//     inherit GenericTaskBuilder2()
//     member inline this.Run([<InlineIfLambda>] code) =
//        this.RunInternal<GenericTaskStateMachineData2<ValueTaskWrapperMethodBuilder<'a>,_,_,_>,_,_,_,_,_>(code)