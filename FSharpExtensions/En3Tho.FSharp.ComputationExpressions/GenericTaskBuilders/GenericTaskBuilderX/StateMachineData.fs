module En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2.StateMachineDataXX

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open System.Threading.Tasks.Sources
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

[<Struct; NoComparison; NoEquality>]
type UnitStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    interface IGenericTaskStateMachineData<UnitStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TBuilderResult>, unit, 'TBuilderResult> with

        member this.CheckCanContinueOrThrow() = true
        member this.AwaitOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.Finish(sm) = this.MethodBuilder.SetResult()
        member this.Initialize(stateMachine, stateMachineData, _) =
            stateMachineData.MethodBuilder <- 'TMethodBuilder.Create()
            stateMachineData.MethodBuilder.Start(&stateMachine)
            stateMachineData.MethodBuilder.Task.Task

        member this.SetException(``exception``: exn) = this.MethodBuilder.SetException(``exception``)
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.MethodBuilder.Start(&stateMachine)

type IStateCheck<'TState> =
    static abstract CanCheckState: bool
    static abstract CheckState: state: byref<'TState> -> bool

    static abstract CanProcessException: bool
    static abstract ProcessException: state: byref<'TState> * ``exception``: exn -> unit

    static abstract CanProcessSuccess: bool
    static abstract ProcessSuccess: state: byref<'TState> -> unit

[<Struct>]
type NoStateCheck<'TState> =
    interface IStateCheck<'TState> with
        static member CanCheckState = false
        static member CheckState(_) = raise (InvalidOperationException("Should not be called"))
        static member CanProcessException = false
        static member ProcessException(_, _) = raise (InvalidOperationException("Should not be called"))
        static member CanProcessSuccess = false
        static member ProcessSuccess(_) = raise (InvalidOperationException("Should not be called"))

[<Struct; NoComparison; NoEquality>]
type UnitStateMachineDataWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TState, 'TStateCheck, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>
    and 'TStateCheck :> IStateCheck<'TState>> =

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable State: 'TState

    interface IGenericTaskStateMachineData<UnitStateMachineDataWithState<'TMethodBuilder, 'TAwaiter, 'TTask, 'TState, 'TStateCheck, 'TBuilderResult>, 'TState, 'TBuilderResult> with

        member this.CheckCanContinueOrThrow() =
            if 'TStateCheck.CanCheckState then
                'TStateCheck.CheckState(&this.State)
            else
                true

        member this.Finish(sm) =
            if 'TStateCheck.CanProcessSuccess then
                'TStateCheck.ProcessSuccess(&this.State)
            this.MethodBuilder.SetResult()

        member this.SetException(``exception``: exn) =
            if 'TStateCheck.CanProcessException then
                'TStateCheck.ProcessException(&this.State, ``exception``)
            this.MethodBuilder.SetException(``exception``)

        member this.Initialize(stateMachine, stateMachineData, state) =
            stateMachineData.State <- state
            stateMachineData.MethodBuilder <- 'TMethodBuilder.Create()
            stateMachineData.MethodBuilder.Start(&stateMachine)
            stateMachineData.MethodBuilder.Task.Task

        member this.AwaitOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitOnCompleted(&awaiter, &stateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, stateMachine) = this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &stateMachine)
        member this.SetStateMachine(stateMachine) = this.MethodBuilder.SetStateMachine(stateMachine)
        member this.Start(stateMachine) = this.MethodBuilder.Start(&stateMachine)

