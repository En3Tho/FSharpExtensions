namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type [<Struct>] AsyncValueTaskMethodBuilderWrapper<'TResult, 'TBehavior
    when 'TBehavior :> IAsyncValueTaskMethodBuilderBehavior<'TResult>> =

    val mutable private builder: AsyncValueTaskMethodBuilder<'TResult>
    new(builder) = { builder = builder }

    static member Create() = AsyncValueTaskMethodBuilderWrapper<'TResult, 'TBehavior>(AsyncValueTaskMethodBuilder<'TResult>.Create())
    member this.SetException(``exception``) = 'TBehavior.SetException(&this.builder, ``exception``)
    member this.SetResult(data) = 'TBehavior.SetResult(&this.builder, data)

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = this.builder.Task

    interface IAsyncMethodBuilderCreator<AsyncValueTaskMethodBuilderWrapper<'TResult, 'TBehavior>> with
        static member Create() = AsyncValueTaskMethodBuilderWrapper<'TResult, 'TBehavior>.Create()

    interface IAsyncMethodBuilder<ValueTask<'TResult>, 'TResult> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult(data) = this.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task

type [<Struct>] AsyncValueTaskMethodBuilderWrapper<'TBehavior
    when 'TBehavior :> IAsyncValueTaskMethodBuilderBehavior> =

    val mutable private builder: AsyncValueTaskMethodBuilder
    new(builder) = { builder = builder }

    static member Create() = AsyncValueTaskMethodBuilderWrapper<'TBehavior>(AsyncValueTaskMethodBuilder.Create())
    member this.SetException(``exception``) = 'TBehavior.SetException(&this.builder, ``exception``)
    member this.SetResult() = 'TBehavior.SetResult(&this.builder)

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = this.builder.Task

    interface IAsyncMethodBuilderCreator<AsyncValueTaskMethodBuilderWrapper<'TBehavior>> with
        static member Create() = AsyncValueTaskMethodBuilderWrapper<'TBehavior>.Create()

    interface IAsyncMethodBuilder<ValueTask> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult() = this.builder.SetResult()
        member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task