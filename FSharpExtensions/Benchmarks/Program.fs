open BenchmarkDotNet.Running
open Benchmarks

open En3Tho.FSharp.ComputationExpressions.Tasks

[<EntryPoint>]
let main argv =

    BenchmarkRunner.Run<TaskBuildersBenchmarks.Benchmark>() |> ignore
    0 // return an integer exit code