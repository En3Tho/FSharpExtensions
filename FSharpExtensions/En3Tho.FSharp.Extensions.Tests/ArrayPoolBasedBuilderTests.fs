module En3Tho.FSharp.Extensions.Tests.ArrayPoolBasedBuilderTests

open System
open Xunit
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.ArrayPoolBasedBuilders
open En3Tho.FSharp.ComputationExpressions.ICollectionBuilder

[<Fact>]
let ``Test that array pool based builders build effectively the same collection`` () =
    let count = Random.Shared.Next(5, 15)

    let libraryArray = [|
        1
        2
        3
        for i = 0 to count do
            i
        let mutable i = count
        while i > 0 do
            i
            i <- i - 1
    |]

    let customArray = arr {
        1
        2
        3
        for i = 0 to count do
            i
        let mutable i = count
        while i > 0 do
            i
            i <- i - 1
    }

    let customBlock = block {
        1
        2
        3
        for i = 0 to count do
            i
        let mutable i = count
        while i > 0 do
            i
            i <- i - 1
    }

    let customResizeArray = rsz {
        1
        2
        3
        for i = 0 to count do
            i
        let mutable i = count
        while i > 0 do
            i
            i <- i - 1
    }

    let customResizeArray2 = ResizeArray() {
        1
        2
        3
        for i = 0 to count do
            i
        let mutable i = count
        while i > 0 do
            i
            i <- i - 1
    }

    Assert.True(libraryArray |> Seq.identical customArray)
    Assert.True(libraryArray |> Seq.identical customBlock)
    Assert.True(libraryArray |> Seq.identical customResizeArray)
    Assert.True(libraryArray |> Seq.identical customResizeArray2)