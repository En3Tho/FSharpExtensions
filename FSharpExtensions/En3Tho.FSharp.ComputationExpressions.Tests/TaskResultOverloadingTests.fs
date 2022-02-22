module En3Tho.FSharp.ComputationExpressions.Tests.TaskResultOverloadingTests

open System
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.Tasks
open Xunit

[<Fact>]
let ``test that exnresultvtask can work with any kind of exception``() = task {
    let getExn1() : ValueTask<Result<int, ArgumentNullException>> =
        ValueTask.FromResult (Error (ArgumentNullException("test1")))

    let getExn2() : ValueTask<Result<int, ArgumentException>> =
        ValueTask.FromResult (Error (ArgumentException("test2")))

    let getExn3() : Result<int, ArgumentOutOfRangeException> =
        (Error (ArgumentOutOfRangeException("test3")))

    let getExn4() : Result<int, InvalidOperationException> =
        (Error (InvalidOperationException("test4")))

    let! result = exnresultvtask {
        match! getExn1() with
        | 0 ->
            match! getExn2() with
            | 0 ->
                return! getExn2()
            | _ ->
                return! getExn3()
        | _ ->
            return! getExn4()
    }

    match result with
    | Error (:? ArgumentNullException as exn) ->
        Assert.True(exn.Message.Contains("test1"))
    | _ ->
        Assert.True(false, $"Should not be possible: {result}")
}

[<Fact>]
let ``test that eresultvtask works exactly as exnresultvtask``() = task {
    let getExn1() : ValueTask<Result<int, Exception>> =
        ValueTask.FromResult (Error (ArgumentNullException("test1")))

    let getExn2() : ValueTask<Result<int, Exception>> =
        ValueTask.FromResult (Error (ArgumentException("test2")))

    let getExn3() : Result<int, Exception> =
        (Error (ArgumentOutOfRangeException("test3")))

    let getExn4() : Result<int, Exception> =
        (Error (InvalidOperationException("test4")))

    let! result = eresultvtask {
        match! getExn1() with
        | 0 ->
            match! getExn2() with
            | 0 ->
                return! getExn2()
            | _ ->
                return! getExn3()
        | _ ->
            return! getExn4()
    }

    match result with
    | Error (:? ArgumentNullException as exn) ->
        Assert.True(exn.Message.Contains("test1"))
    | _ ->
        Assert.True(false, $"Should not be possible: {result}")
}

[<Fact>]
let ``test that resultvtask works exactly as exnresultvtask``() = task {
    let getExn1() : ValueTask<Result<int, Exception>> =
        ValueTask.FromResult (Error (ArgumentNullException("test1")))

    let getExn2() : ValueTask<Result<int, Exception>> =
        ValueTask.FromResult (Error (ArgumentException("test2")))

    let getExn3() : Result<int, Exception> =
        (Error (ArgumentOutOfRangeException("test3")))

    let getExn4() : Result<int, Exception> =
        (Error (InvalidOperationException("test4")))

    let! result = resultvtask {
        match! getExn1() with
        | 0 ->
            match! getExn2() with
            | 0 ->
                return! getExn2()
            | _ ->
                return! getExn3()
        | _ ->
            return! getExn4()
    }

    match result with
    | Error (:? ArgumentNullException as exn) ->
        Assert.True(exn.Message.Contains("test1"))
    | _ ->
        Assert.True(false, $"Should not be possible: {result}")
}