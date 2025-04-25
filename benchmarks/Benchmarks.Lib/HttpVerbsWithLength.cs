using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Benchmarks.Lib;

public static class HttpVerbs
{
    public static ReadOnlySpan<UInt64> All =>
    [
        GetU64,
        PostU64,
        PutU64,
        DeleteU64,
        PatchU64,
        HeadU64,
        OptionsU64,
        TraceU64,
        ConnectU64
    ];

    /// <summary>
    /// utf-8
    /// <code>
    /// "GET"
    /// 0b84_69_71
    /// 0x54_45_47
    /// </code>
    /// </summary>
    public const UInt64 GetU64 = 0x54_45_47;
    /// <summary>
    /// utf-8
    /// <code>
    /// "POST"
    /// 0b84_83_79_80
    /// 0x54_53_4F_50
    /// </code>
    /// </summary>
    public const UInt64 PostU64 = 0x54_53_4F_50;
    /// <summary>
    /// utf-8
    /// <code>
    /// "PUT"
    /// 0b84_85_80
    /// 0x54_55_50
    /// </code>
    /// </summary>
    public const UInt64 PutU64 = 0x54_55_50;
    /// <summary>
    /// utf-8
    /// <code>
    /// "DELETE"
    /// 0b69_84_69_76_69_68
    /// 0x45_54_45_4C_45_44
    /// </code>
    /// </summary>
    public const UInt64 DeleteU64 = 0x45_54_45_4C_45_44;
    /// <summary>
    /// utf-8
    /// <code>
    /// "PATCH"
    /// 0b72_67_84_65_80
    /// 0x48_43_54_41_50
    /// </code>
    /// </summary>
    public const UInt64 PatchU64 = 0x48_43_54_41_50;
    /// <summary>
    /// utf-8
    /// <code>
    /// "HEAD"
    /// 0b68_65_69_72
    /// 0x44_41_45_48
    /// </code>
    /// </summary>
    public const UInt64 HeadU64 = 0x44_41_45_48;
    /// <summary>
    /// utf-8
    /// <code>
    /// "OPTIONS"
    /// 0b83_78_79_73_84_80_79
    /// 0x53_4E_4F_49_54_50_4F
    /// </code>
    /// </summary>
    public const UInt64 OptionsU64 = 0x53_4E_4F_49_54_50_4F;
    /// <summary>
    /// utf-8
    /// <code>
    /// "TRACE"
    /// 0b69_67_65_82_84
    /// 0x45_43_41_52_54
    /// </code>
    /// </summary>
    public const UInt64 TraceU64 = 0x45_43_41_52_54;
    /// <summary>
    /// utf-8
    /// <code>
    /// "CONNECT"
    /// 0b84_67_69_78_78_79_67
    /// 0x54_43_45_4E_4E_4F_43
    /// </code>
    /// </summary>
    public const UInt64 ConnectU64 = 0x54_43_45_4E_4E_4F_43;
}

public static class HttpVerbsAvx2
{
    public static int GetHttpVerbLengthIfElse(ulong value)
    {
        if ((value & 0x_FF_FF_FF_FF_FF_FF_FF) == HttpVerbs.ConnectU64)
        {
            return 7;
        }

        if ((value & 0x_FF_FF_FF) == HttpVerbs.GetU64)
        {
            return 3;
        }

        if ((value & 0x_FF_FF_FF_FF) == HttpVerbs.PostU64)
        {
            return 4;
        }

        if ((value & 0x_FF_FF_FF) == HttpVerbs.PutU64)
        {
            return 3;
        }

        if ((value & 0x_FF_FF_FF_FF_FF) == HttpVerbs.PatchU64)
        {
            return 5;
        }

        if ((value & 0x_FF_FF_FF_FF_FF_FF) == HttpVerbs.DeleteU64)
        {
            return 6;
        }

        if ((value & 0x_FF_FF_FF_FF) == HttpVerbs.HeadU64)
        {
            return 4;
        }

        if ((value & 0x_FF_FF_FF_FF_FF_FF_FF) == HttpVerbs.OptionsU64)
        {
            return 7;
        }

        if ((value & 0x_FF_FF_FF_FF_FF_FF_FF) == HttpVerbs.TraceU64)
        {
            return 5;
        }

        return 0;
    }

