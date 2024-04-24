[<AutoOpen>]
module En3Tho.FSharp.Extensions.Pointers

open System
open Microsoft.FSharp.NativeInterop

type voidptr with
    member inline this.iptr = ecast<_, iptr> this
    member inline this.uptr = ecast<_, uptr> this
    member inline this.As<'T when 'T: unmanaged>() = ucast<_, nativeptr<'T>> this
    member inline this.AsSpan<'T>(length) = Span<'T>(this, length)
    member inline this.AsReadOnlySpan<'T>(length) = ReadOnlySpan<'T>(this, length)

type nativeptr<'T when 'T: unmanaged> with
    member inline this.voidptr = NativePtr.toVoidPtr this
    member inline this.iptr = NativePtr.toNativeInt this
    member inline this.uptr = this.iptr.uptr
    member inline this.byref = NativePtr.toByRef this
    member inline this.value with get() = NativePtr.read this and set value = NativePtr.write this value
    member inline this.As<'U when 'U: unmanaged>() = ucast<_, nativeptr<'U>> this
    member inline this.AsSpan(length) = Span<'T>(this.voidptr, length)
    member inline this.AsReadOnlySpan(length) = ReadOnlySpan<'T>(this.voidptr, length)