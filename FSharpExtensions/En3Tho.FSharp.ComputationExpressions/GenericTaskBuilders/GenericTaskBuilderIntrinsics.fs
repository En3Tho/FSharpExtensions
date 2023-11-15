namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System.Runtime.CompilerServices
open Microsoft.FSharp.Core.CompilerServices

type StateIntrinsic = struct end

[<AutoOpen>]
module Intrinsics =
    let inline getState() = StateIntrinsic()

type BasicBindExtensions = struct end
type YieldExtensions = struct end
type ReturnExtensions = struct end

type GetStateMachineData<'TStateMachine, 'TData> = delegate of sm: byref<'TStateMachine> -> byref<'TData>

type StateMachineBox<'TStateMachine, 'TData
    when 'TStateMachine :> IAsyncStateMachine
    and 'TStateMachine :> IResumableStateMachine<'TData>>() =

    [<DefaultValue(false)>]
    val mutable StateMachine: 'TStateMachine

    [<DefaultValue(false)>]
    val mutable GetDataRef: GetStateMachineData<'TStateMachine, 'TData>

    member this.Data: byref<'TData> =
        &this.GetDataRef.Invoke(&this.StateMachine)

    interface IAsyncStateMachine with
        member this.MoveNext() = this.StateMachine.MoveNext()
        member this.SetStateMachine(stateMachine) = this.StateMachine.SetStateMachine(stateMachine)

[<Struct>]
type internal EmptyAwaiter =
    interface ICriticalNotifyCompletion with
        member this.OnCompleted(_) = ()
        member this.UnsafeOnCompleted(_) = ()