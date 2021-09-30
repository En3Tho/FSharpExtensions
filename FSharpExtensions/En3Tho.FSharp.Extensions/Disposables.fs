namespace En3Tho.FSharp.Extensions.Disposables

open System

[<Struct>]
type ValueDisposable<'a>(value: 'a, dispose: 'a -> unit) =
    member _.Value = value
    interface IDisposable with
        member this.Dispose() = dispose value

[<Struct>]
type UnitDisposable(dispose: unit -> unit) =
    interface IDisposable with
        member this.Dispose() = dispose()

[<Struct>]
type Owned<'a when 'a :> IDisposable>(value: 'a) =
    member _.Value = value
    interface IDisposable with
        member this.Dispose() = value.Dispose()

[<Struct>]
type AsyncOwned<'a when 'a :> IAsyncDisposable>(value: 'a) =
    member _.Value = value
    interface IAsyncDisposable with
        member this.DisposeAsync() = value.DisposeAsync()

[<Struct>]
type Rented<'a when 'a :> IDisposable>(value: 'a) =
    member _.Value = value