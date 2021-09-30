open BenchmarkDotNet.Running
open Benchmarks

[<EntryPoint>]
let main argv =
    BenchmarkRunner.Run<CustomBuildersVsLibraryBuilders.Benchmark>() |> ignore
    0 // return an integer exit code