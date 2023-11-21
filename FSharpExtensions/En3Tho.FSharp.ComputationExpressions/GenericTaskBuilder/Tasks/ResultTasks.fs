namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ResultTask

open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks
open Microsoft.FSharp.Core.CompilerServices

type ResultTaskBuilderBase() =
    inherit GenericTaskBuilderReturnCore<unit>()

type ResultTaskBuilder() =
    inherit ResultTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<Result<'TResult, 'TError>, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

type ResultValueTaskBuilder() =
    inherit ResultTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<Result<'TResult, 'TError>, DefaultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

module TaskLikeLow =

    type ResultTaskBuilderBase with

        member inline this.Return<'TData, 'TResult, 'TError
            when 'TData :> IStateMachineData<'TData, Result<'TResult, 'TError>>>(value: 'TResult) =
            ResumableCode<'TData, Result<'TResult, 'TError>>(fun sm ->
                sm.Data.SetResult(Ok value)
                true
        )

        [<NoEagerConstraintApplication>]
        member inline this.Bind(task: ^TaskLike, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCodeHelpers.Bind(task, continuation)

        member inline this.ReturnFrom(taskLike: ^TaskLike) =
            this.Bind(taskLike, fun v -> this.Return(v))

module TaskLikeHigh =
    open TaskLikeLow

    type ResultTaskBuilderBase with

        [<NoEagerConstraintApplication>]
        member inline _.Bind(task: ^TaskLike, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCodeHelpers.Bind(task, fun res ->
                ResumableCode<'TData, 'TResult2>(fun sm ->
                    match res with
                    | Ok value -> (continuation(value)).Invoke(&sm)
                    | Error error ->
                        sm.ResumptionPoint <- StateMachineCodes.ShouldStop
                        sm.Data.SetResult(Error error)
                        true
            ))

        member inline this.Bind<'TData, 'TResult, 'TResult1, 'TResult2, 'TError
            when 'TData :> IStateMachineData<'TData, Result<'TResult, 'TError>>>
            (result: Result<'TResult1, 'TError>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCode<'TData, 'TResult2>(fun sm ->
                match result with
                | Ok value ->
                    (continuation value).Invoke(&sm)
                | Error error ->
                     sm.ResumptionPoint <- StateMachineCodes.ShouldStop
                     sm.Data.SetResult(Error error)
                     true
            )

        member inline this.ReturnFrom<'TData, 'TResult, 'TError
            when 'TData :> IStateMachineData<'TData, Result<'TResult, 'TError>>>
            (result: Result<'TResult, 'TError>) =
            ResumableCode<'TData, Result<'TResult, 'TError>>(fun sm ->
                match result with
                | Ok result ->
                    sm.Data.SetResult(Ok result)
                | Error error ->
                    sm.Data.SetResult(Error error)
                true
        )

        member inline this.ReturnFrom(taskLike: ^TaskLike) =
            this.Bind(taskLike, fun v -> this.Return(v))

module Low =
    open TaskLikeLow

    type ResultTaskBuilderBase with

        member inline this.Bind(task: Task<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            this.Bind(TaskBindWrapper(task), continuation)

        member inline this.ReturnFrom(task: Task<'TResult>) =
            this.Bind(TaskBindWrapper(task), fun v -> this.Return(v))

module High =

    open TaskLikeHigh

    type ResultTaskBuilderBase with
        member inline this.Bind(task: Task<Result<'TResult1, 'TError>>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            this.Bind(TaskBindWrapper(task), continuation)

        member inline this.ReturnFrom(task: Task<Result<'TResult1, 'TError>>) =
            this.ReturnFrom(TaskBindWrapper(task))