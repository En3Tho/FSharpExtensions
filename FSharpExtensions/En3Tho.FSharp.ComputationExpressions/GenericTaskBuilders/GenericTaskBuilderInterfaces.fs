namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System.Runtime.CompilerServices
open System.Threading

type StateIntrinsic = struct end

[<AutoOpen>]
module Intrinsics =
    let inline getState() = StateIntrinsic()

type BasicBindExtensions = struct end
type YieldExtensions = struct end
type ReturnExtensions = struct end

type internal StateMachineBox<'TStateMachine when 'TStateMachine :> IAsyncStateMachine>() =

    [<DefaultValue(false)>]
    val mutable StateMachine: 'TStateMachine

    // TODO: UnsafeAccessor when generic support is added
    [<DefaultValue(false)>]
    val mutable DataFieldOffset: nativeint

    interface IAsyncStateMachine with
        member this.MoveNext() = this.StateMachine.MoveNext()
        member this.SetStateMachine(stateMachine) = this.StateMachine.SetStateMachine(stateMachine)

    interface IThreadPoolWorkItem with
        member this.Execute() = this.StateMachine.MoveNext()

module internal UnsafeEx =
    type Unsafe with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member ByteOffset<'T, 'U>(origin: byref<'T>, target: byref<'U>) =
            Unsafe.ByteOffset(&Unsafe.As<'T, byte>(&origin), &Unsafe.As<'U, byte>(&target))

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member As<'T, 'U>(origin: byref<'T>, offset: nativeint) =
            Unsafe.As<'T, 'U>(&Unsafe.Add(&origin, offset))

        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        static member Return<'T>(origin: byref<'T>) = &origin