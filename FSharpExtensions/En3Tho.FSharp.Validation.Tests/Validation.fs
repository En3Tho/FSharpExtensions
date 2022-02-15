module Validation

open System
open Xunit
open En3Tho.FSharp.Validation.CommonValidatedTypes

[<Fact>]
let ``Test that Validated's ToString method is printing original value`` () =

    let genericTest value =
        let validated = PlainValue.Make(value = value)
        Assert.Equal(value.ToString(), validated.ToString())

    genericTest "abcd"
    genericTest 123
    genericTest 1234.
    genericTest 'c'

    let complexValue = {| Name = PlainValue.Make "123"; Age = PlainValue.Make 123 |}

    genericTest complexValue

    Assert.Equal($"{complexValue}", $"{PlainValue.Make complexValue}")

[<Fact>]
let ``Test that Validated's generated`` () =

    let genericTest value =
        let validated = PlainValue.Make(value = value)
        Assert.Equal(value.ToString(), validated.ToString())

    genericTest "abcd"
    genericTest 123
    genericTest 1234.
    genericTest 'c'

    let complexValue = {| Name = PlainValue.Make "123"; Age = PlainValue.Make 123 |}

    genericTest complexValue

    Assert.Equal($"{complexValue}", $"{PlainValue.Make complexValue}")