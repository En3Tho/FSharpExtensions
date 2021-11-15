open System.Threading.Tasks
open BenchmarkDotNet.Running
open Benchmarks

open En3Tho.FSharp.ComputationExpressions.Tasks
open En3Tho.FSharp.Extensions

[<EntryPoint>]
let main argv =

    let opt3 = voptionvtask {

        let mutable i = 0

        let! z = ValueTask.FromResult 15
        let! qwe = Some 15 |> ValueTask.FromResult

        let! qwer = Some 25

        while i < 10 do
            printfn "LoopStart"
            i <- i + 1
            //let! result = ValueSome i |> ValueTask.FromResult
            let! result = ValueNone |> ValueTask.FromResult
            printfn $"{result}"
            printfn "LoopNext"

        let! x = ValueSome 1 |> ValueTask.FromResult
        let! y = ValueSome 2 |> ValueTask.FromResult
        return x + y + i
    }

    opt3 |> ValueTask.RunSynchronously |> ignore

    //BenchmarkRunner.Run<TaskBuildersBenchmarks.Benchmark>() |> ignore
    0 // return an integer exit code