namespace En3Tho.FSharp.ComputationExpressions.HttpBuilder

open System
open System.IO
open System.Net.Http
open System.Runtime.ExceptionServices
open System.Text.Json
open System.Threading.Tasks
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.Tasks
open En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.LowPriority
open En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.HighPriority

/// Serializer is responsible for cleanup unless serialization is bypassed
type IResponseSerializer<'a> =
    abstract member Serialize: response: HttpResponseMessage -> ValueTask<'a>

type [<Struct>] RawResponseSerializer =
    member _.Serialize(response: HttpResponseMessage) =
        ValueTask.FromResult(response)

    interface IResponseSerializer<HttpResponseMessage> with
        member this.Serialize(response) = this.Serialize(response)

type [<Struct>] UnitResponseSerializer =
    member _.Serialize(response: HttpResponseMessage) =
        use response = response
        response.EnsureSuccessStatusCode() |> ignore
        ValueTask.FromResult(())

    interface IResponseSerializer<unit> with
        member this.Serialize(response) = this.Serialize(response)

type [<Struct>] JsonResponseSerializer<'a>(options: JsonSerializerOptions) =
    member _.Serialize(response: HttpResponseMessage) =
        let options = options
        vtask {
            use response = response
            // TODO: use System.Net.Http.Json?
            let! content = response.EnsureSuccessStatusCode().Content.ReadAsStreamAsync()
            return! JsonSerializer.DeserializeAsync<'a>(content, options)
        }

    interface IResponseSerializer<'a> with
        member this.Serialize(response) = this.Serialize(response)

type [<Struct>] StringResponseSerializer =
    member _.Serialize<'a>(response: HttpResponseMessage) =
        vtask {
            use response = response
            return! response.EnsureSuccessStatusCode().Content.ReadAsStringAsync()
        }

    interface IResponseSerializer<string> with
        member this.Serialize(response) = this.Serialize(response)

type [<Struct>] ByteArrayResponseSerializer =
    member _.Serialize<'a>(response: HttpResponseMessage) =
        vtask {
            use response = response
            return! response.EnsureSuccessStatusCode().Content.ReadAsByteArrayAsync()
        }

    interface IResponseSerializer<byte[]> with
        member this.Serialize(response) = this.Serialize(response)

type [<Sealed>] private OwnerDisposingStream(owner: IDisposable, stream: Stream) =
    inherit Stream()

    override this.Flush() = stream.Flush()
    override this.Read(buffer, offset, count) = stream.Read(buffer, offset, count)
    override this.Seek(offset, origin) = stream.Seek(offset, origin)
    override this.SetLength(value) = stream.SetLength(value)
    override this.Write(buffer, offset, count) = stream.Write(buffer, offset, count)
    override this.CanRead = stream.CanRead
    override this.CanSeek = stream.CanSeek
    override this.CanWrite = stream.CanWrite
    override this.Length = stream.Length
    override this.Position
        with get() = stream.Position
         and set value = stream.Position <- value

    override this.Close() = owner.Dispose()

type [<Struct>] StreamResponseSerializer =
    member _.Serialize<'a>(response: HttpResponseMessage) =
        vtask {
            try
                let! stream = response.EnsureSuccessStatusCode().Content.ReadAsStreamAsync()
                return OwnerDisposingStream(response, stream) :> Stream
            with e ->
                response.Dispose()
                ExceptionDispatchInfo.Throw(e)
                return Unchecked.defaultof<_>
        }

    interface IResponseSerializer<Stream> with
        member this.Serialize(response) = this.Serialize(response)

type [<Struct>] ValueOrResponseSerializer<'a, 'serializer when 'serializer :> IResponseSerializer<'a>>(serializer: 'serializer) =
    member _.Serialize<'a>(response: HttpResponseMessage) =
        let serializer = serializer
        vtask {
            if response.IsSuccessStatusCode then
                let! res = serializer.Serialize(response)
                return Ok res
            else
                return Error response
        }

    interface IResponseSerializer<Result<'a, HttpResponseMessage>> with
        member this.Serialize(response) = this.Serialize(response)

type [<Struct>] JsonResultResponseSerializer<'a, 'b>(options: JsonSerializerOptions) =
    member _.Serialize(response: HttpResponseMessage) =
        let options = options
        vtask {
            use response = response
            let! content = response.Content.ReadAsStreamAsync()
            if response.IsSuccessStatusCode then
                let! res = JsonSerializer.DeserializeAsync<'a>(content, options)
                return Ok res
            else
                let! res = JsonSerializer.DeserializeAsync<'b>(content, options)
                return Error res
        }

    interface IResponseSerializer<Result<'a, 'b>> with
        member this.Serialize(response) = this.Serialize(response)

type [<Struct>] JsonStringResultResponseSerializer<'a>(options: JsonSerializerOptions) =
    member _.Serialize(response: HttpResponseMessage) =
        let options = options
        vtask {
            use response = response
            if response.IsSuccessStatusCode then
                let! content = response.Content.ReadAsStreamAsync()
                let! res = JsonSerializer.DeserializeAsync<'a>(content, options)
                return Ok res
            else
                let! res = response.Content.ReadAsStringAsync()
                return Error res
        }

    interface IResponseSerializer<Result<'a, string>> with
        member this.Serialize(response) = this.Serialize(response)