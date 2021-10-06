module En3Tho.FSharp.Extensions.Scanf

open System

type ReadOnlySpan<'t> with
    member internal this.SliceForward value =
        if value >= this.Length then
            ReadOnlySpan<'t>()
        else
            this.Slice(value, this.Length - value)

// TODO: Investigate if it is possible to do via inline magic
let private scanfInternal strict (value: string) (fmt: Printf.StringFormat<_>) =

    let mutable capturesSpan: ReadOnlySpan<_> = Span.op_Implicit(fmt.Captures.AsSpan())
    let mutable valueSpan = value.AsSpan()
    let mutable formatSpan = fmt.Value.AsSpan()

    let mutable success = valueSpan.Length > 0 && formatSpan.Length > 0

    while success && capturesSpan.Length > 0 do
        match formatSpan.IndexOf("%P()".AsSpan()) with
        | -1 ->
            success <- false
        | index ->
            if not (formatSpan.Slice(0, index).SequenceEqual(valueSpan.Slice(0, index))) then
                success <- false
            else

            valueSpan <- valueSpan.SliceForward index

            if valueSpan.Length = 0 then
                success <- false
            else

            let mutable charCounter = 0

            match capturesSpan.[0] with
            | :? Ref<char> as ref ->
                ref.Value <- valueSpan.[0]
                charCounter <- 1
            | captureRef ->
                charCounter <-
                    match valueSpan.IndexOf(' ') with
                    | -1 ->
                        valueSpan.Length
                    | index ->
                        index

                let value = valueSpan.Slice(0, charCounter)
                match captureRef with
                | :? Ref<string> as ref ->
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
            capturesSpan <- capturesSpan.SliceForward 1
            formatSpan <- formatSpan.SliceForward (index + 4)

    if success && strict then
        success <- formatSpan.Length = valueSpan.Length && formatSpan.SequenceEqual(valueSpan)

    success

/// Strict vesion of scanf which matches full string
let scanf value fmt = scanfInternal true value fmt

/// Light version of scanf which stops matching when values are
let scanfl value fmt = scanfInternal false value fmt