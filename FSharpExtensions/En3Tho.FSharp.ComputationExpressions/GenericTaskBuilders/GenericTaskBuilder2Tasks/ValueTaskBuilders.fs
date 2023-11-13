namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2.Tasks

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder2
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2

type ValueTaskBuilder2() =
    inherit GenericTaskBuilder2Base()
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<'a>,_,_,_,_>,_,_, DefaultStateMachineDataInitializer<_,_,_,_,_>>(code)

type UnitValueTaskBuilder2() =
    inherit GenericTaskBuilder2Base()
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<UnitStateMachineData<AsyncValueTaskMethodBuilderWrapper,_,_,_>,_,_,DefaultUnitStateMachineDataInitializer<_,_,_,_>>(code)