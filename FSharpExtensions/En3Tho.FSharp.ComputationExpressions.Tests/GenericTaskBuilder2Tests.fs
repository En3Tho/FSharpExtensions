module En3Tho.FSharp.ComputationExpressions.Tests.GenericTaskBuilder2Tests

open System
open System.Collections.Generic
open System.Diagnostics
open System.Threading
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.Tasks2.GenericTaskBuilders

open Xunit
open En3Tho.FSharp.Xunit


module CommonNames =
    let [<Literal>] Delay = "Delay"
    let [<Literal>] Try = "Try"
    let [<Literal>] Finally = "Finally"
    let [<Literal>] Catch = "Catch"
    let [<Literal>] While = "While"
    let [<Literal>] For = "For"
    let [<Literal>] Use = "Use"
    let [<Literal>] Yield = "Yield"

let delay(messages: List<string>) = task {
    do! Task.Delay(1)
    messages.Add(CommonNames.Delay)
}

let test (messages: List<string>) moveNext (ts: IAsyncEnumerable<'T>) = task {
    messages.Clear()
    use e = ts.GetAsyncEnumerator()
    if moveNext then
        let! _ = e.MoveNextAsync()
        ()
    do! Task.Delay(1)
}

let testCatch messages moveNext ts = task {
    try
        do! test messages moveNext ts
    with _ ->
        ()
}

[<Fact>]
let ``test that finally block executes and exception is never thrown if not yielded``() = task {
    let messages = List()

    let ts = taskSeq {
        try
            messages.Add(CommonNames.Try)
            0
            failwith "boom"
        finally
            messages.Add(CommonNames.Finally)
        do! delay(messages)
    }

    do! ts |> test messages false
    Assert.Equal(0, messages.Count)

    do! ts |> test messages true
    Assert.Equal(2, messages.Count)
    Assert.Equal(CommonNames.Try, messages[0])
    Assert.Equal(CommonNames.Finally, messages[1])
}

[<Fact>]
let ``test that finally block executes and message is not added if not yielded``() = task {
    let messages = List()

    let ts = taskSeq {
        try
            0
            messages.Add(CommonNames.Try)
        finally
            messages.Add(CommonNames.Finally)
        do! delay(messages)
    }

    do! ts |> test messages false
    Assert.Equal(0, messages.Count)

    do! ts |> test messages true
    Assert.Equal(1, messages.Count)
    Assert.Equal(CommonNames.Finally, messages[0])
}

[<Fact>]
let ``test how try catch works and sync catch block works even if disposed``() = task {
    let messages = List()

    let ts = taskSeq {
        try
            messages.Add(CommonNames.Try)
            failwith "boom"
        with _ ->
            messages.Add(CommonNames.Catch)
    }

    do! ts |> testCatch messages false
    Assert.Equal(0, messages.Count)

    do! ts |> testCatch messages true
    Assert.Equal(2, messages.Count)
    Assert.Equal(CommonNames.Try, messages[0])
    Assert.Equal(CommonNames.Catch, messages[1])
}

[<Fact>]
let ``test how try catch works and async catch block works even if disposed``() = task {
    let messages = List()

    let ts = taskSeq {
        try
            messages.Add(CommonNames.Try)
            failwith "boom"
        with _ ->
            messages.Add(CommonNames.Catch)
            do! delay(messages)
    }

    do! ts |> testCatch messages false
    Assert.Equal(0, messages.Count)

    do! ts |> testCatch messages true
    Assert.Equal(3, messages.Count)
    Assert.Equal(CommonNames.Try, messages[0])
    Assert.Equal(CommonNames.Catch, messages[1])
    Assert.Equal(CommonNames.Delay, messages[2])
}

[<Fact>]
let ``test how try catch works and exception is never thrown if not yielded``() = task {
    let messages = List()

    let ts = taskSeq {
        try
            messages.Add(CommonNames.Try)
            0
            failwith "boom"
        with _ ->
            messages.Add(CommonNames.Catch)
            do! delay(messages)
    }

    do! ts |> testCatch messages false
    Assert.Equal(0, messages.Count)

    do! ts |> testCatch messages true
    Assert.Equal(1, messages.Count)
    Assert.Equal(CommonNames.Try, messages[0])
}

[<Fact>]
let ``test how try catch works and message is not added if not yielded``() = task {
    let messages = List()

    let ts = taskSeq {
        try
            0
            messages.Add(CommonNames.Try)
        with _ ->
            messages.Add(CommonNames.Catch)
            do! delay(messages)
    }

    do! ts |> testCatch messages false
    Assert.Equal(0, messages.Count)

    do! ts |> testCatch messages true
    Assert.Equal(0, messages.Count)
}

