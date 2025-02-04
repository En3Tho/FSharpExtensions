namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionResultTask

open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks
open Microsoft.FSharp.Core.CompilerServices

[<Struct>]
type ExnResultAsyncTaskMethodBuilderBehavior<'TResult> =

    interface IAsyncTaskMethodBuilderBehavior<Result<'TResult, exn>> with

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetException(builder, ``exception``) = builder.SetResult(Error(``exception``))

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetResult(builder, result) = builder.SetResult(result)

    interface IAsyncValueTaskMethodBuilderBehavior<Result<'TResult, exn>> with

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetException(builder, ``exception``) = builder.SetResult(Error(``exception``))

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetResult(builder, result) = builder.SetResult(result)

type ExceptionResultTaskBuilderBase() =
    inherit GenericTaskBuilderReturnCoreNoLoops<unit>()

type ExceptionResultTaskBuilder() =
    inherit ExceptionResultTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<Result<'TResult, exn>, ExnResultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

type ExceptionResultValueTaskBuilder() =
    inherit ExceptionResultTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<Result<'TResult, exn>, ExnResultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

module TaskLikeLow =

    type ExceptionResultTaskBuilderBase with

        member inline this.Return<'TData, 'TResult, 'TExn
            when 'TData :> IStateMachineData<'TData, Result<'TResult,  'TExn>>
            and 'TExn :> exn>(value: 'TResult) =
            ResumableCode<'TData, Result<'TResult, exn>>(fun sm ->
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

    type ExceptionResultTaskBuilderBase with

        [<NoEagerConstraintApplication>]
        member inline _.Bind(task: ^TaskLike, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCodeHelpers.Bind(task, fun res ->
                ResumableCode<'TData, 'TResult2>(fun sm ->
                    match res with
                    | Ok value -> (continuation(value)).Invoke(&sm)
                    | Error exn ->
                        sm.Data.SetResult(Error (exn :> exn))
                        true
            ))

        member inline this.Bind<'TData, 'TResult, 'TResult1, 'TResult2, 'TExn
            when 'TData :> IStateMachineData<'TData, Result<'TResult, exn>>
            and 'TExn :> exn>
            (result: Result<'TResult1, 'TExn>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCode<'TData, 'TResult2>(fun sm ->
                match result with
                | Ok value ->
                    (continuation value).Invoke(&sm)
                | Error exn ->
                     sm.Data.SetResult(Error (exn :> exn))
                     true
            )

        member inline this.ReturnFrom<'TData, 'TResult, 'TExn
            when 'TData :> IStateMachineData<'TData, Result<'TResult, exn>>
            and 'TExn :> exn>
            (result: Result<'TResult, 'TExn>) =
            ResumableCode<'TData, Result<'TResult, exn>>(fun sm ->
                match result with
                | Ok result ->
                    sm.Data.SetResult(Ok result)
                | Error exn ->
                    sm.Data.SetResult(Error (exn :> exn))
                true
        )

        member inline this.ReturnFrom(taskLike: ^TaskLike) =
            this.Bind(taskLike, fun v -> this.Return(v))

module Low =
    open TaskLikeLow

    type ExceptionResultTaskBuilderBase with

        member inline this.Bind(task: Task<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            this.Bind(TaskBindWrapper(task), continuation)

        member inline this.ReturnFrom(task: Task<'a>) =
            this.Bind(TaskBindWrapper(task), fun v -> this.Return(v))

module High =

    open TaskLikeHigh

    type ExceptionResultTaskBuilderBase with
        member inline this.Bind(task: Task<Result<'TResult1, 'TExn>>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            this.Bind(TaskBindWrapper(task), continuation)

        member inline this.ReturnFrom(task: Task<Result<'TResult1, 'TExn>>) =
            this.ReturnFrom(TaskBindWrapper(task))