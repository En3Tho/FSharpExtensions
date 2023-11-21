[<AutoOpen>]
module En3Tho.FSharp.Extensions.AspNetCore.EndpointInfoFactoryExtensions

open System

// these are lower priority extensions so Func<unit, T> or Func<unit, unit> won't get prioritized over Func<T> or Action
type EndpointInfoFactory with

    static member Get(route: string, handler: Func<'T1, 'TResult>) =
        Get(route, handler :> Delegate)

    static member Post(route: string, handler: Func<'T1, 'TResult>) =
        Post(route, handler :> Delegate)

    static member Put(route: string, handler: Func<'T1, 'TResult>) =
        Put(route, handler :> Delegate)

    static member Delete(route: string, handler: Func<'T1, 'TResult>) =
        Delete(route, handler :> Delegate)

    static member Patch(route: string, handler: Func<'T1, 'TResult>) =
        Patch(route, handler :> Delegate)