module Benchmarks.GSeq

open BenchmarkDotNet.Jobs
open En3Tho.FSharp.Extensions
open BenchmarkDotNet.Attributes
open System.Linq

[<MemoryDiagnoser; DisassemblyDiagnoser(filters = [||])>]
//[<Config(typeof<``Net8, Net9``>)>]
[<SimpleJob(RuntimeMoniker.Net90)>]
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

    [<Benchmark>]
    member this.ActionSeqFilterMapSkipFold() =
        let action =
            (ActionSeq.fold 0 ^ fun x y -> x + y // termination stage, not iteration
            |> ActionSeq.map ^ fun x -> x + 15
            |> ActionSeq.skip 5
            |> ActionSeq.filter ^ fun x -> x % 2 <> 0
            |> ActionSeq.fromArray)

        action.Invoke(this.Array)
        action.next.next.next.next.Result

    [<Benchmark>]
    member this.For() =
        let mutable skip = 0
        let mutable result = 0
        let array = this.Array
        for i = 0 to array.Length - 1 do
            let x = array[i]
            if x % 2 <> 0 then
                if skip < 5 then
                    skip <- skip + 1
                else
                    let x = x + 15
                    result <- result + x
        result