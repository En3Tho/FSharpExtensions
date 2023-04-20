module En3Tho.FSharp.Extensions.Tests.GSeq

open Xunit
open En3Tho.FSharp.Extensions

[<Fact>]
let ``ofArrayRev``() =
    [| 1; 2; 3 |]
    |> GSeq.ofArrayRev
    |> GSeq.toSeq
    |> fun rev -> Assert.True(rev == [| 3; 2; 1 |])

[<Fact>]
let ``filter skip map fold``() =
    [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |]
    |> GSeq.ofArray
    |> GSeq.filter ^ fun x -> x % 2 <> 0
    |> GSeq.skip 5
    |> GSeq.map ^ fun x -> x + 15
    |> GSeq.fold 0 ^ fun x y -> x + y
    |> fun result -> Assert.Equal(result, 26)