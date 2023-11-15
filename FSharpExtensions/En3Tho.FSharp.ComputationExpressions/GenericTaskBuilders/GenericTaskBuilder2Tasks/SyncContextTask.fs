namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2.Tasks

open System
open System.Runtime.CompilerServices
open System.Threading
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder2
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2
open Microsoft.FSharp.Core

type SyncContextData<'TStateMachine, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TStateMachine :> IAsyncStateMachine
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>>() =
        inherit StateMachineRefDataBase<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>()

        [<DefaultValue(false)>]
        val mutable StateMachine: 'TStateMachine

        [<DefaultValue(false)>]
        val mutable Continuation: Action

        override this.MoveNext() = this.StateMachine.MoveNext()

        interface ICriticalNotifyCompletion with
            member this.OnCompleted(continuation) = this.Continuation <- continuation
            member this.UnsafeOnCompleted(continuation) = this.Continuation <- continuation

        interface IGenericTaskStateMachineData<StateMachineRefDataBase<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>> with

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


type [<Struct>] SyncContextTaskStateMachineDataInitializer<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>> =

    interface IGenericTaskBuilderStateMachineDataInitializer<StateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>, SynchronizationContext, 'TBuilderResult> with
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

            data.MethodBuilder.Task.Task

type SyncContextTask(state) =
    inherit GenericTaskBuilder2WithStateBase<SynchronizationContext>(state)
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<'a>,_,_,_,_>,_,_,SyncContextTaskStateMachineDataInitializer<_,_,_,_,_>>(code)