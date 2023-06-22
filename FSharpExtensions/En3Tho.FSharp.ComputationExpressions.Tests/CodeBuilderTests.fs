module En3Tho.FSharp.Extensions.Tests.CodeBuilderTests

open System
open En3Tho.FSharp.ComputationExpressions.CodeBuilder
open Xunit

[<Fact>]
let ``test simple code``() =
    let code = code {
        "let x = 0"
        "Console.WriteLine(x + 1)"
    }

    let expected = String.concat Environment.NewLine [
        "let x = 0"
        "Console.WriteLine(x + 1)"
    ]

    Assert.Equal(expected, code.ToString())

[<Fact>]
let ``test simple code indent``() =
    let code = code {
        indent {
            "let x = 0"
            "Console.WriteLine(x + 1)"
        }
    }

    let expected = String.concat Environment.NewLine [
        "    let x = 0"
        "    Console.WriteLine(x + 1)"
    ]

    Assert.Equal(expected, code.ToString())

[<Fact>]
let ``test simple code brace block``() =
    let code = code {
        braceBlock {
            "let x = 0"
            "Console.WriteLine(x + 1)"
        }
    }

    let expected = String.concat Environment.NewLine [
        "{"
        "    let x = 0"
        "    Console.WriteLine(x + 1)"
        "}"
    ]

    Assert.Equal(expected, code.ToString())

[<Fact>]
let ``test simple code brace block indent``() =
    let code = code {
        braceBlock {
            indent {
                "let x = 0"
                "Console.WriteLine(x + 1)"
            }
        }
    }

    let expected = String.concat Environment.NewLine [
        "{"
        "        let x = 0"
        "        Console.WriteLine(x + 1)"
        "}"
    ]

    Assert.Equal(expected, code.ToString())

[<Fact>]
let ``test simple code trim``() =
    let code = code {
        "let x = 0"
        "Console.WriteLine(x + 1)"
    }

    let expected = String.concat Environment.NewLine [
        "let x = 0"
        "Console.WriteLine(x + 1)"
    ]

    code.TrimEnd()
    Assert.Equal(expected, code.ToString())

[<Fact>]
let ``test simple code trim2``() =
    let code = code {
        "let x = 0"
        "Console.WriteLine(x + 1)"
        ""
        "\n"
        "\r"
        "\t"
    }

    let expected = String.concat Environment.NewLine [
        "let x = 0"
        "Console.WriteLine(x + 1)"
    ]

    code.TrimEnd()
    Assert.Equal(expected, code.ToString())
