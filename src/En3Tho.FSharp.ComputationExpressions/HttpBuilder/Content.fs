module En3Tho.FSharp.ComputationExpressions.HttpBuilder.Content

open System.IO
open System.Net.Http
open System.Net.Http.Headers
open System.Net.Mime
open System.Text.Json

// TODO: try to experiment with Memory<byte> / Memory<char> based HttpContent?

let inline getJsonMediaType() =
    MediaTypeHeaderValue(MediaTypeNames.Application.Json, CharSet = "utf-8")

let inline getStringMediaType() =
    MediaTypeHeaderValue(MediaTypeNames.Text.Plain, CharSet = "utf-8")

let inline getByteArrayOrStreamMediaType() =
    MediaTypeHeaderValue(MediaTypeNames.Application.Octet)

let inline makeJsonContent<'a>(value: 'a, options: JsonSerializerOptions) =
    let content = ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes<'a>(value, options))
    content.Headers.ContentType <- getJsonMediaType()
    content

let inline makeStringContent(value: string) =
    // String content has default utf8 content type anyway
    let content = StringContent(value)
    content

let inline makeUtf8StringContent(value: byte[]) =
    let content = ByteArrayContent(value)
    content.Headers.ContentType <- getStringMediaType()
    content

let inline makeByteArrayContent(value: byte[]) =
    // Byte array content and stream content do not have default content type.
    // I'm not sure I have to put content type manually. Maybe octet stream is default ?
    let content = ByteArrayContent(value)
    content.Headers.ContentType <- getByteArrayOrStreamMediaType()
    content

let inline makeStreamContent(value: Stream) =
    let content = StreamContent(value)
    content.Headers.ContentType <- getByteArrayOrStreamMediaType()
    content

let inline makeFormUrlEncodedContent values =
    let content = FormUrlEncodedContent(values)
    content