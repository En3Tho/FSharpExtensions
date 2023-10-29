module En3Tho.FSharp.ComputationExpressions.Tests.GenericTaskBuilderTests

open System
open System.Collections.Generic
open System.Diagnostics
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
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

type ValueTaskWrapperBuilder() =
    inherit GenericTaskBuilder<BasicBindExtensions>()
    member inline this.Run([<InlineIfLambda>] code: GenericTaskCode<ValueTaskWrapperMethodBuilder<'a>, _, _, _, _>) =
       this.RunInternal(code)

type UnitValueTaskWrapperBuilder() =
    inherit GenericUnitTaskBuilder<BasicBindExtensions>()
    member inline this.Run([<InlineIfLambda>] code: GenericUnitTaskCode<ValueTaskWrapperMethodBuilder, _, _, _>) =
        this.RunInternal(code)

type TaskWrapperBuilder() =
    inherit GenericTaskBuilder<BasicBindExtensions>()
    member inline this.Run([<InlineIfLambda>] code: GenericTaskCode<TaskWrapperMethodBuilder<'a>, _, _, _, _>) =
        this.RunInternal(code)

type UnitTaskWrapperBuilder() =
    inherit GenericUnitTaskBuilder<BasicBindExtensions>()
    member inline this.Run([<InlineIfLambda>] code: GenericUnitTaskCode<TaskWrapperMethodBuilder, _, _, _>) =
        this.RunInternal(code)

type ActivityValueTaskBuilder(activity) =
    inherit GenericTaskBuilderWithState<BasicBindExtensions, Activity, IgnoreStateCheck<Activity>>(activity)
    member inline this.Run([<InlineIfLambda>] code: GenericTaskCode<ActivityValueTaskMethodBuilder<'a>, _, _, _, _>) =
        this.RunInternal(code)

type ActivityUnitValueTaskBuilder(activity) =
    inherit GenericUnitTaskBuilderWithState<BasicBindExtensions, Activity, IgnoreStateCheck<Activity>>(activity)
    member inline this.Run([<InlineIfLambda>] code: GenericUnitTaskCode<ActivityValueTaskMethodBuilder, _, _, _>) =
        this.RunInternal(code)

type ActivityTaskBuilder(activity) =
    inherit GenericTaskBuilderWithState<BasicBindExtensions, Activity, IgnoreStateCheck<Activity>>(activity)
    member inline this.Run([<InlineIfLambda>] code: GenericTaskCode<ActivityTaskMethodBuilder<'a>, _, _, _, _>) =
        this.RunInternal(code)

type ActivityUnitTaskBuilder(activity) =
    inherit GenericUnitTaskBuilderWithState<BasicBindExtensions, Activity, IgnoreStateCheck<Activity>>(activity)
    member inline this.Run([<InlineIfLambda>] code: GenericUnitTaskCode<ActivityTaskMethodBuilder, _, _, _>) =
        this.RunInternal(code)

type ExnResultValueTaskBuilder() =
    inherit GenericTaskBuilder<BasicBindExtensions>()
    member inline this.Run([<InlineIfLambda>] code: GenericTaskCode<ExnResultValueTaskMethodBuilder<'a>, _, _, _, _>) =
        this.RunInternal(code)

let myValueTask = ValueTaskWrapperBuilder()
let myUnitValueTask = UnitValueTaskWrapperBuilder()
let myTask = TaskWrapperBuilder()
let myUnitTask = UnitTaskWrapperBuilder()

let myExnResultValueTask = ExnResultValueTaskBuilder()

[<AbstractClass; Sealed; AutoOpen>]
type ActivityBuilders() =
    static member inline activityValueTask(activity) = ActivityValueTaskBuilder(activity)
    static member inline activityValueTask([<CallerMemberName; Optional; DefaultParameterValue("")>] activityName: string) =

        let activity =
            match Activity.Current with
            | null -> null
            | activity ->
                activity.Source.StartActivity(activityName)

        activityValueTask(activity)

    static member inline activityTask(activity) = ActivityTaskBuilder(activity)
    static member inline activityTask([<CallerMemberName; Optional; DefaultParameterValue("")>] activityName: string) =

        let activity =
            match Activity.Current with
            | null -> null
            | activity ->
                activity.Source.StartActivity(activityName)

        activityTask(activity)

// unit auto inference test as xunit doesn't allow generic methods
[<Fact>]
let ``test that unit is inferred for myTask same as task if no return is called``() = myTask {
    let! _ = task { return 1 }
    Assert.True(true)
}

// unit auto inference test as xunit doesn't allow generic methods
[<Fact>]
let ``test that unit is inferred for myValueTask same as task if no return is called``() = myValueTask {
    let! _ = task { return 1 }
    Assert.True(true)
}

[<Fact>]
let ``test that this thing works``() = task {
    let! z = myValueTask {
        return 1
    }

    Assert.Equal(1, z)
}

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
let ``test that most basic return works with mytask``() = myTask {
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
    let! result = activityTask "NewOne" {

        let activity = Activity.Current
        Assert.NotNull(activity)
        Assert.Equal("NewOne", activity.DisplayName)

        let! activityState = getState()
        Assert.Equal(activityState, activity)

        activityFromTask <- activity

        return 1
    }

    Assert.Equal(1, result)
    Assert.True(activityFromTask.IsStopped)
    Assert.True(activityFromTask.Status = ActivityStatusCode.Ok)
}

[<Fact>]
let ``test that activity task returns correct activity as state``() = task {
    use source = ActivitySource("mySource")
    use listener = ActivityListener(
        ShouldListenTo = (fun _ -> true),
        Sample = (fun _ -> ActivitySamplingResult.AllData)
    )
    ActivitySource.AddActivityListener(listener)

    use _ = source.StartActivity("Test")

    do! activityTask "NewOne" {
        let activityFromCurrent = Activity.Current
        let! activityFromState = getState()
        Assert.True(Object.ReferenceEquals(activityFromState, activityFromCurrent))
    }
}

module TestCallerMemberName =
    let startActivity() =
        activityTask() {
        let! activityFromState = getState()
        Assert.Equal("startActivity", activityFromState.DisplayName)
    }

[<Fact>]
let ``test that caller member name works with activity builder``() = task {

    use source = ActivitySource("mySource")
    use listener = ActivityListener(
        ShouldListenTo = (fun _ -> true),
        Sample = (fun _ -> ActivitySamplingResult.AllData)
    )
    ActivitySource.AddActivityListener(listener)

    use _ = source.StartActivity("Test")

    do! TestCallerMemberName.startActivity()
}

[<Fact>]
let ``test that activity task returns provided activity as state and stops provided activity``() = task {
    use source = ActivitySource("mySource")
    use listener = ActivityListener(
        ShouldListenTo = (fun _ -> true),
        Sample = (fun _ -> ActivitySamplingResult.AllData)
    )
    ActivitySource.AddActivityListener(listener)

    let testActivity = source.StartActivity("Test")

    do! activityTask testActivity {
        let activityFromCurrent = Activity.Current
        let! activityFromState = getState()
        Assert.True(Object.ReferenceEquals(testActivity, activityFromState))
        Assert.True(Object.ReferenceEquals(activityFromState, activityFromCurrent))
    }

    Assert.True(testActivity.IsStopped)
    Assert.True(testActivity.Status = ActivityStatusCode.Ok)
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
        let! _ = activityTask "NewOne" {
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

    let! result = activityValueTask "NewOne" {

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
let ``Test array map does not throw with value task CE``() = myValueTask {
    let w = [| 1 |] |> Array.map (fun w -> w + 1) |> Array.head
    let! x = myTask { return 3 }
    let! y = myValueTask { return 4 }
    let finalResult = w + x + y + 3
    Assert.Equal(11, finalResult)
}

[<Fact>]
let ``Test that non generic value task and task work properly with unittask CE``() = myUnitTask {
    do! task { () }
    do! Task.CompletedTask
    do! ValueTask()
}

[<Fact>]
let ``Test that non generic value task and task work properly with unitvtask CE``() = myUnitValueTask {
    do! task { () }
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

    let dispose() = ValueTask(task = task {
        testCounter <- testCounter + 1
        do! Task.Delay(1)
    })

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
let ``test IAsyncEnumerable support for generic task builder fails if exception is thrown in finally``() = task {
    let count = 10
    let mutable testCounter = 0

    let dispose() = ValueTask(task = task {
        do! Task.Delay(1)
        return failwith "abc"
    })

    let fail() = myUnitTask {
        for _ in MyAsyncEnumerable(count, dispose) do
            testCounter <- testCounter + 1
    }

    do! Assert.ThrowsAsync<Exception>(fail) :> Task

    let fail() =
        myTask {
            for _ in MyAsyncEnumerable(count, dispose) do
                testCounter <- testCounter + 1
        } :> Task

    do! Assert.ThrowsAsync<Exception>(fail) :> Task
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