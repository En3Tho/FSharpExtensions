namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ValueOptionTask

open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks
open Microsoft.FSharp.Core.CompilerServices

type OptionTaskBuilderBase() =
    inherit GenericTaskBuilderReturnCoreNoLoops<unit>()

type ValueOptionTaskBuilder() =
    inherit OptionTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<ValueOption<'TResult>, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

type ValueOptionValueTaskBuilder() =
    inherit OptionTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<ValueOption<'TResult>, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

module TaskLikeLow =

    type OptionTaskBuilderBase with

        member inline this.Return<'TData, 'TResult
            when 'TData :> IStateMachineData<'TData, ValueOption<'TResult>>>(value: 'TResult) =
            ResumableCode<'TData, ValueOption<'TResult>>(fun sm ->
                sm.Data.SetResult(ValueSome value)
                true
            )

        [<NoEagerConstraintApplication>]
        member inline this.Bind(task: ^TaskLike, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCodeHelpers.Bind(task, continuation)

        member inline this.ReturnFrom(taskLike: ^TaskLike) =
            this.Bind(taskLike, fun v -> this.Return(v))

module TaskLikeHighValueOption =
    open TaskLikeLow

    type OptionTaskBuilderBase with

        [<NoEagerConstraintApplication>]
        member inline _.Bind(task: ^TaskLike, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCodeHelpers.Bind(task, fun res ->
                ResumableCode<'TData, 'TResult2>(fun sm ->
                    match res with
                    | ValueSome value -> (continuation(value)).Invoke(&sm)
                    | ValueNone ->
                        sm.Data.SetResult(ValueNone)
                        true
            ))

        [<NoEagerConstraintApplication>]
        member inline this.ReturnFrom(taskLike: ^TaskLike) =
            this.Bind(taskLike, fun v -> this.Return(v))

        member inline this.Bind<'TResult1, 'TResult2, ^Awaiter, 'TData, 'TResult
            when 'TData :> IStateMachineData<'TData, ValueOption<'TResult>>>
            (result: ValueOption<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCode<'TData, 'TResult2>(fun sm ->
                match result with
                | ValueSome value -> (continuation(value)).Invoke(&sm)
                | ValueNone ->
                    sm.Data.SetResult(ValueNone)
                    true
            )

        member inline this.ReturnFrom<'TData, 'TResult
            when 'TData :> IStateMachineData<'TData, ValueOption<'TResult>>>(result: ValueOption<'TResult>) =
            ResumableCode<'TData, ValueOption<'TResult>>(fun sm ->
                sm.Data.SetResult(result)
                true
            )

module TaskLikeHighOption =
    open TaskLikeLow

    type OptionTaskBuilderBase with

        [<NoEagerConstraintApplication>]
        member inline _.Bind(task: ^TaskLike, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCodeHelpers.Bind(task, fun res ->
                ResumableCode<'TData, 'TResult2>(fun sm ->
                    match res with
                    | Some value -> (continuation(value)).Invoke(&sm)
                    | None ->
                        sm.Data.SetResult(ValueNone)
                        true
            ))

        [<NoEagerConstraintApplication>]
        member inline this.ReturnFrom(taskLike: ^TaskLike) =
            this.Bind(taskLike, fun v -> this.Return(v))

        member inline this.Bind<'TResult1, 'TResult2, ^Awaiter, 'TData, 'TResult
            when 'TData :> IStateMachineData<'TData, Option<'TResult>>>
            (result: Option<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCode<'TData, 'TResult2>(fun sm ->
                match result with
                | Some value -> (continuation(value)).Invoke(&sm)
                | None ->
                    sm.Data.SetResult(None)
                    true
            )

        member inline this.ReturnFrom<'TData, 'TResult
            when 'TData :> IStateMachineData<'TData, Option<'TResult>>>(result: Option<'TResult>) =
            ResumableCode<'TData, Option<'TResult>>(fun sm ->
                sm.Data.SetResult(result)
                true
            )

module Low =
    open TaskLikeLow

    type OptionTaskBuilderBase with

        member inline this.Bind(task: Task<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            this.Bind(TaskBindWrapper(task), continuation)

        member inline this.ReturnFrom(task: Task<'a>) =
            this.Bind(TaskBindWrapper(task), fun v -> this.Return(v))

module High =

    open TaskLikeHighValueOption
    open TaskLikeHighOption

    type OptionTaskBuilderBase with

        member inline this.Bind(task: Task<ValueOption<'TResult1>>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            this.Bind(TaskBindWrapper(task), continuation)

        member inline this.ReturnFrom(task: Task<ValueOption<'TResult1>>) =
            this.ReturnFrom(TaskBindWrapper(task))

        member inline this.Bind(task: Task<Option<'TResult1>>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            this.Bind(TaskBindWrapper(task), continuation)

        member inline this.ReturnFrom(task: Task<Option<'TResult1>>) =
            this.ReturnFrom(TaskBindWrapper(task))