[<Fact>]
let ``test how try catch works and catch block is properly executed even if disposed``() = task {
    let messages = List()

    let ts = taskSeq {
        try
            failwith "woops"
            messages.Add(CommonNames.Try)
        with e ->
            messages.Add(CommonNames.Catch)
            do! delay(messages)
    }

    do! ts |> testCatch messages false
    Assert.Equal(0, messages.Count)

    do! ts |> testCatch messages true
    Assert.Equal(2, messages.Count)
    Assert.Equal(CommonNames.Catch, messages[0])
    Assert.Equal(CommonNames.Delay, messages[1])
}

[<Fact>]
let ``test how while works``() = task {
    let messages = List()

    let ts = taskSeq {
        let mutable i = 0
        while i < 10 do
            messages.Add(CommonNames.While)
            i
            do! delay(messages)
    }

    do! ts |> test messages false
    Assert.Equal(0, messages.Count)

    do! ts |> test messages true
    Assert.Equal(1, messages.Count)
    Assert.Equal(CommonNames.While, messages[0])
}

[<Fact>]
let ``test how for works``() = task {
    let messages = List()

    let ts = taskSeq {
        for i = 0 to 10 do
            messages.Add(CommonNames.For)
            i
            do! delay(messages)
    }

    do! ts |> test messages false
    Assert.Equal(0, messages.Count)

    do! ts |> test messages true
    Assert.Equal(1, messages.Count)
    Assert.Equal(CommonNames.For, messages[0])
}

[<Fact>]
let ``test inner fields of iasyncenumerable are reinitialized properly``() = task {
    let messages = List()

    let ts = taskSeq {
        let list = List()
        for i = 0 to 9 do
            list.Add(i)
        list.Count
        do! delay(messages)
    }

    let testInnerFields() = vtask {
        let mutable x = 0
        for i in ts do
            x <- x + i

        Assert.Equal(10, x)
    }

    do! testInnerFields()
    do! testInnerFields()
}

[<Fact>]
let ``test inner fields with capture of iasyncenumerable are reinitialized properly``() = task {
    let messages = List()

    let ts(initial) = taskSeq {
        let mutable initial = initial
        let list = List()
        for i = 0 to 9 do
            list.Add(i)
        initial <- list.Count
        initial
        do! delay(messages)
    }

    let testInnerFields() = vtask {
        let mutable x = 0
        for i in ts(0) do
            x <- x + i

        Assert.Equal(10, x)
    }

    do! testInnerFields()
    do! testInnerFields()
}

[<Fact>]
let ``test inner fields with capture of iasyncenumerable are reinitialized properly2``() = task {
    let messages = List()

    let ts(initial) = taskSeq {
        let mutable initial = initial
        let list = List()
        for i = 0 to 9 do
            list.Add(i)

        do! delay(messages)
        initial <- initial + list.Count
        initial
        do! delay(messages)
    }

    let testInnerFields(ts: IAsyncEnumerable<int>) = vtask {
        let mutable x = 0
        for i in ts do
            x <- x + i

        Assert.Equal(10, x)
    }

    let ts = ts(0)
    do! testInnerFields(ts)
    do! testInnerFields(ts)
}

[<Fact>]
let ``test use won't run if not yielded``() = task {
    let messages = List()

    let ts(x: IDisposable) = taskSeq {
        0
        use x = x
        do! delay(messages)
    }

    let d = { new IDisposable with member _.Dispose() = messages.Add(CommonNames.Use) }

    do! ts(d) |> test messages false
    Assert.Equal(0, messages.Count)

    do! ts(d) |> test messages true
    Assert.Equal(0, messages.Count)
}

[<Fact>]
let ``test sync use will run if yielded``() = task {
    let messages = List()

    let ts(x: IDisposable) = taskSeq {
        use x = x
        0
        do! delay(messages)
    }

    let d = { new IDisposable with member _.Dispose() = messages.Add(CommonNames.Use) }

    do! ts(d) |> test messages false
    Assert.Equal(0, messages.Count)

    do! ts(d) |> test messages true
    Assert.Equal(1, messages.Count)
    Assert.Equal(CommonNames.Use, messages[0])
}

[<Fact>]
let ``test async use will run if yielded``() = task {
    let messages = List()

    let ts(x: IAsyncDisposable) = taskSeq {
        use x = x
        0
        do! delay(messages)
    }

    let d() = ValueTask(task {
            do! Task.Delay(1)
            messages.Add(CommonNames.Use)
        })

    let d = { new IAsyncDisposable with member _.DisposeAsync() = d() }

    do! ts(d) |> test messages false
    Assert.Equal(0, messages.Count)

    do! ts(d) |> test messages true
    Assert.Equal(1, messages.Count)
    Assert.Equal(CommonNames.Use, messages[0])
}

