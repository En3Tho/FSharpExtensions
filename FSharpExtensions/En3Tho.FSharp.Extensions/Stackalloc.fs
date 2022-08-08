namespace En3Tho.FSharp.Extensions.Unsafe

open System
open System.Buffers
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open En3Tho.FSharp.Extensions

#nowarn "9"

module Stackalloc =

    type IValueBag<'a> =
        abstract member FirstElementRef: 'a byref
        abstract member Length: int


    [<Struct>]
    type ValueBag4<'a> =
        [<DefaultValue(false)>] val mutable value1: 'a
        [<DefaultValue(false)>] val mutable private value2: 'a
        [<DefaultValue(false)>] val mutable private value3: 'a
        [<DefaultValue(false)>] val mutable private value4: 'a

        interface IValueBag<'a> with
            member this.FirstElementRef =
                let ptr = Unsafe.AsPointer(&this.value1)
                &Unsafe.AsRef(ptr)
            member this.Length = 4

    [<Struct>]
    type ValueBag8<'a> =
        [<DefaultValue(false)>] val mutable value1: ValueBag4<'a>
        [<DefaultValue(false)>] val mutable private value2: ValueBag4<'a>

        interface IValueBag<'a> with
            member this.FirstElementRef =
                let ptr = Unsafe.AsPointer(&this.value1.value1)
                &Unsafe.AsRef(ptr)
            member this.Length = 8

    [<Struct>]
    type ValueBag16<'a> =
        [<DefaultValue(false)>] val mutable value1: ValueBag8<'a>
        [<DefaultValue(false)>] val mutable private value2: ValueBag8<'a>

        interface IValueBag<'a> with
            member this.FirstElementRef =
                let ptr = Unsafe.AsPointer(&this.value1.value1.value1)
                &Unsafe.AsRef(ptr)
            member this.Length = 16

    [<Struct>]
    type ValueBag32<'a> =
        [<DefaultValue(false)>] val mutable value1: ValueBag16<'a>
        [<DefaultValue(false)>] val mutable private value2: ValueBag16<'a>

        interface IValueBag<'a> with
            member this.FirstElementRef =
                let ptr = Unsafe.AsPointer(&this.value1.value1.value1.value1)
                &Unsafe.AsRef(ptr)
            member this.Length = 32

    [<Struct>]
    type ValueBag64<'a> =
        [<DefaultValue(false)>] val mutable value1: ValueBag32<'a>
        [<DefaultValue(false)>] val mutable private value2: ValueBag32<'a>

        interface IValueBag<'a> with
            member this.FirstElementRef =
                let ptr = Unsafe.AsPointer(&this.value1.value1.value1.value1.value1)
                &Unsafe.AsRef(ptr)
            member this.Length = 64

    [<Struct>]
    type ValueBag128<'a> =
        [<DefaultValue(false)>] val mutable value1: ValueBag64<'a>
        [<DefaultValue(false)>] val mutable private value2: ValueBag64<'a>

        interface IValueBag<'a> with
            member this.FirstElementRef =
                let ptr = Unsafe.AsPointer(&this.value1.value1.value1.value1.value1.value1)
                &Unsafe.AsRef(ptr)
            member this.Length = 128

    [<Struct>]
    type ValueBag256<'a> =
        [<DefaultValue(false)>] val mutable value1: ValueBag128<'a>
        [<DefaultValue(false)>] val mutable private value2: ValueBag128<'a>

        interface IValueBag<'a> with
            member this.FirstElementRef =
                let ptr = Unsafe.AsPointer(&this.value1.value1.value1.value1.value1.value1.value1)
                &Unsafe.AsRef(ptr)
            member this.Length = 256

    [<Struct>]
    type ValueBag512<'a> =
        [<DefaultValue(false)>] val mutable value1: ValueBag256<'a>
        [<DefaultValue(false)>] val mutable private value2: ValueBag256<'a>

        interface IValueBag<'a> with
            member this.FirstElementRef =
                let ptr = Unsafe.AsPointer(&this.value1.value1.value1.value1.value1.value1.value1.value1)
                &Unsafe.AsRef(ptr)
            member this.Length = 512

    module ValueBag =
        let inline getSpan<'a, 'b when 'a: struct and 'a :> IValueBag<'b>> (bag: 'a byref) =
            MemoryMarshal.CreateSpan(&bag.FirstElementRef, bag.Length)

    [<Struct; IsByRefLike; IsReadOnly>]
    type StackOrPooled<'a> =
        val private array: 'a array
        val private span: 'a Span

        new(arr) = { array = arr; span = arr.AsSpan() }
        new(span) = { array = null; span = span }

        member this.Span = this.span

        member this.Dispose() =
            if not (Object.ReferenceEquals(this.array, null)) then
                ArrayPool.Shared.Return this.array

        interface IDisposable with
            member this.Dispose() = this.Dispose()

    [<Struct; IsByRefLike; IsReadOnly>]
    type StackOrCustomPooled<'a> =
        val private array: 'a array
        val private pool: 'a ArrayPool
        val private span: 'a Span

        new(arr, pool) = { array = arr; pool = pool; span = arr.AsSpan() }
        new(span) = { array = null; pool = null; span = span }
        member this.Span = this.span

        member this.Dispose() =
            if not (Object.ReferenceEquals(this.array, null)) then
                this.pool.Return this.array

        interface IDisposable with
            member this.Dispose() = this.Dispose()

    let inline alloc<'a when 'a: unmanaged> len =
        let ptr = NativeInterop.NativePtr.stackalloc<'a> len
        Span<'a>(NativeInterop.NativePtr.toVoidPtr ptr, len)

    let inline allocUsingValueBag<'a, 'b when 'a: struct and 'a: (new: unit -> 'a) and 'a :> IValueBag<'b>>() =
        let mutable bag = new 'a()
        ValueBag.getSpan &bag

    let inline allocOrPool<'a when 'a: unmanaged> len maxLen =
        if len > maxLen then
            new StackOrPooled<'a>(ArrayPool<'a>.Shared.Rent len)
        else
            new StackOrPooled<'a>(alloc<'a> len)

    let inline allocOrNew<'a when 'a: unmanaged> len maxLen =
        if len > maxLen then
            Span<'a>(Array.zeroCreate<'a> len)
        else
            alloc<'a> len

    let inline allocAny4<'a>() =
        allocUsingValueBag<ValueBag4<'a>,_>()

    let inline allocAny8<'a>() =
        allocUsingValueBag<ValueBag8<'a>,_>()

    let inline allocAny16<'a>() =
        allocUsingValueBag<ValueBag16<'a>,_>()

    let inline allocAny32<'a>() =
        allocUsingValueBag<ValueBag32<'a>,_>()

    let inline allocAny64<'a>() =
        allocUsingValueBag<ValueBag64<'a>,_>()

    let inline allocAny128<'a>() =
        allocUsingValueBag<ValueBag128<'a>,_>()

    let inline allocAny256<'a>() =
        allocUsingValueBag<ValueBag256<'a>,_>()

    let inline allocAny512<'a>() =
        allocUsingValueBag<ValueBag512<'a>,_>()

    let inline allocAny16OrNew<'a> len =
        if len <= 16 then
            allocAny16<'a>()
        else
            Span(Array.zeroCreate len)

    let inline allocAny32OrNew<'a> len =
        if len <= 32 then
            allocAny32<'a>()
        else
            Span(Array.zeroCreate len)

    let inline allocAny64OrNew<'a> len =
        if len <= 64 then
            allocAny64<'a>()
        else
            Span(Array.zeroCreate len)

    let inline allocAny128OrNew<'a> len =
        if len <= 128 then
            allocAny128<'a>()
        else
            Span(Array.zeroCreate len)

    let inline allocAny256OrNew<'a> len =
        if len <= 256 then
            allocAny256<'a>()
        else
            Span(Array.zeroCreate len)

    let inline allocAny512OrNew<'a> len =
        if len <= 512 then
            allocAny512<'a>()
        else
            Span(Array.zeroCreate len)

    let inline allocAnyOrNew<'a> len = // TODO: check if those are optimized
        match len with
        | LtEq 16 -> allocAny16OrNew len
        | LtEq 32 -> allocAny32OrNew len
        | LtEq 64 -> allocAny64OrNew len
        | LtEq 128 -> allocAny128OrNew len
        | LtEq 256 -> allocAny256OrNew len
        | _ -> allocAny512OrNew len

    let inline alloc16OrPool<'a> len =
        if len <= 32 then
            new StackOrPooled<_>(allocAny32<'a>())
        else
            new StackOrPooled<'a>(ArrayPool<'a>.Shared.Rent len)

    let inline allocAnyOrPool32<'a> len =
        if len <= 32 then
            new StackOrPooled<_>(allocAny32<'a>())
        else
            new StackOrPooled<'a>(ArrayPool<'a>.Shared.Rent len)

    let inline allocAnyOrPool64<'a> len =
        if len <= 64 then
            new StackOrPooled<_>(allocAny64<'a>())
        else
            new StackOrPooled<'a>(ArrayPool<'a>.Shared.Rent len)

    let inline allocAnyOrPool128<'a> len =
        if len <= 128 then
            new StackOrPooled<_>(allocUsingValueBag<ValueBag128<'a>,_>())
        else
            new StackOrPooled<'a>(ArrayPool<'a>.Shared.Rent len)

    let inline allocAny256OrPool<'a> len =
        if len <= 256 then
            new StackOrPooled<_>(allocUsingValueBag<ValueBag256<'a>,_>())
        else
            new StackOrPooled<'a>(ArrayPool<'a>.Shared.Rent len)

    let inline allocAny512OrPool<'a> len =
        if len <= 512 then
            new StackOrPooled<_>(allocUsingValueBag<ValueBag512<'a>,_>())
        else
            new StackOrPooled<'a>(ArrayPool<'a>.Shared.Rent len)

    let inline allocAnyOrPool<'a> len = // TODO: check if those are optimized
        match len with
        | LtEq 16 -> alloc16OrPool<'a> len
        | LtEq 32 -> allocAnyOrPool32<'a> len
        | LtEq 64 -> allocAnyOrPool64<'a> len
        | LtEq 128 -> allocAnyOrPool128<'a> len
        | LtEq 256 -> allocAny256OrPool<'a> len
        | _ -> allocAny512OrPool<'a> len

