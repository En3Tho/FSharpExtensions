open BenchmarkDotNet.Running
open Benchmarks

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<TaskBuildersBenchmarks.Benchmark>() |> ignore
    0 // return an integer exit code