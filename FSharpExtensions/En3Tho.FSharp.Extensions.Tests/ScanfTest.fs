﻿module En3Tho.FSharp.Extensions.Tests.ScanfTest

open System

open Xunit

open En3Tho.FSharp.Extensions.Scanf

[<Fact>]
let ``Test good cases of scanf`` () =
    let value1 = ref ""
    let value2 = ref 0
    let value3 = ref (ReadOnlyMemory<char>())

    Assert.True ("/authorize myText 123 qwe" |> scanf $"/authorize {value1} {value2} qwe")
    Assert.Equal("myText", value1.Value)
    Assert.Equal(123, value2.Value)

    Assert.True ("/authorize myText 123 qwe" |> scanf $"/authorize {value3} {value2} qwe")
    Assert.True("myText".AsSpan().SequenceEqual value3.Value.Span)

    Assert.True ("/authorize myText  123  qwe" |> scanf $"/authorize {value1}  {value2}  qwe")
    Assert.Equal("myText", value1.Value)
    Assert.Equal(123, value2.Value)

    Assert.True ("/authorize myText 123 qwe" |> scanf $"/authorize {value3} {value2} qwe")
    Assert.True("myText".AsSpan().SequenceEqual value3.Value.Span)

    Assert.True ("/authorize myText 123 qwe" |> scanfl $"/authorize {value1} {value2} qwe")
    Assert.Equal("myText", value1.Value)
    Assert.Equal(123, value2.Value)

    Assert.True ("/authorize myText 123 qwe" |> scanf $"/authorize {value3} {value2} qwe")
    Assert.True("myText".AsSpan().SequenceEqual value3.Value.Span)

    Assert.True ("/authorize myText 123" |> scanfl $"/authorize {value1} {value2} qwe")
    Assert.Equal("myText", value1.Value)
    Assert.Equal(123, value2.Value)

    Assert.True ("/authorize myText 123" |> scanfl $"/authorize {value3} {value2} qwe")
    Assert.True("myText".AsSpan().SequenceEqual value3.Value.Span)

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
    Assert.Equal("0", value1.Value)
    Assert.Equal(0, value2.Value)
    Assert.Equal(0., value3.Value)
    Assert.Equal(0.f, value4.Value)
    Assert.Equal('0', value5.Value)
    Assert.Equal(false, value6.Value)
    Assert.Equal(0u, value7.Value)

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
    Assert.False ("/authorize myText 123 qwe" |> scanfl $"/authorize {value1} {value2}")

[<Fact>]
let ``Test complex cases of scanf`` () =
    let cmd = ref ""
    let login = ref ""
    let password = ref 0

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"/{cmd} -login {login} -password {password}0")
    Assert.Equal("authorize", cmd.Value)
    Assert.Equal("myLogin", login.Value)
    Assert.Equal(password.Value, 123)

    let guid = Guid.NewGuid()
    let token = ref Guid.Empty
    Assert.True($"/authorizePass&token={guid}&password=1234" |> scanf $"/{cmd}&token={token}&password={password}")
    Assert.Equal("authorizePass", cmd.Value)
    Assert.Equal(guid, token.Value)
    Assert.Equal(1234, password.Value)

[<Fact>]
let ``Test complex cases of scanf memory`` () =
    let cmd = ref (ReadOnlyMemory<char>())
    let login = ref (ReadOnlyMemory<char>())
    let password = ref 0

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"/{cmd} -login {login} -password {password}0")
    Assert.True("authorize".AsSpan().SequenceEqual cmd.Value.Span)
    Assert.True("myLogin".AsSpan().SequenceEqual login.Value.Span)
    Assert.Equal(password.Value, 123)

    let guid = Guid.NewGuid()
    let token = ref Guid.Empty
    Assert.True($"/authorizePass&token={guid}&password=1234" |> scanf $"/{cmd}&token={token}&password={password}")
    Assert.True("authorizePass".AsSpan().SequenceEqual cmd.Value.Span)
    Assert.Equal(guid, token.Value)
    Assert.Equal(1234, password.Value)

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
let ``Test good split cases of scanf memory`` () =
    let firstPart = ref (ReadOnlyMemory<char>())
    let secondPart = ref (ReadOnlyMemory<char>())

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"/{firstPart} {secondPart}")
    Assert.True("authorize".AsSpan().SequenceEqual firstPart.Value.Span)
    Assert.True("-login myLogin -password 1230".AsSpan().SequenceEqual secondPart.Value.Span)

    Assert.True("/authorize -login myLogin -password 1230" |> scanfl $"/{firstPart} {secondPart} ")
    Assert.True("authorize".AsSpan().SequenceEqual firstPart.Value.Span)
    Assert.True("-login".AsSpan().SequenceEqual secondPart.Value.Span)

[<Fact>]
let ``Test single string case of scanf`` () =
    let firstPart = ref ""

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"/{firstPart}")
    Assert.Equal(firstPart.Value, "authorize -login myLogin -password 1230")

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"{firstPart}")
    Assert.Equal(firstPart.Value, "/authorize -login myLogin -password 1230")

[<Fact>]
let ``Test single string case of scanf memory`` () =
    let firstPart = ref (ReadOnlyMemory<char>())

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"/{firstPart}")
    Assert.True("authorize -login myLogin -password 1230".AsSpan().SequenceEqual firstPart.Value.Span)

    Assert.True("/authorize -login myLogin -password 1230" |> scanf $"{firstPart}")
    Assert.True("/authorize -login myLogin -password 1230".AsSpan().SequenceEqual firstPart.Value.Span)

[<Fact>]
let ``Test bad split cases of scanf`` () =
    let firstPart = ref ""
    let secondPart = ref ""

    Assert.False("/authorize -login myLogin -password 1230" |> scanf $"/{firstPart} {secondPart} ")
