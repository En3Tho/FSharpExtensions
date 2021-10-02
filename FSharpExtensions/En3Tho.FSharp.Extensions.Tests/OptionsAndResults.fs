module En3Tho.FSharp.Extensions.Tests.OptionsAndResults

open Xunit
open En3Tho.FSharp.ComputationExpressions.OptionBuilder
open En3Tho.FSharp.ComputationExpressions.ResultBuilder
open En3Tho.FSharp.Extensions // this is needed to test if extensions are breaking something

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

[<Fact>]
let ``Test that result builder is working properly`` () =
    let first = 10
    let second = 10

    let opt = result {
        let! x = Ok first
        let! y = Ok second
        return x + y
    }

    Assert.Equal(opt, Ok (first + second))

    let opt2 = result {
        let! x = Ok first
        let! y = Ok second
        let! z = Error()
        return x + y + z
    }

    Assert.Equal(opt2, Error())