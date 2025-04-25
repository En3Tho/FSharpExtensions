module En3Tho.FSharp.Extensions.Tests.DeferTests

open System.Threading.Tasks
open Xunit
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.Tasks

[<Fact>]
let ``Test that defer properly works with both disposable and iasyncdisposable`` () = task {
    let mutable disposeCounter = 0

    do! task {
        let dispose = fun() -> &disposeCounter +<- 1
        let disposev = fun _ -> &disposeCounter +<- 1

        use _ = defer dispose
        use _ = (fun() -> &disposeCounter +<- 1) |> defer
        use _ = defer (fun() -> &disposeCounter +<- 1)
        use _ = defer(1, disposev)

        use _ = defer(fun() -> &disposeCounter +<- 1)
        use _ = defer(1, fun _ -> &disposeCounter +<- 1)
        use _ = defer(1, fun _ -> &disposeCounter +<- 1)

        let disposeAsync = fun() -> &disposeCounter +<- 1; ValueTask()
        let disposevAsync = fun _ -> &disposeCounter +<- 1; ValueTask()

        use _ = Defer.defer disposeAsync
        use _ = (fun() -> &disposeCounter +<- 1; ValueTask()) |> defer
        use _ = defer (fun() -> &disposeCounter +<- 1; ValueTask())
        use _ = defer(1, disposevAsync)
        use _ = defer(1, disposevAsync)
        ()
    }

    Assert.Equal(12, disposeCounter)
}

[<Fact>]
let ``Test that defer properly works with both disposable and iasyncdisposable2`` () = vtask {
    let mutable disposeCounter = 0

    do! task {
        let dispose = fun() -> &disposeCounter +<- 1
        let disposev = fun _ -> &disposeCounter +<- 1

        use _ = defer dispose
        use _ = (fun() -> &disposeCounter +<- 1) |> defer
        use _ = defer (fun() -> &disposeCounter +<- 1)
        use _ = defer(1, disposev)

        use _ = defer (fun() -> &disposeCounter +<- 1)
        use _ = defer(1, fun _ -> &disposeCounter +<- 1)
        use _ = defer(1,fun _ -> &disposeCounter +<- 1)

        let disposeAsync = fun() -> &disposeCounter +<- 1; ValueTask()
        let disposevAsync = fun _ -> &disposeCounter +<- 1; ValueTask()

        use _ = Defer.defer disposeAsync
        use _ = (fun() -> &disposeCounter +<- 1; ValueTask()) |> defer
        use _ = defer (fun() -> &disposeCounter +<- 1; ValueTask())
        use _ = defer(1, disposevAsync)
        use _ = defer(1, disposevAsync)
        ()
    }

    Assert.Equal(12, disposeCounter)
}