module En3Tho.FSharp.Extensions.Tests.Byrefs

open Xunit

open En3Tho.FSharp.Extensions

let inc x = x + 1

[<Fact>]
let ``Test |><- functionality``() =
    let mutable x = 1
    &x |><- inc
    Assert.Equal(2, x)

[<Fact>]
let ``Test ??<- functionality``() =
    let mutable x: string = null
    &x ??<- "Hello"
    Assert.Equal("Hello", x)
    &x ??<- null
    Assert.Equal("Hello", x)

[<Fact>]
let ``Test ???<- functionality``() =
    let mutable x: string = null
    &x ???<- fun() -> "Hello"
    Assert.Equal("Hello", x)
    &x ??<- null
    Assert.Equal("Hello", x)