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
    member inline this.u128 = UInt128.op_Implicit this
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit this
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Implicit this
    member inline this.f32 = float32 this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

type UInt16 with
    member inline this.u8 = byte this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Implicit this
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit this
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit this
    member inline this.f32 = float32 this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

type UInt32 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Implicit this
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit this
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit this
    member inline this.f32 = float32 this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

type UInt64 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u128 = UInt128.op_Implicit this
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit this
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit this
    member inline this.f32 = float32 this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

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
    member inline this.u128 = UInt128.op_Implicit this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit this
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit this
    member inline this.f32 = float32 this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

type SByte with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Explicit this
    member inline this.uptr = unativeint this

    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit this
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Implicit this
    member inline this.f32 = float32 this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

type Int16 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Explicit this
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i32 = int32 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit this
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit this
    member inline this.f32 = float32 this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

type Int32 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Explicit this
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i64 = int64 this
    member inline this.i128 = Int128.op_Implicit this
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit this
    member inline this.f32 = float32 this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

type Int64 with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this
    member inline this.u128 = UInt128.op_Explicit this
    member inline this.uptr = unativeint this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this
    member inline this.i128 = Int128.op_Implicit this
    member inline this.iptr = nativeint this

    member inline this.f16 = Half.op_Explicit this
    member inline this.f32 = float32 this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

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
    member inline this.i128 = Int128.op_Implicit this

    member inline this.f16 = Half.op_Explicit this
    member inline this.f32 = float32 this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

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

    member inline this.f16 = Half.op_Explicit this
    member inline this.f64 = float this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

type Double with
    member inline this.u8 = byte this
    member inline this.u16 = uint16 this
    member inline this.u32 = uint32 this
    member inline this.u64 = uint64 this

    member inline this.i8 = sbyte this
    member inline this.i16 = int16 this
    member inline this.i32 = int32 this

    member inline this.f16 = Half.op_Explicit this
    member inline this.f32 = float32 this

    member inline this.v128 = v128.Create(this)
    member inline this.v256 = v256.Create(this)
    member inline this.v512 = v512.Create(this)

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

