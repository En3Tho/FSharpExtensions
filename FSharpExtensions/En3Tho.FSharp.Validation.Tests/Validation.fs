module Validation

open System
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.ResultBuilder
open En3Tho.FSharp.Validation.CommonValidatedTypes.NonEmptyString
open En3Tho.FSharp.Validation.CommonValidatedTypes.NonNegativeValue
open En3Tho.FSharp.Xunit
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


type Age = NonNegativeValue<int>
type Age2 = Age
type Name = NonEmptyString
type Person = {
    Age: Age
    Name: Name
}

[<Fact>]
let ``Test that multivalidation works for successful case``() =
    let age = 1
    let name = "Bob"

    let person = exnresult {
        let! age = Age.Try age
        and! name = Name.Try name
        return {
            Age = age
            Name = name
        }
    }

    Assert.IsOk({ Age = Age.Make age; Name = Name.Make name }, person)

[<Fact>]
let ``Test that multivalidation works for unsuccessful case``() =
    let age = -1
    let name = ""

    let person = exnresult {
        let! age = Age.Try age
        and! name = Name.Try name
        return {
            Age = age
            Name = name
        }
    }

    Assert.IsError(person)
    match person with
    | Error (:? AggregateException as exn) ->
        let errors = exn.InnerExceptions |> Seq.toArray
        Assert.Equal(2, errors.Length)
        Assert.True(errors[0] :? ValueIsNegative)
        Assert.True(errors[1] :? StringIsEmpty)
    | _ ->
        failwith "Impossible"