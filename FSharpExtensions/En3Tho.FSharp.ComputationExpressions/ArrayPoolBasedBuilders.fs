module En3Tho.FSharp.ComputationExpressions.ArrayPoolBasedBuilders

open System
open System.Buffers
open System.Collections
open System.Collections.Generic
open System.Collections.Immutable
open System.Linq
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
            array.[count] <- value
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
    interface ICollection<'a> with
        member this.Add(item) = ()
        member this.Clear() = ()
        member this.Contains(item) = false
        member this.CopyTo(array, arrayIndex) = ()
        member this.Count = count
        member this.GetEnumerator(): IEnumerator<'a> = Enumerable.Empty<'a>().GetEnumerator()
        member this.GetEnumerator(): IEnumerator = Enumerable.Empty<'a>().GetEnumerator() :> IEnumerator
        member this.IsReadOnly = true
        member this.Remove(item) = false

type ArrayPoolList<'a> with
    member this.ToImmutableArray() =
        ImmutableArray.Create(this.GetArray(), 0, this.Count)

    member this.ToResizeArray() =
        let count = this.Count
        if count = 0 then ResizeArray()
        else
        let array = this.GetArray()
        let fake = FakeCollection(count)
        let lst = ResizeArray(fake)
        array.AsSpan(0, count).CopyTo(CollectionsMarshal.AsSpan(lst))
        lst

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
    member inline this.Yield (value: 'a) =
        this.Builder.Add value
    member inline this.YieldFrom (values: 'a seq) =
        for value in values do this.Builder.Add value

    member inline this.Run runExpr =
        try
            runExpr()
            this.Builder.ToImmutableArray()
        finally
            this.Builder.Dispose()

type [<NoEquality; NoComparison; Struct>] ResizeArrayBuilder<'a>(builder: ArrayPoolList<'a>) =
    member this.Builder = builder
    member inline this.Yield (value: 'a) =
        this.Builder.Add value

    member inline this.YieldFrom (values: 'a seq) =
        for value in values do this.Builder.Add value

    member inline this.Run runExpr =
        try
            runExpr()
            this.Builder.ToResizeArray()
        finally
            this.Builder.Dispose()

type [<NoEquality; NoComparison; Struct>] ArrayBuilder<'a>(builder: ArrayPoolList<'a>) =
    member this.Builder = builder
    member inline this.Yield (value: 'a) =
        this.Builder.Add value
    member inline this.YieldFrom (values: 'a seq) =
        for value in values do this.Builder.Add value

    member inline this.Run runExpr =
        try
            runExpr()
            this.Builder.ToArray()
        finally
            this.Builder.Dispose()
           
let rsz<'a> = ResizeArrayBuilder<'a>(new ArrayPoolList<'a>())

let block<'a> = BlockBuilder<'a>(new ArrayPoolList<'a>())

let arr<'a> = ArrayBuilder<'a>(new ArrayPoolList<'a>())