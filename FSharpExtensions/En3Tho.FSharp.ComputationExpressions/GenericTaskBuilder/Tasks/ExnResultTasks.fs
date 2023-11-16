namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

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

type ExnResultTaskBuilderBase() =
    inherit GenericTaskBuilderBase()

type ExnResultTaskBuilder() =
    inherit ExnResultTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<Result<'a, exn>, ExnResultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

type ExnResultValueTaskBuilder() =
    inherit ExnResultTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<Result<'a, exn>, ExnResultAsyncTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)

module ExnResultTaskBuilderBaseExtensions =
    ()