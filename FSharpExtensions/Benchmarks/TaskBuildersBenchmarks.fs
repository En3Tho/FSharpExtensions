module Benchmarks.TaskBuildersBenchmarks

open System.Threading.Tasks
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Jobs
open En3Tho.FSharp.ComputationExpressions.Tasks

[<
    MemoryDiagnoser;
    DisassemblyDiagnoser(filters = [||]);
    SimpleJob(RuntimeMoniker.Net60)
>]
type Benchmark() =
    let returnSomeValueAsTask x = Task.FromResult x
    let returnSomeValueAsValueTask x = ValueTask.FromResult x
    let returnSomeValueAsAsync x = async.Return x

    let runVTaskTaskAndAsyncAsTask() = task {
        let! x = returnSomeValueAsValueTask 5
        let! y = returnSomeValueAsTask 10
        let! z = returnSomeValueAsAsync 15
        return x + y
    }

    let runVTaskOnlyAsTask() = task {
        let! x = returnSomeValueAsValueTask 5
        let! y = returnSomeValueAsValueTask 10
        return x + y
    }

    let runVTaskOnlyAsVTask() = vtask {
        let! x = returnSomeValueAsValueTask 5
        let! y = returnSomeValueAsValueTask 10
        return x + y
    }

    let runVTaskAndTaskAsTask() = task {
        let! x = returnSomeValueAsValueTask 5
        let! y = returnSomeValueAsValueTask 10
        let! z = returnSomeValueAsTask 15
        return x + y + z
    }

    let runVTaskAndTaskAsVTask() = vtask {
        let! x = returnSomeValueAsValueTask 5
        let! y = returnSomeValueAsValueTask 10
        let! z = returnSomeValueAsTask 15
        return x + y + z
    }

    let runVTaskAndTaskAsUnitTask() = unittask {
        let! x = returnSomeValueAsValueTask 5
        let! y = returnSomeValueAsValueTask 10
        let! z = returnSomeValueAsTask 15
        return ()
    }

    let runVTaskAndTaskAsUnitVTask() = unitvtask {
        let! x = returnSomeValueAsValueTask 5
        let! y = returnSomeValueAsValueTask 10
        let! z = returnSomeValueAsTask 15
        return ()
    }

    [<Benchmark>]
    member _.RunVTaskOnlyAsTask() = runVTaskOnlyAsTask().Result

    [<Benchmark>]
    member _.RunVTaskOnlyAsVTask() = runVTaskOnlyAsVTask().Result

    [<Benchmark>]
    member _.RunVTaskAndTaskAsTask() = runVTaskAndTaskAsTask().Result

    [<Benchmark>]
    member _.RunVTaskAndTaskAsUnitTask() = runVTaskAndTaskAsUnitTask().GetAwaiter().GetResult()

    [<Benchmark>]
    member _.RunVTaskAndTaskAsUnitVTask() = runVTaskAndTaskAsUnitVTask().GetAwaiter().GetResult()

    [<Benchmark>]
    member _.RunVTaskTaskAndAsyncAsTask() = runVTaskTaskAndAsyncAsTask().Result