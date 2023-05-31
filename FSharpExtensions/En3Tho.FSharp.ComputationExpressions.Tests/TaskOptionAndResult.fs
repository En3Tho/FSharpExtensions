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

    let! opt = voptionvtask {
        let! x = ValueSome first |> ValueTask.FromResult
        let! y = ValueSome second |> ValueTask.FromResult
        return x + y
    }

    Assert.Equal(opt, ValueSome (first + second))

    let! opt2 = voptionvtask {
        let! x = ValueSome first |> ValueTask.FromResult
        let! y = ValueSome second |> ValueTask.FromResult
        let! z = ValueNone |> ValueTask.FromResult
        return x + y + z
    }

    Assert.Equal(opt2, ValueNone)

//    let! opt3 = voptionvtask {
//
//        let mutable i = 0
//        while i < 10 do
//            let! _ = ValueNone |> ValueTask.FromResult
//            i <- i + 1
//
//        let! x = ValueSome first |> ValueTask.FromResult
//        let! y = ValueSome second |> ValueTask.FromResult
//        return x + y + i
//    }
//
//    Assert.Equal(opt3, ValueSome (first + second))

    let! opt3 = voptionvtask {

        let mutable i = 0

        for i = 0 to 10 do
            let! i = ValueSome i |> ValueTask.FromResult
            ignore i

        while i < 10 do
            let! _ = ValueSome i |> ValueTask.FromResult
            i <- i + 1

        let! x = ValueSome first |> ValueTask.FromResult
        let! y = ValueSome second |> ValueTask.FromResult
        return x + y + i
    }

    Assert.Equal(opt3, ValueSome (first + second + 10))
}

[<Fact>]
let ``Test that async disposable works properly with voptionvtask CE``() = vtask {
    let mutable disposalCounter = 0
    let inc() = disposalCounter <- disposalCounter + 1
    let! x = voptionvtask {
        use _ = { new IDisposable with member _.Dispose() = inc() }
        use _ = { new IAsyncDisposable with member _.DisposeAsync() = inc(); ValueTask() }
        return 1
    }
    Assert.Equal(ValueSome 1, x)
    Assert.Equal(disposalCounter, 2)
}

[<Fact>]
let ``Test that async disposable works properly with resultvtask CE``() = vtask {
    let mutable disposalCounter = 0
    let inc() = disposalCounter <- disposalCounter + 1
    let! x = resultvtask {
        use _ = { new IDisposable with member _.Dispose() = inc() }
        use _ = { new IAsyncDisposable with member _.DisposeAsync() = inc(); ValueTask() }
        return 1
    }
    Assert.Equal(Ok 1, x)
    Assert.Equal(disposalCounter, 2)
}

[<Fact>]
let ``Test that result builder can process errors`` () = vtask {
    let exn = Exception()

    let! res1 = resultvtask {
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
    let! res2 = resultvtask {
        return! Error exn
    }
    Assert.Equal(res2, Error exn)
}

[<Fact>]
let ``Test that exnresult properly catches exceptions`` () = vtask {
    let exn = Exception()
    let! res1 = evtask {
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

    let! res2 = evtask {
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
    let! res3 = evtask {
        return! Error exn
    }

    Assert.Equal(res3, Error exn)
}

[<Fact>]
let ``test that exnresultvtask properly works with exceptions of different types``() = evtask {
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
    let t = evtask {
        let! (v: Result<int, exn>) = Error (Exception())
        x <- 1
        let! _ = v
        x <- 2
    }
    let! _ = t
    Assert.Equal(1, x)
}

[<Fact>]
let ``test verbatim handling works properly for error case``() = vtask {
    let mutable x = 0
    let t = evtask {
        match! verb (Task.FromResult(Error(Exception()))) with
        | Error _ ->
            x <- 2
        | Ok _ ->
            ()
        Assert.Equal(2, x)

        match! verb (ValueTask.FromResult(Error(Exception()))) with
        | Error _ ->
            x <- 3
        | Ok _ ->
            ()

        Assert.Equal(3, x)
    }
    let! _ = t
    ()
}

[<Fact>]
let ``test verbatim handling works properly for ok case``() = vtask {
    let mutable x = 0
    let t = evtask {

        match! verb (Task.FromResult(Ok 1)) with
        | Ok _ ->
            x <- 2
        | Error _ ->
            ()
        Assert.Equal(2, x)

        match! verb (ValueTask.FromResult(Ok 1)) with
        | Ok _ ->
            x <- 3
        | Error _ ->
            ()

        Assert.Equal(3, x)
    }
    let! _ = t
    ()
}