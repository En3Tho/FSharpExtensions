namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.RepeatableTask

open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

[<AbstractClass>]
type RepeatableTask() =
    abstract StartNew: unit -> Task
    member this.GetAwaiter() = this.StartNew().GetAwaiter()

[<AbstractClass>]
type RepeatableTask<'T>() =
    abstract StartNew: unit -> Task<'T>
    member this.GetAwaiter() = this.StartNew().GetAwaiter()

type [<Struct>] AsyncTaskMethodBuilderWrapperForRepeatableTask<'TBehavior
    when 'TBehavior :> IAsyncTaskMethodBuilderBehavior> =

    val mutable builder: AsyncTaskMethodBuilder
    val mutable private task: RepeatableTask
    new(builder) = { builder = builder; task = Unchecked.defaultof<_> }

    member this.SetException(``exception``) = 'TBehavior.SetException(&this.builder, ``exception``)
    member this.SetResult() = 'TBehavior.SetResult(&this.builder)

    static member Create() = AsyncTaskMethodBuilderWrapperForRepeatableTask<'TBehavior>(AsyncTaskMethodBuilder.Create())
    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) =
        this.task <- RepeatableTaskImpl<_, 'TBehavior>(stateMachine)
    member this.Task = this.task

    interface IAsyncMethodBuilderCreator<AsyncTaskMethodBuilderWrapperForRepeatableTask<'TBehavior>> with
        static member Create() = AsyncTaskMethodBuilderWrapperForRepeatableTask<'TBehavior>.Create()

    interface IAsyncMethodBuilder<RepeatableTask> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult() = this.SetResult()
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Sealed>] RepeatableTaskImpl<'TStateMachine, 'TBehavior
    when 'TStateMachine :> IAsyncStateMachine
    and 'TBehavior :> IAsyncTaskMethodBuilderBehavior>(sm: 'TStateMachine) =
    inherit RepeatableTask()
    override this.StartNew() =
        let mutable sm = sm
        let mutable mb = AsyncTaskMethodBuilderWrapperForRepeatableTask<'TBehavior>.Create()
        mb.builder.Start(&sm)
        mb.builder.Task

type [<Struct>] AsyncTaskMethodBuilderWrapperForRepeatableTask<'TResult, 'TBehavior
    when 'TBehavior :> IAsyncTaskMethodBuilderBehavior<'TResult>> =

    val mutable builder: AsyncTaskMethodBuilder<'TResult>
    val mutable private task: RepeatableTask<'TResult>
    new(builder) = { builder = builder; task = Unchecked.defaultof<_> }

    static member Create() = AsyncTaskMethodBuilderWrapperForRepeatableTask<'TResult, 'TBehavior>(AsyncTaskMethodBuilder<'TResult>.Create())
    member this.SetException(``exception``) = 'TBehavior.SetException(&this.builder, ``exception``)
    member this.SetResult(data) = 'TBehavior.SetResult(&this.builder, data)

    member this.AwaitOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitOnCompleted(&awaiter, &stateMachine)
    member this.AwaitUnsafeOnCompleted(awaiter: byref<#INotifyCompletion>, stateMachine: byref<#IAsyncStateMachine>) =
        this.builder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
    member this.SetStateMachine(stateMachine) = this.builder.SetStateMachine(stateMachine)
    member this.Start(stateMachine: byref<#IAsyncStateMachine>) =
        this.task <- RepeatableTaskImpl<'TResult, _, 'TBehavior>(stateMachine)

    member this.Task = this.task

    interface IAsyncMethodBuilderCreator<AsyncTaskMethodBuilderWrapperForRepeatableTask<'TResult, 'TBehavior>> with
        static member Create() = AsyncTaskMethodBuilderWrapperForRepeatableTask<'TResult, 'TBehavior>.Create()

    interface IAsyncMethodBuilder<RepeatableTask<'TResult>, 'TResult> with
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetException(``exception``) = this.SetException(``exception``)
        member this.SetResult(data) = this.SetResult(data)
        member this.SetStateMachine(stateMachine) = this.SetStateMachine(stateMachine)
        member this.Start(stateMachine: byref<#IAsyncStateMachine>) = this.Start(&stateMachine)
        member this.Task = this.Task

and [<Sealed>] RepeatableTaskImpl<'T, 'TStateMachine, 'TBehavior
    when 'TStateMachine :> IAsyncStateMachine
    and 'TBehavior :> IAsyncTaskMethodBuilderBehavior<'T>>(sm: 'TStateMachine) =
    inherit RepeatableTask<'T>()

    override this.StartNew() =
        let mutable sm = sm
        let mutable mb = AsyncTaskMethodBuilderWrapperForRepeatableTask<'T, 'TBehavior>.Create()
        mb.builder.Start(&sm)
        mb.builder.Task

type RepeatableTaskBuilder() =
    inherit GenericTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapperForRepeatableTask<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

type RepeatableUnitTaskBuilder() =
    inherit GenericTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<UnitStateMachineData<AsyncTaskMethodBuilderWrapperForRepeatableTask<DefaultAsyncTaskMethodBuilderBehavior>,_>,_,_,DefaultUnitStateMachineDataInitializer<_,_>>(code)