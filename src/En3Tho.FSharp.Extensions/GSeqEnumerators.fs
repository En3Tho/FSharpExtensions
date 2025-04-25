module En3Tho.FSharp.Extensions.GSeqEnumerators

open System.Collections.Generic
open System.Collections.Immutable
open System.Numerics
open System.Runtime.CompilerServices

type SStructEnumerator<'T, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> = 'TEnumerator

[<Struct; NoComparison; NoEquality>]
type StructIEnumeratorWrapper<'T, 'TEnumerator when 'TEnumerator :> IEnumerator<'T>> =
    val mutable private enumerator: 'TEnumerator

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (enumerator) = { enumerator = enumerator }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() = this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member this.Dispose() = this.enumerator.Dispose()

    interface IEnumerator<'T> with
        member this.Current = this.enumerator.Current :> obj
        member this.Current = this.enumerator.Current
        member this.Dispose() = this.enumerator.Dispose()
        member this.MoveNext() = this.enumerator.MoveNext()
        member this.Reset() = this.enumerator.Reset()

[<Struct; NoComparison; NoEquality>]
type StructArrayEnumerator<'T> =
    val private data: 'T[]
    val mutable private index: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (data) = { index = -1; data = data; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index + 1
        uint32 this.index < uint32 this.data.Length

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructResizeArrayEnumerator<'T> =
    val private data: 'T ResizeArray
    val mutable private index: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (data) = { index = -1; data = data; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index + 1
        uint32 this.index < uint32 this.data.Count

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructIListEnumerator<'T, 'TList when 'TList :> IList<'T>> =
    val mutable private data: 'TList
    val mutable private index: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (data) = { index = -1; data = data; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index + 1
        uint32 this.index < uint32 this.data.Count

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructArrayRevEnumerator<'T> =
    val private data: 'T[]
    val mutable private index: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (data) = { data = data; index = data.Length }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index - 1
        uint32 this.index < uint32 this.data.Length

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructResizeArrayRevEnumerator<'T> =
    val private data: 'T ResizeArray
    val mutable private index: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (data) = { data = data; index = data.Count }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index - 1
        uint32 this.index < uint32 this.data.Count

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructIListRevEnumerator<'T, 'TList when 'TList :> IList<'T>> =
    val mutable private data: 'TList
    val mutable private index: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (data) = { data = data; index = data.Count }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index - 1
        uint32 this.index < uint32 this.data.Count

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructImmutableArrayEnumerator<'T> =
    val private data: 'T ImmutableArray
    val mutable private index: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (data) = { index = -1; data = data; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index + 1
        uint32 this.index < uint32 this.data.Length

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructImmutableArrayRevEnumerator<'T> =
    val private data: 'T ImmutableArray
    val mutable private index: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (data) = { index = -1; data = data; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.index <- this.index - 1
        uint32 this.index < uint32 this.data.Length

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.data[this.index]

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructFSharpListEnumerator<'T> =
    val mutable private list: 'T list
    val mutable private prev: 'T list

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
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

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapEnumerator<'T, 'TResult, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val private map: 'T -> 'TResult

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (map, enumerator) = { enumerator = enumerator; map = map; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() = this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current |> this.map

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapVEnumerator<'T, 'TState, 'TResult, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val private state: 'TState
    val private map: OptimizedClosures.FSharpFunc<'TState, 'T, 'TResult>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, map, enumerator) =
        { enumerator = enumerator; map = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(map); state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.state, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapiEnumerator<'T, 'TResult, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private count: int
    val private map: OptimizedClosures.FSharpFunc<int, 'T, 'TResult>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (map, enumerator) =
        { enumerator = enumerator; map = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(map); count = -1 }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapiVEnumerator<'T, 'TState, 'TResult, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private count: int
    val private state: 'TState
    val private map: OptimizedClosures.FSharpFunc<int, 'TState, 'T, 'TResult>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, map, enumerator) =
        { enumerator = enumerator; map = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt(map); count = -1; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.state, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMap2Enumerator<'T, 'T2, 'TResult, 'TEnumerator, 'TEnumerator2
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val private map: OptimizedClosures.FSharpFunc<'T2, 'T, 'TResult>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (map, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; map = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(map); }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() = this.enumerator.MoveNext() && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.enumerator2.Current, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

 [<Struct; NoComparison; NoEquality>]
type StructMap2VEnumerator<'T, 'T2, 'TState, 'TResult, 'TEnumerator, 'TEnumerator2
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val private map: OptimizedClosures.FSharpFunc<'TState, 'T2, 'T, 'TResult>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, map, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; map = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt(map); state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() = this.enumerator.MoveNext() && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.state, this.enumerator2.Current, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapi2Enumerator<'T, 'T2, 'TResult, 'TEnumerator, 'TEnumerator2
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private count: int
    val private map: OptimizedClosures.FSharpFunc<int, 'T2, 'T, 'TResult>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (map, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; map = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt(map); count = -1; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.enumerator2.Current, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapi2VEnumerator<'T, 'T2, 'TState, 'TResult, 'TEnumerator, 'TEnumerator2
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private count: int
    val private map: OptimizedClosures.FSharpFunc<int, 'TState, 'T2, 'T, 'TResult>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, map, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; map = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt(map); count = -1; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.state, this.enumerator2.Current, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMap3Enumerator<'T, 'T2, 'T3, 'TResult, 'TEnumerator, 'TEnumerator2, 'TEnumerator3
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>
    and 'TEnumerator3: struct and 'TEnumerator3 :> IEnumerator<'T3>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private enumerator3: SStructEnumerator<'T3, 'TEnumerator3>
    val private map: OptimizedClosures.FSharpFunc<'T2, 'T3, 'T, 'TResult>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (map, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; map = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt(map); }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()
        && this.enumerator3.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMap3VEnumerator<'T, 'T2, 'T3, 'TState, 'TResult, 'TEnumerator, 'TEnumerator2, 'TEnumerator3
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>
    and 'TEnumerator3: struct and 'TEnumerator3 :> IEnumerator<'T3>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private enumerator3: SStructEnumerator<'T3, 'TEnumerator3>
    val private map: OptimizedClosures.FSharpFunc<'TState, 'T2, 'T3, 'T, 'TResult>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, map, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; map = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt(map); state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()
        && this.enumerator3.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.state, this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapi3Enumerator<'T, 'T2, 'T3, 'TResult, 'TEnumerator, 'TEnumerator2, 'TEnumerator3
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>
    and 'TEnumerator3: struct and 'TEnumerator3 :> IEnumerator<'T3>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private enumerator3: SStructEnumerator<'T3, 'TEnumerator3>
    val mutable private count: int
    val private map: OptimizedClosures.FSharpFunc<int, 'T2, 'T3, 'T, 'TResult>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (map, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; map = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt(map); count = -1 }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()
        && this.enumerator3.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructMapi3VEnumerator<'T, 'T2, 'T3, 'TState, 'TResult, 'TEnumerator, 'TEnumerator2, 'TEnumerator3
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>
    and 'TEnumerator3: struct and 'TEnumerator3 :> IEnumerator<'T3>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private enumerator3: SStructEnumerator<'T3, 'TEnumerator3>
    val mutable private count: int
    val private map: OptimizedClosures.FSharpFunc<int, 'TState, 'T2, 'T3, 'T, 'TResult>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, map, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; map = OptimizedClosures.FSharpFunc<_,_,_,_,_,_>.Adapt(map); count = -1; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.count <- this.count + 1
        this.enumerator.MoveNext()
        && this.enumerator2.MoveNext()
        && this.enumerator3.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.map.Invoke(this.count, this.state, this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructFilterEnumerator<'T, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val private filter: 'T -> bool

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (filter, enumerator) = { enumerator = enumerator; filter = filter; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        let mutable found = false
        while not found && this.enumerator.MoveNext() do
            found <- this.enumerator.Current |> this.filter
        found
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructFilterVEnumerator<'T, 'TState, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val private filter: OptimizedClosures.FSharpFunc<'TState, 'T, bool>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, filter, enumerator) = { enumerator = enumerator; filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(filter); state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        let mutable found = false
        while not found && this.enumerator.MoveNext() do
            found <- this.filter.Invoke(this.state, this.enumerator.Current)
        found
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructTakeEnumerator<'T, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private count: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (count, enumerator) = { enumerator = enumerator; count = count; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        let oldCount = this.count
        this.count <- this.count + 1
        oldCount > 0 && this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructTakeWhileEnumerator<'T, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val private filter: 'T -> bool

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (filter, enumerator) = { enumerator = enumerator; filter = filter }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator.MoveNext() && this.filter this.enumerator.Current

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructTakeWhileVEnumerator<'T, 'TState, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val private filter: OptimizedClosures.FSharpFunc<'TState, 'T, bool>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, filter, enumerator) = { enumerator = enumerator; filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(filter); state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator.MoveNext() && this.filter.Invoke(this.state, this.enumerator.Current)

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructSkipEnumerator<'T, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private count: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (count, enumerator) = { enumerator = enumerator; count = count; }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        while this.count > 0 && this.enumerator.MoveNext() do
            this.count <- this.count - 1
        this.enumerator.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator.Current

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructSkipWhileEnumerator<'T, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val private filter: 'T -> bool
    val mutable private flag: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
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

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructSkipWhileVEnumerator<'T, 'TState, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val private filter: OptimizedClosures.FSharpFunc<'TState, 'T, bool>
    val mutable private flag: int
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, filter, enumerator) = { enumerator = enumerator; filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(filter); flag = 0; state = state }

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

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChooseOptionEnumerator<'T, 'TResult, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private current: 'TResult option
    val private chooser: 'T -> 'TResult option

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (chooser, enumerator) = { enumerator = enumerator; chooser = chooser; current = None }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() do
            this.current <- this.chooser this.enumerator.Current
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChooseOptionVEnumerator<'T, 'TState, 'TResult, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private current: 'TResult option
    val private chooser: OptimizedClosures.FSharpFunc<'TState, 'T, 'TResult option>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, chooser, enumerator) = { enumerator = enumerator; chooser = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(chooser); current = None; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChoose2OptionEnumerator<'T, 'T2, 'TResult, 'TEnumerator, 'TEnumerator2
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private current: 'TResult option
    val private chooser: OptimizedClosures.FSharpFunc<'T2, 'T, 'TResult option>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (chooser, enumerator, enumerator2) = { enumerator = enumerator; enumerator2 = enumerator2; chooser = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(chooser); current = None }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() do
            this.current <- this.chooser.Invoke(this.enumerator2.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChooseOption2VEnumerator<'T, 'T2, 'TState, 'TResult, 'TEnumerator, 'TEnumerator2
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private current: 'TResult option
    val private chooser: OptimizedClosures.FSharpFunc<'TState, 'T2, 'T, 'TResult option>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, chooser, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; chooser = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt(chooser); current = None; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator2.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChoose3OptionEnumerator<'T, 'T2, 'T3, 'TResult, 'TEnumerator, 'TEnumerator2, 'TEnumerator3
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>
    and 'TEnumerator3: struct and 'TEnumerator3 :> IEnumerator<'T3>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private enumerator3: SStructEnumerator<'T3, 'TEnumerator3>
    val mutable private current: 'TResult option
    val private chooser: OptimizedClosures.FSharpFunc<'T2, 'T3, 'T, 'TResult option>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (chooser, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; chooser = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt(chooser); current = None }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() && this.enumerator3.MoveNext() do
            this.current <- this.chooser.Invoke(this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChooseOption3VEnumerator<'T, 'T2, 'T3, 'TState, 'TResult, 'TEnumerator, 'TEnumerator2, 'TEnumerator3
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>
    and 'TEnumerator3: struct and 'TEnumerator3 :> IEnumerator<'T3>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private enumerator3: SStructEnumerator<'T3, 'TEnumerator3>
    val mutable private current: 'TResult option
    val private chooser: OptimizedClosures.FSharpFunc<'TState, 'T2, 'T3, 'T, 'TResult option>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, chooser, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; chooser = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt(chooser); current = None; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- None
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() && this.enumerator3.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> Option.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChooseEnumerator<'T, 'TResult, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private current: 'TResult voption
    val private chooser: 'T -> 'TResult voption

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (chooser, enumerator) = { enumerator = enumerator; chooser = chooser; current = ValueNone }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() do
            this.current <- this.chooser this.enumerator.Current
        this.current.IsSome

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructValueChooseVEnumerator<'T, 'TState, 'TResult, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private current: 'TResult voption
    val private chooser: OptimizedClosures.FSharpFunc<'TState, 'T, 'TResult voption>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, chooser, enumerator) = { enumerator = enumerator; chooser = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(chooser); current = ValueNone; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator.Current)
        this.current.IsSome

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChoose2Enumerator<'T, 'T2, 'TResult, 'TEnumerator, 'TEnumerator2
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private current: 'TResult voption
    val private chooser: OptimizedClosures.FSharpFunc<'T2, 'T, 'TResult voption>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (chooser, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; chooser = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(chooser); current = ValueNone }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() do
            this.current <- this.chooser.Invoke(this.enumerator2.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructValueChoose2VEnumerator<'T, 'T2, 'TState, 'TResult, 'TEnumerator, 'TEnumerator2
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private current: 'TResult voption
    val private chooser: OptimizedClosures.FSharpFunc<'TState, 'T2, 'T, 'TResult voption>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, chooser, enumerator, enumerator2) =
        { enumerator = enumerator; enumerator2 = enumerator2; chooser = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt(chooser); current = ValueNone; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator2.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructChoose3Enumerator<'T, 'T2, 'T3, 'TResult, 'TEnumerator, 'TEnumerator2, 'TEnumerator3
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>
    and 'TEnumerator3: struct and 'TEnumerator3 :> IEnumerator<'T3>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private enumerator3: SStructEnumerator<'T3, 'TEnumerator3>
    val mutable private current: 'TResult voption
    val private chooser: OptimizedClosures.FSharpFunc<'T2, 'T3, 'T, 'TResult voption>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (chooser, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; chooser = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt(chooser); current = ValueNone }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() && this.enumerator3.MoveNext() do
            this.current <- this.chooser.Invoke(this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructValueChoose3VEnumerator<'T, 'T2, 'T3, 'TState, 'TResult, 'TEnumerator, 'TEnumerator2, 'TEnumerator3
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>
    and 'TEnumerator3: struct and 'TEnumerator3 :> IEnumerator<'T3>> =

    val mutable private enumerator: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>
    val mutable private enumerator3: SStructEnumerator<'T3, 'TEnumerator3>
    val mutable private current: 'TResult voption
    val private chooser: OptimizedClosures.FSharpFunc<'TState, 'T2, 'T3, 'T, 'TResult voption>
    val private state: 'TState

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (state, chooser, enumerator, enumerator2, enumerator3) =
        { enumerator = enumerator; enumerator2 = enumerator2; enumerator3 = enumerator3; chooser = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt(chooser); current = ValueNone; state = state }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- ValueNone
        while this.current.IsNone && this.enumerator.MoveNext() && this.enumerator2.MoveNext() && this.enumerator3.MoveNext() do
            this.current <- this.chooser.Invoke(this.state, this.enumerator2.Current, this.enumerator3.Current, this.enumerator.Current)
        this.current.IsSome
    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.current |> ValueOption.get

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'TResult> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructAppendEnumerator<'T, 'TEnumerator when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>> =

    val mutable private enumerator1: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T, 'TEnumerator>
    val mutable private index: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
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

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructZipTupleEnumerator<'T, 'T2, 'TEnumerator, 'TEnumerator2
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>> =

    val mutable private enumerator1: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (enumerator1, enumerator2) = { enumerator1 = enumerator1; enumerator2 = enumerator2 }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator1.MoveNext()
        && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        this.enumerator2.Current, this.enumerator1.Current

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T2 * 'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructZipEnumerator<'T, 'T2, 'TEnumerator, 'TEnumerator2
    when 'TEnumerator: struct and 'TEnumerator :> IEnumerator<'T>
    and 'TEnumerator2: struct and 'TEnumerator2 :> IEnumerator<'T2>> =

    val mutable private enumerator1: SStructEnumerator<'T, 'TEnumerator>
    val mutable private enumerator2: SStructEnumerator<'T2, 'TEnumerator2>

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (enumerator1, enumerator2) = { enumerator1 = enumerator1; enumerator2 = enumerator2 }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.enumerator1.MoveNext()
        && this.enumerator2.MoveNext()

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() =
        struct (this.enumerator2.Current, this.enumerator1.Current)

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<struct('T2 * 'T)> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructInitEnumerator<'T> =
    val mutable private length: int
    val mutable private count: int
    val private initializer: int -> 'T

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (count, initializer) = { length = -1; count = count; initializer = initializer }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.length <- this.length + 1
        this.length < this.count

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = this.initializer this.length

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructInitInfiniteEnumerator<'T> =
    val mutable private length: int
    val private initializer: int -> 'T
    
    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (initializer) = { length = -1; initializer = initializer }
    
    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.length <- this.length + 1
        true

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = this.initializer this.length

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

type IRangeEnumeratorOperators<'T when 'T :> INumber<'T>>  =
    static abstract Initialize: left: 'T * right: 'T -> 'T
    static abstract MoveNext: left: 'T * right: 'T -> 'T
    static abstract Compare: left: 'T * right: 'T -> bool


[<Struct>]
type RangeUpToOperators<'T when 'T :> INumber<'T>> =
    interface IRangeEnumeratorOperators<'T> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member Initialize(left, right) = left - right
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member MoveNext(left, right) = left + right
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member Compare(left, right) = left.CompareTo(right) < 0

[<Struct>]
type RangeUpToInclusiveOperators<'T when 'T :> INumber<'T>> =
    interface IRangeEnumeratorOperators<'T> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member Initialize(left, right) = left - right
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member MoveNext(left, right) = left + right
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member Compare(left, right) = left.CompareTo(right) <= 0

[<Struct>]
type RangeDownToOperators<'T when 'T :> INumber<'T>> =
    interface IRangeEnumeratorOperators<'T> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member Initialize(left, right) = left + right
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member MoveNext(left, right) = left - right
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member Compare(left, right) = left.CompareTo(right) > 0

[<Struct>]
type RangeDownToInclusiveOperators<'T when 'T :> INumber<'T>> =
    interface IRangeEnumeratorOperators<'T> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member Initialize(left, right) = left + right
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member MoveNext(left, right) = left - right
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member Compare(left, right) = left.CompareTo(right) >= 0

[<Struct; NoComparison; NoEquality>]
type StructRangeEnumerator<'T, 'TOperators when 'T :> INumber<'T> and 'TOperators :> IRangeEnumeratorOperators<'T>> =
    val mutable private current: 'T
    val private finish: 'T

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (startValue: 'T, endValue: 'T) = { current =  'TOperators.Initialize(startValue, 'T.One); finish = endValue }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- 'TOperators.MoveNext(this.current, 'T.One)
        'TOperators.Compare(this.current, this.finish)

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = this.current

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()

[<Struct; NoComparison; NoEquality>]
type StructRangeStepEnumerator<'T, 'TOperators when 'T :> INumber<'T> and 'TOperators :> IRangeEnumeratorOperators<'T>> =
    val mutable private current: 'T
    val private finish: 'T
    val private step: 'T

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (startValue: 'T, endValue: 'T, step: 'T) = { current =  'TOperators.Initialize(startValue, step); finish = endValue; step = step }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.MoveNext() =
        this.current <- 'TOperators.MoveNext(this.current, this.step)
        'TOperators.Compare(this.current, this.finish)

    member this.Current with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = this.current

    member inline this.Dispose() = ()
    member inline this.GetEnumerator() = this

    interface IEnumerator<'T> with
        member this.Current = this.Current :> obj
        member this.Current = this.Current
        member this.Dispose() = ()
        member this.MoveNext() = this.MoveNext()
        member this.Reset() = ()