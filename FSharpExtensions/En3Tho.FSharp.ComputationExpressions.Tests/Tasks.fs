module En3Tho.FSharp.Extensions.Tests.Tasks

open System
open System.Threading.Tasks
open Xunit
open En3Tho.FSharp.ComputationExpressions.Tasks

[<Fact>]
let ``Test array map does not throw with value task CE``() = vtask {
    let w = [| 1 |] |> Array.map (fun w -> w + 1) |> Array.head
    let! x = task { return 3 }
    let finalResult = w + x + 3
    Assert.Equal(7, finalResult)
}

[<Fact>]
let ``Test that non generic value task and task work properly with unittask CE``() = unittask {
    do! Task.CompletedTask
    do! ValueTask()
}

[<Fact>]
let ``Test that non generic value task and task work properly with unitvtask CE``() = unitvtask {
    do! Task.CompletedTask
    do! ValueTask()
}

[<Fact>]
let ``Test that async disposable works properly with unitvtask CE``() = unitvtask {
    use _ = { new IAsyncDisposable with member _.DisposeAsync() = ValueTask() }
    ()
}

[<Fact>]
let ``Test that async disposable works properly with vtask CE``() = vtask {
    use _ = { new IAsyncDisposable with member _.DisposeAsync() = ValueTask() }
    ()
}

[<Fact>]
let ``Test that async disposable works properly with unittask CE``() = unittask {
    use _ = { new IAsyncDisposable with member _.DisposeAsync() = ValueTask() }
    ()
}

[<Fact>]
let ``Test that dispose works with unittask CE``() =
    Assert.ThrowsAsync<ObjectDisposedException>(fun () -> task { return! unittask {
        use _ = { new IDisposable with member _.Dispose() = raise (ObjectDisposedException("")) }
        ()
    }})

[<Fact>]
let ``Test that dispose works with unitvtask CE``() =
    Assert.ThrowsAsync<ObjectDisposedException>(fun () -> task { return! unitvtask {
        use _ = { new IDisposable with member _.Dispose() = raise (ObjectDisposedException("")) }
        ()
    }})