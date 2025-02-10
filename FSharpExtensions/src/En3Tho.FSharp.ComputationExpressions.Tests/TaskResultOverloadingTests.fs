module En3Tho.FSharp.ComputationExpressions.Tests.TaskResultOverloadingTests

open System
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.Tasks

open Xunit
open En3Tho.FSharp.Xunit

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

    let! result = exnResultValueTask {

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

    let! result = exnResultValueTask {
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
let ``test that etask works exactly as exnresultvtask``() = task {
    let getExn1() : ValueTask<Result<int, Exception>> =
        ValueTask.FromResult (Error (ArgumentNullException("test1")))

    let getExn2() : ValueTask<Result<int, Exception>> =
        ValueTask.FromResult (Error (ArgumentException("test2")))

    let getExn3() : Result<int, Exception> =
        (Error (ArgumentOutOfRangeException("test3")))

    let getExn4() : Result<int, Exception> =
        (Error (InvalidOperationException("test4")))

    let! result = exnResultTask {
        match! getExn1() with
        | 0 ->
            match! getExn2() with
            | 0 ->
                return! ValueTask.FromResult(0)
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

type EE = {
    E: int
}

type AA = {
    A: int
}

[<Fact>]
let ``test exnresultvtask type checking with records``() =
    let getSomething (x: int) (y: int) = exnResultValueTask {
        let x: Task<Result<EE, exn>> = Task.FromResult(Ok { E = 1 })
        return! x
    }

    let getAnother (x: int) (y: int) = exnResultValueTask {
        let x: Task<Result<AA, exn>> = Task.FromResult(Ok { A = 1 })
        return! x
    }

    exnResultValueTask {
        let! z1 = getSomething 1 2
        let! z2 = getAnother z1.E z1.E

        let! z3 = Task.FromResult(1)
        let! z4 = ValueTask.FromResult(1)

        do! Task.CompletedTask
        do! ValueTask.CompletedTask

        return! Ok (Assert.Equal(4, z1.E + z2.A + z3 + z4))
    }

let ``test that exnresult properly works with exceptions of different types``() = exnResultValueTask {
    let! exn1 = Error (AggregateException())
    let! exn2 = Error (ArgumentException())
    return 0
}

[<Fact>]
let ``test exnresultvtask type checking with generic tasks``() =
    let test (x: Task<'a>) = exnResultValueTask {
        let! v = x
        return v
    }

    test (Task.FromResult(1))

[<Fact>]
let ``test exnresultvtask type checking with ints``() =
    let getSomething (x: int) (y: int) = exnResultValueTask {
        let x: Task<Result<int, exn>> = Task.FromResult(Ok 1)
        return! x
    }

    let getAnother (x: int) (y: int) = exnResultValueTask {
        let x: Task<Result<int, exn>> = Task.FromResult(Ok 1)
        return! x
    }

    exnResultValueTask {
        let! z1 = getSomething 1 1
        let! z2 = getAnother z1 z1

        let! z3 = Task.FromResult(1)
        let! z4 = ValueTask.FromResult(1)

        do! Task.CompletedTask
        do! ValueTask.CompletedTask

        return Assert.Equal(4, z1 + z2 + z3 + z4)
    }

[<Fact>]
let ``test that etasks automatically catches exceptions``() = task {
    let! x = exnResultTask {
        do! Task.Delay(1)
        failwith "Boom"
        return Ok 1
    }

    Assert.IsErrorWithMessage("Boom", x)
}

[<Fact>]
let ``test that evtasks automatically catches exceptions``() = task {
    let! x = exnResultValueTask {
        do! Task.Delay(1)
        failwith "Boom"
        return Ok 1
    }

    Assert.IsErrorWithMessage("Boom", x)
}