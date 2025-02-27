﻿module En3Tho.FSharp.Extensions.Tests.OptionsAndResults

open System
open Xunit
open En3Tho.FSharp.ComputationExpressions.OptionBuilder
open En3Tho.FSharp.ComputationExpressions.ResultBuilder

// this is needed to test if extensions are breaking something
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.ICollectionBuilder

[<Fact>]
let ``Test that option builder is working properly`` () =
    let first = 10
    let second = 10

    let opt = option {
        let! x = Some first
        let! y = Some second
        return x + y
    }

    Assert.Equal(opt, Some (first + second))

    let opt2 = option {
        let! x = Some first
        let! y = Some second
        let! z = None
        return x + y + z
    }

    Assert.Equal(opt2, None)

    let opt3 = option {

        let mutable i = 0
        let! x = Some first
        let! y = Some second
        return x + y + i
    }

    Assert.Equal(opt3, Some (first + second))

[<Fact>]
let ``Test that result builder is working properly`` () =
    let first = 10
    let second = 10

    let res1 = result {
        let! x = Ok first
        let! y = Ok second
        return x + y
    }

    Assert.Equal(res1, Ok (first + second))

    let res2 = result {

        let mutable x = 0
        let! x = Ok first
        let! y = Ok second
        let! z = Error()
        return x + y + z
    }

    Assert.Equal(res2, Error())

[<Fact>]
let ``Test that eresult builder is working properly`` () =
    let res1 = eresult {
        return 10
    }

    Assert.Equal(res1, Ok 10)

    let exn = Exception()
    let res2 = eresult {
        return! Error exn
    }

    Assert.Equal(res2, Error exn)

[<Fact>]
let ``Test that eresult builder is can process multiple errors`` () =
    let exn = Exception()
    let res1 = eresult {
        let! a = Error exn
        and! b = Error exn
        and! c = Error exn
        return a + b + c + 10
    }

    match res1 with
    | Error aggExn ->
        Assert.True(
            aggExn.InnerExceptions
            |> Seq.toArray
            |> Seq.forall (fun x -> Object.ReferenceEquals(x, exn)))
    | _ ->
        Assert.Fail("Result should not be OK here")

    let exn = Exception()
    let res2 = eresult {
        return! Error exn
    }

    Assert.Equal(res2, Error exn)

[<Fact>]
let ``Test that exnresult properly catches exceptions`` () =
    let exn = Exception()
    let res1 = exnresult {
        let! a = Error exn
        and! b = Error exn
        and! c = Error exn
        failwith "Exn"
        return a + b + c + 10
    }

    match res1 with
    | Error (:? AggregateException as aggExn) ->
        Assert.True(
            aggExn.InnerExceptions
            |> Seq.toArray
            |> Seq.forall (fun x -> Object.ReferenceEquals(x, exn)))
    | _ ->
        Assert.Fail("Result should not be OK or not an AggregateException here")

    let res2 = exnresult {
        failwith "Exn"
        let! a = Error exn
        and! b = Error exn
        and! c = Error exn

        return a + b + c + 10
    }

    match res2 with
    | Error lolExn ->
        Assert.Equal("Exn", lolExn.Message)
    | _ ->
        Assert.Fail("Result should not be OK here")

    let exn = Exception()
    let res3 = exnresult {
        return! Error exn
    }

    Assert.Equal(res3, Error exn)

let ``test that exnresult properly works with exceptions of different types``() = exnresult {

    let! exn1 = Error (AggregateException())
    let! exn2 = Error (ArgumentException())
    return 0
}