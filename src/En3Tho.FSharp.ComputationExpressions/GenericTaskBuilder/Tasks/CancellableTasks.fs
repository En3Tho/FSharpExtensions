namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.CancellableTask

open System
open System.Runtime.CompilerServices
open System.Threading
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks
open Microsoft.FSharp.Core

[<Struct>]
type CancellableStateCheck =
    interface IStateCheck<CancellationToken> with
        static member CanCheckState = true
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member CheckState(state) =
            state.ThrowIfCancellationRequested()
            true

        static member CanProcessException = false
        static member ProcessException(_, _) =
            raise (InvalidOperationException("Should not be called"))
            
        static member CanProcessSuccess = false
        static member ProcessSuccess(_) =
            raise (InvalidOperationException("Should not be called"))

type CancellableTaskBuilder(state) =
    inherit GenericTaskBuilderWithStateReturnBase<CancellationToken>(state)
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineDataWithState<AsyncTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>, CancellableStateCheck,_,_,_>,_,_, DefaultStateMachineDataWithStateInitializer<_,_,_,_,_>>(code)

type CancellableValueTaskBuilder(state) =
    inherit GenericTaskBuilderWithStateReturnBase<CancellationToken>(state)
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineDataWithState<AsyncValueTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>, CancellableStateCheck,_,_,_>,_,_, DefaultStateMachineDataWithStateInitializer<_,_,_,_,_>>(code)