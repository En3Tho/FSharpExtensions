module En3Tho.FSharp.Extensions.Tests.Vectors

open System
open System.Numerics
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open System.Runtime.Intrinsics.X86
open Xunit
open En3Tho.FSharp.Extensions

let [<Literal>] GetU64 = 0x20_54_45_47UL
let [<Literal>] PostU64 = 0x20_54_53_4F_50UL
let [<Literal>] PutU64 = 0x20_54_55_50UL
let [<Literal>] DeleteU64 = 0x20_45_54_45_4C_45_44UL
let [<Literal>] PatchU64 = 0x20_48_43_54_41_50UL
let [<Literal>] HeadU64 = 0x20_44_41_45_48UL
let [<Literal>] OptionsU64 = 0x20_53_4E_4F_49_54_50_4FUL
let [<Literal>] TraceU64 = 0x20_45_43_41_52_54UL
let [<Literal>] ConnectU64 = 0x20_54_43_45_4E_4E_4F_43UL

let getHttpVerbLength value =
    // d e l e t e
    if (value &&& 0xFF_FF_FF_FF_FF_FF_FFUL) == DeleteU64 then
        6
    else
        // [ G E T   _ _ _ _ ] [ P O S T   _ _ _ ] [ T R A C E   _ _ ] [ C O N N E C T   ]
        // [ P U T   _ _ _ _ ] [ H E A D   _ _ _ ] [ P A T C H   _ _ ] [ O P T I O N S   ]
        // [ F F F F _ _ _ _ ] [ F F F F F _ _ _ ] [ F F F F F F _ _ ] [ F F F F F F F F ]
        let vVerbs = value.v256 &&& v256(0xFF_FF_FF_FFUL, 0xFF_FF_FF_FF_FFUL, 0xFF_FF_FF_FF_FF_FFUL, 0xFF_FF_FF_FF_FF_FF_FF_FFUL)

        // [ G E T _ _ _ _ _ ] [ G E T _ _ _ _ _ ] [ G E T _ _ _ _ _ ] [ G E T _ _ _ _ _ ]
        // [ F F F F F F F F ] [ _ _ _ _ _ _ _ _ ] [ _ _ _ _ _ _ _ _ ] [ _ _ _ _ _ _ _ _ ]
        let vMask1 = Avx2.CompareEqual(vVerbs, v256(GetU64, PostU64, TraceU64, ConnectU64))
        let vMask2 = Avx2.CompareEqual(vVerbs, v256(PutU64, HeadU64, PatchU64, OptionsU64))

        // [ G E T   x x x x _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ] => 0x00_00_00_FF, 0
        // [ _ _ _ _ _ _ _ _ P O S T   x x x _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ] => 0x00_00_FF_00, 8
        // [ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ P A T C H   x x _ _ _ _ _ _ _ _ ] => 0x00_FF_00_00, 16
        // [ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ O P T I O N S   ] => 0xFF_00_00_00, 24
        let vNonZeroMask = Avx2.MoveMask((vMask1 ||| vMask2).u8)

        let trailingZeroCount = BitOperations.TrailingZeroCount(vNonZeroMask)
        (vNonZeroMask &&& 0x07_05_04_03) >>> trailingZeroCount

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
type HexU32 =

    [<SkipLocalsInit>]
    member _.U32ToHexSSE(value: u32) =
        // 0xD4_C3_B2_A1 => [ 'D', '4', 'C', '3', 'B', '2', 'A', '1' ] => "D4C3B2A1"
        // [ A1, B2, C3, D4, A1, B2, C3, D4, A1, B2, C3, D4, A1, B2, C3, D4 ]
        // [ D4, 00, D4, 00, C3, 00, C3, 00, B2, 00, B2, 00, A1, 00, A1, 00 ]
        let shuffled = Ssse3.Shuffle(v128(value).u8, v128(0xF0_02_F0_02_F0_03_F0_03UL, 0xF0_00_F0_00_F0_01_F0_01UL).u8)

        // [ 00D4, 00D4, 00C3, 00C3, 00B2, 00B2, 00A1, 00A1 ]
        // [ D400, 4000, C300, 3000, B200, 2000, A100, 1000 ]
        let shiftedLeft = Sse2.MultiplyLow(shuffled.u16, v128(0x1000_0100_1000_0100UL).u16)

        // [ 000D, 0004, 000C, 0003, 000B, 0002, 000A, 0001 ]
        let shiftedRight = Sse2.ShiftRightLogical(shiftedLeft, 12uy)

        // 0 - 48, 9 - 57, 10 - "58", A - 65, 7 - diff
        let mask = Sse2.CompareGreaterThan(shiftedRight.i16, v128(9s))
        let diff = mask.u16 &&& v128(7us)
        let mutable result = shiftedRight + diff + v128(48us)

        String(Unsafe.AsPointer(&result).AsReadOnlySpan(8))

    [<SkipLocalsInit>]
    member _.U32ToHexVector(value: u32) =
        // 0xD4_C3_B2_A1 => [ 'D', '4', 'C', '3', 'B', '2', 'A', '1' ] => "D4C3B2A1"
        // [ A1, B2, C3, D4, A1, B2, C3, D4, A1, B2, C3, D4, A1, B2, C3, D4 ]
        // [ D4, 00, D4, 00, C3, 00, C3, 00, B2, 00, B2, 00, A1, 00, A1, 00 ]
        let bytes = v128.Shuffle(value.v128.u8, v128(0xF0_02_F0_02_F0_03_F0_03UL, 0xF0_00_F0_00_F0_01_F0_01UL).u8)

        // [ 00D4, 00D4, 00C3, 00C3, 00B2, 00B2, 00A1, 00A1 ]
        // [ D400, 4000, C300, 3000, B200, 2000, A100, 1000 ]
        // [ 000D, 0004, 000C, 0003, 000B, 0002, 000A, 0001 ]
        let nibbles = (bytes.u16 * v128(0x1000_0100_1000_0100UL).u16) >>> 12

        // 9 - 57, 10 - "58" => A - 65, 7 - diff
        let diff = v128.GreaterThan(nibbles.u16, 9us.v128) &&& 7us.v128
        let mutable result = nibbles + diff + 48us.v128

        String(Unsafe.AsPointer(&result).As<char>(), 0, 8)

    [<SkipLocalsInit>]
    member _.U64ToHexVector(value: u64) =
        // 0xD4_C3_B2_A1 => [ 'D', '4', 'C', '3', 'B', '2', 'A', '1' ] => "D4C3B2A1"
        // [ A1, B2, C3, D4, A1, B2, C3, D4, A1, B2, C3, D4, A1, B2, C3, D4 ]
        // [ D4, 00, D4, 00, C3, 00, C3, 00, B2, 00, B2, 00, A1, 00, A1, 00 ]
        let bytes = v256.Shuffle(value.v256.u8, v256(0xF0_06_F0_06_F0_07_F0_07UL, 0xF0_04_F0_04_F0_05_F0_05UL, 0xF0_02_F0_02_F0_03_F0_03UL, 0xF0_00_F0_00_F0_01_F0_01UL).u8)

        // [ 00D4, 00D4, 00C3, 00C3, 00B2, 00B2, 00A1, 00A1 ]
        // [ D400, 4000, C300, 3000, B200, 2000, A100, 1000 ]
        // [ 000D, 0004, 000C, 0003, 000B, 0002, 000A, 0001 ]
        let nibbles = (bytes.u16 * v256(0x1000_0100_1000_0100UL).u16) >>> 12

        // 9 - 57, 10 - "58" => A - 65, 7 - diff
        let diff = v256.GreaterThan(nibbles.u16, 9us.v256) &&& 7us.v256
        let mutable result = nibbles + diff + 48us.v256

        String(Unsafe.AsPointer(&result).As<char>(), 0, 16)

    [<SkipLocalsInit>]
    member _.U64ToHexVectorLookup(value: u64) =
        // [ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' ]
        let lookup = v256(48us, 49us, 50us, 51us, 52us, 53us, 54us, 55us, 56us, 57us, 65us, 66us, 67us, 68us, 69us, 70us)

        // 0xF8_E7_D6_C5_B4_A3_92_81 => [ 'F', '8', 'E', '7', 'D', '6', 'C', '5', 'B', '4', 'A', '3', '9', '2', '8', '1' ] => "F8E7D6C5B4A39281"
        // [ 81, 92, A3, B4, C5, D6, E7, F8, 81, 92, A3, B4, C5, D6, E7, F8, 81, 92, A3, B4, C5, D6, E7, F8, 81, 92, A3, B4, C5, D6, E7, F8 ]
        // [ F8, 00, F8, 00, E7, 00, E7, 00, D6, 00, D6, 00, C5, 00, C5, 00, B4, 00, B4, 00, A3, 00, A3, 00, 92, 00, 92, 00, 81, 00, 81, 00 ]
        let bytes = v256.Shuffle(value.v256.u8, v256(0xF0_06_F0_06_F0_07_F0_07UL, 0xF0_04_F0_04_F0_05_F0_05UL, 0xF0_02_F0_02_F0_03_F0_03UL, 0xF0_00_F0_00_F0_01_F0_01UL).u8)

        // [ 00F8, 00F8, 00E7, 00E7, 00D6, 00D6, 00C5, 00C5, 00B4, 00B4, 00A3, 00A3, 0092, 0092, 0081, 0081 ]
        // [ F800, 8000, E700, 7000, D600, 6000, C500, 5000, B400, 4000, A300, 3000, 9200, 2000, 8100, 1000 ]
        // [ 000F, 0008, 000E, 0007, 000D, 0006, 000C, 0005, 000B, 0004, 000A, 0003, 0009, 0002, 0008, 0001 ]

        let nibbles = (bytes.u16 * v256(0x1000_0100_1000_0100UL).u16) >>> 12
        let mutable result = v256.Shuffle(lookup, nibbles)

        String(Unsafe.AsPointer(&result).As<char>(), 0, 16)

[<Fact>]
let ``test hex struct``() =
    let x = 0xD4_C3_B2_A1u
    let hexu32 = HexU32()
    Assert.Equal("D4C3B2A1", hexu32.U32ToHexSSE(x))
    Assert.Equal("D4C3B2A1", hexu32.U32ToHexVector(x))

    let x = 0xF8_E7_D6_C5_B4_A3_92_81UL
    Assert.Equal("F8E7D6C5B4A39281", hexu32.U64ToHexVector(x))
    Assert.Equal("F8E7D6C5B4A39281", hexu32.U64ToHexVectorLookup(x))