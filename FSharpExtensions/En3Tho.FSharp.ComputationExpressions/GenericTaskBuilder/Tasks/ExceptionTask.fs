module En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionTask

open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionResultTask
open Microsoft.FSharp.Core.CompilerServices

type ExceptionTaskBuilderBase() =
    inherit GenericTaskBuilderReturnCore<unit>()
    interface IBindExtensions

    member inline this.Return<'TData, 'TResult, 'TExn
        when 'TData :> IStateMachineData<'TData, Result<'TResult,  'TExn>>
        and 'TExn :> exn>(value: 'TResult) =
        ResumableCode<'TData, Result<'TResult, exn>>(fun sm ->
            sm.Data.SetResult(Ok value)
            true
    )

type ExceptionTaskBuilder() =
    inherit ExceptionTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<Result<'TResult, exn>, ExnResultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

type ExceptionValueTaskBuilder() =
    inherit ExceptionTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<Result<'TResult, exn>, ExnResultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

module Low =
    open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.Low
    type ExceptionTaskBuilderBase with
        [<NoEagerConstraintApplication>]
        member inline this.ReturnFrom(task: ^TaskLike) =
            this.Bind(task, (fun v -> this.Return v))

module High =
    open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.High
    type ExceptionTaskBuilderBase with
        member inline this.ReturnFrom(task: Task<'TResult>) =
            this.Bind(task, (fun v -> this.Return v))

        member inline this.ReturnFrom(task: Async<'TResult>) =
            this.Bind(task, (fun v -> this.Return v))