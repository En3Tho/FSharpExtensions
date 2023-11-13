namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2

open System.Collections.Generic
open System.Runtime.CompilerServices
open System.Threading
open System.Threading.Tasks
open System.Threading.Tasks.Sources
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core.CompilerServices

type internal GenericTaskSeqAsyncEnumerable<'TData, 'TResult, 'TStateMachine, 'TInitializer
    when 'TData :> IGenericTaskBuilderStateMachineDataYield<'TData, 'TResult>
    and 'TInitializer :> IGenericTaskBuilderStateMachineDataInitializer<'TData, unit, IAsyncEnumerable<'TResult>>
    and 'TStateMachine :> IResumableStateMachine<'TData>
    and 'TStateMachine :> IAsyncStateMachine>(sm: 'TStateMachine) =

    //inherit StateMachineBox<'TStateMachine>()

    let mutable stateMachine = sm

    interface IAsyncStateMachine with
        member this.MoveNext() = stateMachine.MoveNext()
        member this.SetStateMachine(_) = ()

    interface IAsyncEnumerator<'TResult> with
        member this.MoveNextAsync() =
            if stateMachine.ResumptionPoint = -1 then
                ValueTask.FromResult(false)
            else
                stateMachine.Data.MoveNext()

        member this.DisposeAsync() =
            if stateMachine.ResumptionPoint = -1 then
                ValueTask()
            else
                stateMachine.Data.Dispose()

        member this.Current =
            stateMachine.Data.GetResult()

    interface IAsyncEnumerable<'TResult> with
        member this.GetAsyncEnumerator(_: CancellationToken) =
            // not sure about this and field initialization
            // need to test more?
            let mutable stateMachineCopy = stateMachine
            let mutable data = stateMachineCopy.Data

            'TInitializer.Initialize(&stateMachineCopy, &data, ()) :?> IAsyncEnumerator<'TResult>

[<NoComparison; NoEquality>]
type TaskSeqStateMachineDataRef<'TMethodBuilder, 'TResult
    when 'TMethodBuilder :> IAsyncIteratorMethodBuilder>() =

    [<DefaultValue(false)>]
    val mutable Current: 'TResult

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable ValueTaskSource: ManualResetValueTaskSourceCore<bool>

    [<DefaultValue(false)>]
    val mutable DisposeMode: bool

    // TODO: find a way to access 'TData ref on state machine
    // This way this field and a copy of state machine could be avoided
    [<DefaultValue(false)>]
    val mutable AsyncStateMachine: IAsyncStateMachine

    interface IValueTaskSource<bool> with
        member this.GetResult(token) = this.ValueTaskSource.GetResult(token)
        member this.GetStatus(token) = this.ValueTaskSource.GetStatus(token)
        member this.OnCompleted(continuation, state, token, flags) = this.ValueTaskSource.OnCompleted(continuation, state, token, flags)

    interface IValueTaskSource with
        member this.GetResult(token) = this.ValueTaskSource.GetResult(token) |> ignore
        member this.GetStatus(token) = this.ValueTaskSource.GetStatus(token)
        member this.OnCompleted(continuation, state, token, flags) = this.ValueTaskSource.OnCompleted(continuation, state, token, flags)

[<Struct; NoComparison; NoEquality>]
type SeqStateMachineData<'TMethodBuilder, 'TResult
    when 'TMethodBuilder :> IAsyncIteratorMethodBuilder
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>> =

    [<DefaultValue(false)>]
    val mutable Data: TaskSeqStateMachineDataRef<'TMethodBuilder, 'TResult>

    interface IGenericTaskStateMachineData<SeqStateMachineData<'TMethodBuilder, 'TResult>> with

        member this.CheckCanContinueOrThrow() = not this.Data.DisposeMode
        member this.AwaitOnCompleted(awaiter, _) = this.Data.MethodBuilder.AwaitOnCompleted(&awaiter, &this.Data.AsyncStateMachine)
        member this.AwaitUnsafeOnCompleted(awaiter, _) = this.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &this.Data.AsyncStateMachine)

        member this.Finish(sm) =
            let data = this.Data
            data.MethodBuilder.Complete()
            data.ValueTaskSource.SetResult(false)

        member this.SetException(``exception``: exn) =
            let data = this.Data
            data.MethodBuilder.Complete()
            data.ValueTaskSource.SetException(``exception``)
        member this.SetStateMachine(stateMachine) = ()

    interface IGenericTaskBuilderStateMachineDataYield<SeqStateMachineData<'TMethodBuilder, 'TResult>, 'TResult> with
        member this.Dispose() =
            let data = this.Data
            data.DisposeMode <- true
            data.ValueTaskSource.Reset()
            data.MethodBuilder.MoveNext(&data.AsyncStateMachine)
            ValueTask(data, data.ValueTaskSource.Version)

        member this.MoveNext() =
            let data = this.Data

            data.ValueTaskSource.Reset()
            data.MethodBuilder.MoveNext(&data.AsyncStateMachine)

            let version = data.ValueTaskSource.Version
            if (data.ValueTaskSource.GetStatus(version) = ValueTaskSourceStatus.Succeeded) then
                ValueTask.FromResult(data.ValueTaskSource.GetResult(version))
            else
                ValueTask<bool>(data, version)

        member this.GetResult() =
            this.Data.Current

        member this.SetResult(result) =
            let data = this.Data
            data.Current <- result
            data.ValueTaskSource.SetResult(true)

// open UnsafeEx
type [<Struct>] DefaultSeqStateMachineDataInitializer<'TMethodBuilder, 'TResult
    when 'TMethodBuilder :> IAsyncIteratorMethodBuilder
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>> =

    interface IGenericTaskBuilderStateMachineDataInitializer<SeqStateMachineData<'TMethodBuilder, 'TResult>, unit, IAsyncEnumerable<'TResult>> with
        static member Initialize(stateMachine, data, _) =
            // this is somewhat tricky: we have to make sure state machine already has a proper ref when being promoted to the heap
            // let box = StateMachineBox(StateMachine = stateMachine, DataFieldOffset = Unsafe.ByteOffset(&stateMachine, &data))
            // TODO: remove data ref and use only box?

            data.Data <- TaskSeqStateMachineDataRef(MethodBuilder = 'TMethodBuilder.Create())
            stateMachine.Data <- data
            let result = GenericTaskSeqAsyncEnumerable<SeqStateMachineData<'TMethodBuilder, 'TResult>, 'TResult, 'TStateMachine, DefaultSeqStateMachineDataInitializer<'TMethodBuilder, 'TResult>>(stateMachine)
            data.Data.AsyncStateMachine <- result
            result