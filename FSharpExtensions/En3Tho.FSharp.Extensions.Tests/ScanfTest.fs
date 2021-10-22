module En3Tho.FSharp.Extensions.Tests.ScanfTest

open Xunit

open En3Tho.FSharp.Extensions.Scanf

[<Fact>]
let ``Test good cases of scanf`` () =
    let value1 = ref ""
    let value2 = ref 0

    Assert.True ("/authorize myText 123 qwe" |> scanf $"/authorize {value1} {value2} qwe")
    Assert.Equal(value1.Value, "myText")
    Assert.Equal(value2.Value, 123)

    Assert.True ("/authorize myText  123  qwe" |> scanf $"/authorize {value1}  {value2}  qwe")
    Assert.Equal(value1.Value, "myText")
    Assert.Equal(value2.Value, 123)

    Assert.True ("/authorize myText 123 qwe" |> scanfl $"/authorize {value1} {value2} qwe")
    Assert.Equal(value1.Value, "myText")
    Assert.Equal(value2.Value, 123)

    Assert.True ("/authorize myText 123" |> scanfl $"/authorize {value1} {value2} qwe")
    Assert.Equal(value1.Value, "myText")
    Assert.Equal(value2.Value, 123)

    Assert.True ("/authorize myText 123 qwe" |> scanfl $"/authorize {value1} {value2}")
    Assert.Equal(value1.Value, "myText")
    Assert.Equal(value2.Value, 123)

[<Fact>]
let ``Test scanf supports basic types`` () =
    let value1 = ref "0"
    let value2 = ref 0
    let value3 = ref 0.
    let value4 = ref 0.f
    let value5 = ref '0'
    let value6 = ref false
    let value7 = ref 0u

    let text = $"{value1.Value} {value2.Value} {value3.Value} {value4.Value} {value5.Value} {value6.Value} {value7.Value}"
    Assert.True(text |> scanf $"{value1} {value2} {value3} {value4} {value5} {value6} {value7}")
    Assert.Equal(value1.Value, "0")
    Assert.Equal(value2.Value, 0)
    Assert.Equal(value3.Value, 0.)
    Assert.Equal(value4.Value, 0.f)
    Assert.Equal(value5.Value, '0')
    Assert.Equal(value6.Value, false)
    Assert.Equal(value7.Value, 0u)

[<Fact>]
let ``Test bad cases of scanf`` () =
    let value1 = ref ""
    let value2 = ref 0

    Assert.False ("/authorize myText 123 qwe1" |> scanf $"/authorize {value1} {value2} qwe")
    Assert.False ("/authorize myText 123 qwe1" |> scanf $"/authorize {value1}  {value2}  qwe")
    Assert.False ("/authorize myText 123 qwe" |> scanf  $"/authorize {value1}  {value2}  qwe")
    Assert.False ("/authorize myText 123 qwe" |> scanf  $"/authorize {value1} {value2} qwe1")
    Assert.False ("/authorize myText 123 qwe" |> scanf  $"/authorize {value1}1 {value2} qwe")
    Assert.False ("/authorize myText 123 qwe" |> scanf  $"/authorize {value1}1 {value2}")
    Assert.False ("/authorize 123 qwe" |> scanfl  $"/authorize {value1} {value2} qwe")
    Assert.False ("/authorize1 myText 123" |> scanfl $"/authorize {value1} {value2} qwe")
    Assert.False ("/authorize myText 123 qwe" |> scanfl $"/authorize {value1} 123 {value2}")

[<Fact>]
let ``Test complex cases of scanf`` () =
    let cmd = ref ""
    let login = ref ""
    let password = ref 0

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"/{cmd} -login {login} -password {password}0")
    Assert.Equal(cmd.Value, "authorize")
    Assert.Equal(login.Value, "myLogin")
    Assert.Equal(password.Value, 123)

[<Fact>]
let ``Test good split cases of scanf`` () =
    let firstPart = ref ""
    let secondPart = ref ""

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"/{firstPart} {secondPart}")
    Assert.Equal(firstPart.Value, "authorize")
    Assert.Equal(secondPart.Value, "-login myLogin -password 1230")

    Assert.True("/authorize -login myLogin -password 1230" |> scanfl $"/{firstPart} {secondPart} ")
    Assert.Equal(firstPart.Value, "authorize")
    Assert.Equal(secondPart.Value, "-login")

[<Fact>]
let ``Test single string case of scanf`` () =
    let firstPart = ref ""

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"/{firstPart}")
    Assert.Equal(firstPart.Value, "authorize -login myLogin -password 1230")

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"{firstPart}")
    Assert.Equal(firstPart.Value, "/authorize -login myLogin -password 1230")


[<Fact>]
let ``Test bad split cases of scanf`` () =
    let firstPart = ref ""
    let secondPart = ref ""

    Assert.False("/authorize -login myLogin -password 1230" |> scanf $"/{firstPart} {secondPart} ")
