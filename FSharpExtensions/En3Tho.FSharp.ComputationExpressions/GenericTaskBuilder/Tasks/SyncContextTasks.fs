namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open System
open System.Runtime.CompilerServices
open System.Threading
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core

type SyncContextData<'TStateMachine, 'TMethodBuilder, 'TTask, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask, 'TResult>
    and 'TStateMachine :> IAsyncStateMachine>() =
        inherit StateMachineRefDataBase<'TMethodBuilder, 'TTask, 'TResult>()

        [<DefaultValue(false)>]
        val mutable StateMachine: 'TStateMachine

        [<DefaultValue(false)>]
        val mutable Continuation: Action

        override this.MoveNext() = this.StateMachine.MoveNext()

        interface ICriticalNotifyCompletion with
            member this.OnCompleted(continuation) = this.Continuation <- continuation
            member this.UnsafeOnCompleted(continuation) = this.Continuation <- continuation

        interface IGenericTaskStateMachineData<StateMachineRefDataBase<'TMethodBuilder, 'TTask, 'TResult>> with

            member this.CheckCanContinueOrThrow() = true
            member this.AwaitOnCompleted(awaiter, arg) =
                let mutable this = this
                this.MethodBuilder.AwaitOnCompleted(&awaiter, &this)
            member this.AwaitUnsafeOnCompleted(awaiter, arg) =
                let mutable this = this
                this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &this)
            member this.Finish(sm) =
                this.MethodBuilder.SetResult(this.Result)
            member this.SetException(``exception``: exn) = this.MethodBuilder.SetException(``exception``)
            member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)

type [<Struct>] SyncContextTaskStateMachineDataInitializer<'TMethodBuilder, 'TTask, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>> =

    interface IGenericTaskBuilderStateMachineDataInitializer<StateMachineData<'TMethodBuilder, 'TTask, 'TResult>, SynchronizationContext, 'TTask> with
        static member Initialize(sm: byref<'a>, data, state) =
            data.MethodBuilder <- 'TMethodBuilder.Create()

            let currentSyncContext = SynchronizationContext.Current
            if Object.ReferenceEquals(state, currentSyncContext) then
                data.MethodBuilder.Start(&sm)
            else
                // force state machine box creation with traditional async method builders
                // this is a total AsyncTaskMethodBuilder implementation detail hack -_-
                // but it will initialize task field properly
                let mutable fakeAwaiter = FakeAwaiter()
                data.MethodBuilder.AwaitUnsafeOnCompleted(&fakeAwaiter, &sm)
                state.Post((fun continuation ->
                    (continuation :?> Action).Invoke()
                ), fakeAwaiter.Continuation)

            data.MethodBuilder.Task

type SyncContextTask(state) =
    inherit GenericTaskBuilderWithStateBase<SynchronizationContext>(state)
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,SyncContextTaskStateMachineDataInitializer<_,_,_>>(code)

type SyncContextValueTask(state) =
    inherit GenericTaskBuilderWithStateBase<SynchronizationContext>(state)
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,SyncContextTaskStateMachineDataInitializer<_,_,_>>(code)