    public static int GetHttpVerbLengthAvx2(ulong value)
    {
        if ((value & 0x_FF_FF_FF_FF_FF_FF) == HttpVerbs.DeleteU64)
        {
            return 6;
        }

        // d e l e t e
        // [ g e t _ _ _ _ _ ] [ c o n n e c t _ ] [ p o s t _ _ _ _ ] [ t r a c e _ _ _ ]
        // [ p u t _ _ _ _ _ ] [ o p t i o n s _ ] [ h e a d _ _ _ _ ] [ p a t c h _ _ _ ]

        var vecValue = Vector256.Create(value);
        var vecMaskedValue = Avx2.And(vecValue, Vector256.Create(0xFF_FF_FFUL, 0xFF_FF_FF_FF_FF_FF_FF, 0xFF_FF_FF_FF, 0xFF_FF_FF_FF_FF));

        var vec1 = Vector256.Create(HttpVerbs.GetU64, HttpVerbs.ConnectU64, HttpVerbs.PostU64, HttpVerbs.TraceU64);
        var vec1Result = Avx2.CompareEqual(vecMaskedValue, vec1);

        var vec2 = Vector256.Create(HttpVerbs.PutU64, HttpVerbs.OptionsU64, HttpVerbs.HeadU64, HttpVerbs.PatchU64);
        var vec2Result = Avx2.CompareEqual(vecMaskedValue, vec2);

        var vecResult = Avx2.Or(vec1Result, vec2Result);
        var vecLengths = Avx2.And(vecResult, Vector256.Create((ulong)3, 7, 4, 5));

        return (int) Vector256.Sum(vecLengths);
    }

    public static int GetHttpVerbLengthAvx2_2(ulong value)
    {
        if ((value & 0x_FF_FF_FF_FF_FF_FF) == HttpVerbs.DeleteU64)
        {
            return 6;
        }

        // d e l e t e
        // [ g e t _ _ _ _ _ ] [ p o s t _ _ _ _ ] [ t r a c e _ _ _ ] [ c o n n e c t _ ]
        // [ p u t _ _ _ _ _ ] [ h e a d _ _ _ _ ] [ p a t c h _ _ _ ] [ o p t i o n s _ ]

        var vecValue = Vector256.Create(value);
        var vecMaskedValue = Avx2.And(vecValue, Vector256.Create(0xFF_FF_FFUL, 0xFF_FF_FF_FF, 0xFF_FF_FF_FF_FF, 0xFF_FF_FF_FF_FF_FF_FF));

        var vec1 = Vector256.Create(HttpVerbs.GetU64, HttpVerbs.PostU64, HttpVerbs.TraceU64, HttpVerbs.ConnectU64);
        var vec1Result = Avx2.CompareEqual(vecMaskedValue, vec1);

        var vec2 = Vector256.Create(HttpVerbs.PutU64, HttpVerbs.HeadU64, HttpVerbs.PatchU64, HttpVerbs.OptionsU64);
        var vec2Result = Avx2.CompareEqual(vecMaskedValue, vec2);

        var vecResult = Avx2.Or(vec1Result, vec2Result);
        var nonZeroMask = Avx2.MoveMask(vecResult.AsByte());

        var trailingZeroCount = BitOperations.TrailingZeroCount(nonZeroMask);
        return (nonZeroMask & 0x07_05_04_03) >> trailingZeroCount;
    }

