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

    let! result = evtask {
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

[<Fact>]
let ``test that resulttask works exactly as exnresultvtask``() = task {
    let getExn1() : ValueTask<Result<int, Exception>> =
        ValueTask.FromResult (Error (ArgumentNullException("test1")))

    let getExn2() : ValueTask<Result<int, Exception>> =
        ValueTask.FromResult (Error (ArgumentException("test2")))

    let getExn3() : Result<int, Exception> =
        (Error (ArgumentOutOfRangeException("test3")))

    let getExn4() : Result<int, Exception> =
        (Error (InvalidOperationException("test4")))

    let! result = resulttask {
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

type EE = {
    E: int
}

type AA = {
    A: int
}

[<Fact>]
let ``test task type checking``() =
    let getSomething (x: int) (y: int) = voptionvtask {
        let x: ValueTask<EE> = ValueTask.FromResult({ E = 123 })
        return x
    }

    let getAnother (x: int) (y: int) = voptionvtask {
        let x: ValueTask<AA> = ValueTask.FromResult({ A = 123 })
        return x
    }

    voptionvtask {
        let! z1 = getSomething 1 2
        let! z1 = z1 |> verb
        let! z2 = getAnother z1.E z1.E
        let! z2 = z2 |> verb
        Assert.Equal(z1.E + z2.A, 246)
    }

[<Fact>]
let ``test exnresultvtask type checking with records``() =
    let getSomething (x: int) (y: int) = evtask {
        let x: Task<Result<EE, exn>> = Task.FromResult(Ok { E = 123 })
        return! x
    }

    let getAnother (x: int) (y: int) = evtask {
        let x: Task<Result<AA, exn>> = Task.FromResult(Ok { A = 123 })
        return! x
    }

    evtask {
        let! z1 = getSomething 1 2
        let! z2 = getAnother z1.E z1.E
        return Assert.Equal(z1.E + z2.A, 246)
    }

[<Fact>]
let ``test exnresultvtask type checking with ints``() =
    let getSomething (x: int) (y: int) = evtask {
        let x: Task<Result<int, exn>> = Task.FromResult(Ok 123)
        return! x
    }

    let getAnother (x: int) (y: int) = evtask {
        let x: Task<Result<int, exn>> = Task.FromResult(Ok 123)
        return! x
    }

    evtask {
        let! z1 = getSomething 1 2
        let! z2 = getAnother z1 z1
        return Assert.Equal(z1 + z2, 246)
    }

[<Fact>]
let ``test eresultvtask type checking with records``() =
    let getSomething (x: int) (y: int) = rtask {
        let x: Task<Result<EE, exn>> = Task.FromResult(Ok { E = 123 })
        return! x
    }

    let getAnother (x: int) (y: int) = rtask {
        let x: Task<Result<AA, exn>> = Task.FromResult(Ok { A = 123 })
        return! x
    }

    rtask {
        let! z1 = getSomething 1 2
        let! z2 = getAnother z1.E z1.E
        return Assert.Equal(z1.E + z2.A, 123 * 2)
    }

[<Fact>]
let ``test eresultvtask type checking with ints``() =
    let getSomething (x: int) (y: int) = rtask {
        let x: Task<Result<int, exn>> = Task.FromResult(Ok 123)
        return! x
    }

    let getAnother (x: int) (y: int) = rtask {
        let x: Task<Result<int, exn>> = Task.FromResult(Ok 123)
        return! x
    }

    rtask {
        let! z1 = getSomething 1 2
        let! z2 = getAnother z1 z1
        return Assert.Equal(z1 + z2, 123 * 2)
    }