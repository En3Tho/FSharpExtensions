module En3Tho.FSharp.ComputationExpressions.ArrayPoolBasedBuilders

open System
open System.Buffers
open System.Collections
open System.Collections.Generic
open System.Collections.Immutable
open System.Linq
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open En3Tho.FSharp.Extensions

module ArrayPoolList =
    type ArrayPool<'a> with
        member this.ReRent (array: 'a[] byref, newLength) =
            let newArray = this.Rent newLength
            Array.Copy(array, newArray, array.Length)
            this.Return array
            array <- newArray
    let [<Literal>] private InitialSize = 256
    let [<Literal>] private GrowMultiplier = 2

    type [<NoComparison;NoEquality>] ArrayPoolList<'a>(initialSize) =
        let mutable array = ArrayPool.Shared.Rent initialSize
        let mutable count = 0

        new () = new ArrayPoolList<'a>(InitialSize)

        member inline private this.UnsafeAdd value =
            let ref = &Unsafe.Add(&MemoryMarshal.GetArrayDataReference(array), count)
            ref <- value
            count <- count + 1
        
        member private this.EnsureArray() =
            if count = array.Length then
                ArrayPool.Shared.ReRent(&array, array.Length * 2)
        
        member this.Count = count
        
        member this.Add value =
            this.EnsureArray()
            this.UnsafeAdd value
        
        member this.Dispose() =
            if not (Object.ReferenceEquals(array, null)) then
                ArrayPool.Shared.Return array
        
        member this.CopyTo(memory: 'a Span) =
            Memory(array, 0, count).Span.CopyTo(memory)
        
        member this.GetArray() = if Object.ReferenceEquals(array, null) then [||] else array
        
        interface IDisposable with
            member this.Dispose() = this.Dispose()
    
open ArrayPoolList

// This is needed to set ResizeArray to initial count and get span from it
type private FakeCollection<'a>(count) =

    member val Count = count with get, set

    [<DefaultValue; ThreadStatic>]
    static val mutable private threadStaticInstance: FakeCollection<'a>

    static member GetInstance(count) =
        if Object.ReferenceEquals(FakeCollection<'a>.threadStaticInstance, null) then
            FakeCollection<'a>.threadStaticInstance <- FakeCollection(0)
        FakeCollection<'a>.threadStaticInstance.Count <- count
        FakeCollection<'a>.threadStaticInstance

    interface ICollection<'a> with
        member this.Add(item) = ()
        member this.Clear() = ()
        member this.Contains(item) = false
        member this.CopyTo(array, arrayIndex) = ()
        member this.Count = this.Count
        member this.GetEnumerator(): IEnumerator<'a> = Enumerable.Empty<'a>().GetEnumerator()
        member this.GetEnumerator(): IEnumerator = Enumerable.Empty<'a>().GetEnumerator() :> IEnumerator
        member this.IsReadOnly = true
        member this.Remove(item) = false

type ArrayPoolList<'a> with
    member this.ToImmutableArray() =
        ImmutableArray.Create(this.GetArray(), 0, this.Count)

    member this.ToResizeArray() =
        let count = this.Count
        if count = 0 then
            ResizeArray()
        else
            let array = this.GetArray()
            let fake = FakeCollection.GetInstance(count)
            let result = ResizeArray(fake)
            array.AsSpan(0, count).CopyTo(CollectionsMarshal.AsSpan(result))
            result

    member this.ToArray() =
        match this.GetArray() with
        | [||] as result -> result
        | source ->
            let count = this.Count
            let result = Array.zeroCreate count
            Array.Copy(source, result, count)
            result

type [<NoEquality; NoComparison; Struct>] BlockBuilder<'a>(builder: ArrayPoolList<'a>) =
    member this.Builder = builder
    member inline this.Yield (value: 'a) : CollectionCode =
        let this = this
        fun() -> this.Builder.Add value
    member inline this.YieldFrom (values: 'a seq) : CollectionCode =
        let this = this
        fun() -> for value in values do this.Builder.Add value

    member inline this.Zero() : CollectionCode = fun() -> ()
    member inline this.Run ([<InlineIfLambda>] runExpr) =
        try
            runExpr()
            this.Builder.ToImmutableArray()
        finally
            this.Builder.Dispose()

type [<NoEquality; NoComparison; Struct>] ResizeArrayBuilder<'a>(builder: ArrayPoolList<'a>) =
    member this.Builder = builder
    member inline this.Yield (value: 'a) : CollectionCode =
        let this = this
        fun() -> this.Builder.Add value
    member inline this.YieldFrom (values: 'a seq) : CollectionCode =
        let this = this
        fun() -> for value in values do this.Builder.Add value

    member inline this.Zero() : CollectionCode = fun() -> ()
    member inline this.Run ([<InlineIfLambda>] runExpr) =
        try
            runExpr()
            this.Builder.ToResizeArray()
        finally
            this.Builder.Dispose()

type [<NoEquality; NoComparison; Struct>] ArrayBuilder<'a>(builder: ArrayPoolList<'a>) =
    member this.Builder = builder
    member inline this.Yield (value: 'a) : CollectionCode =
        let this = this
        fun() -> this.Builder.Add value
    member inline this.YieldFrom (values: 'a seq) : CollectionCode =
        let this = this
        fun() -> for value in values do this.Builder.Add value

    member inline this.Zero() : CollectionCode = fun() -> ()
    member inline this.Run ([<InlineIfLambda>] runExpr) =
        try
            runExpr()
            this.Builder.ToArray()
        finally
            this.Builder.Dispose()

type [<NoEquality; NoComparison; Struct>] UnsafeBlockBuilder<'a>(builder: ArrayPoolList<'a>) =
    member this.Builder = builder
    member inline this.Yield (value: 'a) : CollectionCode =
        let this = this
        fun() -> this.Builder.Add value
    member inline this.YieldFrom (values: 'a seq) : CollectionCode =
        let this = this
        fun() -> for value in values do this.Builder.Add value

    member inline this.Zero() : CollectionCode = fun() -> ()
    member inline this.Run ([<InlineIfLambda>] runExpr) =
        runExpr()
        let result = this.Builder.ToImmutableArray()
        this.Builder.Dispose()
        result

type [<NoEquality; NoComparison; Struct>] UnsafeResizeArrayBuilder<'a>(builder: ArrayPoolList<'a>) =
    member this.Builder = builder
    member inline this.Yield (value: 'a) : CollectionCode =
        let this = this
        fun() -> this.Builder.Add value
    member inline this.YieldFrom (values: 'a seq) : CollectionCode =
        let this = this
        fun() -> for value in values do this.Builder.Add value

    member inline this.Zero() : CollectionCode = fun() -> ()
    member inline this.Run ([<InlineIfLambda>] runExpr) =
        runExpr()
        let result = this.Builder.ToResizeArray()
        this.Builder.Dispose()
        result

type [<NoEquality; NoComparison; Struct>] UnsafeArrayBuilder<'a>(builder: ArrayPoolList<'a>) =
    member this.Builder = builder
    member inline this.Yield (value: 'a) : CollectionCode =
        let this = this
        fun() -> this.Builder.Add value
    member inline this.YieldFrom (values: 'a seq) : CollectionCode =
        let this = this
        fun() -> for value in values do this.Builder.Add value

    member inline this.Zero() : CollectionCode = fun() -> ()
    member inline this.Run ([<InlineIfLambda>] runExpr) =
        runExpr()
        let result = this.Builder.ToArray()
        this.Builder.Dispose()
        result

let rsz<'a> = ResizeArrayBuilder<'a>(new ArrayPoolList<'a>())

let block<'a> = BlockBuilder<'a>(new ArrayPoolList<'a>())

let arr<'a> = ArrayBuilder<'a>(new ArrayPoolList<'a>())

let ursz<'a> = UnsafeResizeArrayBuilder<'a>(new ArrayPoolList<'a>())

let ublock<'a> = UnsafeBlockBuilder<'a>(new ArrayPoolList<'a>())

let uarr<'a> = UnsafeArrayBuilder<'a>(new ArrayPoolList<'a>())

let rszOf<'a> initialLength = ResizeArrayBuilder<'a>(new ArrayPoolList<'a>(initialLength))

let blockOf<'a> initialLength = BlockBuilder<'a>(new ArrayPoolList<'a>(initialLength))

let arrOf<'a> initialLength = ArrayBuilder<'a>(new ArrayPoolList<'a>(initialLength))

let urszOf<'a> initialLength = UnsafeResizeArrayBuilder<'a>(new ArrayPoolList<'a>(initialLength))

let ublockOf<'a> initialLength = UnsafeBlockBuilder<'a>(new ArrayPoolList<'a>(initialLength))

let uarrOf<'a> initialLength = UnsafeArrayBuilder<'a>(new ArrayPoolList<'a>(initialLength))