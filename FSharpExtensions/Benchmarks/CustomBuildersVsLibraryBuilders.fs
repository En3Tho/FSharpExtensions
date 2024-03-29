module Benchmarks.CustomBuildersVsLibraryBuilders

open BenchmarkDotNet.Attributes
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.ArrayPoolBasedBuilders

[<
  MemoryDiagnoser;
  Config(typeof<``Net 6, Pgo``>)
>]
type Benchmark() =
    
    [<Params(10, 100, 1000, 10000)>]
    member val Count = 100 with get, set
    
    [<Benchmark>]
    member this.LibraryArray() = [|
        1
        2
        3
        for i = 0 to this.Count do
            i
        let mutable i = this.Count
        while i > 0 do
            i
            i <- i - 1
    |]
    
    [<Benchmark>]
    member this.LibraryList() = [
        1
        2
        3
        for i = 0 to this.Count do
            i
        let mutable i = this.Count
        while i > 0 do
            i
            i <- i - 1
    ]
    
    [<Benchmark>]
    member this.CustomArray() = arr {
        1
        2
        3
        for i = 0 to this.Count do
            i
        let mutable i = this.Count
        while i > 0 do
            i
            i <- i - 1
    }
    
    [<Benchmark>]
    member this.CustomResizeArray() = rsz {
        1
        2
        3
        for i = 0 to this.Count do
            i
        let mutable i = this.Count
        while i > 0 do
            i
            i <- i - 1
    }
    
    [<Benchmark>]
    member this.CustomImmutableArray() = block {
        1
        2
        3
        for i = 0 to this.Count do
            i
        let mutable i = this.Count
        while i > 0 do
            i
            i <- i - 1
    }