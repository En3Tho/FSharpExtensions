module En3Tho.FSharp.Extensions.Tests.FunkyPipesTests

open System
open En3Tho.FSharp.Extensions
open Xunit

let f1 x = x
let f2 x y = x + y
let f3 x y z = x + y + z

let q1 = Func<_,_>(f1)
let q2 = Func<_,_,_>(f2)
let q3 = Func<_,_,_,_>(f3)

[<Fact>]
let ``test that pipe one does not break usual type inference``() =
    1
    |> fun x -> x + 1
    |> ignore

[<Fact>]
let ``test that pipe1 works as expected``() =

    let computation (x: int) =
        x
        |> f1
        |> fun x -> x
        |> q1
        |> (f1 >> q1)
        |> fun result ->
            Assert.Equal(x, result)

    computation 5

[<Fact>]
let ``test that pipe2 works as expected``() =
    let computation x y =
        let z =
            (x, y)
            ||> f2
        (z, z)
        ||> q2
        |> fun result ->
            Assert.Equal((x + y) * 2, result)

    computation 5 10

[<Fact>]
let ``test that pipe3 works as expected``()  =

    let computation x y z =
        let w =
            (x, y, z)
            |||> f3
        (w, w, w)
        |||> q3
        |> fun result ->
            Assert.Equal((x + y + z) * 3, result)

    computation 5 10 15

[<Fact>]
let ``test that composition works as expected``() =

    let computation (x: int) =
        x
        |> (f1
        >> q1
        >> (f1 >> q1)
        >> fun result ->
            Assert.Equal(x, result))

    computation 5

[<Fact>]
let ``test that invoke works as expected``() =

    let computation (x: int) =
        (q1 ^ x) + (f1 ^ x)
        |> fun result -> Assert.Equal(x * 2, result)

    computation 5