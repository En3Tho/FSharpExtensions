module En3Tho.FSharp.Extensions.Tests.Vectors

open System.Numerics
open System.Runtime.Intrinsics.X86
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

let testHttpVerb value =
    if (value &&& 0xFF_FF_FF_FF_FF_FF_FFUL) == DeleteU64 then
        6
    else
        let vecMaskedValue = value.v256 &&& v256(0xFF_FF_FF_FFUL, 0xFF_FF_FF_FF_FFUL, 0xFF_FF_FF_FF_FF_FFUL, 0xFF_FF_FF_FF_FF_FF_FF_FFUL);

        let vec1 = v256(GetU64, PostU64, TraceU64, ConnectU64);
        let vec1Result = Avx2.CompareEqual(vecMaskedValue, vec1);

        let vec2 = v256(PutU64, HeadU64, PatchU64, OptionsU64);
        let vec2Result = Avx2.CompareEqual(vecMaskedValue, vec2);

        let vecResult = vec1Result ||| vec2Result;
        let nonZeroMask = Avx2.MoveMask(vecResult.u8);

        let trailingZeroCount = BitOperations.TrailingZeroCount(nonZeroMask);
        (nonZeroMask &&& 0x07_05_04_03) >>> trailingZeroCount;

[<Fact>]
let ``test that vector extensions are working correcly``() =
    Assert.Equal(3, testHttpVerb GetU64)
    Assert.Equal(3, testHttpVerb PutU64)

    Assert.Equal(4, testHttpVerb PostU64)
    Assert.Equal(4, testHttpVerb HeadU64)

    Assert.Equal(5, testHttpVerb TraceU64)
    Assert.Equal(5, testHttpVerb PatchU64)

    Assert.Equal(6, testHttpVerb DeleteU64)

    Assert.Equal(7, testHttpVerb OptionsU64)
    Assert.Equal(7, testHttpVerb ConnectU64)

    Assert.Equal(0, testHttpVerb 0UL)
    Assert.Equal(0, testHttpVerb u64.MaxValue)