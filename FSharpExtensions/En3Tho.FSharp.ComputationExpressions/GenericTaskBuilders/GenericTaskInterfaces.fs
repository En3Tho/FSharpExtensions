namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core.CompilerServices

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

type IAsyncMethodBuilderCreator<'TSelf> =
    static abstract member Create: unit -> 'TSelf

type IAsyncMethodBuilderCreator<'TState, 'TSelf> = // TODO: remove?
    static abstract member Create: state: 'TState -> 'TSelf

type IAsyncMethodBuilderBase =
    abstract SetStateMachine: stateMachine: IAsyncStateMachine -> unit
    abstract AwaitOnCompleted<'TAwaiter, 'TStateMachine when 'TAwaiter :> INotifyCompletion and 'TStateMachine :> IAsyncStateMachine> :
        awaiter: byref<'TAwaiter> * stateMachine: byref<'TStateMachine> -> unit
    abstract AwaitUnsafeOnCompleted<'TAwaiter, 'TStateMachine when 'TAwaiter :> ICriticalNotifyCompletion and 'TStateMachine :> IAsyncStateMachine> :
        awaiter: byref<'TAwaiter>  * stateMachine: byref<'TStateMachine>  -> unit

type IAsyncMethodBuilderWithReturnBase =
    inherit IAsyncMethodBuilderBase

    abstract Start<'TStateMachine when 'TStateMachine :> IAsyncStateMachine> : stateMachine: byref<'TStateMachine> -> unit
    abstract SetException: ``exception``: Exception -> unit

type IAsyncIteratorMethodBuilder =
    inherit IAsyncMethodBuilderBase
    abstract MoveNext<'TStateMachine when 'TStateMachine :> IAsyncStateMachine> : stateMachine: byref<'TStateMachine> -> unit
    abstract Complete: unit -> unit

type IAsyncMethodBuilder<'TAwaiter, 'TTask when 'TTask :> ITaskLike<'TAwaiter> and 'TAwaiter :> ITaskAwaiter> =
    inherit IAsyncMethodBuilderWithReturnBase
    abstract SetResult: unit -> unit
    abstract Task: 'TTask

type IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult when 'TTask :> ITaskLike<'TAwaiter, 'TResult> and 'TAwaiter :> ITaskAwaiter<'TResult>> =
    inherit IAsyncMethodBuilderWithReturnBase
    abstract SetResult: data: 'TResult -> unit
    abstract Task: 'TTask

type IGenericTaskBuilderStateMachineDataInitializer<'TData, 'TState, 'TBuilderResult when 'TData :> IGenericTaskBuilderStateMachineDataInitializer<'TData, 'TState, 'TBuilderResult>> =
    abstract Initialize<'TStateMachine when 'TStateMachine :> IAsyncStateMachine and 'TStateMachine :> IResumableStateMachine<'TData>> : stateMachine: byref<'TStateMachine> * state: 'TState -> 'TBuilderResult

type IStateCheck<'TState> =
    static abstract CanCheckState: bool
    static abstract CheckState: state: byref<'TState> -> bool

    static abstract CanProcessException: bool
    static abstract ProcessException: state: byref<'TState> * ``exception``: exn -> unit

    static abstract CanProcessSuccess: bool
    static abstract ProcessSuccess: state: byref<'TState> -> unit

type IGenericTaskBuilderStateMachineDataWithCheck<'TData> =
    abstract CheckCanContinueOrThrow: unit -> bool

type IGenericTaskStateMachineData<'TData, 'TState when 'TData :> IGenericTaskStateMachineData<'TData, 'TState>> =
    inherit IGenericTaskBuilderStateMachineDataWithCheck<'TData>
    inherit IAsyncMethodBuilderBase
    abstract Finish<'TStateMachine when 'TStateMachine :> IAsyncStateMachine> : sm: byref<'TStateMachine> -> unit
    abstract SetException: ``exception``: Exception -> unit

type IGenericTaskBuilderStateMachineDataResult<'TResult> =
    abstract SetResult: result: 'TResult -> unit

type IGenericTaskBuilderStateMachineDataYield<'TData, 'TResult when 'TData :> IGenericTaskBuilderStateMachineDataYield<'TData, 'TResult>> =
    inherit IGenericTaskBuilderStateMachineDataResult<'TResult>
    abstract GetResult: unit -> 'TResult
    abstract MoveNext: unit -> ValueTask<bool>
    abstract Dispose: unit -> ValueTask