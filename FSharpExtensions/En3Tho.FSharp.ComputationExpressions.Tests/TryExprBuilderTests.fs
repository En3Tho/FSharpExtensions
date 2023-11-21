module En3Tho.FSharp.ComputationExpressions.Tests.TryExprBuilderTests

open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions
open Xunit

[<Fact>]
let ``test that try expr works correctly on successful cases``() =
    let mutable x = 0
    let inc() = &x +<- 1; x

    Assert.Equal(1, try' { inc() })
    Assert.Equal(2, tryWith(2) { inc() })

    Assert.Equal(2, x)

[<Fact>]
let ``test that try expr works correctly on failure cases``() =
    let mutable x = 0
    let inc() = &x +<- 1; failwith<int> "test"

    Assert.Equal(0, try' { inc() })
    Assert.Equal(2, tryWith(2) { inc() })

    try' { x <- x + 1; failwith "boom" }

    Assert.Equal(3, x)