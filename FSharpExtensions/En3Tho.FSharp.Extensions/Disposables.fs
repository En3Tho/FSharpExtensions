namespace En3Tho.FSharp.Extensions.Disposables

open System
open System.Threading.Tasks

[<Struct>]
type ValueDisposable<'a>(value: 'a, dispose: 'a -> unit) =
    interface IDisposable with
        member this.Dispose() = dispose value

[<Struct>]
type ValueDisposable2<'a, 'b>(value: 'a, value2: 'b, dispose: 'a -> 'b -> unit) =
    interface IDisposable with
        member this.Dispose() = dispose value value2

[<Struct>]
type ValueDisposable3<'a, 'b, 'c>(value: 'a, value2: 'b, value3: 'c, dispose: 'a -> 'b -> 'c -> unit) =
    interface IDisposable with
        member this.Dispose() = dispose value value2 value3

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
type ValueAsyncDisposable2<'a, 'b>(value: 'a, value2: 'b, dispose: 'a -> 'b -> ValueTask) =
    interface IAsyncDisposable with
        member this.DisposeAsync() = dispose value value2

[<Struct>]
type ValueAsyncDisposable3<'a, 'b, 'c>(value: 'a, value2: 'b, value3: 'c, dispose: 'a -> 'b -> 'c -> ValueTask) =
    interface IAsyncDisposable with
        member this.DisposeAsync() = dispose value value2 value3

// Hints to myself when dealing with lifetimes
type Rented<'a> = 'a
type Owned<'a> = 'a