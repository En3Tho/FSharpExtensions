module [<AutoOpen>] En3Tho.FSharp.ComputationExpressions.HttpBuilder.Extensions

open System.IO
open System.Text.Json
open System.Net.Http
open En3Tho.FSharp.ComputationExpressions.HttpBuilder.Content

let requestHeaders = RequestHeadersBuilder()
let contentHeaders = ContentHeadersBuilder()

let getRequest() = GetRequestIntrinsic()
let getHeaders() = GetHeadersIntrinsic()

type HttpRequestMessageStage with

    member inline this.CustomContent(value: HttpContent) =
        let request = this.Request
        request.Content <- value
        HttpRequestMessageContentStage(this.Client, request)

    member inline this.Json<'a>(value: 'a, options: JsonSerializerOptions) =
        this.CustomContent(makeJsonContent(value, options))

    member inline this.Json<'a>(value: 'a) =
        this.Json(value, null)

    member inline this.String(value: string) =
        this.CustomContent(makeStringContent(value))

    member inline this.String(value: byte[]) =
        this.CustomContent(makeUtf8StringContent(value))

    member inline this.ByteArray(value: byte[]) =
        this.CustomContent(makeByteArrayContent(value))

    member inline this.Stream(value: Stream) =
        this.CustomContent(makeStreamContent(value))

    member inline this.Form(values) =
        this.CustomContent(makeFormUrlEncodedContent(values))

    member inline this.EmptyContent() =
        HttpRequestMessageContentStage(this.Client, this.Request)

type HttpRequestMessageContentStage with
    member inline this.AsString() =
        HttpRequestMessageResponseStage(this.Client, this.Request, StringResponseSerializer())

    member inline this.AsStringOrResponse() =
        HttpRequestMessageResponseStage(this.Client, this.Request, ValueOrResponseSerializer(StringResponseSerializer()))

    member inline this.AsStream() =
        HttpRequestMessageResponseStage(this.Client, this.Request, StreamResponseSerializer())

    member inline this.AsStreamOrResponse() =
        HttpRequestMessageResponseStage(this.Client, this.Request, ValueOrResponseSerializer(StreamResponseSerializer()))

    member inline this.AsByteArray() =
        HttpRequestMessageResponseStage(this.Client, this.Request, ByteArrayResponseSerializer())

    member inline this.AsByteArrayOrResponse() =
        HttpRequestMessageResponseStage(this.Client, this.Request, ValueOrResponseSerializer(ByteArrayResponseSerializer()))

    member inline this.AsResponse() =
        HttpRequestMessageResponseStage(this.Client, this.Request, RawResponseSerializer())

    member inline this.AsNoResponse() =
        HttpRequestMessageResponseStage(this.Client, this.Request, UnitResponseSerializer())

    member inline this.AsJson<'a>(options: JsonSerializerOptions) =
        HttpRequestMessageResponseStage(this.Client, this.Request, JsonResponseSerializer<'a>(options))

    member inline this.AsJsonOrResponse<'a>(options: JsonSerializerOptions) =
        HttpRequestMessageResponseStage(this.Client, this.Request, ValueOrResponseSerializer(JsonResponseSerializer<'a>(options)))

    member inline this.AsJson<'a>() = this.AsJson<'a>(null)

    member inline this.AsJsonOrResponse<'a>() = this.AsJsonOrResponse<'a>(null)

    member inline this.AsJsonResult<'a, 'b>(options: JsonSerializerOptions) =
        HttpRequestMessageResponseStage(this.Client, this.Request, JsonResultResponseSerializer<'a, 'b>(options))

    member inline this.AsJsonResult<'a, 'b>() = this.AsJson<'a>(null)

type HttpClient with
    member inline this.Get(url: string) = HttpRequestMessageContentStage(this, HttpRequestMessage(HttpMethod.Get, url))
    member inline this.Post(url: string) = HttpRequestMessageStage(this, HttpRequestMessage(HttpMethod.Post, url))
    member inline this.Put(url: string) = HttpRequestMessageStage(this, HttpRequestMessage(HttpMethod.Put, url))
    member inline this.Delete(url: string) = HttpRequestMessageStage(this, HttpRequestMessage(HttpMethod.Delete, url))