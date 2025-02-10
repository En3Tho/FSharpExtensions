module En3Tho.FSharp.ComputationExpressions.Tests.CancellableTaskTests

open System
open System.Collections.Generic
open System.Threading
open System.Threading.Tasks
open Xunit
open En3Tho.FSharp.ComputationExpressions.Tasks

[<Fact>]
let ``test that cancellable task cancels in move next calls``() = task {
    use cts = CancellationTokenSource()
    cts.CancelAfter(5)

    let stateCancellableTask() = cancellableTask cts.Token {
        do! Task.Delay(50)
        do! Task.Delay(50)
    }

    let cancellable = stateCancellableTask()

    let f() =
        task {
            let! _ = cancellable
            ()
        } :> Task

    return! Assert.ThrowsAsync<OperationCanceledException>(f)
}

[<Fact>]
let ``test that cancellable task propagates cancellation token correctly``() = task {
    use cts = CancellationTokenSource()

    let stateCancellableTask() = cancellableTask cts.Token {
        let! token = getState()
        do! Task.Delay(5, token)
        do! Task.Delay(5, token)
    }

    let cancellable = stateCancellableTask()

    cts.Cancel()

    let f() =
        task {
            let! _ = cancellable
            ()
        } :> Task

    return! Assert.ThrowsAsync<TaskCanceledException>(f)
}

[<Fact>]
let ``Test that disposables will be disposed even if task is canceled``() = task {
    use cts = CancellationTokenSource()
    let messages = List()

    let cancellable = cancellableTask (cts.Token) {
        use _ = { new IDisposable with member _.Dispose() = messages.Add("Disposed") } // should be there
        cts.Cancel()
        use _ = { new IAsyncDisposable with
                    member _.DisposeAsync() =
                        uvtask {
                            do! Task.Delay(50)
                            messages.Add("DisposedAsync")
                        } } // should be there
        try
            Console.WriteLine("Try")
        finally
            Console.WriteLine("Finally")
    }

    let f() =
        task {
            let! _ = cancellable
            ()
        } :> Task

    let! _ = Assert.ThrowsAsync<OperationCanceledException>(f)
    Assert.Equal(2, messages.Count)
    Assert.Equal("DisposedAsync", messages[0])
    Assert.Equal("Disposed", messages[1])
}

[<Fact>]
let ``Test that disposables will be disposed even if task is canceled 2``() = task {
    use cts = CancellationTokenSource()
    let messages = List()

    let cancellable = cancellableTask (cts.Token) {
        cts.Cancel()
        use _ = { new IDisposable with member _.Dispose() = messages.Add("Disposed") } // should be there
        use _ = { new IAsyncDisposable with
                    member _.DisposeAsync() =
                        uvtask {
                            do! Task.Delay(50)
                            messages.Add("DisposedAsync")
                        } } // should be there
        try
            Console.WriteLine("Try")
        finally
            Console.WriteLine("Finally")
    }

    let f() =
        task {
            let! _ = cancellable
            ()
        } :> Task

    let! _ = Assert.ThrowsAsync<OperationCanceledException>(f)
    Assert.Equal(1, messages.Count)
    Assert.Equal("Disposed", messages[0])
}