[<AbstractClass; Sealed; AutoOpen>]
type VectorFactories() =

    static member inline v64(value: u8) = v64.Create(value)
    static member inline v64(value: u8, v1, v2, v3, v4, v5, v6, v7) =
        v64.Create(value, v1, v2, v3, v4, v5, v6, v7)

    static member inline v64(value: u16) = v64.Create(value)
    static member inline v64(value: u16, v1, v2, v3) =
        v64.Create(value, v1, v2, v3)

    static member inline v64(value: u32) = v64.Create(value)
    static member inline v64(value: u32, v1) =
        v64.Create(value, v1)

    static member inline v64(value: u64) = v64.Create(value)
    static member inline v64(value: uptr) = v64.Create(value)

    static member inline v64(value: i8) = v64.Create(value)
    static member inline v64(value: i8, v1, v2, v3, v4, v5, v6, v7) =
        v64.Create(value, v1, v2, v3, v4, v5, v6, v7)

    static member inline v64(value: i16) = v64.Create(value)
    static member inline v64(value: i16, v1, v2, v3) =
        v64.Create(value, v1, v2, v3)

    static member inline v64(value: i32) = v64.Create(value)
    static member inline v64(value: i32, v1) =
        v64.Create(value, v1)

    static member inline v64(value: i64) = v64.Create(value)
    static member inline v64(value: iptr) = v64.Create(value)

    static member inline v64(value: f32) = v64.Create(value)
    static member inline v64(value: f32, v1) =
        v64.Create(value, v1)

    static member inline v64(value: f64) = v64.Create(value)

    static member inline v128(value: u8) = v128.Create(value)
    static member inline v128(value: u8, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15) =
        v128.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15)
    static member inline v128(lower: v64<u8>, upper) =
        v128.Create(lower, upper)

    static member inline v128(value: u16) = v128.Create(value)
    static member inline v128(value: u16, v1, v2, v3, v4, v5, v6, v7) =
        v128.Create(value, v1, v2, v3, v4, v5, v6, v7)
    static member inline v128(lower: v64<u16>, upper) =
        v128.Create(lower, upper)

    static member inline v128(value: u32) = v128.Create(value)
    static member inline v128(value: u32, v1, v2, v3) =
        v128.Create(value, v1, v2, v3)
    static member inline v128(lower: v64<u32>, upper) =
        v128.Create(lower, upper)

    static member inline v128(value: u64) = v128.Create(value)
    static member inline v128(value: u64, v1) =
        v128.Create(value, v1)
    static member inline v128(lower: v64<u64>, upper) =
        v128.Create(lower, upper)
    
    static member inline v128(value: uptr) = v128.Create(value)
    static member inline v128(lower: v64<uptr>, upper) =
        v128.Create(lower, upper)
    
    static member inline v128(value: i8) = v128.Create(value)
    static member inline v128(value: i8, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15) =
        v128.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15)
    static member inline v128(lower: v64<i8>, upper) =
        v128.Create(lower, upper)

    static member inline v128(value: i16) = v128.Create(value)
    static member inline v128(value: i16, v1, v2, v3, v4, v5, v6, v7) =
        v128.Create(value, v1, v2, v3, v4, v5, v6, v7)
    static member inline v128(lower: v64<i16>, upper) =
        v128.Create(lower, upper)

    static member inline v128(value: i32) = v128.Create(value)
    static member inline v128(value: i32, v1, v2, v3) =
        v128.Create(value, v1, v2, v3)
    static member inline v128(lower: v64<i32>, upper) =
        v128.Create(lower, upper)

    static member inline v128(value: i64) = v128.Create(value)
    static member inline v128(value: i64, v1) =
        v128.Create(value, v1)
    static member inline v128(lower: v64<i64>, upper) =
        v128.Create(lower, upper)

    static member inline v128(value: iptr) = v128.Create(value)
    static member inline v128(lower: v64<iptr>, upper) =
        v128.Create(lower, upper)

    static member inline v128(value: f32) = v128.Create(value)
    static member inline v128(value: f32, v1, v2, v3) =
        v128.Create(value, v1, v2, v3)
    static member inline v128(lower: v64<f32>, upper) =
        v128.Create(lower, upper)

    static member inline v128(value: f64) = v128.Create(value)
    static member inline v128(value: f64, v1) =
        v128.Create(value, v1)
    static member inline v128(lower: v64<f64>, upper) =
        v128.Create(lower, upper)

    static member inline v256(value: u8) = v256.Create(value)
    static member inline v256(value: u8, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31) =
        v256.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31)
    static member inline v256(lower: v128<u8>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: u16) = v256.Create(value)
    static member inline v256(value: u16, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15) =
        v256.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15)
    static member inline v256(lower: v128<u16>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: u32) = v256.Create(value)
    static member inline v256(value: u32, v1, v2, v3, v4, v5, v6, v7) =
        v256.Create(value, v1, v2, v3, v4, v5, v6, v7)
    static member inline v256(lower: v128<u32>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: u64) = v256.Create(value)
    static member inline v256(value: u64, v1, v2, v3) =
        v256.Create(value, v1, v2, v3)
    static member inline v256(lower: v128<u64>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: uptr) = v256.Create(value)
    static member inline v256(lower: v128<uptr>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: i8) = v256.Create(value)
    static member inline v256(value: i8, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31) =
        v256.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31)
    static member inline v256(lower: v128<i8>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: i16) = v256.Create(value)
    static member inline v256(value: i16, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15) =
        v256.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15)
    static member inline v256(lower: v128<i16>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: i32) = v256.Create(value)
    static member inline v256(value: i32, v1, v2, v3, v4, v5, v6, v7) =
        v256.Create(value, v1, v2, v3, v4, v5, v6, v7)
    static member inline v256(lower: v128<i32>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: i64) = v256.Create(value)
    static member inline v256(value: i64, v1, v2, v3) =
            v256.Create(value, v1, v2, v3)
    static member inline v256(lower: v128<i64>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: iptr) = v256.Create(value)
    static member inline v256(lower: v128<iptr>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: f32) = v256.Create(value)
    static member inline v256(value: f32, v1, v2, v3, v4, v5, v6, v7) =
        v256.Create(value, v1, v2, v3, v4, v5, v6, v7)
    static member inline v256(lower: v128<f32>, upper) =
        v256.Create(lower, upper)

    static member inline v256(value: f64) = v256.Create(value)
    static member inline v256(value: f64, v1, v2, v3) =
        v256.Create(value, v1, v2, v3)
    static member inline v256(lower: v128<f64>, upper) =
        v256.Create(lower, upper)

    static member inline v512(value: u8) = v512.Create(value)
    static member inline v512(value: u8, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34, v35, v36, v37, v38, v39, v40, v41, v42, v43, v44, v45, v46, v47, v48, v49, v50, v51, v52, v53, v54, v55, v56, v57, v58, v59, v60, v61, v62, v63) =
        v512.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34, v35, v36, v37, v38, v39, v40, v41, v42, v43, v44, v45, v46, v47, v48, v49, v50, v51, v52, v53, v54, v55, v56, v57, v58, v59, v60, v61, v62, v63)
    static member inline v512(lower: v256<u8>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: u16) = v512.Create(value)
    static member inline v512(value: u16, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31) =
        v512.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31)
    static member inline v512(lower: v256<u16>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: u32) = v512.Create(value)
    static member inline v512(value: u32, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15) =
        v512.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15)
    static member inline v512(lower: v256<u32>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: u64) = v512.Create(value)
    static member inline v512(value: u64, v1, v2, v3, v4, v5, v6, v7) =
        v512.Create(value, v1, v2, v3, v4, v5, v6, v7)
    static member inline v512(lower: v256<u64>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: uptr) = v512.Create(value)
    static member inline v512(lower: v256<uptr>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: i8) = v512.Create(value)
    static member inline v512(value: i8, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34, v35, v36, v37, v38, v39, v40, v41, v42, v43, v44, v45, v46, v47, v48, v49, v50, v51, v52, v53, v54, v55, v56, v57, v58, v59, v60, v61, v62, v63) =
        v512.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34, v35, v36, v37, v38, v39, v40, v41, v42, v43, v44, v45, v46, v47, v48, v49, v50, v51, v52, v53, v54, v55, v56, v57, v58, v59, v60, v61, v62, v63)
    static member inline v512(lower: v256<i8>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: i16) = v512.Create(value)
    static member inline v512(value: i16, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31) =
        v512.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31)
    static member inline v512(lower: v256<i16>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: i32) = v512.Create(value)
    static member inline v512(value: i32, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15) =
        v512.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15)
    static member inline v512(lower: v256<i32>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: i64) = v512.Create(value)
    static member inline v512(value: i64, v1, v2, v3, v4, v5, v6, v7) =
        v512.Create(value, v1, v2, v3, v4, v5, v6, v7)
    static member inline v512(lower: v256<i64>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: iptr) = v512.Create(value)
    static member inline v512(lower: v256<iptr>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: f32) = v512.Create(value)
    static member inline v512(value: f32, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15) =
        v512.Create(value, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15)
    static member inline v512(lower: v256<f32>, upper) =
        v512.Create(lower, upper)

    static member inline v512(value: f64) = v512.Create(value)
    static member inline v512(value: f64, v1, v2, v3, v4, v5, v6, v7) =
        v512.Create(value, v1, v2, v3, v4, v5, v6, v7)
    static member inline v512(lower: v256<f64>, upper) =
        v512.Create(lower, upper)