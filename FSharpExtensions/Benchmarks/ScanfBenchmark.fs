module Benchmarks.ScanfBenchmark

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Jobs
open En3Tho.FSharp.Extensions.Scanf

[<
    MemoryDiagnoser;
    DisassemblyDiagnoser;
    SimpleJob(RuntimeMoniker.Net60)
>]
type Benchmark() =

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
        scanf text $"{value1} {value2} {value3} {value4} {value5} {value6} {value7}"

    [<Benchmark>]
    member _.ManySmallValuesTextPreallocated() =
        scanf manySmallValuesText $"{value1} {value2} {value3} {value4} {value5} {value6} {value7}"

    [<Benchmark>]
    member _.ManySmallValuesFullyPreallocated() =
        scanf manySmallValuesText manySmallValuesFmt

    [<Benchmark>]
    member _.Realistic() =
        let value1 = ref "0"
        let value2 = ref 0
        scanf "/authorize myText 123" $"/authorize {value1} {value2}"

    [<Benchmark>]
    member _.RealisticPreallocated() =
        scanf "/authorize myText 123" realisticFmt