// [<Struct; NoComparison; NoEquality>]
// type GenericTaskStateMachineData2<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult
//     when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
//     and 'TAwaiter :> ITaskAwaiter<'TResult>
//     and 'TTask :> ITaskLike<'TAwaiter, 'TResult>> =
//
//     [<DefaultValue(false)>]
//     val mutable Result: 'TResult
//
//     [<DefaultValue(false)>]
//     val mutable MethodBuilder: 'TMethodBuilder
//
//     interface IGenericTaskBuilderStateMachineDataHasMethodBuilder<'TMethodBuilder> with
//         member this.MethodBuilder = &(GenericTaskBuilder2StateMachineData.getMethodBuilder &this)
//
//     interface IGenericTaskBuilderStateMachineDataWithCheck<GenericTaskStateMachineData2<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>> with
//         static member CheckCanContinueOrThrow(data) = true
//
//     interface IGenericTaskBuilderStateMachineDataGetStateMachineBox<GenericTaskStateMachineData2<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>> with
//         static member ShouldBoxStateMachine = false
//         static member GetStateMachineBox(sm, data) = raise (InvalidOperationException("Should not be called"))
//
//     interface IGenericTaskBuilderStateMachineDataResult<'TResult> with
//         member this.GetResult() = this.Result
//         member this.SetResult(result) = this.Result <- result
//
// // TODO: merge?
// [<NoComparison; NoEquality>]
// type TaskSeqStateMachineDataInternal<'TMethodBuilder, 'TResult
//     when 'TMethodBuilder :> IAsyncIteratorMethodBuilder>() =
//
//     [<DefaultValue(false)>]
//     val mutable Current: 'TResult
//
//     [<DefaultValue(false)>]
//     val mutable MethodBuilder: 'TMethodBuilder
//
//     [<DefaultValue(false)>]
//     val mutable ValueTaskSource: ManualResetValueTaskSourceCore<bool>
//
//     [<DefaultValue(false)>]
//     val mutable DisposeMode: bool
//
//     [<DefaultValue(false)>]
//     val mutable AsyncStateMachine: IAsyncStateMachine
//
//     interface IValueTaskSource<bool> with
//         member this.GetResult(token) = this.ValueTaskSource.GetResult(token)
//         member this.GetStatus(token) = this.ValueTaskSource.GetStatus(token)
//         member this.OnCompleted(continuation, state, token, flags) = this.ValueTaskSource.OnCompleted(continuation, state, token, flags)
//
//     interface IValueTaskSource with
//         member this.GetResult(token) = this.ValueTaskSource.GetResult(token) |> ignore
//         member this.GetStatus(token) = this.ValueTaskSource.GetStatus(token)
//         member this.OnCompleted(continuation, state, token, flags) = this.ValueTaskSource.OnCompleted(continuation, state, token, flags)
//
// type [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>] HasThis() =
//     [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
//     member this.This = this // F# !!
//
// // TODO: merge enumerable and enumerator into single object
//
// type GenericTaskSeqAsyncEnumerator<'TData, 'TResult, 'TStateMachine
//     when 'TData :> IAsyncIteratorMethodBuilder
//     and 'TData :> IGenericTaskBuilderStateMachineDataInitializer<'TData, unit>
//     and 'TData :> IGenericTaskBuilderStateMachineDataYield<'TResult>
//     and 'TStateMachine :> IResumableStateMachine<'TData>
//     and 'TStateMachine :> IAsyncStateMachine>(sm: 'TStateMachine) =
//
//     inherit HasThis()
//
//     let mutable stateMachine = sm
//     do
//         let mutable thisStateMachine = base.This :?> GenericTaskSeqAsyncEnumerator<'TData, 'TResult, 'TStateMachine>
//         stateMachine.Data <- 'TData.Create(&thisStateMachine, ())
//
//     member this.MoveNextAsync() =
//         if stateMachine.ResumptionPoint = Flags.StateMachineFinished then
//             ValueTask.FromResult(false)
//         else
//             stateMachine.Data.MoveNext()
//
//     interface IAsyncStateMachine with
//         member this.MoveNext() = stateMachine.MoveNext()
//         member this.SetStateMachine(_) = ()
//
//     interface IAsyncEnumerator<'TResult> with
//         member this.MoveNextAsync() =
//             this.MoveNextAsync()
//
//         member this.DisposeAsync() =
//             if stateMachine.ResumptionPoint = Flags.StateMachineFinished then
//                 ValueTask()
//             else
//                 stateMachine.Data.Dispose()
//
//         member this.Current =
//             stateMachine.Data.GetResult()
//
// type GenericTaskSeqAsyncEnumerable<'TData, 'TResult, 'TStateMachine
//     when 'TData :> IAsyncIteratorMethodBuilder
//     and 'TData :> IGenericTaskBuilderStateMachineDataInitializer<'TData, unit>
//     and 'TData :> IGenericTaskBuilderStateMachineDataYield<'TResult>
//     and 'TStateMachine :> IResumableStateMachine<'TData>
//     and 'TStateMachine :> IAsyncStateMachine>(sm: 'TStateMachine) =
//     interface IAsyncEnumerable<'TResult> with
//         member _.GetAsyncEnumerator(_: CancellationToken) = GenericTaskSeqAsyncEnumerator<'TData, 'TResult, 'TStateMachine>(sm)
//
// [<Struct; NoComparison; NoEquality>]
// type GenericTaskSeqXStateMachineData<'TMethodBuilder, 'TResult
//     when 'TMethodBuilder :> IAsyncIteratorMethodBuilder> =
//
//         [<DefaultValue(false)>]
//         val mutable Data: TaskSeqStateMachineDataInternal<'TMethodBuilder, 'TResult>
//
//         interface IGenericTaskBuilderStateMachineDataHasMethodBuilder<'TMethodBuilder> with
//             member this.MethodBuilder = &this.Data.MethodBuilder
//
//         interface IGenericTaskBuilderStateMachineDataGetStateMachineBox<GenericTaskSeqXStateMachineData<'TMethodBuilder, 'TResult>> with
//             static member ShouldBoxStateMachine = true
//             static member GetStateMachineBox(sm, data) = data.Data.AsyncStateMachine
//
//         interface IGenericTaskBuilderStateMachineDataWithCheck<GenericTaskSeqXStateMachineData<'TMethodBuilder, 'TResult>> with
//             member this.CheckCanContinueOrThrow(data) = not data.Data.DisposeMode
//
//         interface IGenericTaskBuilderStateMachineDataYield<'TResult> with
//             member this.Dispose() =
//                 this.Data.DisposeMode <- true
//                 this.Data.ValueTaskSource.Reset()
//                 this.Data.MethodBuilder.MoveNext(&this.Data.AsyncStateMachine)
//                 ValueTask(this.Data, this.Data.ValueTaskSource.Version)
//
//             member this.Finish() =
//                 this.Data.ValueTaskSource.SetResult(false)
//
//             member this.GetResult() =
//                 this.Data.Current
//
//             member this.MoveNext() =
//                 let data = this.Data
//
//                 data.ValueTaskSource.Reset()
//                 data.MethodBuilder.MoveNext(&this.Data.AsyncStateMachine)
//
//                 let version = data.ValueTaskSource.Version
//                 if (data.ValueTaskSource.GetStatus(version) = ValueTaskSourceStatus.Succeeded) then
//                     ValueTask.FromResult(data.ValueTaskSource.GetResult(version))
//                 else
//                     ValueTask<bool>(data, version)
//
//             member this.SetException(``exception``) =
//                 this.Data.ValueTaskSource.SetException(``exception``)
//
//             member this.SetResult(result) =
//                 this.Data.Current <- result
//                 this.Data.ValueTaskSource.SetResult(true)
//
//         interface IGenericTaskBuilderStateMachineDataInitializer<GenericTaskSeqXStateMachineData<'TMethodBuilder, 'TResult>> with
//             member this.Create(stateMachine) =
//                 let data = TaskSeqStateMachineDataInternal(AsyncStateMachine = stateMachine)
//                 GenericTaskSeqXStateMachineData(Data = data)