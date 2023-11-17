namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.AsyncEnumerable

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

type TaskSeq() =
    inherit GenericTaskBuilderYieldBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<GenericTaskSeqAsyncEnumerableData<AsyncIteratorMethodBuilderWrapper,_,_>, _, _, DefaultSeqStateMachineDataInitializer<_,_>>(code)