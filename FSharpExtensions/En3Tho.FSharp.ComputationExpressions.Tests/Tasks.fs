module En3Tho.FSharp.Extensions.Tests.Tasks

open System
open Xunit
open En3Tho.FSharp.ComputationExpressions.Tasks

[<Fact>]
let ``Test array map throws with task CE now``() =
    Assert.ThrowsAsync<NullReferenceException>(fun() -> task {
        let w = [| 1 |] |> Array.map (fun w -> w + 1) |> Array.head
        let! x = task { return 3 }
        let finalResult = w + x + 3
        Assert.Equal(7, finalResult)
    })

[<Fact>]
let ``Test array map does not throw with value task CE``() = vtask {
    let w = [| 1 |] |> Array.map (fun w -> w + 1) |> Array.head
    let! x = task { return 3 }
    let finalResult = w + x + 3
    Assert.Equal(7, finalResult)
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