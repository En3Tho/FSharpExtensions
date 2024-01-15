module En3Tho.FSharp.ComputationExpressions.Tests.RepeatableTaskTests

open System.Threading.Tasks
open Xunit
open En3Tho.FSharp.ComputationExpressions.Tasks

[<Fact>]
let ``test that repeatable task doesn't start until awaited``() = task {
    let mutable counter = 0
    let lz = repeatableTask {
        counter <- 1
    }
    Assert.Equal(0, counter)
    do! lz
    Assert.Equal(1, counter)
}

[<Fact>]
let ``test that repeatable task repeats itself``() = task {
    let mutable counter = 0
    let lz = repeatableTask {
        do! Task.Delay(100)
        counter <- counter + 1
    }

    do! Parallel.ForAsync(0, 100, (fun i _ -> uvtask {
        do! lz
    }))

    Assert.Equal(100, counter)
}

[<Fact>]
let ``test that repeatable task correctly awaits multiple times``() = task {
    let mutable counter = 0
    let lz = repeatableTask {
        do! Task.Delay(100)
        counter <- counter + 1
        do! Task.Delay(100)
        counter <- counter + 1
    }
    Assert.Equal(0, counter)
    do! lz
    Assert.Equal(2, counter)
    do! lz
    Assert.Equal(4, counter)
}