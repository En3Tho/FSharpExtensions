namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.SynchronizationContextTask

open System
open System.Threading
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks
open Microsoft.FSharp.Core

type [<Struct>] SyncContextTaskStateMachineDataInitializer<'TMethodBuilder, 'TTask, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>> =

    interface IStateMachineDataInitializer<StateMachineData<'TMethodBuilder, 'TTask, 'TResult>, SynchronizationContext, 'TTask> with
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

type SynchronizationContextTask(state) =
    inherit GenericTaskBuilderWithStateReturnBase<SynchronizationContext>(state)
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,SyncContextTaskStateMachineDataInitializer<_,_,_>>(code)

type SynchronizationContextValueTask(state) =
    inherit GenericTaskBuilderWithStateReturnBase<SynchronizationContext>(state)
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,SyncContextTaskStateMachineDataInitializer<_,_,_>>(code)