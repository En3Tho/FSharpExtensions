module En3Tho.FSharp.ComputationExpressions.Tests.LazyTaskTests

open System.Threading.Tasks
open Xunit
open En3Tho.FSharp.ComputationExpressions.Tasks

[<Fact>]
let ``test that lazy task doesn't start until awaited``() = task {
    let mutable counter = 0
    let lz = lazyTask {
        counter <- 1
    }
    Assert.Equal(0, counter)
    do! lz
    Assert.Equal(1, counter)

    do! lz
    Assert.Equal(1, counter)
}

[<Fact>]
let ``test that lazy task doesn't execute move next multiple times``() = task {
    let mutable counter = 0
    let lz = lazyTask {
        do! Task.Delay(100)
        counter <- counter + 1
    }

    do! Parallel.ForAsync(0, 100, (fun i _ -> uvtask {
        do! lz
    }))

    Assert.Equal(1, counter)
}

[<Fact>]
let ``test that lazy task correctly awaits multiple times``() = task {
    let mutable counter = 0
    let lz = lazyTask {
        do! Task.Delay(100)
        counter <- 1
        do! Task.Delay(100)
        counter <- 2
    }
    Assert.Equal(0, counter)
    do! lz
    Assert.Equal(2, counter)
}