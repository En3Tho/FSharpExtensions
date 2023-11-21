namespace En3Tho.FSharp.ComputationExpressions.Tasks

// open System
// open System.Runtime.CompilerServices
// open System.Threading

// type MultiAwaiterData() =
//
//     let [<Literal>] InitFlag = 2147483648u
//     let [<Literal>] ClearFlag = 2147483647u
//
//     let mutable counter = InitFlag
//     let mutable proceedAction = Unchecked.defaultof<Action>
//
//     let completionAction = Action(fun () ->
//         if Interlocked.Decrement(&counter) = 0u then
//             proceedAction.Invoke()
//     )
//
//     member _.AddAwaiter(awaiter: #ICriticalNotifyCompletion) =
//         Interlocked.Increment(&counter) |> ignore
//         awaiter.UnsafeOnCompleted(completionAction)
//
//     member this.GetAwaiter() = this
//     member this.GetResult() = ()
//     member this.IsCompleted = Volatile.Read(&counter) = 0u
//
//     interface ICriticalNotifyCompletion with
//         member this.OnCompleted(continuation) =
//             Volatile.Write(&proceedAction, continuation)
//             if Interlocked.And(&counter, ClearFlag) = 0u
//                 then continuation.Invoke()
//
//         member this.UnsafeOnCompleted(continuation) =
//             Volatile.Write(&proceedAction, continuation)
//             if Interlocked.And(&counter, ClearFlag) = 0u
//                 then continuation.Invoke()