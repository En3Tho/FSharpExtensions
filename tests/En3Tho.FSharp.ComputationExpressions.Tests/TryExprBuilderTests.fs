module En3Tho.FSharp.ComputationExpressions.Tests.TryExprBuilderTests

open System
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.Xunit
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

[<Fact>]
let ``test that tryE expr works correctly on successful cases``() =
    let mutable x = 0
    let inc() = &x +<- 1; x

    Assert.Null(tryE { inc() |> ignore })
    Assert.Equal(1, x)

[<Fact>]
let ``test that tryE expr works correctly on failure cases``() =
    let mutable x = 0
    let inc() = &x +<- 1; failwith<int> "test"

    Assert.NotNull(tryE { inc() |> ignore })
    Assert.Equal(1, x)

[<Fact>]
let ``test that tryR expr works correctly on successful cases``() =
    let mutable x = 0
    let inc() = &x +<- 1; x

    Assert.IsOk(tryR { inc() |> ignore })
    Assert.Equal(1, x)

[<Fact>]
let ``test that tryR expr works correctly on failure cases``() =
    let mutable x = 0
    let inc() = &x +<- 1; failwith<int> "test"

    Assert.IsErrorOfType<Exception, _>(tryR { inc() |> ignore })
    Assert.Equal(1, x)