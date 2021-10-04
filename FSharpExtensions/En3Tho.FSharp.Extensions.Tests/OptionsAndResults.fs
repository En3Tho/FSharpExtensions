module En3Tho.FSharp.Extensions.Tests.OptionsAndResults

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
        while i < 10 do
            let! _ = None
            i <- i + 1

        let! x = Some first
        let! y = Some second
        return x + y + i
    }

    Assert.Equal(opt3, Some (first + second))

    let opt3 = option {

        let mutable i = 0

        for i = 0 to 10 do
            let! i = Some i
            ignore i

        while i < 10 do
            let! _ = Some i
            i <- i + 1

        let! x = Some first
        let! y = Some second
        return x + y + i
    }

    Assert.Equal(opt3, Some (first + second + 10))

[<Fact>]
let ``Test that result builder is working properly`` () =
    let first = 10
    let second = 10

    let res1 = result {

        for i = 0 to 10 do
           let! i = Ok()
           ignore i

        let! x = Ok first
        let! y = Ok second
        return x + y
    }

    Assert.Equal(res1, Ok (first + second))

    let res2 = result {

        let mutable x = 0
        while x < 10 do
            x <- x + 1

        let! x = Ok first
        let! y = Ok second
        let! z = Error()
        return x + y + z
    }

    Assert.Equal(res2, Error())

    let res3 = result {

        for i = 0 to 10 do
           let! x = Ok i
           let! y = Error "" // will exit a for loop
           ignore (x + y)

        let! x = Ok first
        let! y = Ok second
        return x + y
    }

    Assert.Equal(res3, Ok (first + second))

[<Fact>]
let ``Test that eresult builder is working properly`` () =
    let res1 = eresult {
        return 10
    }

    Assert.Equal(res1, Ok 10)

    let exn = Exception()
    let res2 = eresult {
        return exn
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

    Assert.Equal(res1, Error (AggregateException([exn; exn; exn])))

    let exn = Exception()
    let res2 = eresult {
        return exn
    }

    Assert.Equal(res2, Error exn)