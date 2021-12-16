open System.Runtime.InteropServices
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Running
open Benchmarks.FSharp
open Benchmarks.FSharp.Lib

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

[<EntryPoint>]
let main argv =

    BenchmarkRunner.Run<
//                        FSharpOptimizer.Benchmark,
//                        FSharpOptimizerWithExperimentalPipe.Benchmark,
//                        PushStream.Benchmark,
                        PushStream.Benchmark2,
                        PushStream.Benchmark3
                        >() |> ignore
    0 // return an integer exit code