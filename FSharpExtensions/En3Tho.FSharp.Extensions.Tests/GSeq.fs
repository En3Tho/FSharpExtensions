module En3Tho.FSharp.Extensions.Tests.GSeq

open Xunit
open En3Tho.FSharp.Extensions

let filterSkipMapFoldAssert gSeq =
    gSeq
    |> GSeq.filter ^ fun x -> x % 2 <> 0
    |> GSeq.skip 5
    |> GSeq.map ^ fun x -> x + 15
    |> GSeq.fold 0 ^ fun x y -> x + y
    |> fun result -> Assert.Equal(result, 26)

[<Fact>]
let ``ofArrayRev``() =
    [| 1; 2; 3 |]
    |> GSeq.ofArrayRev
    |> GSeq.toSeq
    |> fun rev -> Assert.True(rev === [| 3; 2; 1 |])

[<Fact>]
let ``filter skip map fold``() =
    [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |]
    |> GSeq.ofArray
    |> filterSkipMapFoldAssert

[<Fact>]
let ``filter skip map fold using wrapped enumerator``() =
    let enum =
        [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |]
        |> GSeq.ofArray

    enum
    |> GSeq.ofIEnumerator
    |> filterSkipMapFoldAssert

[<Fact>]
let ``filter skip map fold using seq``() =
    [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |]
    |> GSeq.ofSeq
    |> GSeq.ofIEnumerator
    |> filterSkipMapFoldAssert

[<Fact>]
let ``filter skip map fold using seq-seq``() =
    [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |]
    |> GSeq.ofSeq
    |> GSeq.toSeq
    |> GSeq.ofSeq
    |> filterSkipMapFoldAssert

[<Fact>]
let ``from array to seq``() =
    [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |]
    |> GSeq.ofArray
    |> GSeq.toSeq
    |> fun array -> Assert.True(array === [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |])

[<Fact>]
let ``from array to array``() =
    [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |]
    |> GSeq.ofArray
    |> GSeq.toArray
    |> fun array -> Assert.True(array :> seq<_> === [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |])

[<Fact>]
let ``from array to resize array``() =
    [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |]
    |> GSeq.ofArray
    |> GSeq.toResizeArray
    |> fun array -> Assert.True(array :> seq<_> === [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |])

[<Fact>]
let ``from array to block``() =
    [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |]
    |> GSeq.ofArray
    |> GSeq.toBlock
    |> fun array -> Assert.True(array :> seq<_> === [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12 |])