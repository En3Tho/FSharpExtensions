namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type IAsyncTaskMethodBuilderBehavior<'TResult> =
    static abstract SetException: builder: byref<AsyncTaskMethodBuilder<'TResult>> * ``exception``: exn -> unit
    static abstract SetResult: builder: byref<AsyncTaskMethodBuilder<'TResult>> * result: 'TResult -> unit

type IAsyncTaskMethodBuilderBehavior =
    static abstract SetException: builder: byref<AsyncTaskMethodBuilder> * ``exception``: exn -> unit
    static abstract SetResult: builder: byref<AsyncTaskMethodBuilder> -> unit

[<Struct>]
type DefaultAsyncTaskMethodBuilderBehavior<'TResult> =
    interface IAsyncTaskMethodBuilderBehavior<'TResult> with

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetException(builder, ``exception``) = builder.SetException(``exception``)

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetResult(builder, result) = builder.SetResult(result)

[<Struct>]
type DefaultAsyncTaskMethodBuilderBehavior =
    interface IAsyncTaskMethodBuilderBehavior with

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetException(builder, ``exception``) = builder.SetException(``exception``)

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetResult(builder) = builder.SetResult()

type [<Struct>] AsyncTaskMethodBuilderWrapper<'TResult, 'TBehavior
    when 'TBehavior :> IAsyncTaskMethodBuilderBehavior<'TResult>> =

    val mutable private builder: AsyncTaskMethodBuilder<'TResult>
    new(builder) = { builder = builder }

    static member Create() = AsyncTaskMethodBuilderWrapper(AsyncTaskMethodBuilder<'TResult>.Create())
    member this.SetException(``exception``) = 'TBehavior.SetException(&this.builder, ``exception``)
    member this.SetResult(data) = 'TBehavior.SetResult(&this.builder, data)

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = this.builder.Task

    interface IAsyncMethodBuilderCreator<AsyncTaskMethodBuilderWrapper<'TResult, 'TBehavior>> with
        static member Create() = AsyncTaskMethodBuilderWrapper<'TResult, 'TBehavior>.Create()

    interface IAsyncMethodBuilder<Task<'TResult>, 'TResult> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult(data) = this.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.Start(&stateMachine)
        member this.Task = this.Task

type [<Struct>] AsyncTaskMethodBuilderWrapper<'TBehavior
    when 'TBehavior :> IAsyncTaskMethodBuilderBehavior> =

    val mutable private builder: AsyncTaskMethodBuilder
    new(builder) = { builder = builder }

    member this.SetException(``exception``) = 'TBehavior.SetException(&this.builder, ``exception``)
    member this.SetResult() = 'TBehavior.SetResult(&this.builder)

    static member Create() = AsyncTaskMethodBuilderWrapper<'TBehavior>(AsyncTaskMethodBuilder.Create())
    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.builder.Start(&stateMachine)
    member this.Task = this.builder.Task

    interface IAsyncMethodBuilderCreator<AsyncTaskMethodBuilderWrapper<'TBehavior>> with
        static member Create() = AsyncTaskMethodBuilderWrapper<'TBehavior>.Create()

    interface IAsyncMethodBuilder<Task> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult() = this.SetResult()
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task