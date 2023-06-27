module En3Tho.FSharp.Extensions.GSeqEnumerators

open System.Collections.Generic
open System.Collections.Immutable
open System.Runtime.CompilerServices

type SStructEnumerator<'i, 'e when 'e: struct
                              and 'e :> IEnumerator<'i>> = 'e

[<Struct; NoComparison; NoEquality>]
type StructIEnumeratorWrapper<'i, 'e when 'e :> IEnumerator<'i>> =
    val mutable private enumerator: 'e

    new (enumerator) = { enumerator = enumerator }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() = this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member this.Dispose() = this.enumerator.Dispose()

    interface IEnumerator<'i> with
        member this.Current = this.enumerator.Current :> obj
        member this.Current = this.enumerator.Current
        member this.Dispose() = this.enumerator.Dispose()
        member this.MoveNext() = this.enumerator.MoveNext()
        member this.Reset() = this.enumerator.Reset()

[<Struct; NoComparison; NoEquality>]
type StructArrayEnumerator<'i> =
    val private data: 'i[]
    val mutable private index: int

    new (data) = { index = -1; data = data; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index + 1
        uint32 this.index < uint32 this.data.Length

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructResizeArrayEnumerator<'i> =
    val private data: 'i ResizeArray
    val mutable private index: int

    new (data) = { index = -1; data = data; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index + 1
        uint32 this.index < uint32 this.data.Count

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructIListEnumerator<'i, 'list when 'list :> IList<'i>> =
    val mutable private data: 'list
    val mutable private index: int

    new (data) = { index = -1; data = data; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index + 1
        uint32 this.index < uint32 this.data.Count

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructArrayRevEnumerator<'i> =
    val private data: 'i[]
    val mutable private index: int

    new (data) = { data = data; index = data.Length }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index - 1
        uint32 this.index < uint32 this.data.Length

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructResizeArrayRevEnumerator<'i> =
    val private data: 'i ResizeArray
    val mutable private index: int

    new (data) = { data = data; index = data.Count }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index - 1
        uint32 this.index < uint32 this.data.Count

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructIListRevEnumerator<'i, 'list when 'list :> IList<'i>> =
    val mutable private data: 'list
    val mutable private index: int

    new (data) = { data = data; index = data.Count }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index - 1
        uint32 this.index < uint32 this.data.Count

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructImmutableArrayEnumerator<'i> =
    val private data: 'i ImmutableArray
    val mutable private index: int

    new (data) = { index = -1; data = data; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index + 1
        uint32 this.index < uint32 this.data.Length

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructImmutableArrayRevEnumerator<'i> =
    val private data: 'i ImmutableArray
    val mutable private index: int

    new (data) = { index = -1; data = data; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index - 1
        uint32 this.index < uint32 this.data.Length

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructFSharpListEnumerator<'i> =
    val mutable private list: 'i list
    val mutable private prev: 'i list

    new (list) = { prev = list; list = list; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        match this.list with
        | [] -> false
        | _ :: rest ->
            this.prev <- this.list
            this.list <- rest
            true
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = this.prev.Head

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapEnumerator<'i, 'res, 'e when 'e: struct
                                       and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val private map: 'i -> 'res

    new (map, enumerator) = { enumerator = enumerator; map = map; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() = this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current |> this.map

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapVEnumerator<'i, 'state, 'res, 'e when 'e: struct
                                       and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val private state: 'state
    val private map: OptimizedClosures.FSharpFunc<'state, 'i, 'res>

    new (state, map, enumerator) =
        { enumerator = enumerator; map = OptimizedClosures.FSharpFunc<_,_,_>.Adapt map; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.state, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapiEnumerator<'i, 'res, 'e when 'e: struct
                                       and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private count: int
    val private map: OptimizedClosures.FSharpFunc<int, 'i, 'res>

    new (map, enumerator) =
        { enumerator = enumerator; map = OptimizedClosures.FSharpFunc<_,_,_>.Adapt map; count = -1 }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapiVEnumerator<'i, 'state, 'res, 'e when 'e: struct
                                       and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private count: int
    val private state: 'state
    val private map: OptimizedClosures.FSharpFunc<int, 'state, 'i, 'res>

    new (state, map, enumerator) =
        { enumerator = enumerator; map = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt map; count = -1; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.state, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMap2Enumerator<'i, 'i2, 'res, 'e, 'e2 when 'e: struct
                                                  and 'e :> IEnumerator<'i>
                                                  and 'e2: struct
                                                  and 'e2 :> IEnumerator<'i2>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val private map: OptimizedClosures.FSharpFunc<'i2, 'i, 'res>

    new (map, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; map = OptimizedClosures.FSharpFunc<_,_,_>.Adapt map; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() = this.enumerator.MoveNext() && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.enumerator2.Current, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

 [<Struct; NoComparison; NoEquality>]
type StructMap2VEnumerator<'i, 'i2, 'state, 'res, 'e, 'e2 when 'e: struct
                                                  and 'e :> IEnumerator<'i>
                                                  and 'e2: struct
                                                  and 'e2 :> IEnumerator<'i2>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val private map: OptimizedClosures.FSharpFunc<'state, 'i2, 'i, 'res>
    val private state: 'state

    new (state, map, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; map = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt map; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() = this.enumerator.MoveNext() && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.state, this.enumerator2.Current, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapi2Enumerator<'i, 'i2, 'res, 'e, 'e2 when 'e: struct
                                                  and 'e :> IEnumerator<'i>
                                                  and 'e2: struct
                                                  and 'e2 :> IEnumerator<'i2>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private count: int
    val private map: OptimizedClosures.FSharpFunc<int, 'i2, 'i, 'res>

    new (map, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; map = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt map; count = -1; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.enumerator2.Current, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapi2VEnumerator<'i, 'i2, 'state, 'res, 'e, 'e2 when 'e: struct
                                                  and 'e :> IEnumerator<'i>
                                                  and 'e2: struct
                                                  and 'e2 :> IEnumerator<'i2>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private count: int
    val private map: OptimizedClosures.FSharpFunc<int, 'state, 'i2, 'i, 'res>
    val private state: 'state

    new (state, map, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; map = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt map; count = -1; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.state, this.enumerator2.Current, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMap3Enumerator<'i, 'i2, 'i3, 'res, 'e, 'e2, 'e3 when 'e: struct
                                                            and 'e :> IEnumerator<'i>
                                                            and 'e2: struct
                                                            and 'e2 :> IEnumerator<'i2>
                                                            and 'e3: struct
                                                            and 'e3 :> IEnumerator<'i3>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private enumerator3: SStructEnumerator<'i3, 'e3>
    val private map: OptimizedClosures.FSharpFunc<'i2, 'i3, 'i, 'res>

    new (map, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; map = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt map; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()
        && this.enumerator3.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMap3VEnumerator<'i, 'i2, 'i3, 'state, 'res, 'e, 'e2, 'e3 when 'e: struct
                                                            and 'e :> IEnumerator<'i>
                                                            and 'e2: struct
                                                            and 'e2 :> IEnumerator<'i2>
                                                            and 'e3: struct
                                                            and 'e3 :> IEnumerator<'i3>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private enumerator3: SStructEnumerator<'i3, 'e3>
    val private map: OptimizedClosures.FSharpFunc<'state, 'i2, 'i3, 'i, 'res>
    val private state: 'state

    new (state, map, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; map = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt map; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()
        && this.enumerator3.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.state, this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapi3Enumerator<'i, 'i2, 'i3, 'res, 'e, 'e2, 'e3 when 'e: struct
                                                            and 'e :> IEnumerator<'i>
                                                            and 'e2: struct
                                                            and 'e2 :> IEnumerator<'i2>
                                                            and 'e3: struct
                                                            and 'e3 :> IEnumerator<'i3>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private enumerator3: SStructEnumerator<'i3, 'e3>
    val mutable private count: int
    val private map: OptimizedClosures.FSharpFunc<int, 'i2, 'i3, 'i, 'res>

    new (map, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; map = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt map; count = -1 }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()
        && this.enumerator3.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapi3VEnumerator<'i, 'i2, 'i3, 'state, 'res, 'e, 'e2, 'e3 when 'e: struct
                                                            and 'e :> IEnumerator<'i>
                                                            and 'e2: struct
                                                            and 'e2 :> IEnumerator<'i2>
                                                            and 'e3: struct
                                                            and 'e3 :> IEnumerator<'i3>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private enumerator3: SStructEnumerator<'i3, 'e3>
    val mutable private count: int
    val private map: OptimizedClosures.FSharpFunc<int, 'state, 'i2, 'i3, 'i, 'res>
    val private state: 'state

    new (state, map, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; map = OptimizedClosures.FSharpFunc<_,_,_,_,_,_>.Adapt map; count = -1; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()
        && this.enumerator3.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.state, this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructFilterEnumerator<'i, 'e when 'e: struct
                                    and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val private filter: 'i -> bool

    new (filter, enumerator) = { enumerator = enumerator; filter = filter; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        let mutable found = false
        while not found && this.enumerator.MoveNext() do
            found <- this.enumerator.Current |> this.filter
        found
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructFilterVEnumerator<'i, 'state, 'e when 'e: struct
                                    and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val private filter: OptimizedClosures.FSharpFunc<'state, 'i, bool>
    val private state: 'state

    new (state, filter, enumerator) = { enumerator = enumerator; filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt filter; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        let mutable found = false
        while not found && this.enumerator.MoveNext() do
            found <- this.filter.Invoke(this.state, this.enumerator.Current)
        found
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructTakeEnumerator<'i, 'e when 'e: struct
                                  and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private count: int

    new (count, enumerator) = { enumerator = enumerator; count = count; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        let oldCount = this.count
        this.count <- this.count + 1
        oldCount > 0 && this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructTakeWhileEnumerator<'i, 'e when 'e: struct
                                       and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val private filter: 'i -> bool

    new (filter, enumerator) = { enumerator = enumerator; filter = filter }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator.MoveNext() && this.filter this.enumerator.Current

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructTakeWhileVEnumerator<'i, 'state, 'e when 'e: struct
                                       and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val private filter: OptimizedClosures.FSharpFunc<'state, 'i, bool>
    val private state: 'state

    new (state, filter, enumerator) = { enumerator = enumerator; filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt filter; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator.MoveNext() && this.filter.Invoke(this.state, this.enumerator.Current)

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructSkipEnumerator<'i, 'e when 'e: struct
                                  and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private count: int

    new (count, enumerator) = { enumerator = enumerator; count = count; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        while this.count > 0 && this.enumerator.MoveNext() do
            this.count <- this.count - 1
        this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructSkipWhileEnumerator<'i, 'e when 'e: struct
                                       and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val private filter: 'i -> bool
    val mutable private flag: int

    new (filter, enumerator) = { enumerator = enumerator; filter = filter; flag = 0 }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        match this.flag with
        | 0 ->
            let mutable skip = true
            while skip && this.enumerator.MoveNext() do
                skip <- this.filter this.enumerator.Current
            this.flag <- 1
            not skip
        | _ ->
            this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructSkipWhileVEnumerator<'i, 'state, 'e when 'e: struct
                                       and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val private filter: OptimizedClosures.FSharpFunc<'state, 'i, bool>
    val mutable private flag: int
    val private state: 'state

    new (state, filter, enumerator) = { enumerator = enumerator; filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt filter; flag = 0; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        match this.flag with
        | 0 ->
            let mutable skip = true
            while skip && this.enumerator.MoveNext() do
                skip <- this.filter.Invoke(this.state, this.enumerator.Current)
            this.flag <- 1
            not skip
        | _ ->
            this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChooseEnumerator<'i, 'res, 'e when 'e: struct
                                          and 'e :> IEnumerator<'i>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private current: 'res option
    val private chooser: 'i -> 'res option

    new (chooser, enumerator) = { enumerator = enumerator; chooser = chooser; current = None }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() do
            this.current <- this.chooser this.enumerator.Current
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChooseVEnumerator<'i, 'state, 'res, 'e when 'e: struct
                                                     and 'e :> IEnumerator<'i>> =

    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private current: 'res option
    val private chooser: OptimizedClosures.FSharpFunc<'state, 'i, 'res option>
    val private state: 'state

    new (state, chooser, enumerator) = { enumerator = enumerator; chooser = OptimizedClosures.FSharpFunc<_,_,_>.Adapt chooser; current = None; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChoose2Enumerator<'i, 'i2, 'res, 'e, 'e2 when 'e: struct
                                                     and 'e :> IEnumerator<'i>
                                                     and 'e2: struct
                                                     and 'e2 :> IEnumerator<'i2>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private current: 'res option
    val private chooser: OptimizedClosures.FSharpFunc<'i2, 'i, 'res option>

    new (chooser, enumerator, enumerator2) = { enumerator = enumerator; enumerator2 = enumerator2; chooser = OptimizedClosures.FSharpFunc<_,_,_>.Adapt chooser; current = None }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() do
            this.current <- this.chooser.Invoke(this.enumerator2.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChoose2VEnumerator<'i, 'i2, 'state, 'res, 'e, 'e2 when 'e: struct
                                                     and 'e :> IEnumerator<'i>
                                                     and 'e2: struct
                                                     and 'e2 :> IEnumerator<'i2>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private current: 'res option
    val private chooser: OptimizedClosures.FSharpFunc<'state, 'i2, 'i, 'res option>
    val private state: 'state

    new (state, chooser, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; chooser = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt chooser; current = None; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator2.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChoose3Enumerator<'i, 'i2, 'i3, 'res, 'e, 'e2, 'e3 when 'e: struct
                                                               and 'e :> IEnumerator<'i>
                                                               and 'e2: struct
                                                               and 'e2 :> IEnumerator<'i2>
                                                               and 'e3: struct
                                                               and 'e3 :> IEnumerator<'i3>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private enumerator3: SStructEnumerator<'i3, 'e3>
    val mutable private current: 'res option
    val private chooser: OptimizedClosures.FSharpFunc<'i2, 'i3, 'i, 'res option>

    new (chooser, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; chooser = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt chooser; current = None }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() && this.enumerator3.MoveNext() do
            this.current <- this.chooser.Invoke(this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChoose3VEnumerator<'i, 'i2, 'i3, 'state, 'res, 'e, 'e2, 'e3 when 'e: struct
                                                               and 'e :> IEnumerator<'i>
                                                               and 'e2: struct
                                                               and 'e2 :> IEnumerator<'i2>
                                                               and 'e3: struct
                                                               and 'e3 :> IEnumerator<'i3>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private enumerator3: SStructEnumerator<'i3, 'e3>
    val mutable private current: 'res option
    val private chooser: OptimizedClosures.FSharpFunc<'state, 'i2, 'i3, 'i, 'res option>
    val private state: 'state

    new (state, chooser, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; chooser = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt chooser; current = None; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() && this.enumerator3.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructValueChooseEnumerator<'i, 'res, 'e when 'e: struct
                                               and 'e :> IEnumerator<'i>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private current: 'res voption
    val private chooser: 'i -> 'res voption

    new (chooser, enumerator) = { enumerator = enumerator; chooser = chooser; current = ValueNone }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() do
            this.current <- this.chooser this.enumerator.Current
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructValueChooseVEnumerator<'i, 'state, 'res, 'e when 'e: struct
                                               and 'e :> IEnumerator<'i>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private current: 'res voption
    val private chooser: OptimizedClosures.FSharpFunc<'state, 'i, 'res voption>
    val private state: 'state

    new (state, chooser, enumerator) = { enumerator = enumerator; chooser = OptimizedClosures.FSharpFunc<_,_,_>.Adapt chooser; current = ValueNone; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructValueChoose2Enumerator<'i, 'i2, 'res, 'e, 'e2 when 'e: struct
                                                          and 'e :> IEnumerator<'i>
                                                          and 'e2: struct
                                                          and 'e2 :> IEnumerator<'i2>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private current: 'res voption
    val private chooser: OptimizedClosures.FSharpFunc<'i2, 'i, 'res voption>

    new (chooser, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; chooser = OptimizedClosures.FSharpFunc<_,_,_>.Adapt chooser; current = ValueNone }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() do
            this.current <- this.chooser.Invoke(this.enumerator2.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructValueChoose2VEnumerator<'i, 'i2, 'state, 'res, 'e, 'e2 when 'e: struct
                                                          and 'e :> IEnumerator<'i>
                                                          and 'e2: struct
                                                          and 'e2 :> IEnumerator<'i2>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private current: 'res voption
    val private chooser: OptimizedClosures.FSharpFunc<'state, 'i2, 'i, 'res voption>
    val private state: 'state

    new (state, chooser, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; chooser = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt chooser; current = ValueNone; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator2.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructValueChoose3Enumerator<'i, 'i2, 'i3, 'res, 'e, 'e2, 'e3 when 'e: struct
                                                                    and 'e :> IEnumerator<'i>
                                                                    and 'e2: struct
                                                                    and 'e2 :> IEnumerator<'i2>
                                                                    and 'e3: struct
                                                                    and 'e3 :> IEnumerator<'i3>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private enumerator3: SStructEnumerator<'i3, 'e3>
    val mutable private current: 'res voption
    val private chooser: OptimizedClosures.FSharpFunc<'i2, 'i3, 'i, 'res voption>

    new (chooser, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; chooser = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt chooser; current = ValueNone }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() && this.enumerator3.MoveNext() do
            this.current <- this.chooser.Invoke(this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructValueChoose3VEnumerator<'i, 'i2, 'i3, 'state, 'res, 'e, 'e2, 'e3 when 'e: struct
                                                                    and 'e :> IEnumerator<'i>
                                                                    and 'e2: struct
                                                                    and 'e2 :> IEnumerator<'i2>
                                                                    and 'e3: struct
                                                                    and 'e3 :> IEnumerator<'i3>> =
    val mutable private enumerator: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>
    val mutable private enumerator3: SStructEnumerator<'i3, 'e3>
    val mutable private current: 'res voption
    val private chooser: OptimizedClosures.FSharpFunc<'state, 'i2, 'i3, 'i, 'res voption>
    val private state: 'state

    new (state, chooser, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; chooser = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt chooser; current = ValueNone; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() && this.enumerator3.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member this.Dispose() = ()

    interface IEnumerator<'res> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructAppendEnumerator<'i, 'e when 'e: struct
                                    and 'e :> IEnumerator<'i>> =

    val mutable private enumerator1: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i, 'e>
    val mutable private index: int

    new (enumerator1, enumerator2) = { enumerator1 = enumerator1; enumerator2 = enumerator2; index = 0 }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        match this.index with
        | 0 ->
            match this.enumerator1.MoveNext() with
            | true -> true
            | _ ->
                this.index <- 1
                this.enumerator2.MoveNext()
        | _ ->
            this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        match this.index with
        | 0 -> this.enumerator1.Current
        | _ -> this.enumerator2.Current

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructZipEnumerator<'i, 'i2, 'e, 'e2 when 'e: struct
                                           and 'e :> IEnumerator<'i>
                                           and 'e2: struct
                                           and 'e2 :> IEnumerator<'i2>> =

    val mutable private enumerator1: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>

    new (enumerator1, enumerator2) = { enumerator1 = enumerator1; enumerator2 = enumerator2 }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator1.MoveNext()
        && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator2.Current, this.enumerator1.Current

    member this.Dispose() = ()

    interface IEnumerator<'i2 * 'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructValueZipEnumerator<'i, 'i2, 'e, 'e2 when 'e: struct
                                                and 'e :> IEnumerator<'i>
                                                and 'e2: struct
                                                and 'e2 :> IEnumerator<'i2>> =
    val mutable private enumerator1: SStructEnumerator<'i, 'e>
    val mutable private enumerator2: SStructEnumerator<'i2, 'e2>

    new (enumerator1, enumerator2) = { enumerator1 = enumerator1; enumerator2 = enumerator2 }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator1.MoveNext()
        && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        struct (this.enumerator2.Current, this.enumerator1.Current)

    member this.Dispose() = ()

    interface IEnumerator<struct('i2 * 'i)> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructInitEnumerator<'i> =
    val mutable private length: int
    val mutable private count: int
    val private initializer: int -> 'i

    new (count, initializer) = { length = -1; count = count; initializer = initializer }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.length <- this.length + 1
        this.length < this.count

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = this.initializer this.length

    member this.Dispose() = ()

    interface IEnumerator<'i> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructInitInfiniteEnumerator<'i> =
    val mutable private length: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() = true
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = this.length - 1

    member this.Dispose() = ()

    interface IEnumerator<int> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()