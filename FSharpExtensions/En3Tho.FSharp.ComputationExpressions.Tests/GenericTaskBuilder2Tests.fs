module En3Tho.FSharp.ComputationExpressions.Tests.GenericTaskBuilder2Tests

open System
open System.Collections.Generic
open System.Linq.Expressions
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder2
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2.Tasks

open GenericTaskBuilderExtensionsLowPriority
open GenericTaskBuilder2ExtensionsMediumPriority
open GenericTaskBuilder2ExtensionsHighPriority

open Xunit

let vtask2 = ValueTaskBuilder2()
let unitvtask2 = UnitValueTaskBuilder2()
let taskSeq = TaskSeqBuilder()

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

    let testInnerFields() = vtask2 {
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

    let testInnerFields() = vtask2 {
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

    let testInnerFields(ts: IAsyncEnumerable<int>) = vtask2 {
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

    let! res = vtask2 {
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
        do! unitvtask2 { do! Task.Delay(1) }
        3
    }

    let! res = vtask2 {
        let mutable result = 0
        for v in ts do
            result <- result + v
        return result
    }

    Assert.Equal(6, res)

    let! res = vtask2 {
        let mutable result = 0
        for v in ts do
            result <- result + v
        return result
    }

    Assert.Equal(6, res)

    let! res = vtask2 {
        let mutable result = 0
        for v in ts do
            result <- result + v
        return result
    }

    Assert.Equal(6, res)
}