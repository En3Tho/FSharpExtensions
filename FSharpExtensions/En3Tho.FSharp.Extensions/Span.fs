namespace En3Tho.FSharp.Extensions

open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop

// TODO: add functions from String module
// TODO: active patterns for Span and ReadOnlySpan

module Span =
    let inline isEmpty (span: Span<_>) = span.Length = 0
    let inline isNotEmpty (span: Span<_>) = span.Length <> 0
    let inline slice start count (span: Span<_>) = span.Slice(start, count)
    let inline sliceFrom start (span: Span<_>) = span.Slice(start)
    let inline sliceTo index (span: Span<_>) = span.Slice(0, index)
    let inline fromPtr count (ptr: nativeptr<'a>) = Span<'a>(ptr |> NativePtr.toVoidPtr, count)
    let inline fromVoidPtr<'a when 'a: unmanaged> count ptr = Span<'a>(ptr, count)
    let inline fromArray (array: 'a[]): Span<_> = Span.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): Span<_> = Span.op_Implicit array
    let inline fromMemory (memory: Memory<_>) = memory.Span

    let inline (|Empty|_|) (str: Span<'a>) =
        str.IsEmpty

    let inline (|NotEmpty|_|) (str: Span<'a>) =
        str.IsEmpty |> not

    let inline stackalloc<'a when 'a: unmanaged> len =
        let ptr = NativePtr.stackalloc<'a> len
        Span<'a>(NativePtr.toVoidPtr ptr, len)

    let inline alloc<'a> len =
        Span<'a>(Array.zeroCreate<'a> len)

    let inline malloc<'a when 'a: unmanaged> len =
        let ptr = NativeMemory.Alloc(ecast<_, unativeint> (Unsafe.SizeOf<'a>()) * ecast<_, unativeint> len)
        Span<'a>(ptr, len)

    let inline mfree<'a when 'a: unmanaged> (span: Span<'a>) =
        NativeMemory.Free(Unsafe.AsPointer(&span.GetPinnableReference()))

module ReadOnlySpan =
    let inline isEmpty (span: ReadOnlySpan<_>) = span.Length = 0
    let inline isNotEmpty (span: ReadOnlySpan<_>) = span.Length <> 0
    let inline slice start count (span: ReadOnlySpan<_>) = span.Slice(start, count)
    let inline sliceFrom start (span: ReadOnlySpan<_>) = span.Slice(start)
    let inline sliceTo index (span: ReadOnlySpan<_>) = span.Slice(0, index)
    let inline fromPtr count (ptr: nativeptr<'a>) = ReadOnlySpan<'a>(ptr |> NativePtr.toVoidPtr, count)
    let inline fromVoidPtr<'a when 'a: unmanaged> count ptr = ReadOnlySpan<'a>(ptr, count)
    let inline fromString (str: string): ReadOnlySpan<char> = str.AsSpan()
    let inline fromSpan (span: Span<_>): ReadOnlySpan<_> = Span.op_Implicit span
    let inline fromArray (array: 'a[]): ReadOnlySpan<_> = ReadOnlySpan.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): ReadOnlySpan<_> = ReadOnlySpan.op_Implicit array
    let inline fromMemory (memory: ReadOnlyMemory<_>) = memory.Span

    let inline (|Empty|_|) (str: ReadOnlySpan<'a>) =
        str.IsEmpty

    let inline (|NotEmpty|_|) (str: ReadOnlySpan<'a>) =
        str.IsEmpty |> not