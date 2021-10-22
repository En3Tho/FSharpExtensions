module En3Tho.FSharp.Extensions.Scanf

open System

let [<Literal>] private InterpolationFormat = "%P()"

// TODO: Investigate if it is possible to do via inline magic
// TODO: Parse complex values via JSON
let private scanfInternal strict (value: ReadOnlySpan<char>) (fmt: Printf.StringFormat<_>) =

    let mutable capturesSpan: ReadOnlySpan<_> = Span.op_Implicit(fmt.Captures.AsSpan())
    let mutable valueSpan = value
    let mutable formatSpan = fmt.Value.AsSpan()

    let mutable success = valueSpan.Length > 0 && formatSpan.Length > 0

    while success && capturesSpan.Length > 0 do
        match formatSpan.IndexOf(InterpolationFormat.AsSpan()) with // interpolation format
        | -1 ->
            success <- false
        | index ->
            if not (formatSpan.Slice(0, index).SequenceEqual(valueSpan.Slice(0, index))) then
                success <- false
            else

            let currentCapture = capturesSpan.[0]

            formatSpan <- formatSpan.SliceForward (index + 4)
            valueSpan <- valueSpan.SliceForward index
            capturesSpan <- capturesSpan.SliceForward 1

            if valueSpan.Length = 0 then
                success <- false
            else

            let mutable charCounter = 0

            match currentCapture with
            | :? Ref<char> as ref ->
                ref.Value <- valueSpan.[0]
                charCounter <- 1
            | _ ->
                charCounter <-
                    match valueSpan.IndexOf(' ') with
                    | -1 ->
                        valueSpan.Length
                    | index ->
                        index

                if charCounter = 0 then
                    success <- false
                else

                let mutable value = valueSpan.Slice(0, charCounter)

                if formatSpan.Length > 0 && not (formatSpan.[0] = ' ') then
                    let literalLength =
                        match formatSpan.SliceForward(1).IndexOf(' ') with
                        | 0 -> 1
                        | -1 -> formatSpan.Length
                        | index -> index

                    let literalSpan = formatSpan.Slice(0, literalLength)
                    if valueSpan.EndsWith(literalSpan) then
                        value <- value.Slice(0, value.Length - literalLength)
                        charCounter <- charCounter + literalLength
                        formatSpan <- formatSpan.SliceForward(literalLength)
                    else
                        success <- false

                match currentCapture with
                | :? Ref<string> as ref ->
                    if formatSpan.Length = 0 && capturesSpan.Length = 0 then
                        ref.Value <- valueSpan.ToString()
                        charCounter <- valueSpan.Length
                    else
                        ref.Value <- value.ToString()
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
                | _ ->
                    raise (NotImplementedException("Only Ref<PrimitiveType> is supported"))

            valueSpan <- valueSpan.SliceForward charCounter

    if success && strict then
        success <- formatSpan.Length = valueSpan.Length && formatSpan.SequenceEqual(valueSpan)

    success

/// Strict vesion of scanf which matches full string
let scanf fmt (value: string) = scanfInternal true (value.AsSpan()) fmt

/// Light version of scanf which stops matching when values are found
let scanfl fmt (value: string) = scanfInternal false (value.AsSpan()) fmt

/// Strict vesion of scanf which matches full string
let scanfSpan fmt value = scanfInternal true value fmt

/// Light version of scanf which stops matching when values are found
let scanflSpan fmt value = scanfInternal false value fmt