[<Fact>]
let ``test that simple task seq can be partially consumed``() = task {

    let messages = List()

    let ts = taskSeq {
        messages.Add(CommonNames.Yield)
        1
        do! delay(messages)

        messages.Add(CommonNames.Yield)
        2
        do! delay(messages)

        messages.Add(CommonNames.Yield)
        3
        do! delay(messages)
    }

    let! res = vtask {
        let mutable result = 0
        use e = ts.GetAsyncEnumerator()
        let mutable i = 2
        while i > 0 do
            let! _ = e.MoveNextAsync()
            result <- result + e.Current
            i <- i - 1
        return result
    }

    Assert.Equal(3, messages.Count)
    Assert.Equal(CommonNames.Yield, messages[0])
    Assert.Equal(CommonNames.Delay, messages[1])
    Assert.Equal(CommonNames.Yield, messages[0])
    Assert.Equal(3, res)
}

[<Fact>]
let ``test that simple task seq works``() = task {

    let ts = taskSeq {
        1
        do! Task.Delay(1)
        2
        do! unitvtask { do! Task.Delay(1) }
        3
    }

    let! res = vtask {
        let mutable result = 0
        for v in ts do
            result <- result + v
        return result
    }

    Assert.Equal(6, res)

    let! res = vtask {
        let mutable result = 0
        for v in ts do
            result <- result + v
        return result
    }

    Assert.Equal(6, res)

    let! res = vtask {
        let mutable result = 0
        for v in ts do
            result <- result + v
        return result
    }

    Assert.Equal(6, res)
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

    use initial = source.StartActivity("Test")

    do! activityTask "NewOne" {
        let activityFromCurrent = Activity.Current
        let! activityFromState = getState()
        Assert.True(Object.ReferenceEquals(activityFromState, activityFromCurrent))
    }
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
let ``test that synccontext task works for default sync context``() = task {

    let testSyncContext = SynchronizationContext.Current
    // make sure we have a test sync context here
    Assert.True(not (Object.ReferenceEquals(null, testSyncContext)))

    // default is sending to the thread pool
    let syncContext = SynchronizationContext()
    let sncTask = syncCtxTask(syncContext)

    let! x = sncTask {
        // assert we've jumped
        Assert.False(Object.ReferenceEquals(testSyncContext, SynchronizationContext.Current))
        Assert.Equal(null, SynchronizationContext.Current)

        do! Task.Delay(1)
        Assert.Equal(null, SynchronizationContext.Current)
        return 1
    }

    Assert.Equal(1, x)
}

type BoolBox() =
    let mutable value = true
    member _.Value = value

    interface IDisposable with
        member _.Dispose() = value <- false

[<Fact>]
let ``test that synccontext task works for custom sync context``() = task {

    let collection = Queue()

    use spinThead = BoolBox()

    let singleThreadSyncContext = {
        new SynchronizationContext() with
            member _.Post(cb, o) = collection.Enqueue((cb, o))
    }

    let th = Thread(fun() ->
        SynchronizationContext.SetSynchronizationContext(singleThreadSyncContext)
        while spinThead.Value do
            Thread.Sleep(1)
            match collection.TryDequeue() with
            | true, (cb, o) ->
                cb.Invoke(o)
            | _ ->
                ()
    )

    th.Start()

    let sncTask = syncCtxTask(singleThreadSyncContext)

    let testSyncContext = SynchronizationContext.Current

    let _ = sncTask {
        Assert.True(Object.ReferenceEquals(singleThreadSyncContext, SynchronizationContext.Current))
    }

    // test that snc task does not change the context just by the fact of running
    Assert.True(Object.ReferenceEquals(testSyncContext, SynchronizationContext.Current))

    let! x = sncTask {
        Assert.True(Object.ReferenceEquals(singleThreadSyncContext, SynchronizationContext.Current))
        let! x = task {
            do! Task.Delay(5)
            return 1
        }
        Assert.True(Object.ReferenceEquals(singleThreadSyncContext, SynchronizationContext.Current))
        return x
    }

    Assert.Equal(1, x)
}

[<Fact>]
let ``test that synccontext task works for multiple custom sync contexts``() = task {

    use spinThead = BoolBox()
    let collection1 = Queue()

    let singleThreadSyncContext1 = {
        new SynchronizationContext() with
            member _.Post(cb, o) = collection1.Enqueue((cb, o))
    }

    let th1 = Thread(fun() ->
        SynchronizationContext.SetSynchronizationContext(singleThreadSyncContext1)
        while spinThead.Value do
            Thread.Sleep(1)
            match collection1.TryDequeue() with
            | true, (cb, o) ->
                cb.Invoke(o)
            | _ ->
                ()
    )

    let collection2 = Queue()

    let singleThreadSyncContext2 = {
        new SynchronizationContext() with
            member _.Post(cb, o) = collection2.Enqueue((cb, o))
    }

    let th2 = Thread(fun() ->
        SynchronizationContext.SetSynchronizationContext(singleThreadSyncContext2)
        while spinThead.Value do
            Thread.Sleep(1)
            match collection2.TryDequeue() with
            | true, (cb, o) ->
                cb.Invoke(o)
            | _ ->
                ()
    )

    th1.Start()
    th2.Start()

    let sncTask1 = syncCtxTask(singleThreadSyncContext1)
    let sncTask2 = syncCtxTask(singleThreadSyncContext2)

    let! x = sncTask1 {
        Assert.True(Object.ReferenceEquals(singleThreadSyncContext1, SynchronizationContext.Current))
        do! Task.Delay(5)
        Assert.True(Object.ReferenceEquals(singleThreadSyncContext1, SynchronizationContext.Current))

        let! y = sncTask2 {
            Assert.True(Object.ReferenceEquals(singleThreadSyncContext2, SynchronizationContext.Current))
            do! Task.Delay(5)
            Assert.True(Object.ReferenceEquals(singleThreadSyncContext2, SynchronizationContext.Current))
            return 1
        }

        Assert.True(Object.ReferenceEquals(singleThreadSyncContext1, SynchronizationContext.Current))

        let! z = backgroundTask {
            Assert.True(Object.ReferenceEquals(null, SynchronizationContext.Current))
            do! Task.Delay(5)
            Assert.True(Object.ReferenceEquals(null, SynchronizationContext.Current))
            return 1
        }

        Assert.True(Object.ReferenceEquals(singleThreadSyncContext1, SynchronizationContext.Current))

        return y + z
    }

    Assert.Equal(2, x)
}

[<AbstractClass; Sealed>]
type TestAsyncLocal() =

    [<DefaultValue(false)>]
    static val mutable private s_X: AsyncLocal<int>

    static do
        TestAsyncLocal.s_X <- AsyncLocal()

    static member X = TestAsyncLocal.s_X


[<Fact>]
let ``test that synccontext task propagates execution context just like default tasks``() = task {

    use spinThead = BoolBox()

    let collection = Queue()
    let singleThreadSyncContext = {
        new SynchronizationContext() with
            member _.Post(cb, o) = collection.Enqueue((cb, o))
    }

    let th = Thread(fun() ->
        SynchronizationContext.SetSynchronizationContext(singleThreadSyncContext)
        while spinThead.Value do
            Thread.Sleep(1)
            match collection.TryDequeue() with
            | true, (cb, o) ->
                cb.Invoke(o)
            | _ ->
                ()
    )

    th.Start()

    let sncTask = syncCtxTask(singleThreadSyncContext)

    TestAsyncLocal.X.Value <- 1

    let! y = sncTask {
        return TestAsyncLocal.X.Value
    }

    Assert.Equal(1, y)

    do! sncTask {
        TestAsyncLocal.X.Value <- 2

        do! backgroundTask {
            do! Task.Delay(1)
            Assert.Equal(2, TestAsyncLocal.X.Value)
        }

    }
}

// just for the reference to myself
[<Fact>]
let ``test that background task propagates execution context just like default tasks``() = task {
    TestAsyncLocal.X.Value <- 1

    let! y = backgroundTask {
        return TestAsyncLocal.X.Value
    }

    Assert.Equal(1, y)

    do! backgroundTask {
        TestAsyncLocal.X.Value <- 2

        do! backgroundTask {
            do! Task.Delay(1)
            Assert.Equal(2, TestAsyncLocal.X.Value)
        }
    }
}

[<Fact>]
let ``test that etasks automatically catches exceptions``() = task {
    let! x = etask {
        do! Task.Delay(1)
        failwith "Boom"
        return Ok 1
    }

    Assert.IsErrorWithMessage("Boom", x)
}

[<Fact>]
let ``test that evtasks automatically catches exceptions``() = task {
    let! x = evtask {
        do! Task.Delay(1)
        failwith "Boom"
        return Ok 1
    }

    Assert.IsErrorWithMessage("Boom", x)
}