    public static int GetHttpVerbLengthAvx2_3(ulong value)
    {
        if ((value & 0x_FF_FF_FF_FF_FF_FF) == HttpVerbs.DeleteU64)
        {
            return 6;
        }

        // d e l e t e
        // [ g e t _ _ _ _ _ ] [ p o s t _ _ _ _ ] [ t r a c e _ _ _ ] [ c o n n e c t _ ]
        // [ p u t _ _ _ _ _ ] [ h e a d _ _ _ _ ] [ p a t c h _ _ _ ] [ o p t i o n s _ ]

        var vecValue = Vector256.Create(value);
        var vecMaskedValue = vecValue & Vector256.Create(0xFF_FF_FFUL, 0xFF_FF_FF_FF, 0xFF_FF_FF_FF_FF, 0xFF_FF_FF_FF_FF_FF_FF);

        var vec1 = Vector256.Create(HttpVerbs.GetU64, HttpVerbs.PostU64, HttpVerbs.TraceU64, HttpVerbs.ConnectU64);
        var vec1Result = Vector256.Equals(vecMaskedValue, vec1);

        var vec2 = Vector256.Create(HttpVerbs.PutU64, HttpVerbs.HeadU64, HttpVerbs.PatchU64, HttpVerbs.OptionsU64);
        var vec2Result = Vector256.Equals(vecMaskedValue, vec2);

        var vecResult = vec1Result | vec2Result;

        var nonZeroMask = Avx2.MoveMask(vecResult.AsByte());

        var trailingZeroCount = BitOperations.TrailingZeroCount(nonZeroMask);
        return (nonZeroMask & 0x07_05_04_03) >> trailingZeroCount;
    }
}

public static class HttpVerbsWithLength
{
    public static int GetHttpVerbLengthWithLoop(ulong value)
    {
        foreach (var verb in All)
        {
            var byteCount = (int)(verb >> 60); // last byte
            var mask = ulong.MaxValue >> byteCount;
            if ((value & mask) == verb)
            {
                return byteCount;
            }

        }
        return 0;
    }

    public static ReadOnlySpan<UInt64> All =>
    [
        ConnectU64,
        GetU64,
        PostU64,
        PutU64,
        PatchU64,
        DeleteU64,
        HeadU64,
        OptionsU64,
        TraceU64
    ];

    /// <summary>
    /// utf-8
    /// <code>
    /// "GET"
    /// 0b84_69_71
    /// 0x54_45_47
    /// </code>
    /// </summary>
    public const UInt64 GetU64 = 0x03_00_00_00_00_54_45_47;
    /// <summary>
    /// utf-8
    /// <code>
    /// "POST"
    /// 0b84_83_79_80
    /// 0x54_53_4F_50
    /// </code>
    /// </summary>
    public const UInt64 PostU64 = 0x04_00_00_00_54_53_4F_50;
    /// <summary>
    /// utf-8
    /// <code>
    /// "PUT"
    /// 0b84_85_80
    /// 0x54_55_50
    /// </code>
    /// </summary>
    public const UInt64 PutU64 = 0x03_00_00_00_00_54_55_50;
    /// <summary>
    /// utf-8
    /// <code>
    /// "DELETE"
    /// 0b69_84_69_76_69_68
    /// 0x45_54_45_4C_45_44
    /// </code>
    /// </summary>
    public const UInt64 DeleteU64 = 0x06_00_45_54_45_4C_45_44;
    /// <summary>
    /// utf-8
    /// <code>
    /// "PATCH"
    /// 0b72_67_84_65_80
    /// 0x48_43_54_41_50
    /// </code>
    /// </summary>
    public const UInt64 PatchU64 = 0x05_00_00_48_43_54_41_50;
    /// <summary>
    /// utf-8
    /// <code>
    /// "HEAD"
    /// 0b68_65_69_72
    /// 0x44_41_45_48
    /// </code>
    /// </summary>
    public const UInt64 HeadU64 = 0x04_00_00_00_44_41_45_48;
    /// <summary>
    /// utf-8
    /// <code>
    /// "OPTIONS"
    /// 0b83_78_79_73_84_80_79
    /// 0x53_4E_4F_49_54_50_4F
    /// </code>
    /// </summary>
    public const UInt64 OptionsU64 = 0x07_53_4E_4F_49_54_50_4F;
    /// <summary>
    /// utf-8
    /// <code>
    /// "TRACE"
    /// 0b69_67_65_82_84
    /// 0x45_43_41_52_54
    /// </code>
    /// </summary>
    public const UInt64 TraceU64 = 0x05_00_00_45_43_41_52_54;
    /// <summary>
    /// utf-8
    /// <code>
    /// "CONNECT"
    /// 0b84_67_69_78_78_79_67
    /// 0x54_43_45_4E_4E_4F_43
    /// </code>
    /// </summary>
    public const UInt64 ConnectU64 = 0x07_54_43_45_4E_4E_4F_43;
}