type [<Struct; IsByRefLike>] StackHeapList<'a>(span: 'a Span) =
    [<DefaultValue(false)>] val mutable private count: int
    [<DefaultValue(false)>] val mutable private list: ResizeArray<'a>

    member this.StackSpan = span.Slice(0, Math.Max(this.count, span.Length))
    member this.HeapSpan = CollectionsMarshal.AsSpan(this.list)
    member this.Count = this.count
    
    member private this.AddToList value =
        if this.list = null then
            this.list <- ResizeArray(span.Length)
        this.list.Add value

    member this.Add value =
        if uint this.count < uint span.Length then
            this.StackSpan.[this.count] <- value
        else
            this.AddToList value
        this.count <- this.count + 1

type [<Struct; IsByRefLike>] StackList<'a>(span: 'a Span) =
    [<DefaultValue(false)>] val mutable private count: int

    member this.Span = span.Slice(0, Math.Max(this.count, span.Length))
    member this.Count = this.count

    member this.Add value =
        if uint this.count < uint span.Length then
            this.Span.[this.count] <- value
        else
            invalidOp "Items count is exceeded"
        this.count <- this.count + 1

module StackHeapList =
    open Stackalloc

    let inline make<'bag, 'a when 'bag: struct and 'bag: (new: unit -> 'bag) and 'bag :> IValueBag<'a>>() =
        let span = allocUsingValueBag<'bag, 'a>()
        StackHeapList<'a>(span)

    let inline of8<'a>() = make<ValueBag8<'a>,_>()
    let inline of16<'a>() = make<ValueBag16<'a>,_>()
    let inline of32<'a>() = make<ValueBag32<'a>,_>()
    let inline of64<'a>() = make<ValueBag64<'a>,_>()
    let inline of128<'a>() = make<ValueBag128<'a>,_>()
    let inline of256<'a>() = make<ValueBag256<'a>,_>()
    let inline of512<'a>() = make<ValueBag512<'a>,_>()

module StackList =
    open Stackalloc

    let inline make<'bag, 'a when 'bag: struct and 'bag: (new: unit -> 'bag) and 'bag :> IValueBag<'a>>() =
        let span = allocUsingValueBag<'bag, 'a>()
        StackList<'a>(span)

    let inline of8<'a>() = make<ValueBag8<'a>,_>()
    let inline of16<'a>() = make<ValueBag16<'a>,_>()
    let inline of32<'a>() = make<ValueBag32<'a>,_>()
    let inline of64<'a>() = make<ValueBag64<'a>,_>()
    let inline of128<'a>() = make<ValueBag128<'a>,_>()
    let inline of256<'a>() = make<ValueBag256<'a>,_>()
    let inline of512<'a>() = make<ValueBag512<'a>,_>()