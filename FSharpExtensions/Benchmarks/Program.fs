open System
open System.Runtime.InteropServices
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Running
open Benchmarks
open Benchmarks.FSharp

BenchmarkRunner.Run<
    // GenericEqualityBenchmark.Benchmark,
    // NodeCode.ReturnBenchmark,
    // FSharpOptimizer.Benchmark,
    // FSharpOptimizerWithExperimentalPipe.Benchmark,
    GenericTaskBuilder.AsyncSeqBenchmark
    >() |> ignore