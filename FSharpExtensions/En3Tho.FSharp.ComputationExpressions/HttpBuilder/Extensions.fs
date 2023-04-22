module [<AutoOpen>] En3Tho.FSharp.ComputationExpressions.HttpBuilder.Extensions

open System.Collections.Generic
open System.IO
open System.Text.Json
open System.Net.Http

let (@) key value = KeyValuePair(key, value)

type HttpRequestMessageStage with
    member inline this.Json<'a>(value: 'a, options: JsonSerializerOptions) =
        let request = this.Request
        request.Content <- ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes<'a>(value, options))
        request.Headers.Add("Content-Type", "application/json")
        HttpRequestMessageContentStage(this.Client, request)

    member inline this.Json<'a>(value: 'a) =
        this.Json(value, null)

    member inline this.String(value: string) =
        let request = this.Request
        request.Content <- StringContent(value)
        request.Headers.Add("Content-Type", "application/text")
        HttpRequestMessageContentStage(this.Client, request)

    member inline this.ByteArray(value: byte[]) =
        let request = this.Request
        request.Content <- ByteArrayContent(value)
        request.Headers.Add("Content-Type", "application/octet-stream")
        HttpRequestMessageContentStage(this.Client, request)

    member inline this.Stream(value: Stream) =
        let request = this.Request
        request.Content <- StreamContent(value)
        request.Headers.Add("Content-Type", "application/octet-stream")
        HttpRequestMessageContentStage(this.Client, request)

type HttpRequestMessageContentStage with
    member inline this.AsString() =
        HttpRequestMessageResponseStage(this.Client, this.Request, StringResponseSerializer())

    member inline this.AsStream() =
        HttpRequestMessageResponseStage(this.Client, this.Request, StreamResponseSerializer())

    member inline this.AsByteArray() =
        HttpRequestMessageResponseStage(this.Client, this.Request, ByteArrayResponseSerializer())

    member inline this.AsResponse() =
        HttpRequestMessageResponseStage(this.Client, this.Request, RawResponseSerializer())

    member inline this.AsNoResponse() =
        HttpRequestMessageResponseStage(this.Client, this.Request, UnitResponseSerializer())

    member inline this.AsJson<'a>(options: JsonSerializerOptions) =
        HttpRequestMessageResponseStage(this.Client, this.Request, JsonResponseSerializer<'a>(options))

    member inline this.AsJson<'a>() = this.AsJson<'a>(null)

    member inline this.AsJsonResult<'a, 'b>(options: JsonSerializerOptions) =
        HttpRequestMessageResponseStage(this.Client, this.Request, JsonResultResponseSerializer<'a, 'b>(options))

    member inline this.AsJsonResult<'a, 'b>() = this.AsJson<'a>(null)

type HttpClient with
    member inline this.Get(url: string) = HttpRequestMessageContentStage(this, HttpRequestMessage(HttpMethod.Get, url))
    member inline this.Post(url: string) = HttpRequestMessageStage(this, HttpRequestMessage(HttpMethod.Post, url))
    member inline this.Put(url: string) = HttpRequestMessageStage(this, HttpRequestMessage(HttpMethod.Put, url))
    member inline this.Delete(url: string) = HttpRequestMessageStage(this, HttpRequestMessage(HttpMethod.Delete, url))

type TaskBuilder with // ValueTaskBuilder etc?
    member inline this.Bind<'TResponseSerializer, 'TResult, 'TOverall,'TResult2 when 'TResponseSerializer :> IResponseSerializer<'TResult>>
        (respStage: HttpRequestMessageResponseStage<'TResponseSerializer, 'TResult>, [<InlineIfLambda>] continuation: 'TResult -> TaskCode<'TOverall,'TResult2>) =
        this.Bind(task {
            use request = respStage.Request
            let! response = respStage.Client.SendAsync(request)
            return! respStage.Serializer.Serialize(response)
        }, continuation)

    member inline this.Bind<'TOverall,'TResult2>
        (respStage: HttpRequestMessageContentStage, [<InlineIfLambda>] continuation: unit -> TaskCode<'TOverall,'TResult2>) =
        this.Bind(task {
            use request = respStage.Request
            let! response = respStage.Client.SendAsync(request)
            do! UnitResponseSerializer().Serialize(response)

        }, continuation)