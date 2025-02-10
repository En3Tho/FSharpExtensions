module Benchmarks.MathematicalOperatorsBenchmarks

open System
open En3Tho.FSharp.Extensions
open BenchmarkDotNet.Attributes

[<MemoryDiagnoser>]
type Benchmark() =

    [<Params(5)>]
    member val IntValue1 = 0L with get, set
    member val TimeSpanValue1 = TimeSpan() with get, set
    member val DateTimeValue1 = DateTime() with get, set

    member val IntValue2 = 0L with get, set
    member val TimeSpanValue2 = TimeSpan() with get, set
    member val DateTimeValue2 = DateTime() with get, set

    member this.GlobalSetup() =
        this.TimeSpanValue1 <- TimeSpan.FromMinutes(this.IntValue1.f64)
        this.DateTimeValue1 <- DateTime(this.IntValue1)

        this.IntValue2 <- this.IntValue1
        this.TimeSpanValue2 <- TimeSpan.FromMinutes(this.IntValue1.f64)
        this.DateTimeValue2 <- DateTime(this.IntValue1)

    [<Benchmark>]
    member this.TestInt() = this.IntValue1 - this.IntValue2

    [<Benchmark>]
    member this.TimeSpan() = this.TimeSpanValue1 - this.TimeSpanValue2

    [<Benchmark>]
    member this.DateTime() = this.DateTimeValue1 - this.DateTimeValue2