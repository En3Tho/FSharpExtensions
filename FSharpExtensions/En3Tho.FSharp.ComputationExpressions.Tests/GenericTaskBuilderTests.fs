module En3Tho.FSharp.ComputationExpressions.Tests.GenericTaskBuilderTests

open System
open System.Collections.Generic
open System.Diagnostics
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.Xunit

open GenericTaskBuilderExtensionsLowPriority
open GenericTaskBuilderBasicBindExtensionsLowPriority
open GenericTaskBuilderBasicBindExtensionsMediumPriority
open GenericTaskBuilderBasicBindExtensionsHighPriority

open GenericUnitTaskBuilderExtensionsLowPriority
open GenericUnitTaskBuilderBasicBindExtensionsLowPriority
open GenericUnitTaskBuilderBasicBindExtensionsMediumPriority
open GenericUnitTaskBuilderBasicBindExtensionsHighPriority

open Xunit

type ValueTaskWrapperBuilder<'a>() =
    inherit GenericTaskBuilder<ValueTaskWrapperMethodBuilder<'a>, ValueTaskWrapperAwaiter<'a>, ValueTaskWrapper<'a>, 'a, ValueTask<'a>, IGenericTaskBuilderBasicBindExtensions>()

type ValueTaskWrapperBuilder() =
    inherit GenericUnitTaskBuilder<ValueTaskWrapperMethodBuilder, ValueTaskWrapperAwaiter, ValueTaskWrapper, ValueTask, IGenericUnitTaskBuilderBasicBindExtensions>()

type TaskWrapperBuilder<'a>() =
    inherit GenericTaskBuilder<TaskWrapperMethodBuilder<'a>, TaskWrapperAwaiter<'a>, TaskWrapper<'a>, 'a, Task<'a>, IGenericTaskBuilderBasicBindExtensions>()

type TaskWrapperBuilder() =
    inherit GenericUnitTaskBuilder<TaskWrapperMethodBuilder, TaskWrapperAwaiter, TaskWrapper, Task, IGenericUnitTaskBuilderBasicBindExtensions>()

type ActivityValueTaskBuilder<'a>(activityName) =
    inherit GenericTaskBuilderWithState<ActivityValueTaskMethodBuilder<'a>, ActivityValueTaskAwaiter<'a>, ActivityValueTask<'a>, 'a, ActivityValueTask<'a>, string, IGenericTaskBuilderBasicBindExtensions>(activityName)

type ActivityTaskBuilder<'a>(activityName) =
    inherit GenericTaskBuilderWithState<ActivityTaskMethodBuilder<'a>, ActivityTaskAwaiter<'a>, ActivityTask<'a>, 'a, ActivityTask<'a>, string, IGenericTaskBuilderBasicBindExtensions>(activityName)

type ExnResultValueTaskBuilder<'a>() = // TODO: non-basic binds
    inherit GenericTaskBuilder<ExnResultValueTaskMethodBuilder<'a>, ExnResultValueTaskAwaiter<'a>, ExnResultValueTask<'a>, Result<'a, exn>, ExnResultValueTask<'a>, IGenericTaskBuilderBasicBindExtensions>()

let inline myValueTask<'a> = Unchecked.defaultof<ValueTaskWrapperBuilder<'a>>
let myUnitValueTask = ValueTaskWrapperBuilder()

let inline myTask<'a> = Unchecked.defaultof<TaskWrapperBuilder<'a>>
let myUnitTask = TaskWrapperBuilder()

let inline myExnResultValueTask<'a> = Unchecked.defaultof<ExnResultValueTaskBuilder<'a>>

let inline myActivityValueTask activityName = ActivityValueTaskBuilder(activityName)
let inline myActivityTask activityName = ActivityTaskBuilder<'a>(activityName)

[<Fact>]
let ``test that most basic return works with task``() = task {
    let! result = myValueTask {
        return 1
    }
    Assert.Equal(1, result)

    let! result = myTask {
        return 1
    }
    Assert.Equal(1, result)
}

[<Fact>]
let ``test that most basic return works with mytask``() = myTask<unit> {
    let! result = myValueTask {
        return 1
    }
    Assert.Equal(1, result)

    let! result = myTask {
        return 1
    }
    Assert.Equal(1, result)
}

