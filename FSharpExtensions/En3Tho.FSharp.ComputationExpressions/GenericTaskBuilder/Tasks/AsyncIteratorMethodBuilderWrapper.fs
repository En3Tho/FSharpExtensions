namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type [<Struct>] AsyncIteratorMethodBuilderWrapper =
    val mutable private builder: AsyncIteratorMethodBuilder
    new(builder) = { builder = builder }

    static member Create() = AsyncIteratorMethodBuilderWrapper(AsyncIteratorMethodBuilder.Create())

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.Complete() = this.builder.Complete()
    member this.MoveNext(stateMachine: byref<#IAsyncStateMachine>) = this.builder.MoveNext(&stateMachine)
    member this.SetStateMachine(_) = ()

    interface IAsyncIteratorMethodBuilder with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.Complete() = this.Complete()
        member this.MoveNext(stateMachine) = this.MoveNext(&stateMachine)
        member this.SetStateMachine(_) = this.SetStateMachine()

    interface IAsyncMethodBuilderCreator<AsyncIteratorMethodBuilderWrapper> with
        static member Create() = AsyncIteratorMethodBuilderWrapper.Create()