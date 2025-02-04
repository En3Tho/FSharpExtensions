module En3Tho.FSharp.Extensions.Tests.TaskOptionAndResult

open System
open System.Threading.Tasks
open En3Tho.FSharp.Extensions
open Xunit
open En3Tho.FSharp.ComputationExpressions.Tasks

[<Fact>]
let ``Test that option builder is working properly`` () = vtask {
    let first = 10
    let second = 10

    let! opt = voptionValueTask {
        let! x = ValueSome first |> ValueTask.FromResult
        let! y = ValueSome second |> ValueTask.FromResult
        return x + y
    }

    Assert.Equal(opt, ValueSome (first + second))

    let! opt2 = voptionValueTask {
        let! x = ValueSome first |> ValueTask.FromResult
        let! y = ValueSome second |> ValueTask.FromResult
        let! z = ValueNone |> ValueTask.FromResult
        return x + y + z
    }

    Assert.Equal(opt2, ValueNone)

    let! opt3 = voptionValueTask {
        let! x = ValueSome first |> ValueTask.FromResult
        let! y = ValueSome second |> ValueTask.FromResult
        return x + y
    }

    Assert.Equal(opt3, ValueSome (first + second))
}

[<Fact>]
let ``Test that async disposable works properly with voptionValueTask CE``() = vtask {
    let mutable disposalCounter = 0
    let inc() = disposalCounter <- disposalCounter + 1
    let! x = voptionValueTask {
        use _ = { new IDisposable with member _.Dispose() = inc() }
        use _ = { new IAsyncDisposable with member _.DisposeAsync() = inc(); ValueTask() }
        return 1
    }
    Assert.Equal(ValueSome 1, x)
    Assert.Equal(disposalCounter, 2)
}

[<Fact>]
let ``Test that async disposable works properly with resultValueTask CE``() = vtask {
    let mutable disposalCounter = 0
    let inc() = disposalCounter <- disposalCounter + 1
    let! x = resultValueTask {
        use _ = { new IDisposable with member _.Dispose() = inc() }
        use _ = { new IAsyncDisposable with member _.DisposeAsync() = inc(); ValueTask() }
        return 1
    }
    Assert.Equal(Ok 1, x)
    return Assert.Equal(disposalCounter, 2)
}

[<Fact>]
let ``Test that result builder can process errors`` () = vtask {
    let exn = Exception()

    let! res1 = resultValueTask {
        let! a = Error exn
        let! b = Ok 10
        let! c = Ok 15
        return a + b + c + 10
    }

    match res1 with
    | Error res1Exn ->
        Assert.Equal(res1Exn, exn)
    | _ ->
        Assert.True(false, "Result should not be OK here")

    let exn = Exception()
    let! res2 = resultValueTask {
        return! Error exn
    }
    return Assert.Equal(res2, Error exn)
}

[<Fact>]
let ``Test that exnresult properly catches exceptions`` () = vtask {
    let exn = Exception()
    let! res1 = exnResultValueTask {
        let! a = Error exn // exits here
        let! b = Ok 10
        let! c = Ok 15
        failwith "Exn"
        return a + b + c + 10
    }

    match res1 with
    | Error exn ->
        Assert.Same(exn, exn)
    | _ ->
        Assert.True(false, "Result should not be OK here")

    let! res2 = exnResultValueTask {
        failwith "Exn"
        let! a = Error exn
        let! b = Ok 10
        let! c = Ok 15
        return a + b + c + 10
    }

    match res2 with
    | Error exn ->
        Assert.Contains(exn.Message, "Exn", StringComparison.Ordinal)
    | _ ->
        Assert.True(false, "Result should not be OK here")

    let exn = Exception()
    let! res3 = exnResultValueTask {
        return! Error exn
    }

    Assert.Equal(res3, Error exn)
}

[<Fact>]
let ``test that exnresultValueTask properly works with exceptions of different types``() = exnResultValueTask {
    let exnTask = task { failwith "" }
    let tryExnTask (t: Task<_>) = task {
        try
            let! _ = t
            return Ok t
        with exn ->
            return Error exn
    }

    let! _ = tryExnTask exnTask

    let! _ = Error (AggregateException()) |> ValueTask.FromResult
    let! _ = Error (ArgumentException()) |> ValueTask.FromResult
    return 0
}

[<Fact>]
let ``test that explicit type does not exit early``() = vtask {
    let mutable x = 0
    let t = exnResultValueTask {
        let! (v: Result<int, exn>) = Error (Exception())
        x <- 1
        let! _ = v
        x <- 2
        return ()
    }
    let! _ = t
    return Assert.Equal(1, x)
}