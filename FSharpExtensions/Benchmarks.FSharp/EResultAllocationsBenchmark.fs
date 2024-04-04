module Benchmarks.EResultAllocationsBenchmark

open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Jobs
open En3Tho.FSharp.ComputationExpressions.ResultBuilder


[<
    MemoryDiagnoser;
    DisassemblyDiagnoser(filters = [||]);
    SimpleJob(RuntimeMoniker.Net60)
>]
type Benchmark() =
    
    let exnInstance = Exception()
    
    [<Benchmark>]
    member _.EResultManual() =
        match Error exnInstance, Error exnInstance with
        | Ok q, Ok w -> Ok (q + w)
        | Error exn, _
        | _, Error exn -> Error exn

    [<Benchmark>]
    member _.EResultSimple() = eresult {
        let! q = Error exnInstance
        let! w = Error exnInstance
        return q + w
    }

    [<Benchmark>]
    member _.EResultMultiManual2() =
       let q = Error exnInstance
       let w = Error exnInstance
       match q, w with
       | Ok q, Ok w ->
          Ok (q + w)
       | Error exn1, Error exn2 ->
         Error (AggregateException(exn1, exn2))
       | Error exn, _
       | _, Error exn ->
          Error (AggregateException(exn))

    [<Benchmark>]
    member _.EResultMulti2() = eresult {
       let! q = Error exnInstance
       and! w = Error exnInstance
       return q + w
    }

    [<Benchmark>]
    member _.EResultMultiManual3() =
       let q = Error exnInstance
       let w = Error exnInstance
       let e = Error exnInstance
       match q, w, e with
       | Ok q, Ok w, Ok e -> Ok (q + w + e)
       | _ ->
          [
             match q with Error exn -> exn | _ -> ()
             match w with Error exn -> exn | _ -> ()
             match e with Error exn -> exn | _ -> ()
          ] |> AggregateException :> exn |> Error

    [<Benchmark>]
    member _.EResultMulti3() = eresult {
       let! q = Error exnInstance
       and! w = Error exnInstance
       and! e = Error exnInstance
       return q + w + e
    }

    [<Benchmark>]
    member _.EResultMulti4() = eresult {
       let! q = Error exnInstance
       and! w = Error exnInstance
       and! e = Error exnInstance
       and! r = Error exnInstance
       return q + w + e + r
    }

    [<Benchmark>]
    member _.EResultMulti5() = eresult {
       let! q = Error exnInstance
       and! w = Error exnInstance
       and! e = Error exnInstance
       and! r = Error exnInstance
       and! t = Error exnInstance
       return q + w + e + r + t
    }

     [<Benchmark>]
     member _.EResultMulti6() = eresult {
        let! q = Error exnInstance
        and! w = Error exnInstance
        and! e = Error exnInstance
        and! r = Error exnInstance
        and! t = Error exnInstance
        and! y = Error exnInstance
        return q + w + e + r + t + y
    }

    [<Benchmark>]
    member _.EResultMulti7() = eresult {
       let! q = Error exnInstance
       and! w = Error exnInstance
       and! e = Error exnInstance
       and! r = Error exnInstance
       and! t = Error exnInstance
       and! y = Error exnInstance
       and! u = Error exnInstance
       return q + w + e + r + t + y + u
    }