[<Fact>]
let ``test that most basic return works from results``() = task {
    let! result = ValueTaskWrapper<int>(ValueTask.FromResult(1))
    Assert.Equal(1, result)

    let! result = TaskWrapper<int>(Task.FromResult(1))
    Assert.Equal(1, result)
}

[<Fact>]
let ``test that activity task works``() = task {
    use source = ActivitySource("mySource")
    use listener = ActivityListener(
        ShouldListenTo = (fun _ -> true),
        Sample = (fun _ -> ActivitySamplingResult.AllData)
    )
    ActivitySource.AddActivityListener(listener)

    use _ = source.StartActivity("Test")

    let activity = Activity.Current
    Assert.NotNull(activity)
    Assert.Equal("Test", activity.DisplayName)

    let mutable activityFromTask = null
    let! result = myActivityTask "NewOne" {

        let activity = Activity.Current
        Assert.NotNull(activity)
        Assert.Equal("NewOne", activity.DisplayName)

        activityFromTask <- activity

        return 1
    }

    Assert.Equal(1, result)
    Assert.True(activityFromTask.IsStopped)
    Assert.True(activityFromTask.Status = ActivityStatusCode.Ok)
}

[<Fact>]
let ``test that activity task sets activity error code``() = task {
    use source = ActivitySource("mySource")
    use listener = ActivityListener(
        ShouldListenTo = (fun _ -> true),
        Sample = (fun _ -> ActivitySamplingResult.AllData)
    )
    ActivitySource.AddActivityListener(listener)

    use _ = source.StartActivity("Test")

    let mutable activityFromTask = null

    let! _ = myExnResultValueTask {
        let! _ = myActivityTask "NewOne" {
            activityFromTask <- Activity.Current
            failwith "boom"
        }
        return Ok()
    }

    Assert.True(activityFromTask.IsStopped)
    Assert.True(activityFromTask.Status = ActivityStatusCode.Error)
    Assert.Equal("boom", activityFromTask.StatusDescription)
}

[<Fact>]
let ``test that activity value task works``() = task {
    use source = ActivitySource("mySource")
    use listener = ActivityListener(
        ShouldListenTo = (fun _ -> true),
        Sample = (fun _ -> ActivitySamplingResult.AllData)
    )
    ActivitySource.AddActivityListener(listener)

    use _ = source.StartActivity("Test")

    let activity = Activity.Current
    Assert.NotNull(activity)
    Assert.Equal("Test", activity.DisplayName)

    let! result = myActivityValueTask "NewOne" {

        let activity = Activity.Current
        Assert.NotNull(activity)
        Assert.Equal("NewOne", activity.DisplayName)

        return 1
    }
    Assert.Equal(1, result)
}

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

type MyAsyncEnumerator(count: int, dispose: unit -> ValueTask) =
    let mutable counter = 0

    interface IAsyncEnumerator<int> with
        member this.DisposeAsync() = dispose()
        member this.MoveNextAsync() = myValueTask {
            if counter < count then
                counter <- counter + 1
                do! Task.Delay(1)
                return true
            else
                return false
        }

        member this.Current = counter

type MyAsyncEnumerable(count, dispose) =

    interface IAsyncEnumerable<int> with
        member this.GetAsyncEnumerator(_) = MyAsyncEnumerator(count, dispose)

[<Fact>]
let ``test IAsyncEnumerable support for generic task builder``() = task {
    let count = 10
    let mutable testCounter = 0

    let dispose() =
        testCounter <- testCounter + 1
        ValueTask.CompletedTask

    do! myTask {
        for _ in MyAsyncEnumerable(count, dispose) do
            testCounter <- testCounter + 1
    }

    Assert.Equal(11, testCounter)

    let! testCounterCopy = myTask {
        for _ in MyAsyncEnumerable(count, dispose) do
            testCounter <- testCounter + 1

        return testCounter
    }

    Assert.Equal(22, testCounterCopy)
    Assert.Equal(22, testCounter)
}

[<Fact>]
let ``test IAsyncEnumerable support for generic unit task builder``() = task {
    let count = 10
    let mutable testCounter = 0

    let dispose() =
        testCounter <- testCounter + 1
        ValueTask.CompletedTask

    do! myUnitTask {
        for _ in MyAsyncEnumerable(count, dispose) do
            testCounter <- testCounter + 1
    }

    Assert.Equal(11, testCounter)
}