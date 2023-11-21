namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.SemaphoreSlimTask

open System
open System.Runtime.CompilerServices
open System.Threading
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks
open Microsoft.FSharp.Core

[<Struct>]
type SemaphoreSlimStateCheck =
    interface IStateCheck<SemaphoreSlim> with
        static member CanCheckState = false
        static member CheckState(_) =
            raise (InvalidOperationException("Should not be called"))

        static member CanProcessException = true
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member ProcessException(state, _) =
            state.Release() |> ignore

        static member CanProcessSuccess = true
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member ProcessSuccess(state) =
            state.Release() |> ignore

type [<Struct>] SemaphoreSlimTaskStateMachineDataInitializer<'TMethodBuilder, 'TTask, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>> =

    interface IStateMachineDataInitializer<StateMachineDataWithState<'TMethodBuilder, SemaphoreSlimStateCheck, 'TTask, SemaphoreSlim, 'TResult>, SemaphoreSlim, 'TTask> with
        static member Initialize(sm: byref<'a>, data, state) =
            data.MethodBuilder <- 'TMethodBuilder.Create()

            let mutable awaiter = state.WaitAsync().GetAwaiter()
            if awaiter.IsCompleted then
                data.MethodBuilder.Start(&sm)
            else
                data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)

            data.MethodBuilder.Task

type SemaphoreSlimTaskBuilder(state) =
    inherit GenericTaskBuilderWithStateReturnBase<SemaphoreSlim>(state)
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineDataWithState<AsyncTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>, SemaphoreSlimStateCheck,_,_,_>,_,_, DefaultStateMachineDataWithStateInitializer<_,_,_,_,_>>(code)

type SemaphoreSlimValueTaskBuilder(state) =
    inherit GenericTaskBuilderWithStateReturnBase<SemaphoreSlim>(state)
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineDataWithState<AsyncValueTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>, SemaphoreSlimStateCheck,_,_,_>,_,_, DefaultStateMachineDataWithStateInitializer<_,_,_,_,_>>(code)