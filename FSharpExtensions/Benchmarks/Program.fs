open BenchmarkDotNet.Running
open Benchmarks

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<ScanfBenchmark.Benchmark>() |> ignore
    0 // return an integer exit code