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
    let! res1 = exnresultvtask {
        let! a = Error exn // exits here
        let! b = Ok 10
        let! c = Ok 15
        failwith "Lol"
        return a + b + c + 10
    }

    match res1 with
    | Error lolExn ->
        Assert.Same(exn, lolExn)
    | _ ->
        Assert.True(false, "Result should not be OK here")

    let! res2 = exnresultvtask {
        failwith "LolException"
        let! a = Error exn
        let! b = Ok 10
        let! c = Ok 15
        return a + b + c + 10
    }

    match res2 with
    | Error lolExn ->
        Assert.Contains(lolExn.Message, "LolException", StringComparison.Ordinal)
    | _ ->
        Assert.True(false, "Result should not be OK here")

    let exn = Exception()
    let! res3 = exnresultvtask {
        return! Error exn
    }

    Assert.Equal(res3, Error exn)
}

let ``test that exnresultvtask properly works with exceptions of different types``() = exnresultvtask {
    let exnTask = task { failwith "" }
    let tryExnTask (t: Task<_>) = task {
        try
            let! result = t
            return Ok t
        with exn ->
            return Error exn
    }

    let! exn0 = tryExnTask exnTask

    let! exn1 = Error (AggregateException()) |> ValueTask.FromResult
    let! exn2 = Error (ArgumentException()) |> ValueTask.FromResult
    return 0
}