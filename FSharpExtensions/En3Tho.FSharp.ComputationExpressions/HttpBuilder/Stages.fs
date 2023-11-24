namespace En3Tho.FSharp.ComputationExpressions.HttpBuilder

open System.ComponentModel
open System.Net.Http
open System.Runtime.CompilerServices

[<Struct; IsReadOnly; EditorBrowsable(EditorBrowsableState.Never)>]
type HttpRequestMessageStage(client: HttpClient, request: HttpRequestMessage) =
    member _.Client = client
    member _.Request = request

[<Struct; IsReadOnly; EditorBrowsable(EditorBrowsableState.Never)>]
type HttpRequestMessageContentStage(client: HttpClient, request: HttpRequestMessage) =
    member _.Client = client
    member _.Request = request

[<EditorBrowsable(EditorBrowsableState.Never)>]
type HttpRequestMessageResponseStageCode = delegate of HttpRequestMessage -> unit

[<Struct; IsReadOnly; EditorBrowsable(EditorBrowsableState.Never)>]
type GetRequestIntrinsic = struct end
type [<Struct; IsReadOnly>] HttpRequestMessageResponseStage<'TResponseSerializer, 'TResult
    when 'TResponseSerializer :> IResponseSerializer<'TResult>>
    (client: HttpClient, request: HttpRequestMessage, serializer: 'TResponseSerializer) =
    member _.Client = client
    member _.Request = request
    member _.Serializer = serializer

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.Send() =
        let request = this.Request
        let client = this.Client
        let serializer = this.Serializer
        task {
            use request = request
            let! response = client.SendAsync(request)
            return! serializer.Serialize(response)
        }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.GetAwaiter() = this.Send().GetAwaiter()

    member inline this.Bind(_: GetRequestIntrinsic, [<InlineIfLambda>] code: HttpRequestMessage -> HttpRequestMessageResponseStageCode) : HttpRequestMessageResponseStageCode =
        HttpRequestMessageResponseStageCode(fun request -> (code request).Invoke request)

    member inline this.Yield([<InlineIfLambda>] code: RequestHeadersBuilderCode) : HttpRequestMessageResponseStageCode =
        HttpRequestMessageResponseStageCode(fun request -> code.Invoke request.Headers)

    member inline this.Yield([<InlineIfLambda>] code: ContentHeadersBuilderCode) : HttpRequestMessageResponseStageCode =
        HttpRequestMessageResponseStageCode(fun request -> code.Invoke request.Content.Headers)

    member inline _.Combine([<InlineIfLambda>] first: HttpRequestMessageResponseStageCode, [<InlineIfLambda>] second: HttpRequestMessageResponseStageCode) : HttpRequestMessageResponseStageCode =
        HttpRequestMessageResponseStageCode(fun builder ->
            first.Invoke(builder)
            second.Invoke(builder)
        )

    member inline _.Delay([<InlineIfLambda>] delay: unit -> HttpRequestMessageResponseStageCode) =
        fun builder -> (delay()).Invoke(builder)

    member inline this.Zero() : HttpRequestMessageResponseStageCode = HttpRequestMessageResponseStageCode(fun _ -> ())

    member inline this.Run([<InlineIfLambda>] code: HttpRequestMessageResponseStageCode) =
        code.Invoke(this.Request)
        this