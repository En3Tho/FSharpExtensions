module En3Tho.FSharp.ComputationExpressions.Tests.TaskAndCollectionsOverloadingTests

open System.Collections.Generic
open System.ComponentModel
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.Tasks
open En3Tho.FSharp.ComputationExpressions.ICollectionBuilder
open En3Tho.FSharp.ComputationExpressions.SCollectionBuilder
open En3Tho.FSharp.ComputationExpressions.IDictionaryBuilder
open Xunit

type CustomSCollection() =
    let mutable sum = 0
    member _.Sum = sum
    member _.Add(v: int) = sum <- sum + v

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.Run([<InlineIfLambda>] expr: CollectionCode) = expr(); this
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.Zero _ : CollectionCode = fun() -> ()

[<Fact>]
let ``should use both custom collection and exntask``() = exnTask {
    let value = CustomSCollection() {
        1
        2
        3
    }

    Assert.Equal(1 + 2 + 3, value.Sum)
    return ()
}

[<Fact>]
let ``should use both resize array and exntask``() = exnTask {
    let value = ResizeArray() {
        1
        2
        3
    }

    Assert.Equal(1, value[0])
    Assert.Equal(2, value[1])
    Assert.Equal(3, value[2])
    return ()
}

[<Fact>]
let ``should use both dictionary and exntask``() = exnTask {
    let value = Dictionary() {
        1 -- 1
        2 -- 2
        3 -- 3
    }

    Assert.Equal(1, value[1])
    Assert.Equal(2, value[2])
    Assert.Equal(3, value[3])
    return! ``should use both resize array and exntask``()
}

[<Fact>]
let ``should return from exntask``() = exnTask {
    return 1
}