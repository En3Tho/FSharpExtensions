namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

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

type IStateMachineDataInitializer<'TData, 'TState, 'TBuilderResult> =
    static abstract Initialize<'TStateMachine, 'TData, 'TState when 'TStateMachine :> IAsyncStateMachine and 'TStateMachine :> IResumableStateMachine<'TData>>
        : stateMachine: byref<'TStateMachine> * data: byref<'TData> * state: 'TState  -> 'TBuilderResult

type IStateCheck<'TState> =

    static abstract CanCheckState: bool
    static abstract CheckState: state: byref<'TState> -> bool

    static abstract CanProcessException: bool
    static abstract ProcessException: state: byref<'TState> * ``exception``: exn -> unit

    static abstract CanProcessSuccess: bool
    static abstract ProcessSuccess: state: byref<'TState> -> unit

type IStateMachineDataWithCheck<'TData> =
    abstract CheckCanContinueOrThrow: unit -> bool

type IStateMachineData<'TData, 'TResult when 'TData :> IStateMachineData<'TData, 'TResult>> =
    inherit IStateMachineDataWithCheck<'TData>
    inherit IAsyncMethodBuilderBase
    abstract Finish<'TStateMachine when 'TStateMachine :> IAsyncStateMachine> : sm: byref<'TStateMachine> -> unit
    abstract SetException: ``exception``: Exception -> unit
    abstract SetResult: result: 'TResult -> unit

type IStateMachineDataGetResult<'TData, 'TResult
    when 'TData :> IStateMachineDataGetResult<'TData, 'TResult>> =
    abstract GetResult: unit -> 'TResult

type IStateMachineDataWithState<'TData, 'TState
    when 'TData :> IStateMachineDataWithState<'TData, 'TState>> =
    abstract State: 'TState

type IStateMachineDataYield<'TData, 'TResult
    when 'TData :> IStateMachineDataYield<'TData, 'TResult>> =
    inherit IStateMachineData<'TData, 'TResult>
    inherit IStateMachineDataGetResult<'TData, 'TResult>

    abstract MoveNextAsync: unit -> ValueTask<bool>
    abstract Dispose: unit -> ValueTask

module StateMachineData =
    let inline getResult<'TData, 'TResult when 'TData :> IStateMachineDataGetResult<'TData, 'TResult>>(data: 'TData) =
        data.GetResult()

    let inline moveNext<'TData, 'TResult when 'TData :> IStateMachineDataYield<'TData, 'TResult>>(data: 'TData) =
        data.MoveNextAsync()

    let inline dispose<'TData, 'TResult when 'TData :> IStateMachineDataYield<'TData, 'TResult>>(data: 'TData) =
        data.Dispose()