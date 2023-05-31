module En3Tho.FSharp.ComputationExpressions.HttpBuilder.Content

open System.IO
open System.Net.Http
open System.Net.Http.Headers
open System.Net.Mime
open System.Text.Json

// TODO: try to experiment with Memory<byte> / Memory<char> based HttpContent?

let getJsonMediaType() =
    MediaTypeHeaderValue(MediaTypeNames.Application.Json, CharSet = "utf-8");
let getStringMediaType() =
    MediaTypeHeaderValue(MediaTypeNames.Text.Plain, CharSet = "utf-8");
let getByteArrayOrStreamMediaType() =
    MediaTypeHeaderValue(MediaTypeNames.Application.Octet);

let makeJsonContent<'a>(value: 'a, options: JsonSerializerOptions) =
    let content = ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes<'a>(value, options))
    content.Headers.ContentType <- getJsonMediaType()
    content

let makeStringContent(value: string) =
    // String content has default utf8 content type anyway
    let content = StringContent(value)
    content

let makeUtf8StringContent(value: byte[]) =
    let content = ByteArrayContent(value)
    content.Headers.ContentType <- getStringMediaType()
    content

let makeByteArrayContent(value: byte[]) =
    // Byte array content and stream content do not have default content type.
    // I'm not sure I have to put content type manually. Maybe octet stream is default ?
    let content = ByteArrayContent(value)
    content.Headers.ContentType <- getByteArrayOrStreamMediaType()
    content

let makeStreamContent(value: Stream) =
    let content = StreamContent(value)
    content.Headers.ContentType <- getByteArrayOrStreamMediaType()
    content

let makeFormUrlEncodedContent values =
    let content = FormUrlEncodedContent(values)
    content