namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks

type StateIntrinsic = struct end
type BasicBindExtensions = struct end
type YieldExtensions = struct end
type ReturnExtensions = struct end

[<Struct>]
type TaskBindWrapper<'a>(task: Task<'a>) =
    member this.Task = task
    member inline this.GetAwaiter() = this.Task.GetAwaiter()

[<Struct>]
type internal FakeAwaiter =

    [<DefaultValue(false)>]
    val mutable Continuation: Action

    interface ICriticalNotifyCompletion with
        member this.OnCompleted(continuation) = this.Continuation <- continuation
        member this.UnsafeOnCompleted(continuation) = this.Continuation <- continuation