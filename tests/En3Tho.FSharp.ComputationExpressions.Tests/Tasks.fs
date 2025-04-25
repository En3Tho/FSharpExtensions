module En3Tho.FSharp.Extensions.Tests.Tasks

open System
open System.Threading.Tasks
open Xunit
open En3Tho.FSharp.ComputationExpressions.Tasks

[<Fact>]
let ``Test array map does not throw with value task CE``() = vtask {
    let w = [| 1 |] |> Array.map (fun w -> w + 1) |> Array.head
    let! x = task { return 3 }
    let! y = vtask { return 4 }
    let finalResult = w + x + y + 3
    return Assert.Equal(11, finalResult)
}

[<Fact>]
let ``Test that non generic value task and task work properly with utask CE``() = utask {
    do! Task.CompletedTask
    do! ValueTask()
}

[<Fact>]
let ``Test that non generic value task and task work properly with uvtask CE``() = uvtask {
    do! Task.CompletedTask
    do! ValueTask()
}

[<Fact>]
let ``Test that async disposable works properly with uvtask CE``() = uvtask {
    use _ = { new IAsyncDisposable with member _.DisposeAsync() = ValueTask() }
    ()
}

[<Fact>]
let ``Test that async disposable works properly with vtask CE``() = vtask {
    use _ = { new IAsyncDisposable with member _.DisposeAsync() = ValueTask() }
    return ()
}

[<Fact>]
let ``Test that async disposable works properly with utask CE``() = utask {
    use _ = { new IAsyncDisposable with member _.DisposeAsync() = ValueTask() }
    ()
}

[<Fact>]
let ``Test that dispose works with utask CE``() =
    Assert.ThrowsAsync<ObjectDisposedException>(fun () -> task { return! utask {
        use _ = { new IDisposable with member _.Dispose() = raise (ObjectDisposedException("")) }
        ()
    }})

[<Fact>]
let ``Test that dispose works with uvtask CE``() =
    Assert.ThrowsAsync<ObjectDisposedException>(fun () -> task { return! uvtask {
        use _ = { new IDisposable with member _.Dispose() = raise (ObjectDisposedException("")) }
        ()
    }})