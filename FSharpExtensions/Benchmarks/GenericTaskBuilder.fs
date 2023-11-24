module Benchmarks.GenericTaskBuilder

open System.Collections.Generic
open System.Threading.Tasks
open BenchmarkDotNet.Attributes
open En3Tho.FSharp.ComputationExpressions.Tasks
open En3Tho.FSharp.Extensions

let task2 = En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.Native.TaskBuilder()

type MapAsyncEnumerableEnumerator<'a, 'b>(source1: IAsyncEnumerable<'a>, map: 'a -> 'b) =
    let enumerator = source1.GetAsyncEnumerator()

    interface IAsyncEnumerator<'b> with
        member this.Current = enumerator.Current |> map
        member this.DisposeAsync() = enumerator.DisposeAsync()
        member this.MoveNextAsync() = enumerator.MoveNextAsync()

module AsyncEnumerable =
    let map map source = { new IAsyncEnumerable<'b> with member _.GetAsyncEnumerator(_) = MapAsyncEnumerableEnumerator<'a, 'b>(source, map) }

    let init v = taskSeq {
        for i in 1 .. v do
            do! Task.Yield()
            yield i
    }

[<MemoryDiagnoser; DisassemblyDiagnoser(filters = [||])>]
[<SimpleJob>]
type Benchmark() =

    [<Benchmark>]
    member _.WaitTask() = task {
        do! Task.Yield()
    }

    [<Benchmark>]
    member _.WaitTask2() = task2 {
        do! Task.Yield()
    }

[<MemoryDiagnoser; DisassemblyDiagnoser(filters = [||])>]
[<SimpleJob>]
type AsyncSeqBenchmark() =

    [<Benchmark>]
    member _.Direct() = task2 {
        for v in AsyncEnumerable.init 10 do
            string v |> ignore
    }

    [<Benchmark>]
    member _.WihtoutGseqMap() = task2 {
        let values =
            AsyncEnumerable.init 10
            |> AsyncEnumerable.map string

        for _ in values do ()
    }

    [<Benchmark>]
    member _.WithGSeqMap() = task2 { // slower and allocates more for single map case. TODO: more?
        let values =
            AsyncEnumerable.init 10
            |> GSeq.withAsyncSeq (GSeq.map string)

        for _ in values do ()
    }