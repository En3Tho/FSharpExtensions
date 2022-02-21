module Benchmarks.IEquatableEqualityOperatorBenchmark

open BenchmarkDotNet.Attributes
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.ICollectionBuilder

type [<Struct>] CustomRecordType = {
    V1: string
    V2: int
    V3: int64
}

[<MemoryDiagnoser; DisassemblyDiagnoser>]
type Benchmark() =

    let value1 = {
        V1 = "test"
        V2 = 10
        V3 = 10
    }

    let value2 = {
        V1 = "test"
        V2 = 10
        V3 = 11
    }

    let mkSeq1 count =
        Seq.init count (fun _ -> value1)

    let mkSeq2 count =
        let left = Seq.init (count - 1) (fun _ -> value1)
        let right = seq { value2 }
        Seq.append left right

    let array1 = mkSeq1 100 |> Seq.toArray
    let array2 = mkSeq2 100 |> Seq.toArray
    let list1 = mkSeq1 100 |> Seq.toList
    let list2 = mkSeq2 100 |> Seq.toList
    let sequence1 = array1 :> seq<_>
    let sequence2 = array2 :> seq<_>
    let resizeArray1 = ResizeArray() { yield! mkSeq1 100 }
    let resizeArray2 = ResizeArray() { yield! mkSeq2 100 }

    [<Benchmark>]
    member _.TestValues() = value1 == value2

    [<Benchmark>]
    member _.TestArrays() = array1 == array2

    [<Benchmark>]
    member _.TestLists() = list1 == list2

    [<Benchmark>]
    member _.TestSequences() = sequence1 == sequence2

    [<Benchmark>]
    member _.TestResizeArrays() = resizeArray1 == resizeArray2

    [<Benchmark>]
    member _.TestValuesUsingDefaultEqualityOperator() = value1 = value2

    [<Benchmark>]
    member _.TestArraysUsingDefaultEqualityOperator() = array1 = array2

    [<Benchmark>]
    member _.TestListsUsingDefaultEqualityOperator() = list1 = list2

    [<Benchmark>]
    member _.TestSequencesUsingDefaultEqualityOperator() = sequence1 = sequence2

    [<Benchmark>]
    member _.TestResizeArraysUsingDefaultEqualityOperator() = resizeArray1 = resizeArray2