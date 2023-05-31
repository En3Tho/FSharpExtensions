module ComputationExpressionBuildTests

open System.Collections.Concurrent
open System.Collections.Generic
open System.ComponentModel
open System.Threading.Tasks
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.SCollectionBuilder
open En3Tho.FSharp.ComputationExpressions.ICollectionBuilder
open En3Tho.FSharp.ComputationExpressions.IDictionaryBuilder
open FSharp.Control
open Xunit
open System.Linq

[<Fact>]
let ``Test that task computation expression compiles`` () =
    let rec taskTest (job: Task<_>) = task {

        let mutable i = 0
        while i < 10 do
            let _ = None
            i <- i + 1

        return! job
    }
    let expected = 0
    let result = (taskTest (Task.FromResult expected)).ConfigureAwait(false).GetAwaiter().GetResult()
    Assert.Equal(result, expected)

[<Fact>]
let ``Test that async computation expression compiles`` () =

    let rec taskTest job = async {

        let mutable i = 0
        while i < 10 do
            let _ = None
            i <- i + 1

        return! job
    }
    let expected = 0
    let result = taskTest (async.Return expected) |> Async.RunSynchronously
    Assert.Equal(result, expected)

[<Fact>]
let ``Test that dictionary is supported as builder`` () =
    let intIntDict1 = Dictionary() {
        struct (1, 10)
        2, 10
        3 -- 10

        let mutable x = 4
        while x < 10 do
            x -- 10
            x <- x + 1
    }

    Assert.Equal(intIntDict1.Count, 9)

    let intIntDict12 = ConcurrentDictionary() {
        struct (1, 10)
        1, 10
        1 -- 10

        let mutable x = 0
        while x < 10 do
            x <- x + 1
    }
    Assert.True(intIntDict1.GetType().GenericTypeArguments.SequenceEqual(intIntDict12.GetType().GenericTypeArguments))

[<Fact>]
let ``Test that dictionary builder doesn't throw when same key values are inserted``() =
    let d = Dictionary() {
        1 -- 10
        1 -- 10
    }
    Assert.Equal(d.Count, 1)

[<Fact>]
let ``Test that hashset builder doesn't throw when same key values are inserted``() =
    let d = HashSet() {
        1 -- 10
        1 -- 10
    }
    Assert.Equal(d.Count, 1)

[<Fact>]
let ``Test that generics are working properly and builders are not conflicting with each other`` () =
    let genericList (value: 'a) = ResizeArray() { value }
    let list1 = genericList 10
    let list2 = genericList "10"

    Assert.True(list1.GetType().GenericTypeArguments.SequenceEqual [| typeof<int> |])
    Assert.True(list2.GetType().GenericTypeArguments.SequenceEqual [| typeof<string> |])
    Assert.Equal(list1[0], 10)
    Assert.Equal(list2[0], "10")

[<Fact>]
let ``Test that custom types are supported and builders are not conflicting with each other`` () =
    let genericList (value: 'a) = ResizeArray() { value }
    let list1 = genericList 10
    let list2 = genericList "10"

    let genericHashSet (value: 'a) = HashSet() { value }
    let adder1 = genericHashSet 10
    let adder2 = genericHashSet "10"

    Assert.True(list1.GetType().GenericTypeArguments.SequenceEqual (adder1.GetType().GenericTypeArguments))
    Assert.True(list2.GetType().GenericTypeArguments.SequenceEqual (adder2.GetType().GenericTypeArguments))

type MyAdder() =
    member val AddCount = 0 with get, set
    member this.Add _ = this.AddCount <- this.AddCount + 1

type MyAdder with
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.Run([<InlineIfLambda>] expr: CollectionCode) = expr(); this
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.Zero _ : CollectionCode = fun() -> ()

[<Fact>]
let ``Test that nested builders are supported`` () =
    let addCountInForLoop = 3
    let adder =
        MyAdder() {
            for _ = 0 to addCountInForLoop - 1 do
                MyAdder() {
                     MyAdder()
                }
        }
    let manualAddCount = 1

    adder {
        MyAdder() {
             MyAdder()
        }
    } |> ignore

    let expectedAddCount = addCountInForLoop + manualAddCount
    Assert.Equal(adder.AddCount, expectedAddCount)