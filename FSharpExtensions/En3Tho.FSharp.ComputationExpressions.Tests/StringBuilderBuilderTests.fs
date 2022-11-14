module En3Tho.FSharp.Extensions.Tests.StringBuilderBuilderTests

open System
open System.Text
open Xunit
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.SStringBuilderBuilder

[<Fact>]
let ``Test string builder computation expression is working property`` () =
    let sb1 = StringBuilder() {
        1
        1.
        1.f
        '1'
        "1"
    }

    let sb2 =
        StringBuilder()
            .Append(1)
            .Append(1.)
            .Append(1.f)
            .Append('1')
            .Append("1")
    Assert.Equal(sb1.ToString(), sb2.ToString())

let ``Test that string builder computation expression supports complex operations`` () =
    let sb1 = StringBuilder() {
        let mutable x = 0
        while x < 10 do
            x
        for i = 0 to x do
            i
        try
            x
        with e ->
            x
        x
        x
        x
    }

    let sb2 = StringBuilder()
    let mutable x = 0
    while x < 10 do
        sb2.Append x |> ignore
    for i = 0 to x do
        sb2.Append i |> ignore
    try
        sb2.Append x |> ignore
    with e ->
        sb2.Append x |> ignore
    sb2.Append x |> ignore
    sb2.Append x |> ignore
    sb2.Append x |> ignore

    Assert.Equal(sb1.ToString(), sb2.ToString())