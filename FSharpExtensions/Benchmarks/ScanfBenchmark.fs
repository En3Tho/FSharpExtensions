module Benchmarks.ScanfBenchmark

open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Jobs
open En3Tho.FSharp.Extensions.Scanf

module Assets = // for CSharp version of bench

    let value1 = ref "0"
    let value2 = ref 0
    let value3 = ref 0.
    let value4 = ref 0.f
    let value5 = ref '0'
    let value6 = ref false
    let value7 = ref 0u

    let manySmallValuesText = $"{value1.Value} {value2.Value} {value3.Value} {value4.Value} {value5.Value} {value6.Value} {value7.Value}"
    let manySmallValuesFmt : Printf.StringFormat<_> = $"{value1} {value2} {value3} {value4} {value5} {value6} {value7}"

    let realisticFmt : Printf.StringFormat<_> = $"/authorize {value1} {value2}"

    let cmd = ref ""
    let userName = ref ""
    let userCode = ref 0
    let realisticCommandFmt : Printf.StringFormat<_> = $"/{cmd} {userName} {userCode}"

    let intRef = ref 0
    let floatRef = ref 0.
    let primitivesOnlyFmt : Printf.StringFormat<_> = $"{intRef} {floatRef}"

open Assets

[<
    MemoryDiagnoser;
    DisassemblyDiagnoser;
    SimpleJob(RuntimeMoniker.Net60)
    //Config(typeof<BenchmarkConfig.``Net 6, Pgo``>)
>]
type Benchmark() =

    [<Benchmark>]
    member _.ManySmallValues() =
        let value1 = ref "0"
        let value2 = ref 0
        let value3 = ref 0.
        let value4 = ref 0.f
        let value5 = ref '0'
        let value6 = ref false
        let value7 = ref 0u

        let text = $"{value1.Value} {value2.Value} {value3.Value} {value4.Value} {value5.Value} {value6.Value} {value7.Value}"
        scanf $"{value1} {value2} {value3} {value4} {value5} {value6} {value7}" text

    [<Benchmark>]
    member _.ManySmallValuesTextPreallocated() =
        scanf $"{value1} {value2} {value3} {value4} {value5} {value6} {value7}" manySmallValuesText

    [<Benchmark>]
    member _.ManySmallValuesFullyPreallocated() =
        scanf manySmallValuesFmt manySmallValuesText

    [<Benchmark>]
    member _.Realistic() =
        let value1 = ref "0"
        let value2 = ref 0
        scanf "/authorize myText 123" $"/authorize {value1} {value2}"

    [<Benchmark>]
    member _.RealisticPreallocated() =
        scanf realisticFmt "/authorize myText 123"

    [<Benchmark>]
    member _.RealisticCommand() =
        scanf "/authorize myText 123" $"/{cmd} {userName} {userCode}"

    [<Benchmark>]
    member _.RealisticCommandPreallocated() =
        scanf realisticCommandFmt "/authorize myText 123"

    [<Benchmark>]
    member _.RealisticCommandPreallocatedSpan() =
        scanfSpan realisticCommandFmt ("/authorize myText 123".AsSpan())

    [<Benchmark>]
    member _.PrimitivesOnlyPreallocated() =
        scanf primitivesOnlyFmt "123456 123456.123456"

    [<Benchmark>]
    member _.PrimitivesOnlyPreallocatedSpan() =
        scanfSpan primitivesOnlyFmt ("123456 123456.123456".AsSpan())