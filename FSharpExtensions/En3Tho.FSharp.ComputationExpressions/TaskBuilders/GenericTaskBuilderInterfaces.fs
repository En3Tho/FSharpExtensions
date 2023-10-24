namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open Microsoft.FSharp.Core.CompilerServices

// TODO: Explore srtp-based approach?

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

// unit for most cases and 'TInitialState for special ones like activity stuff or cancellable etc?
type IAsyncMethodBuilderCreator<'TSelf> =
    static abstract member Create: unit -> 'TSelf

type IAsyncMethodBuilderCreator<'TInitialState, 'TSelf> =
    static abstract member Create: initialState: 'TInitialState -> 'TSelf

type IAsyncMethodBuilder<'TAwaiter, 'TTask when 'TTask :> ITaskLike<'TAwaiter> and 'TAwaiter :> ITaskAwaiter> =
    inherit IAsyncMethodBuilderBase
    // static abstract member Create: unit -> 'TSelf // TODO: move to IAsyncMethodBuilderBase<'TSelf>
    abstract SetResult: unit -> unit
    abstract Task: 'TTask

type IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult when 'TTask :> ITaskLike<'TAwaiter, 'TResult> and 'TAwaiter :> ITaskAwaiter<'TResult>> =
    inherit IAsyncMethodBuilderBase
    // static abstract member Create: unit -> 'TSelf // // TODO: move to IAsyncMethodBuilderBase<'TSelf>
    abstract SetResult: data: 'TResult -> unit
    abstract Task: 'TTask

[<Struct; NoComparison; NoEquality>]
type GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>> =

    [<DefaultValue(false)>]
    val mutable Result: 'TResult

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

and GenericTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
    and 'TAwaiter :> ITaskAwaiter<'TOverall>
    and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>> =
        ResumableStateMachine<GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>>

and GenericTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
    and 'TAwaiter :> ITaskAwaiter<'TOverall>
    and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>> =
        ResumptionFunc<GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>>

and GenericTaskResumptionDynamicInfo<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
    and 'TAwaiter :> ITaskAwaiter<'TOverall>
    and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>> =
        ResumptionDynamicInfo<GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>>

and GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
    and 'TAwaiter :> ITaskAwaiter<'TOverall>
    and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>> =
        ResumableCode<GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>, 'TResult>