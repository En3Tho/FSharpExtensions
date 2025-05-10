module Benchmarks.GSeq

open BenchmarkDotNet.Jobs
open En3Tho.FSharp.Extensions
open BenchmarkDotNet.Attributes
open System.Linq

// module ActionTest =
//
//     let inline (!|>) ([<InlineIfLambda>] x: 'a -> 'b) ([<InlineIfLambda>] f: 'b -> 'c) = fun a -> f(x a)
//
//     let inline (<!>) a b c = ()
//
//     let expected (array) =
//         let terminalAction = ActionSeq.fold 0 ^ fun x y -> x + y
//
//
//         let qqq = ActionSeq.fromArray
//         let qqqq = ActionSeq.filter ^ fun x -> x % 2 <> 0
//
//         1 <!> 2 3
//
//         // let zxczxc = fun next -> qqq (qqqq next)
//         // let zxzz = qqqq !|> qqq
//
//         // let qq =
//         //     ActionSeq.fromArray
//         //     >> ActionSeq.filter ^ fun x -> x % 2 <> 0
//
//         //
//
//         Action.run (fromArray) (computation) (fold)
//
//         let zxczxc =
//             (ActionSeq.filter ^ fun x -> x % 2 <> 0
//             >> ActionSeq.skip 5
//             >> ActionSeq.map ^ fun x -> x + 15
//             <| ActionSeq.fold 0 ^ fun x y -> x + y)
//             |> ActionSeq.fromArray
//
//         zxczxc.Invoke(array)
//         let res = zxczxc.next.next.next.next.Result
//
//         // so everything becomes a function that takes "next"
//         let action =
//             (ActionSeq.fromArray
//             >> ActionSeq.filter ^ fun x -> x % 2 <> 0
//             >> ActionSeq.skip 5
//             >> ActionSeq.map ^ fun x -> x + 15)
//             <| terminalAction // termination stage, not iteration
//
//         action.Invoke(array)
//         action.next.next.next.next.Result
//
//     let pipe (array) =
//         let action =
//             (ActionSeq.fold 0 ^ fun x y -> x + y // termination stage, not iteration
//             |> ActionSeq.map ^ fun x -> x + 15
//             |> ActionSeq.skip 5
//             |> ActionSeq.filter ^ fun x -> x % 2 <> 0
//             |> ActionSeq.fromArray)
//
//         action.Invoke(array)
//         action.next.next.next.next.Result

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

    [<Benchmark>]
    member this.For2() =
        let mutable skip = 0
        let mutable result = 0
        let array = this.Array

        let mutable i = 0
        while skip < 5 && i < array.Length do
            let x = array[i]
            if x % 2 <> 0 then
                if skip < 5 then
                    skip <- skip + 1
            &i +<- 1

        while i < array.Length do
            let x = array[i]
            if x % 2 <> 0 then
                let x = x + 15
                result <- result + x
            &i +<- 1

        result