module Benchmarks.VectorsBase64

open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open BenchmarkDotNet.Attributes
open En3Tho.FSharp.Extensions

[<MemoryDiagnoser; DisassemblyDiagnoser(filters = [||])>]
[<SimpleJob>]
type Benchmark() =

    member val Bytes = null with get, set

    [<GlobalSetup>]
    member this.GlobalSetup() =
        this.Bytes <- Array.init 24 (fun _ -> 0xFFuy)

    member _.BytesToBase64VectorLookup1(source: ReadOnlySpan<byte>, destination: Span<char>) =
        // Convert from
        // [ 00000000_66666666_55555555_44444444_00000000_33333333_22222222_11111111 ]
        // To
        // [ 00666666_00665555_00555544_00444444_00333333_00332222_00222211_00111111 ]

        // [ 00000000_33333333_22222222_11111111 ]
        // [ 00333333_00332222_00222211_00111111 ]


        // [ 00000000_00000000_00000000_00111111 ] // and 0x3F
        // [ 00000000_00000000_00000000_00111111 ] // shift 0

        // [ 00000000_00000000_00002222_11000000 ] // and 0xFC0
        // [ 00000000_00000000_00222211_00000000 ] // shift 2

        // [ 00000000_00000033_22220000_00000000 ] // and 0x3F000
        // [ 00000000_00332222_00000000_00000000 ] // shift 4

        // [ 00000000_33333300_00000000_00000000 ] // and 0xFC0000
        // [ 00333333_00000000_00000000_00000000 ] // shift 6

        // ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/
        let lookup0_31 = v256(0x48_47_46_45_44_43_42_41UL, 0x50_4F_4E_4D_4C_4B_4A_49UL, 0x58_57_56_55_54_53_52_51UL, 0x66_65_64_63_62_61_5A_59UL).u8
        let lookup32_63 = v256(0x6E_6D_6C_6B_6A_69_68_67UL, 0x76_75_74_73_72_71_70_6FUL, 0x33_32_31_30_7A_79_78_77UL, 0x2F_2B_39_38_37_36_35_34UL).u8
        let valuesU64 = MemoryMarshal.Cast<_, u64>(source)

        let mutable i = 0
        while i + 2 < valuesU64.Length do
            let v1 = valuesU64[i]
            let v2 = valuesU64[i + 1]
            let v3 = valuesU64[i + 2]

            let vec = v256(v1, v2, v3, 0UL)

            // [ 00000000_66666666_55555555_44444444_00000000_33333333_22222222_11111111 ]
            let shuffled = v256.Shuffle(vec.u8, v256(0xF0_05_04_03_F0_02_01_00UL, 0xF0_0B_0A_09_F0_08_07_06UL, 0xF0_11_10_0F_F0_0E_0D_0CUL, 0xF0_17_16_15_F0_14_13_12UL).u8).u32

            // [ 00666666_00665555_00555544_00444444_00333333_00332222_00222211_00111111 ]
            let vec6Bits =
                (shuffled &&& v256(0x3Fu))
                ||| ((shuffled &&& v256(0xFC0u)) <<< 2)
                ||| ((shuffled &&& v256(0x3F000u)) <<< 4)
                ||| ((shuffled &&& v256(0xFC0000u)) <<< 6)

            let mask32_63 = v256.GreaterThan(vec6Bits.u8, v256(31uy))
            let mask0_31 = v256.Equals(mask32_63, v256.Zero)

            let byteChars0_31 = v256.Shuffle(lookup0_31, vec6Bits.u8 &&& mask0_31)
            let byteChars32_63 = v256.Shuffle(lookup32_63, vec6Bits.u8 &&& v256(0x1Fuy) &&& mask32_63)
            let byteChars = (byteChars0_31 &&& mask0_31) ||| (byteChars32_63 &&& mask32_63)

            let chars1 = v256.Shuffle(byteChars, v256(0xF0_03_F0_02_F0_01_F0_00UL, 0xF0_07_F0_06_F0_05_F0_04UL, 0xF0_0B_F0_0A_F0_09_F0_08UL, 0xF0_0F_F0_0E_F0_0D_F0_0CUL).u8)
            let chars2 = v256.Shuffle(byteChars, v256(0xF0_13_F0_12_F0_11_F0_10UL, 0xF0_17_F0_16_F0_15_F0_14UL, 0xF0_1B_F0_1A_F0_19_F0_18UL, 0xF0_1F_F0_1E_F0_1D_F0_1CUL).u8)

            v256.StoreUnsafe(chars1.u16, &Unsafe.As<_, u16>(&destination[i * 32]))
            v256.StoreUnsafe(chars2.u16, &Unsafe.As<_, u16>(&destination[i * 32 + 16]))

            i <- i + 3

    [<Benchmark>]
    member this.ConvertToBase64() =
        Convert.ToBase64String(this.Bytes) |> ignore

    [<Benchmark>]
    member this.ConvertToBase64Naive() =
        let converted = Array.zeroCreate 32
        this.BytesToBase64VectorLookup1(this.Bytes, converted.AsSpan())