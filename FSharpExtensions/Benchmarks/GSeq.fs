module Benchmarks.GSeq

open En3Tho.FSharp.Extensions
open BenchmarkDotNet.Attributes
open System.Linq

[<MemoryDiagnoser; DisassemblyDiagnoser(filters = [||])>]
[<Config(typeof<``Net8, Net9``>)>]
type Benchmark() =

    [<Params(1000)>]
    member val Count = 0 with get, set

    member val Array = [||] with get, set

    [<GlobalSetup>]
    member this.GlobalSetup() =
        this.Array <- Array.init this.Count id

    [<Benchmark>]
    member this.IEnumerableFilterMapSkipFold() =
        this.Array
            .Where(fun x -> x % 2 <> 0)
            .Skip(5)
            .Select(fun x -> x + 15)
            .Aggregate(0, (fun x y -> x + y))

    [<Benchmark>]
    member this.SeqFilterMapSkipFold() =
        this.Array
        |> Seq.filter ^ fun x -> x % 2 <> 0
        |> Seq.skip 5
        |> Seq.map ^ fun x -> x + 15
        |> Seq.fold (fun x y -> x + y) 0

    [<Benchmark>]
    member this.GSeqFilterMapSkipFold() =
        this.Array
        |> GSeq.ofArray
        |> GSeq.filter ^ fun x -> x % 2 <> 0
        |> GSeq.skip 5
        |> GSeq.map ^ fun x -> x + 15
        |> GSeq.fold 0 ^ fun x y -> x + y