module En3Tho.FSharp.Extensions.Tests.DeferTests

open System.Threading.Tasks
open Xunit
open En3Tho.FSharp.Extensions

[<Fact>]
let ``Test that defer properly works with both disposable and iasyncdisposable`` () = task {
    let mutable disposeCounter = 0

    do! task {
        let dispose = fun() -> &disposeCounter +<- 1
        let disposev = fun _ -> &disposeCounter +<- 1

        use _ = defer dispose
        use _ = (fun() -> &disposeCounter +<- 1) |> defer
        use _ = defer (fun() -> &disposeCounter +<- 1)
        use _ = 1 |> deferv disposev

        use _ = defer (fun() -> &disposeCounter +<- 1)
        use _ = 1 |> deferv (fun _ -> &disposeCounter +<- 1)
        use _ = deferv (fun _ -> &disposeCounter +<- 1) 1

        let disposeAsync = fun() -> &disposeCounter +<- 1; ValueTask()
        let disposevAsync = fun _ -> &disposeCounter +<- 1; ValueTask()

        use _ = defer disposeAsync
        use _ = (fun() -> &disposeCounter +<- 1; ValueTask()) |> defer
        use _ = defer (fun() -> &disposeCounter +<- 1; ValueTask())
        use _ = 1 |> deferv disposevAsync
        use _ = deferv disposevAsync 1
        ()
    }

    Assert.Equal(12, disposeCounter)
}