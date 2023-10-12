// auto-generated
namespace En3Tho.FSharp.Extensions.AspNetCore

open System

[<AbstractClass; Sealed; AutoOpen>]
type EndpointInfoFactory() =

    static member Get(route: string, handler) =
        EndpointInfo(EndpointType.Get, route, handler)
    static member Get(route: string, handler: Action) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'TResult>) =
        Get(route, handler :> Delegate)
    static member Get(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, 'TResult>) =
        Get(route, handler :> Delegate)

    static member Post(route: string, handler) =
        EndpointInfo(EndpointType.Post, route, handler)
    static member Post(route: string, handler: Action) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'TResult>) =
        Post(route, handler :> Delegate)
    static member Post(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, 'TResult>) =
        Post(route, handler :> Delegate)

    static member Put(route: string, handler) =
        EndpointInfo(EndpointType.Put, route, handler)
    static member Put(route: string, handler: Action) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'TResult>) =
        Put(route, handler :> Delegate)
    static member Put(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, 'TResult>) =
        Put(route, handler :> Delegate)

    static member Delete(route: string, handler) =
        EndpointInfo(EndpointType.Delete, route, handler)
    static member Delete(route: string, handler: Action) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'TResult>) =
        Delete(route, handler :> Delegate)
    static member Delete(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, 'TResult>) =
        Delete(route, handler :> Delegate)

    static member Patch(route: string, handler) =
        EndpointInfo(EndpointType.Patch, route, handler)
    static member Patch(route: string, handler: Action) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'TResult>) =
        Patch(route, handler :> Delegate)
    static member Patch(route: string, handler: Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, 'TResult>) =
        Patch(route, handler :> Delegate)