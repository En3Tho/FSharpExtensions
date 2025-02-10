[<AutoOpen>]
module Benchmarks.BenchmarkRunner

open System.Runtime.InteropServices
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Running

type BenchmarkRunner with
    static member Run<'T1, 'T2>([<Optional; DefaultParameterValue(null: IConfig)>] config: IConfig) =
        BenchmarkRunner.Run([|
            BenchmarkConverter.TypeToBenchmarks(typeof<'T1>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T2>, config)
        |])

    static member Run<'T1, 'T2, 'T3>([<Optional; DefaultParameterValue(null: IConfig)>] config: IConfig) =
        BenchmarkRunner.Run([|
            BenchmarkConverter.TypeToBenchmarks(typeof<'T1>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T2>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T3>, config)
        |])

    static member Run<'T1, 'T2, 'T3, 'T4>([<Optional; DefaultParameterValue(null: IConfig)>] config: IConfig) =
        BenchmarkRunner.Run([|
            BenchmarkConverter.TypeToBenchmarks(typeof<'T1>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T2>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T3>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T4>, config)
        |])

    static member Run<'T1, 'T2, 'T3, 'T4, 'T5>([<Optional; DefaultParameterValue(null: IConfig)>] config: IConfig) =
        BenchmarkRunner.Run([|
            BenchmarkConverter.TypeToBenchmarks(typeof<'T1>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T2>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T3>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T4>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T5>, config)
        |])

    static member Run<'T1, 'T2, 'T3, 'T4, 'T5, 'T6>([<Optional; DefaultParameterValue(null: IConfig)>] config: IConfig) =
        BenchmarkRunner.Run([|
            BenchmarkConverter.TypeToBenchmarks(typeof<'T1>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T2>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T3>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T4>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T5>, config)
            BenchmarkConverter.TypeToBenchmarks(typeof<'T6>, config)
        |])