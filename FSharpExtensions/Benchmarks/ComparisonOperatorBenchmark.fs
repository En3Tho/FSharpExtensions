module Benchmarks.ComparisonOperatorBenchmark

open System
open BenchmarkDotNet.Attributes

// TODO: benchmark more operators to find allocating ones

type [<Struct>] CustomRecordType = {
    V1: string
    V2: int
    V3: int64
}

let inline compareReferences<'a when 'a: not struct> (left: 'a) (right: 'a) =
    match box left, box right with
    | null, null -> 0
    | _, null -> -1
    | null, _ -> 1
    | _ -> 0

let inline compare<'a when 'a :> IComparable<'a>> (left: 'a) (right: 'a) = left.CompareTo(right)

let inline compareArrays<'a when 'a :> IComparable<'a>> (left: 'a[]) (right: 'a[]) =
    match compareReferences left right with
    | 0 ->
        match left.Length.CompareTo(right.Length) with
        | 0 ->
            let rec go index =
                if uint index < uint left.Length then
                    match left[index].CompareTo(right[index]) with
                    | 0 ->
                        go (index + 1)
                    | result ->
                        result
                 else
                     0
            go 0
        | result ->
            result
    | result ->
            result

let inline (<.) left right = compare left right = -1
let inline (<..) left right = compareArrays left right = -1
let inline (<=.) left right = compare left right <= 0
let inline (<=..) left right = compareArrays left right <= 0


let inline (>.) left right = compare left right = 1
let inline (>=.) left right = compare left right >= 0

[<MemoryDiagnoser>]
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

    let values1 = [| value1; value1; value1; value1; value1; |]
    let values2 = [| value1; value1; value1; value1; value2; |]

    [<Benchmark>]
    member _.CompareCustomTypesUsingCustomOperator() = value1 <. value2

    [<Benchmark>]
    member _.CompareCustomTypesUsingNativeOperator() = value1 < value2

    [<Benchmark>]
    member _.CompareCustomArrayTypesUsingCustomOperator() = values1 <.. values2

    [<Benchmark>]
    member _.CompareCustomArrayTypesUsingNativeOperator() = value1 < value2

    [<Benchmark>]
    member _.CompareTimeSpanUsingCustomOperator() = TimeSpan() <. TimeSpan()

    [<Benchmark>]
    member _.CompareTimeSpanUsingNativeOperator() = TimeSpan() < TimeSpan()