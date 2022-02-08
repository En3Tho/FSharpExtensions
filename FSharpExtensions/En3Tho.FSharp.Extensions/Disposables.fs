namespace En3Tho.FSharp.Extensions.Disposables

open System
open System.Threading.Tasks

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
type UnitAsyncDisposable(dispose: unit -> ValueTask) =
    interface IAsyncDisposable with
        member this.DisposeAsync() = dispose()

[<Struct>]
type ValueAsyncDisposable<'a>(value: 'a, dispose: 'a -> ValueTask) =
    interface IAsyncDisposable with
        member this.DisposeAsync() = dispose value

[<Struct>]
type Rented<'a>(value: 'a) =
    member _.Value = value

[<Struct>]
type Owned<'a when 'a :> IDisposable>(value: 'a) =
    member _.Value = value
    member _.Rent() = Rented value
    interface IDisposable with
        member this.Dispose() = value.Dispose()

[<Struct>]
type AsyncOwned<'a when 'a :> IAsyncDisposable>(value: 'a) =
    member _.Value = value
    member _.Rent() = Rented value
    interface IAsyncDisposable with
        member this.DisposeAsync() = value.DisposeAsync()

[<Struct>]
type SyncAsyncOwned<'a when 'a :> IDisposable and 'a :> IAsyncDisposable>(value: 'a) =
    member _.Value = value
    member _.Rent() = Rented value
    interface IAsyncDisposable with
        member this.DisposeAsync() = value.DisposeAsync()
    interface IDisposable with
        member this.Dispose() = value.Dispose()