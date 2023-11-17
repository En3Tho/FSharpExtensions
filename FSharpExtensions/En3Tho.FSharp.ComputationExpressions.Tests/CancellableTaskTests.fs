module En3Tho.FSharp.ComputationExpressions.Tests.CancellableTaskTests

open System
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