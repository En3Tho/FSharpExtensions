module Benchmarks.FSharp.FSharpOptimizerWithExperimentalPipe

open System
open BenchmarkDotNet.Attributes
open En3Tho.FSharp.Extensions

module Printf =
    type T = T with
        static member inline ($) (T, arg : unit) = ()
        static member inline ($) (T, arg : int) = 0 // mandatory second terminal case; is unused in runtime but is required for the code to compile
        static member inline ($) (T, func : ^a -> ^b) : ^a -> ^b = fun (_ : 'a) -> T $ Unchecked.defaultof<'b>

    let inline iprintf format : 'a =
        if false then // for type inference
            printf format
        else
            T $ Unchecked.defaultof<'a>

let printIfOdd num = if num % 2 = 0 then Printf.iprintf "%i" num

let printOnlyOdd =
    Array.filter ((<>) 0)
    >> Array.iter printIfOdd

let printOnlyOddUnrolled arr =
    arr
    |> Array.filter ((<>) 0)
    |> Array.iter printIfOdd

let inline toString obj = obj.ToString()

let callGuidParseToString2 (str : string) = str |> (Guid.Parse >> toString)
let callGuidParseToString2Unrolled (str : string) = str |> Guid.Parse |> toString

let callGuidParseToString4 (str : string) =
    str
    |> (Guid.Parse >> toString >> Guid.Parse >> toString)

let callGuidParseToString4Unrolled (str : string) =
    str
    |> Guid.Parse
    |> toString
    |> Guid.Parse
    |> toString
    |> fun x -> Guid.Parse x
    |> fun x -> toString x

let plus1 x = x + 1

let callSeq2<'a> = Seq.map plus1 >> Seq.map plus1
let callSeq2Unrolled<'a> values = values |> Seq.map plus1 |> Seq.map plus1

let callSeq3<'a> =
    Seq.map plus1 >> Seq.map plus1 >> Seq.map plus1

let callSeq3Unrolled<'a> values =
    values
    |> Seq.map plus1
    |> fun x -> Seq.map plus1 x
    |> Seq.map plus1

let callSeq4<'a> =
    Seq.map plus1
    >> Seq.map plus1
    >> Seq.map plus1
    >> Seq.map plus1

let callSeq4Unrolled<'a> values =
    values
    |> Seq.map plus1
    |> fun x -> Seq.map plus1 x
    |> Seq.map plus1
    |> fun x -> Seq.map plus1 x

let callSeq2InPlace num =
    Seq.init num id
    |> (Seq.map plus1 >> Seq.map plus1)

let callSeq2InPlaceUnrolled num =
    Seq.init num id |> Seq.map plus1 |> Seq.map plus1

let callSeq3InPlace num =
    Seq.init num id
    |> (Seq.map plus1 >> Seq.map plus1 >> Seq.map plus1)

let callSeq3InPlaceUnrolled num =
    Seq.init num id
    |> Seq.map plus1
    |> Seq.map plus1
    |> Seq.map plus1

let callSeq4InPlace num =
    Seq.init num id
    |> (Seq.map plus1
        >> Seq.map plus1
        >> Seq.map plus1
        >> Seq.map plus1)

let callSeq4InPlaceUnrolled num =
    Seq.init num id
    |> Seq.map plus1
    |> fun x -> Seq.map plus1 x
    |> Seq.map plus1
    |> fun x -> Seq.map plus1 x

[<MemoryDiagnoser; DisassemblyDiagnoser>]
type Benchmark() =

    [<Params(10)>]
    member val Count = 0 with get, set

    [<Benchmark>]
    member _.PrintOnlyOdd() = [| 1; 2; 3; 4; 5 |] |> printOnlyOdd

    [<Benchmark>]
    member _.PrintOnlyOddUnrolled() =
        [| 1; 2; 3; 4; 5 |] |> printOnlyOddUnrolled

    [<Benchmark>]
    member _.CallGuidParseToString2() =
        Guid.NewGuid().ToString()
        |> callGuidParseToString2

    [<Benchmark>]
    member _.CallGuidParseToString2Unrolled() =
        Guid.NewGuid().ToString()
        |> callGuidParseToString2Unrolled

    [<Benchmark>]
    member _.CallGuidParseToString4() =
        Guid.NewGuid().ToString()
        |> callGuidParseToString4

    [<Benchmark>]
    member _.CallGuidParseToString4Unrolled() =
        Guid.NewGuid().ToString()
        |> callGuidParseToString2Unrolled

    [<Benchmark>]
    member this.CallSeq2() =
        Seq.init this.Count id
        |> callSeq2
        |> Seq.iter ignore

    [<Benchmark>]
    member this.CallSeq2Unrolled() =
        Seq.init this.Count id
        |> callSeq2Unrolled
        |> fun x -> Seq.iter ignore x

    [<Benchmark>]
    member this.CallSeq3() =
        Seq.init this.Count id
        |> callSeq3
        |> fun x -> Seq.iter ignore x

    [<Benchmark>]
    member this.CallSeq3Unrolled() =
        Seq.init this.Count id
        |> callSeq3Unrolled
        |> Seq.iter ignore

    [<Benchmark>]
    member this.CallSeq4() =
        Seq.init this.Count id
        |> callSeq4
        |> Seq.iter ignore

    [<Benchmark>]
    member this.CallSeq4Unrolled() =
        Seq.init this.Count id
        |> callSeq4Unrolled
        |> fun x -> Seq.iter ignore x

    [<Benchmark>]
    member this.CallSeq2InPlace() =
        callSeq3InPlace this.Count |> Seq.iter ignore

    [<Benchmark>]
    member this.CallSeq2InPlaceUnrolled() =
        callSeq3InPlaceUnrolled this.Count
        |> Seq.iter ignore

    [<Benchmark>]
    member this.CallSeq3InPlace() =
        callSeq4InPlace this.Count |> Seq.iter ignore

    [<Benchmark>]
    member this.CallSeq3InPlaceUnrolled() =
        callSeq4InPlaceUnrolled this.Count
        |> Seq.iter ignore
