﻿namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices

type StateIntrinsic = struct end

[<AutoOpen>]
module Intrinsics =
    let inline getState() = StateIntrinsic()

type ITaskAwaiterBase =
    inherit ICriticalNotifyCompletion
    abstract IsCompleted: bool

type ITaskAwaiter =
    inherit ITaskAwaiterBase
    abstract GetResult: unit -> unit

type ITaskLike<'TAwaiter when 'TAwaiter :> ITaskAwaiter> =
    abstract GetAwaiter: unit -> 'TAwaiter

type ITaskAwaiter<'T> =
    inherit ITaskAwaiterBase
    abstract GetResult: unit -> 'T

type ITaskLike<'TAwaiter, 'TResult when 'TAwaiter :> ITaskAwaiter<'TResult>> =
    abstract GetAwaiter: unit -> 'TAwaiter

type ITaskLikeTask<'TTask> =
    abstract Task: 'TTask

type IAsyncMethodBuilderBase =

    abstract Start<'TStateMachine when 'TStateMachine :> IAsyncStateMachine> : stateMachine: byref<'TStateMachine> -> unit
    abstract SetStateMachine: stateMachine: IAsyncStateMachine -> unit
    abstract SetException: ``exception``: Exception -> unit
    abstract AwaitOnCompleted<'TAwaiter, 'TStateMachine when 'TAwaiter :> INotifyCompletion and 'TStateMachine :> IAsyncStateMachine> :
        awaiter: byref<'TAwaiter> * stateMachine: byref<'TStateMachine> -> unit
    abstract AwaitUnsafeOnCompleted<'TAwaiter, 'TStateMachine when 'TAwaiter :> ICriticalNotifyCompletion and 'TStateMachine :> IAsyncStateMachine> :
        awaiter: byref<'TAwaiter>  * stateMachine: byref<'TStateMachine>  -> unit

// unit for most cases and 'Tstate for special ones like activity stuff or cancellable etc
type IAsyncMethodBuilderCreator<'TSelf> =
    static abstract member Create: unit -> 'TSelf

type IAsyncMethodBuilderCreator<'TState, 'TSelf> =
    static abstract member Create: state: 'TState -> 'TSelf

type IAsyncMethodBuilder<'TAwaiter, 'TTask when 'TTask :> ITaskLike<'TAwaiter> and 'TAwaiter :> ITaskAwaiter> =
    inherit IAsyncMethodBuilderBase
    abstract SetResult: unit -> unit
    abstract Task: 'TTask

type IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult when 'TTask :> ITaskLike<'TAwaiter, 'TResult> and 'TAwaiter :> ITaskAwaiter<'TResult>> =
    inherit IAsyncMethodBuilderBase
    abstract SetResult: data: 'TResult -> unit
    abstract Task: 'TTask