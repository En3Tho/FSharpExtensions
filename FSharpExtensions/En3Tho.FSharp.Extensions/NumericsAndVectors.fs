[<AutoOpen>]
module En3Tho.FSharp.Extensions.NumericsAndVectors

open System
open System.Runtime.Intrinsics

type u8 = Byte
type u16 = UInt16
type u32 = UInt32
type u64 = UInt64
type u128 = UInt128
type uptr = UIntPtr

type i8 = SByte
type i16 = Int16
type i32 = Int32
type i64 = Int64
type i128 = Int128
type iptr = IntPtr

type f16 = Half
type f32 = Single
type f64 = Double

type v64u8 = Vector64<u8>
type v64u16 = Vector64<u16>
type v64u32 = Vector64<u32>
type v64u64 = Vector64<u64>
type v64uptr = Vector64<uptr>

type v64i8 = Vector64<i8>
type v64i16 = Vector64<i16>
type v64i32 = Vector64<i32>
type v64i64 = Vector64<i64>
type v64iptr = Vector64<iptr>

type v64f32 = Vector64<f32>
type v64f64 = Vector64<f64>

type v128u8 = Vector128<u8>
type v128u16 = Vector128<u16>
type v128u32 = Vector128<u32>
type v128u64 = Vector128<u64>
type v128uptr = Vector128<uptr>

type v128i8 = Vector128<i8>
type v128i16 = Vector128<i16>
type v128i32 = Vector128<i32>
type v128i64 = Vector128<i64>
type v128iptr = Vector128<iptr>

type v128f32 = Vector128<f32>
type v128f64 = Vector128<f64>

type v256u8 = Vector256<u8>
type v256u16 = Vector256<u16>
type v256u32 = Vector256<u32>
type v256u64 = Vector256<u64>
type v256uptr = Vector256<uptr>

type v256i8 = Vector256<i8>
type v256i16 = Vector256<i16>
type v256i32 = Vector256<i32>
type v256i64 = Vector256<i64>
type v256iptr = Vector256<iptr>

type v256f32 = Vector256<f32>
type v256f64 = Vector256<f64>

type v512u8 = Vector512<u8>
type v512u16 = Vector512<u16>
type v512u32 = Vector512<u32>
type v512u64 = Vector512<u64>
type v512uptr = Vector512<uptr>

type v512i8 = Vector512<i8>
type v512i16 = Vector512<i16>
type v512i32 = Vector512<i32>
type v512i64 = Vector512<i64>
type v512iptr = Vector512<iptr>

type v512f32 = Vector512<f32>
type v512f64 = Vector512<f64>

type v64<'a> = Vector64<'a>
type v128<'a> = Vector128<'a>
type v256<'a> = Vector256<'a>
type v512<'a> = Vector512<'a>

type v64 = Vector64
type v128 = Vector128
type v256 = Vector256
type v512 = Vector512

type Byte with
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Implicit(this)
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit(this)
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Implicit(this)
    member inline this.f32 = float32 this
    member inline this.f64 = float this

type UInt16 with
    member inline this.u8 = byte this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Implicit(this)
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit(this)
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit(this)
    member inline this.f32 = float32 this
    member inline this.f64 = float this

type UInt32 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Implicit(this)
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit(this)
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit(this)
    member inline this.f32 = float32 this
    member inline this.f64 = float this

type UInt64 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u128 = UInt128.op_Implicit(this)
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit(this)
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit(this)
    member inline this.f32 = float32 this
    member inline this.f64 = float this

type UInt128 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128: Int128 = ecast this
    member inline this.iptr = nativeint this

    member inline this.f32 = float32 this
    member inline this.f64 = float this

type UIntPtr with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Implicit(this)

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit(this)
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit(this)
    member inline this.f32 = float32 this
    member inline this.f64 = float this

type SByte with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Explicit(this)
    member inline this.uptr = unativeint this

    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit(this)
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Implicit(this)
    member inline this.f32 = float32 this
    member inline this.f64 = float this

type Int16 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Explicit(this)
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit(this)
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit(this)
    member inline this.f32 = float32 this
    member inline this.f64 = float this

type Int32 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Explicit(this)
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit(this)
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit(this)
    member inline this.f32 = float32 this
    member inline this.f64 = float this

type Int64 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Explicit(this)
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i128 = Int128.op_Implicit(this)
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit(this)
    member inline this.f32 = float32 this
    member inline this.f64 = float this

type Int128 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128: UInt128 = ecast this
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.iptr = nativeint this

    member inline this.f32 = float32 this
    member inline this.f64 = float this

type IntPtr with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit(this)

    member inline this.f16 = Half.op_Explicit(this)
    member inline this.f32 = float32 this
    member inline this.f64 = float this

type Half with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this

    member inline this.f32 = float32 this
    member inline this.f64 = float this

type Single with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this

    member inline this.f16 = Half.op_Explicit(this)
    member inline this.f64 = float this

type Double with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this

    member inline this.f16 = Half.op_Explicit(this)
    member inline this.f32 = float32 this

// Not sure Vector64 is used at all? Old mmx stuff?
type Vector64<'a> with
    member inline this.u8 = this.AsByte()
    member inline this.u16 = this.AsUInt16()
    member inline this.u32 = this.AsUInt32()
    member inline this.u64 = this.AsUInt64()
    member inline this.uptr = this.AsNUInt()

    member inline this.i8 = this.AsSByte()
    member inline this.i16 = this.AsInt16()
    member inline this.i32 = this.AsInt32()
    member inline this.i64 = this.AsInt64()
    member inline this.iptr = this.AsNInt()

    member inline this.f32 = this.AsSingle()
    member inline this.f64 = this.AsDouble()

type Vector128<'a> with
    member inline this.u8 = this.AsByte()
    member inline this.u16 = this.AsUInt16()
    member inline this.u32 = this.AsUInt32()
    member inline this.u64 = this.AsUInt64()
    member inline this.uptr = this.AsNUInt()

    member inline this.i8 = this.AsSByte()
    member inline this.i16 = this.AsInt16()
    member inline this.i32 = this.AsInt32()
    member inline this.i64 = this.AsInt64()
    member inline this.iptr = this.AsNInt()

    member inline this.f32 = this.AsSingle()
    member inline this.f64 = this.AsDouble()

type Vector256<'a> with
    member inline this.u8 = this.AsByte()
    member inline this.u16 = this.AsUInt16()
    member inline this.u32 = this.AsUInt32()
    member inline this.u64 = this.AsUInt64()
    member inline this.uptr = this.AsNUInt()

    member inline this.i8 = this.AsSByte()
    member inline this.i16 = this.AsInt16()
    member inline this.i32 = this.AsInt32()
    member inline this.i64 = this.AsInt64()
    member inline this.iptr = this.AsNInt()

    member inline this.f32 = this.AsSingle()
    member inline this.f64 = this.AsDouble()

type Vector512<'a> with
    member inline this.u8 = this.AsByte()
    member inline this.u16 = this.AsUInt16()
    member inline this.u32 = this.AsUInt32()
    member inline this.u64 = this.AsUInt64()
    member inline this.uptr = this.AsNUInt()

    member inline this.i8 = this.AsSByte()
    member inline this.i16 = this.AsInt16()
    member inline this.i32 = this.AsInt32()
    member inline this.i64 = this.AsInt64()
    member inline this.iptr = this.AsNInt()

    member inline this.f32 = this.AsSingle()
    member inline this.f64 = this.AsDouble()