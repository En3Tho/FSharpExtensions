module En3Tho.FSharp.Extensions.Tests.Vectors

open System
open System.Numerics
open System.Runtime.CompilerServices
open System.Runtime.Intrinsics.X86
open Microsoft.FSharp.NativeInterop
open Xunit
open En3Tho.FSharp.Extensions

let [<Literal>] GetU64 = 0x20_54_45_47UL;
let [<Literal>] PostU64 = 0x20_54_53_4F_50UL;
let [<Literal>] PutU64 = 0x20_54_55_50UL;
let [<Literal>] DeleteU64 = 0x20_45_54_45_4C_45_44UL;
let [<Literal>] PatchU64 = 0x20_48_43_54_41_50UL;
let [<Literal>] HeadU64 = 0x20_44_41_45_48UL;
let [<Literal>] OptionsU64 = 0x20_53_4E_4F_49_54_50_4FUL;
let [<Literal>] TraceU64 = 0x20_45_43_41_52_54UL;
let [<Literal>] ConnectU64 = 0x20_54_43_45_4E_4E_4F_43UL;

let getHttpVerbLength value =
    if (value &&& 0xFF_FF_FF_FF_FF_FF_FFUL) == DeleteU64 then
        6
    else
        let vecMaskedValue = value.v256 &&& v256(0xFF_FF_FF_FFUL, 0xFF_FF_FF_FF_FFUL, 0xFF_FF_FF_FF_FF_FFUL, 0xFF_FF_FF_FF_FF_FF_FF_FFUL);

        let v1Mask = Avx2.CompareEqual(vecMaskedValue, v256(GetU64, PostU64, TraceU64, ConnectU64));
        let v2Mask = Avx2.CompareEqual(vecMaskedValue, v256(PutU64, HeadU64, PatchU64, OptionsU64));

        let vecResult = v1Mask ||| v2Mask;
        let nonZeroMask = Avx2.MoveMask(vecResult.u8);

        let trailingZeroCount = BitOperations.TrailingZeroCount(nonZeroMask);
        (nonZeroMask &&& 0x07_05_04_03) >>> trailingZeroCount;

[<Fact>]
let ``test that vector extensions are working correcly``() =
    Assert.Equal(3, getHttpVerbLength GetU64)
    Assert.Equal(3, getHttpVerbLength PutU64)

    Assert.Equal(4, getHttpVerbLength PostU64)
    Assert.Equal(4, getHttpVerbLength HeadU64)

    Assert.Equal(5, getHttpVerbLength TraceU64)
    Assert.Equal(5, getHttpVerbLength PatchU64)

    Assert.Equal(6, getHttpVerbLength DeleteU64)

    Assert.Equal(7, getHttpVerbLength OptionsU64)
    Assert.Equal(7, getHttpVerbLength ConnectU64)

    Assert.Equal(0, getHttpVerbLength 0UL)
    Assert.Equal(0, getHttpVerbLength u64.MaxValue)

[<Struct>]
type HexU32(value: u32) =
    member _.Value = value

    [<SkipLocalsInit>]
    member _.ToHexString() =
        let values =
            let v1 = v128(value).u16
            let v2 = Sse2.ShuffleHigh(v1, 0x0uy)
            let v3 = Sse2.ShuffleLow(v2, 0xFFuy)

            let v4 = Sse2.And(v3, v128(0x00_0F_00_F0_0F_00_F0_00UL).u16)
            let v5 = Sse2.MultiplyLow(v4, v128(1us, 16us, 256us, 4096us, 1us, 16us, 256us, 4096us))
            Sse2.ShiftRightLogical(v5, 12uy)

        let mutable added =
            let v1 = Sse2.CompareGreaterThan(values.i16, v128(9s))
            let v2 = Sse2.And(v1.u16, v128(7us))
            Sse2.Add(values, v2)

        let pAdded = Unsafe.AsPointer(&added)
        String(pAdded |> NativePtr.ofVoidPtr<char>, 0, 8)