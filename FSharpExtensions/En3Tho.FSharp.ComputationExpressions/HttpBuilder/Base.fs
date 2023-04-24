namespace En3Tho.FSharp.ComputationExpressions.HttpBuilder

open System.Collections.Generic
open System.Net.Http
open System.Runtime.CompilerServices
open En3Tho.FSharp.ComputationExpressions

type [<Struct; IsReadOnly>] HttpRequestMessageStage(client: HttpClient, request: HttpRequestMessage) =
    member _.Client = client
    member _.Request = request

type [<Struct; IsReadOnly>] HttpRequestMessageContentStage(client: HttpClient, request: HttpRequestMessage) =
    member _.Client = client
    member _.Request = request

    member this.SendRequest() =
        let this = this
        task {
            use request = this.Request
            let! response = this.Client.SendAsync(request)
            do! UnitResponseSerializer().Serialize(response)
        }

    member this.GetAwaiter() = this.SendRequest().GetAwaiter()

type [<Struct; IsReadOnly>] HttpRequestMessageResponseStage<'TResponseSerializer, 'TResult
                                                    when 'TResponseSerializer :> IResponseSerializer<'TResult>>
    (client: HttpClient, request: HttpRequestMessage, serializer: 'TResponseSerializer) =
    member _.Client = client
    member _.Request = request
    member _.Serializer = serializer

    member this.SendRequest() =
        let this = this
        task {
            use request = this.Request
            let! response = this.Client.SendAsync(request)
            return! this.Serializer.Serialize(response)
        }

    member this.GetAwaiter() = this.SendRequest().GetAwaiter()

    member inline this.Add(kvp: KeyValuePair<string, string>) =
        this.Request.Headers.Add(kvp.Key, kvp.Value)

    member inline this.Add((key, value): string * string) =
        this.Request.Headers.Add(key, value)

    member inline this.Add(key: string, value: string) =
        this.Request.Headers.Add(key, value)

    member inline this.Zero() : CollectionCode = fun() -> ()

    member inline this.Run([<InlineIfLambda>] code: CollectionCode) =
        code()
        this