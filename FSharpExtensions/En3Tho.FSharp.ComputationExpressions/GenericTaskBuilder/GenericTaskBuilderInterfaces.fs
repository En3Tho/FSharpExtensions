﻿namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core.CompilerServices

type IAsyncMethodBuilderCreator<'TSelf> =
    static abstract member Create: unit -> 'TSelf

type IAsyncMethodBuilderCreator<'TState, 'TSelf> =
    static abstract member Create: state: 'TState -> 'TSelf

type IAsyncMethodBuilderBase =
    abstract SetStateMachine: stateMachine: IAsyncStateMachine -> unit
    abstract AwaitOnCompleted<'TAwaiter, 'TStateMachine when 'TAwaiter :> INotifyCompletion and 'TStateMachine :> IAsyncStateMachine> :
        awaiter: byref<'TAwaiter> * stateMachine: byref<'TStateMachine> -> unit
    abstract AwaitUnsafeOnCompleted<'TAwaiter, 'TStateMachine when 'TAwaiter :> ICriticalNotifyCompletion and 'TStateMachine :> IAsyncStateMachine> :
        awaiter: byref<'TAwaiter>  * stateMachine: byref<'TStateMachine> -> unit

type IAsyncMethodBuilderWithReturnBase =
    inherit IAsyncMethodBuilderBase

    abstract Start<'TStateMachine when 'TStateMachine :> IAsyncStateMachine> : stateMachine: byref<'TStateMachine> -> unit
    abstract SetException: ``exception``: Exception -> unit

type IAsyncIteratorMethodBuilder =
    inherit IAsyncMethodBuilderBase
    abstract MoveNext<'TStateMachine when 'TStateMachine :> IAsyncStateMachine> : stateMachine: byref<'TStateMachine> -> unit
    abstract Complete: unit -> unit

type IAsyncMethodBuilder<'TTask> =
    inherit IAsyncMethodBuilderWithReturnBase
    abstract SetResult: unit -> unit
    abstract Task: 'TTask

type IAsyncMethodBuilder<'TTask, 'TResult> =
    inherit IAsyncMethodBuilderWithReturnBase
    abstract SetResult: data: 'TResult -> unit
    abstract Task: 'TTask

type IGenericTaskStateMachineDataInitializer<'TData, 'TState, 'TBuilderResult> =
    static abstract Initialize<'TStateMachine, 'TData, 'TState when 'TStateMachine :> IAsyncStateMachine and 'TStateMachine :> IResumableStateMachine<'TData>>
        : stateMachine: byref<'TStateMachine> * data: byref<'TData> * state: 'TState  -> 'TBuilderResult

type IStateCheck<'TState> =

    static abstract CanCheckState: bool
    static abstract CheckState: state: byref<'TState> -> bool

    static abstract CanProcessException: bool
    static abstract ProcessException: state: byref<'TState> * ``exception``: exn -> unit

    static abstract CanProcessSuccess: bool
    static abstract ProcessSuccess: state: byref<'TState> -> unit

type IGenericTaskStateMachineDataWithCheck<'TData> =
    abstract CheckCanContinueOrThrow: unit -> bool

type IGenericTaskStateMachineData<'TData, 'TResult when 'TData :> IGenericTaskStateMachineData<'TData, 'TResult>> =
    inherit IGenericTaskStateMachineDataWithCheck<'TData>
    inherit IAsyncMethodBuilderBase
    abstract Finish<'TStateMachine when 'TStateMachine :> IAsyncStateMachine> : sm: byref<'TStateMachine> -> unit
    abstract SetException: ``exception``: Exception -> unit
    abstract SetResult: result: 'TResult -> unit

type IGenericTaskStateMachineDataGetResult<'TData, 'TResult
    when 'TData :> IGenericTaskStateMachineDataGetResult<'TData, 'TResult>> =
    abstract GetResult: unit -> 'TResult

type IGenericTaskStateMachineDataWithState<'TData, 'TState
    when 'TData :> IGenericTaskStateMachineDataWithState<'TData, 'TState>> =
    abstract State: 'TState

type IGenericTaskStateMachineDataYield<'TData, 'TResult
    when 'TData :> IGenericTaskStateMachineDataYield<'TData, 'TResult>> =
    inherit IGenericTaskStateMachineData<'TData, 'TResult>
    inherit IGenericTaskStateMachineDataGetResult<'TData, 'TResult>

    abstract MoveNextAsync: unit -> ValueTask<bool>
    abstract Dispose: unit -> ValueTask


module GenericTaskBuilderStateMachineData =
    let inline getResult<'TData, 'TResult when 'TData :> IGenericTaskStateMachineDataGetResult<'TData, 'TResult>>(data: 'TData) =
        data.GetResult()

    let inline moveNext<'TData, 'TResult when 'TData :> IGenericTaskStateMachineDataYield<'TData, 'TResult>>(data: 'TData) =
        data.MoveNextAsync()

    let inline dispose<'TData, 'TResult when 'TData :> IGenericTaskStateMachineDataYield<'TData, 'TResult>>(data: 'TData) =
        data.Dispose()