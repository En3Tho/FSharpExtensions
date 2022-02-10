module Benchmarks.GenericEqualityObjectArrayBenchmark

open System
open BenchmarkDotNet.Attributes

type WootRecord = {
    Value1: string[]
    Value2: string[]
    Value3: string[]
}

let makeStringArray n = Array.init n (ignore >> Guid.NewGuid >> string)

let makeWootRecord() : WootRecord = {
    Value1 = makeStringArray 5
    Value2 = makeStringArray 10
    Value3 = makeStringArray 5
}

let customStringArrayEquality (array1: string[]) (array2: string[]) =
    let mutable result = array1.Length = array2.Length
    let mutable i = 0
    while result && i < array1.Length do
        result <- array1[i].Equals(array2[i])
        i <- i + 1
    result

let customIEquatableArrayEquality<'a when 'a :> IEquatable<'a>> (array1: 'a[]) (array2: 'a[]) =
    let mutable result = array1.Length = array2.Length
    let mutable i = 0
    while result && i < array1.Length do
        result <- array1[i].Equals(array2[i])
        i <- i + 1
    result

let customWootRecordEquality (woot1: WootRecord) (woot2: WootRecord) =
    customStringArrayEquality woot1.Value1 woot2.Value1
    && customStringArrayEquality woot1.Value2 woot2.Value2
    && customStringArrayEquality woot1.Value3 woot2.Value3

let customWootRecordIEquatableEquality (woot1: WootRecord) (woot2: WootRecord) =
    customIEquatableArrayEquality woot1.Value1 woot2.Value1
    && customIEquatableArrayEquality woot1.Value2 woot2.Value2
    && customIEquatableArrayEquality woot1.Value3 woot2.Value3

[<MemoryDiagnoser; DisassemblyDiagnoser>]
type Benchmark() =

    let woot1 = makeWootRecord()
    let woot2 = makeWootRecord()
    let woot1Copy = { { woot1 with Value1 = woot2.Value1 } with Value1 = woot1.Value1 }

    let longStringArray = makeStringArray 1000
    let longStringArrayCopy =
        let result = Array.copy longStringArray
        result[result.Length - 1] <- Guid.NewGuid().ToString()
        result

    [<Benchmark>]
    member x.TestAutoGeneratedEquality() =
        woot1 = woot2

    [<Benchmark>]
    member x.TestManualEquality() =
        customWootRecordEquality woot1 woot2

    [<Benchmark>]
    member x.TestIEquatableEquality() =
        customWootRecordIEquatableEquality woot1 woot2

    [<Benchmark>]
    member x.TestAutoGeneratedEqualityCopy() =
        woot1 = woot1Copy

    [<Benchmark>]
    member x.TestManualEqualityCopy() =
        customWootRecordEquality woot1 woot1Copy

    [<Benchmark>]
    member x.TestIEquatableEqualityCopy() =
        customWootRecordIEquatableEquality woot1 woot1Copy

    // arrays only

    [<Benchmark>]
    member x.TestAutoGeneratedArrayEquality() =
        woot1.Value1 = woot2.Value1

    [<Benchmark>]
    member x.TestManualArrayEquality() =
        customStringArrayEquality woot1.Value1 woot2.Value1

    [<Benchmark>]
    member x.TestIEquatableArrayEquality() =
        customIEquatableArrayEquality woot1.Value1 woot2.Value1

    [<Benchmark>]
    member x.TestAutoGeneratedArrayEqualityCopy() =
        woot1.Value1 = woot1Copy.Value1

    [<Benchmark>]
    member x.TestManualArrayEqualityCopy() =
        customStringArrayEquality woot1.Value1 woot1Copy.Value1

    [<Benchmark>]
    member x.TestIEquatableArrayEqualityCopy() =
        customIEquatableArrayEquality woot1.Value1 woot1Copy.Value1

    [<Benchmark>]
    member x.TestLongArrayAutoGenerated() =
        longStringArray = longStringArrayCopy

    [<Benchmark>]
    member x.TestLongArrayManual() =
        customStringArrayEquality longStringArray longStringArrayCopy