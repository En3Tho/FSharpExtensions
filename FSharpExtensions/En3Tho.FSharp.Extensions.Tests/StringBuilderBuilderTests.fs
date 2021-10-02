module En3Tho.FSharp.Extensions.Tests.StringBuilderBuilderTests

open System
open System.Text
open Xunit
open En3Tho.FSharp.Extensions
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