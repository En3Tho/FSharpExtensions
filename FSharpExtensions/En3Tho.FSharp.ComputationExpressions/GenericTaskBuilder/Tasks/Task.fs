namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type TaskBuilder() =
    inherit GenericTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<'a, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

type UnitTaskBuilder() =
    inherit GenericTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<UnitStateMachineData<AsyncTaskMethodBuilderWrapper<DefaultAsyncTaskMethodBuilderBehavior>,_>,_,_,DefaultUnitStateMachineDataInitializer<_,_>>(code)