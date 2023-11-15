namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

[<Struct>]
type ExnResultAsyncValueTaskMethodBuilderBehavior<'TResult> =
    interface IAsyncValueTaskMethodBuilderBehavior<Result<'TResult, exn>> with

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetException(builder, ``exception``) = builder.SetResult(Error(``exception``))

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetResult(builder, result) = builder.SetResult(result)

type ExnResultValueTaskBuilder() =
    inherit GenericTaskBuilderBase()
    member inline this.Run([<InlineIfLambda>] code) =
       this.RunInternal<StateMachineData<AsyncValueTaskMethodBuilderWrapper<Result<'a, exn>, ExnResultAsyncValueTaskMethodBuilderBehavior<_>>,_,_>,_,_,DefaultStateMachineDataInitializer<_,_,_>>(code)