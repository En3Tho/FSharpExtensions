namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type ValueTaskBuilder() =
    inherit GenericTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<'a, DefaultAsyncValueTaskMethodBuilderBehavior<_>>,_,_>,_,_, DefaultStateMachineDataInitializer<_,_,_>>(code)

type UnitValueTaskBuilder() =
    inherit GenericTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<UnitStateMachineData<AsyncValueTaskMethodBuilderWrapper<DefaultAsyncValueTaskMethodBuilderBehavior>,_>,_,_,DefaultUnitStateMachineDataInitializer<_,_>>(code)