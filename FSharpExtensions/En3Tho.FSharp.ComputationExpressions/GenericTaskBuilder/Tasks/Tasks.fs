namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.Native

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

type TaskBuilder() =
    inherit GenericTaskBuilderReturnBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

type UnitTaskBuilder() =
    inherit GenericTaskBuilderReturnBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<UnitStateMachineData<AsyncTaskMethodBuilderWrapper<DefaultAsyncTaskMethodBuilderBehavior>,_>,_,_,DefaultUnitStateMachineDataInitializer<_,_>>(code)

type ValueTaskBuilder() =
    inherit GenericTaskBuilderReturnBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_, DefaultStateMachineDataInitializer<_,_,_>>(code)

type UnitValueTaskBuilder() =
    inherit GenericTaskBuilderReturnBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<UnitStateMachineData<AsyncValueTaskMethodBuilderWrapper<DefaultAsyncTaskMethodBuilderBehavior>,_>,_,_,DefaultUnitStateMachineDataInitializer<_,_>>(code)