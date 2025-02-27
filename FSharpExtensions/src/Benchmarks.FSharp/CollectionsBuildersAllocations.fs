﻿module Benchmarks.CollectionsBuildersAllocations

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Jobs
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.ICollectionBuilder

[<
    MemoryDiagnoser;
    DisassemblyDiagnoser(filters = [||]);
    SimpleJob(RuntimeMoniker.Net60)
>]
type Benchmark() =

    member val Count = 10 with get, set

    member val Items = [ for i = 0 to 10 do i ] :> seq<_>

    [<Benchmark>]
    member this.RunResizeArrayBuilder() = ResizeArray() {
        1
        2
        3
        for i in this.Items do
            i
        let mutable i = this.Count
        while i > 0 do
            i
            i <- i - 1
        try
            i
        with e ->
            i
        try
            i
        finally
            ()
    }

    [<Benchmark>]
    member this.RunResizeArray() =
        let rsz = ResizeArray()

        rsz.Add(1)
        rsz.Add(2)
        rsz.Add(3)

        for i in this.Items do
            rsz.Add(i)
        let mutable i = this.Count
        while i > 0 do
            rsz.Add(i)
            i <- i - 1
        try
            rsz.Add(i)
        with e ->
            rsz.Add(i)
        try
            rsz.Add(i)
        finally
            ()
        rsz