module En3Tho.FSharp.ComputationExpressions.Tests.GenericTaskBuilderTests

open System
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.TaskBuilders.ExnResultValueTask
open En3Tho.FSharp.ComputationExpressions.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.Xunit

open En3Tho.FSharp.ComputationExpressions.Tasks.GenericTaskBuilderExtensions.LowPriority
open En3Tho.FSharp.ComputationExpressions.Tasks.GenericTaskBuilderExtensions.MediumPriority
open En3Tho.FSharp.ComputationExpressions.Tasks.GenericTaskBuilderExtensions.HighPriority

open En3Tho.FSharp.ComputationExpressions.Tasks.GenericUnitTaskBuilderExtensions.LowPriority
open En3Tho.FSharp.ComputationExpressions.Tasks.GenericUnitTaskBuilderExtensions.MediumPriority
open En3Tho.FSharp.ComputationExpressions.Tasks.GenericUnitTaskBuilderExtensions.HighPriority
open Xunit

type ValueTaskWrapperBuilder<'a> =
    GenericTaskBuilder<ValueTaskWrapperMethodBuilder<'a>, ValueTaskWrapperAwaiter<'a>, ValueTaskWrapper<'a>, 'a, ValueTask<'a>>

type ValueTaskWrapperBuilder =
    GenericUnitTaskBuilder<ValueTaskWrapperMethodBuilder, ValueTaskWrapperAwaiter, ValueTaskWrapper, ValueTask>

type TaskWrapperBuilder<'a> =
    GenericTaskBuilder<TaskWrapperMethodBuilder<'a>, TaskWrapperAwaiter<'a>, TaskWrapper<'a>, 'a, Task<'a>>

type TaskWrapperBuilder =
    GenericUnitTaskBuilder<TaskWrapperMethodBuilder, TaskWrapperAwaiter, TaskWrapper, Task>

type ExnResultValueTaskBuilder<'a> =
    GenericTaskBuilder<ExnResultValueTaskMethodBuilder<'a>, ExnResultValueTaskAwaiter<'a>, ExnResultValueTask<'a>, Result<'a, exn>, ExnResultValueTask<'a>>

let inline myValueTask<'a> = Unchecked.defaultof<ValueTaskWrapperBuilder<'a>>
let myUnitValueTask = ValueTaskWrapperBuilder()

let inline myTask<'a> = Unchecked.defaultof<TaskWrapperBuilder<'a>>
let myUnitTask = TaskWrapperBuilder()

let inline myExnResultValueTask<'a> = Unchecked.defaultof<ExnResultValueTaskBuilder<'a>>

[<Fact>]
let ``test that exn result task caches exceptions automatically``() = task {
    let! ok = myExnResultValueTask {
        return Ok 1
    }

    Assert.IsOk(1, ok)

    let! err = myExnResultValueTask {
        failwith "test"
        return Ok 1
    }

    Assert.IsErrorOfType<Exception, _>(err)
    Assert.IsErrorWithMessage("test", err)
}

[<Fact>]
let ``Test array map does not throw with value task CE``() = myValueTask<unit> {
    let w = [| 1 |] |> Array.map (fun w -> w + 1) |> Array.head
    let! x = myTask { return 3 }
    let! y = myValueTask { return 4 }
    let finalResult = w + x + y + 3
    Assert.Equal(11, finalResult)
}

[<Fact>]
let ``Test that non generic value task and task work properly with unittask CE``() = myUnitTask {
    do! Task.CompletedTask
    do! ValueTask()
}

[<Fact>]
let ``Test that non generic value task and task work properly with unitvtask CE``() = myUnitValueTask {
    do! Task.CompletedTask
    do! ValueTask()
}

[<Fact>]
let ``Test that async disposable works properly with vtask CE``() = myValueTask {
    use _ = { new IAsyncDisposable with member _.DisposeAsync() = ValueTask() }
    ()
}

[<Fact>]
let ``Test that async disposable works properly with unitvtask CE``() = myUnitValueTask {
    use _ = { new IAsyncDisposable with member _.DisposeAsync() = ValueTask() }
    ()
}

[<Fact>]
let ``Test that async disposable works properly with task CE``() = myTask {
    use _ = { new IAsyncDisposable with member _.DisposeAsync() = ValueTask() }
    ()
}

[<Fact>]
let ``Test that async disposable works properly with unittask CE``() = myUnitTask {
    use _ = { new IAsyncDisposable with member _.DisposeAsync() = ValueTask() }
    ()
}

[<Fact>]
let ``Test that dispose works with unittask CE``() =
    Assert.ThrowsAsync<ObjectDisposedException>(fun () -> task { return! myUnitTask {
        use _ = { new IDisposable with member _.Dispose() = raise (ObjectDisposedException("")) }
        ()
    }})

[<Fact>]
let ``Test that dispose works with unitvtask CE``() =
    Assert.ThrowsAsync<ObjectDisposedException>(fun () -> task { return! myUnitValueTask {
        use _ = { new IDisposable with member _.Dispose() = raise (ObjectDisposedException("")) }
        ()
    }})

[<Fact>]
let ``Test that dispose works with task CE``() =
    Assert.ThrowsAsync<ObjectDisposedException>(fun () -> task { return! myTask {
        use _ = { new IDisposable with member _.Dispose() = raise (ObjectDisposedException("")) }
        ()
    }})

[<Fact>]
let ``Test that dispose works with vtask CE``() =
    Assert.ThrowsAsync<ObjectDisposedException>(fun () -> task { return! myValueTask {
        use _ = { new IDisposable with member _.Dispose() = raise (ObjectDisposedException("")) }
        ()
    }})