module En3Tho.FSharp.Extensions.Tests.DeferTests

open System
open System.Threading.Tasks
open En3Tho.FSharp.Extensions.Disposables
open Xunit
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.Extensions.Byref.Operators

let deferx f = new UnitDisposable(f)

[<Fact>]
let ``Test that defer properly works with both disposable and iasyncdisposable`` () = task {
    let mutable disposeCounter = 0

    do! task {
        let dispose = fun() -> &disposeCounter +<- 1
        let disposev = fun _ -> &disposeCounter +<- 1

        use _ = defer dispose
        use _ = (fun() -> &disposeCounter +<- 1) |> defer
        use _ = 1 |> deferv disposev

        //use _ = defer (fun() -> &disposeCounter +<- 1)
        //use _ = 1 |> deferv (fun _ -> &disposeCounter +<- 1)

        let disposeAsync = fun() -> &disposeCounter +<- 1; ValueTask()
        let disposevAsync = fun _ -> &disposeCounter +<- 1; ValueTask()

        use _ = defer disposeAsync
        use _ = (fun() -> &disposeCounter +<- 1; ValueTask()) |> defer
        use _ = 1 |> deferv disposevAsync
        ()
    }

    Assert.Equal(6, disposeCounter)
}