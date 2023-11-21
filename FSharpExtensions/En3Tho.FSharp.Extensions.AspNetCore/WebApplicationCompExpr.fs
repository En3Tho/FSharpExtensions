[<AutoOpen>]
module En3Tho.FSharp.Extensions.AspNetCore.WebApplicationCompExpr

open System
open System.ComponentModel
open Microsoft.AspNetCore.Builder

[<RequireQualifiedAccess>]
type EndpointType =
    | Get
    | Post
    | Put
    | Delete
    | Patch

type IEndpointInfo =
    abstract Register: webApplication: WebApplication -> RouteHandlerBuilder

type [<Struct>] EndpointInfo(type': EndpointType, route: string, handler: Delegate) =
    member this.Configure(configure) =
        ConfiguredEndpointInfo(this, configure)

    member this.Configure(configure: Action<RouteHandlerBuilder>) =
        ConfiguredEndpointInfo(this, fun builder -> configure.Invoke(builder); builder)

    member _.Register(webApplication: WebApplication) =
        match type' with
        | EndpointType.Get -> webApplication.MapGet(route, handler)
        | EndpointType.Post -> webApplication.MapPost(route, handler)
        | EndpointType.Put -> webApplication.MapPut(route, handler)
        | EndpointType.Delete -> webApplication.MapDelete(route, handler)
        | EndpointType.Patch -> webApplication.MapPatch(route, handler)


    interface IEndpointInfo with
         member this.Register(webApplication) =
             this.Register(webApplication)

and [<Struct>] ConfiguredEndpointInfo<'a when 'a :> IEndpointInfo>(endpointInfo: 'a, configure: Func<RouteHandlerBuilder, RouteHandlerBuilder>) =
    member _.Register(webApplication) =
        let routeHandlerBuilder = endpointInfo.Register(webApplication)
        configure.Invoke(routeHandlerBuilder)

    interface IEndpointInfo with
        member this.Register(webApplication) =
            this.Register(webApplication)

type WebApplication with
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.Zero() = fun() -> ()

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.Run([<InlineIfLambda>] runExpr) = runExpr(); this

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.Yield(endpointInfo: #IEndpointInfo) =
        fun () -> endpointInfo.Register(this) |> ignore