module En3Tho.FSharp.Extensions.Tests.GSeq

open Xunit
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.ArrayPoolBasedBuilders

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

[<Fact>]
let ``test how upTo works with for no ce``() =
    let upTo = GSeq.upTo 0 5
    let values = ResizeArray()
    for v in upTo do
        values.Add(v)

    Assert.True([| 0; 1; 2; 3; 4 |] :> seq<_> === values)

[<Fact>]
let ``test how upTo works``() =
    let upTo = GSeq.upTo 0 5
    let values = arr {
        for v in upTo do
            v
    }

    Assert.True([| 0; 1; 2; 3; 4 |] === values)

[<Fact>]
let ``test how upTo' works``() =
    let upTo = GSeq.upTo' 0 5
    let values = arr {
        for v in upTo do
            v
    }

    Assert.True([| 0; 1; 2; 3; 4; 5 |] === values)

[<Fact>]
let ``test how downTo works``() =
    let upTo = GSeq.downTo 5 0
    let values = arr {
        for v in upTo do
            v
    }

    Assert.True([| 5; 4; 3; 2; 1 |] === values)

[<Fact>]
let ``test how downTo' works``() =
    let upTo = GSeq.downTo' 5 0
    let values = arr {
        for v in upTo do
            v
    }

    Assert.True([| 5; 4; 3; 2; 1; 0 |] === values)

[<Fact>]
let ``test how upToStep works``() =
    let upTo = GSeq.upToStep 0 6 2
    let values = arr {
        for v in upTo do
            v
    }

    Assert.True([| 0; 2; 4 |] === values)

[<Fact>]
let ``test how upToStep' works``() =
    let upTo = GSeq.upToStep' 0 6 2
    let values = arr {
        for v in upTo do
            v
    }

    Assert.True([| 0; 2; 4; 6 |] === values)

[<Fact>]
let ``test how downToStep works``() =
    let upTo = GSeq.downToStep 6 0 2
    let values = arr {
        for v in upTo do
            v
    }

    Assert.True([| 6; 4; 2; |] === values)

[<Fact>]
let ``test how downToStep' works``() =
    let upTo = GSeq.downToStep' 6 0 2
    let values = arr {
        for v in upTo do
            v
    }

    Assert.True([| 6; 4; 2; 0 |] === values)