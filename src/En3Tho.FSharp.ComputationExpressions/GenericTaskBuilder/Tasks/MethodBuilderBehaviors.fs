namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

open System.Runtime.CompilerServices

type IAsyncValueTaskMethodBuilderBehavior<'TResult> =
    static abstract SetException: builder: byref<AsyncValueTaskMethodBuilder<'TResult>> * ``exception``: exn -> unit
    static abstract SetResult: builder: byref<AsyncValueTaskMethodBuilder<'TResult>> * result: 'TResult -> unit

type IAsyncValueTaskMethodBuilderBehavior =
    static abstract SetException: builder: byref<AsyncValueTaskMethodBuilder> * ``exception``: exn -> unit
    static abstract SetResult: builder: byref<AsyncValueTaskMethodBuilder> -> unit

type IAsyncTaskMethodBuilderBehavior<'TResult> =
    static abstract SetException: builder: byref<AsyncTaskMethodBuilder<'TResult>> * ``exception``: exn -> unit
    static abstract SetResult: builder: byref<AsyncTaskMethodBuilder<'TResult>> * result: 'TResult -> unit

type IAsyncTaskMethodBuilderBehavior =
    static abstract SetException: builder: byref<AsyncTaskMethodBuilder> * ``exception``: exn -> unit
    static abstract SetResult: builder: byref<AsyncTaskMethodBuilder> -> unit

[<Struct>]
type DefaultAsyncTaskMethodBuilderBehavior<'TResult> =
    interface IAsyncValueTaskMethodBuilderBehavior<'TResult> with

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetException(builder, ``exception``) = builder.SetException(``exception``)

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetResult(builder, result) = builder.SetResult(result)

    interface IAsyncTaskMethodBuilderBehavior<'TResult> with

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetException(builder, ``exception``) = builder.SetException(``exception``)

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetResult(builder, result) = builder.SetResult(result)

[<Struct>]
type DefaultAsyncTaskMethodBuilderBehavior =
    interface IAsyncValueTaskMethodBuilderBehavior with

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetException(builder, ``exception``) = builder.SetException(``exception``)

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetResult(builder) = builder.SetResult()

    interface IAsyncTaskMethodBuilderBehavior with

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetException(builder, ``exception``) = builder.SetException(``exception``)

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member SetResult(builder) = builder.SetResult()