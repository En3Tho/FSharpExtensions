namespace En3Tho.FSharp.ComputationExpressions.HttpBuilder

open System.Collections.Generic
open System.ComponentModel
open System.Net.Http.Headers
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions

type [<Struct; IsReadOnly; EditorBrowsable(EditorBrowsableState.Value)>] GetHeadersIntrinsic = struct end

type ContentHeadersBuilderCode = delegate of HttpContentHeaders -> unit

type [<Struct; IsReadOnly>] ContentHeadersBuilder =

    member inline _.Bind(_: GetHeadersIntrinsic, [<InlineIfLambda>] builderCode: HttpContentHeaders -> ContentHeadersBuilderCode) : ContentHeadersBuilderCode =
        ContentHeadersBuilderCode(fun builder ->
            builderCode(builder).Invoke(builder))

    member inline _.Combine([<InlineIfLambda>] first: ContentHeadersBuilderCode, [<InlineIfLambda>] second: ContentHeadersBuilderCode) : ContentHeadersBuilderCode =
        ContentHeadersBuilderCode(fun builder ->
            first.Invoke(builder)
            second.Invoke(builder)
        )

    member inline _.Delay([<InlineIfLambda>] delay: unit -> ContentHeadersBuilderCode) =
        fun builder -> (delay()).Invoke(builder)

    member inline _.Yield(kvp: KeyValuePair<string, string>) : ContentHeadersBuilderCode =
        ContentHeadersBuilderCode(fun headers ->
            headers.Add(kvp.Key, kvp.Value)
        )

    member inline this.Zero() : ContentHeadersBuilderCode = ContentHeadersBuilderCode(fun _ -> ())

    member inline this.Run([<InlineIfLambda>] code: ContentHeadersBuilderCode) =
        ContentHeadersBuilderCode(fun builder -> code.Invoke(builder))

type RequestHeadersBuilderCode = delegate of HttpRequestHeaders -> unit

type [<Struct; IsReadOnly>] RequestHeadersBuilder =

    member inline _.Bind(_: GetHeadersIntrinsic, [<InlineIfLambda>] builderCode: HttpRequestHeaders -> RequestHeadersBuilderCode) : RequestHeadersBuilderCode =
        RequestHeadersBuilderCode(fun builder ->
            builderCode(builder).Invoke(builder))

    member inline _.Combine([<InlineIfLambda>] first: RequestHeadersBuilderCode, [<InlineIfLambda>] second: RequestHeadersBuilderCode) : RequestHeadersBuilderCode =
        RequestHeadersBuilderCode(fun builder ->
            first.Invoke(builder)
            second.Invoke(builder)
        )

    member inline _.Delay([<InlineIfLambda>] delay: unit -> RequestHeadersBuilderCode) =
        fun builder -> (delay()).Invoke(builder)

    member inline _.Yield(kvp: KeyValuePair<string, string>) : RequestHeadersBuilderCode =
        RequestHeadersBuilderCode(fun headers ->
            headers.Add(kvp.Key, kvp.Value)
        )

    member inline this.Zero() : RequestHeadersBuilderCode = RequestHeadersBuilderCode(fun _ -> ())

    member inline this.Run([<InlineIfLambda>] code: RequestHeadersBuilderCode) =
        RequestHeadersBuilderCode(fun builder -> code.Invoke(builder))