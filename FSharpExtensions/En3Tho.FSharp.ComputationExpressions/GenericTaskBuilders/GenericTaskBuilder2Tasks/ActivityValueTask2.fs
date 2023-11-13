namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2.Tasks

open System
open System.Diagnostics
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder2
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2
open Microsoft.FSharp.Core

[<Struct>]
type ActivityStateCheck =
    interface IStateCheck<Activity> with
        static member CanCheckState = false
        static member CheckState(_) =
            raise (InvalidOperationException("Should not be called"))
            
        static member CanProcessException = true
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member ProcessException(state, ``exception``) =
            match state with
            | null -> ()
            | activity ->
                activity.SetStatus(ActivityStatusCode.Error, ``exception``.Message).Dispose()
                // activity.RecordException?
            
        static member CanProcessSuccess = true
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member ProcessSuccess(state) =
            match state with
            | null -> ()
            | activity ->
                activity.SetStatus(ActivityStatusCode.Ok).Dispose()

type ActivityTaskBuilder2(state) =
    inherit GenericTaskBuilder2WithStateBase<Activity>(state)
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<StateMachineDataWithState<AsyncValueTaskMethodBuilderWrapper<'a>,_,_,_,ActivityStateCheck,_,_>,_,_, DefaultStateMachineDataWithStateInitializer<_,_,_,_,_,_,_>>(code)