open BenchmarkDotNet.Running
open Benchmarks

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<EResultAllocationsBenchmark.Benchmark>() |> ignore
    0 // return an integer exit code