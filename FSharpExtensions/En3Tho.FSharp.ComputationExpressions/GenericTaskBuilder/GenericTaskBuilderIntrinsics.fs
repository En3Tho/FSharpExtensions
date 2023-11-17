namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks

type StateIntrinsic = struct end

// these indicate default extensions for "straightforward" task builders
type IBindExtensions = interface end
type IYieldExtensions = interface end
type IReturnExtensions = interface end

module StateMachineCodes =
    let [<Literal>] Finished = -1
    let [<Literal>] ShouldStop = -2

[<Struct>]
type TaskBindWrapper<'a>(task: Task<'a>) =
    member this.Task = task
    member inline this.GetAwaiter(): TaskAwaiter<'a> = this.Task.GetAwaiter()

[<Struct>]
type internal FakeAwaiter =

    [<DefaultValue(false)>]
    val mutable Continuation: Action

    interface ICriticalNotifyCompletion with
        member this.OnCompleted(continuation) = this.Continuation <- continuation
        member this.UnsafeOnCompleted(continuation) = this.Continuation <- continuation