﻿module En3Tho.FSharp.Extensions.Scanf

open System
open System.Diagnostics
open En3Tho.FSharp.Extensions

let [<Literal>] private InterpolationFormat = "%P()"

// TODO: Investigate if it is possible to do via inline magic
// TODO: Parse complex values via JSON
let private scanfInternal strict (memory: ReadOnlyMemory<char>) (fmt: Printf.StringFormat<string>) =

    let mutable capturesSpan = fmt.Captures.AsSpan()
    let mutable valueSpan = memory.Span
    let mutable formatSpan = fmt.Value.AsSpan()

    // first, find the first occurence of %P() format substring and check if anything before it actually matches a corresponding part in a value string

    let mutable success =
        capturesSpan.Length > 0
        && formatSpan.Length > 0
        && (match formatSpan.IndexOf(InterpolationFormat, StringComparison.Ordinal) with
           | -1 ->
               false
           | index ->
               if valueSpan.StartsWith(formatSpan.Slice(0, index)) then
                   formatSpan <- formatSpan.Advance(index + 4)
                   valueSpan <- valueSpan.Advance(index)
                   true
               else
                   false)

    // then, in a loop, similarly repeat the process by finding next %P() occurence and searching for a  literal substring between those formats in a value string
    // if success then process substring between value string start and a literal string that we've just found

    while success && capturesSpan.Length > 0 do

        let currentCapture = capturesSpan[0]
        capturesSpan <- capturesSpan.Advance(1)

        let literalAfterFormat =
            match formatSpan.IndexOf(InterpolationFormat) with
            | -1 ->
                formatSpan
            | index ->
                formatSpan.Slice(0, index)

        let charCounter =
            if literalAfterFormat.IsEmpty then
                valueSpan.Length
            else
                match valueSpan.IndexOf(literalAfterFormat) with
                | -1 ->
                    if strict then 0
                    else valueSpan.Length
                | index ->
                    index

        if strict && charCounter = 0 then
            success <- false
        else
            let value = valueSpan.Slice(0, charCounter)

            match currentCapture with
            | :? Ref<string> as ref ->
                if formatSpan.IsEmpty && capturesSpan.IsEmpty then
                    ref.Value <- valueSpan.ToString()
                    valueSpan <- ReadOnlySpan()
                else
                    ref.Value <- value.ToString()
            | :? Ref<ReadOnlyMemory<char>> as ref ->
                if formatSpan.IsEmpty && capturesSpan.IsEmpty then
                    ref.Value <- memory.Slice(memory.Length - valueSpan.Length, valueSpan.Length)
                    valueSpan <- ReadOnlySpan()
                else
                    ref.Value <- memory.Slice(memory.Length - valueSpan.Length, valueSpan.Length).Slice(0, charCounter)
            | :? Ref<char> as ref ->
                if value.Length = 1 then
                    ref.Value <- value[0]
                else
                    success <- false
            | :? Ref<DateTime> as ref ->
                success <- DateTime.TryParse(value, ref)
            | :? Ref<TimeSpan> as ref ->
                success <- TimeSpan.TryParse(value, ref)
            | :? Ref<int> as ref ->
                success <- Int32.TryParse(value, ref)
            | :? Ref<int8> as ref ->
                success <- SByte.TryParse(value, ref)
            | :? Ref<int16> as ref ->
                success <- Int16.TryParse(value, ref)
            | :? Ref<int64> as ref ->
                success <- Int64.TryParse(value, ref)
            | :? Ref<uint> as ref ->
                success <- UInt32.TryParse(value, ref)
            | :? Ref<Byte> as ref ->
                success <- Byte.TryParse(value, ref)
            | :? Ref<uint16> as ref ->
                success <- UInt16.TryParse(value, ref)
            | :? Ref<uint64> as ref ->
                success <- UInt64.TryParse(value, ref)
            | :? Ref<float> as ref ->
                success <- Double.TryParse(value, ref)
            | :? Ref<float32> as ref ->
                success <- Single.TryParse(value, ref)
            | :? Ref<bool> as ref ->
                success <- Boolean.TryParse(value, ref)
            | :? Ref<Guid> as ref ->
                success <- Guid.TryParse(value, ref)
            | _ ->
                Debug.Fail("Only Ref<PrimitiveType> is supported")
                success <- false

            formatSpan <- formatSpan.Advance (literalAfterFormat.Length + 4)
            valueSpan <- valueSpan.Advance (charCounter + literalAfterFormat.Length)

    if success && strict then
        success <- formatSpan.IsEmpty && valueSpan.IsEmpty
                   || formatSpan.SequenceEqual(valueSpan)

    success

/// Strict vesion of scanf which matches full string, forbids empty strings
let scanf fmt (value: string) = scanfInternal true (value.AsMemory()) fmt

/// Light version of scanf which stops matching when values are found, can return empty strings
let scanfl fmt (value: string) = scanfInternal false (value.AsMemory()) fmt

/// Strict vesion of scanf which matches full memory, forbids empty strings
let scanfMemory fmt (value: ReadOnlyMemory<char>) = scanfInternal true value fmt

/// Light version of scanf which stops matching when values are found, can return empty strings
let scanflMemory fmt (value: ReadOnlyMemory<char>) = scanfInternal false value fmt