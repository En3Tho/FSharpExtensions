namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System.Collections.Generic
open System.Runtime.CompilerServices
open System.Threading
open System.Threading.Tasks
open System.Threading.Tasks.Sources
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core.CompilerServices

type [<AbstractClass>] GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer
    when 'TInitializer :> IStateMachineDataInitializer<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>, unit, IAsyncEnumerable<'TResult>>
    and 'TMethodBuilder :> IAsyncIteratorMethodBuilder>() =

    [<DefaultValue(false)>]
    val mutable Current: 'TResult

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable ValueTaskSource: ManualResetValueTaskSourceCore<bool>

    [<DefaultValue(false)>]
    val mutable DisposeMode: bool
    abstract member MoveNext: unit -> unit

    interface IStateMachineDataYield<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>, 'TResult> with
        member this.DisposeAsync() =
            let mutable this = this
            this.DisposeMode <- true
            this.ValueTaskSource.Reset()
            this.MethodBuilder.MoveNext(&this)
            ValueTask(this, this.ValueTaskSource.Version)

        member this.MoveNextAsync() =
            let mutable this = this
            this.ValueTaskSource.Reset()
            this.MethodBuilder.MoveNext(&this)

            let version = this.ValueTaskSource.Version
            if (this.ValueTaskSource.GetStatus(version) = ValueTaskSourceStatus.Succeeded) then
                ValueTask.FromResult(this.ValueTaskSource.GetResult(version))
            else
                ValueTask<bool>(this, version)

        member this.GetResult() =
            this.Current

        member this.SetResult(result) =
            this.Current <- result
            this.ValueTaskSource.SetResult(true)

        member this.CheckCanContinueOrThrow() = not this.DisposeMode
        member this.AwaitOnCompleted(awaiter, _) =
            let mutable this = this
            this.MethodBuilder.AwaitOnCompleted(&awaiter, &this)
        member this.AwaitUnsafeOnCompleted(awaiter, _) =
            let mutable this = this
            this.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &this)

        member this.Finish(sm) =
            this.MethodBuilder.Complete()
            this.ValueTaskSource.SetResult(false)

        member this.SetException(``exception``: exn) =
            this.MethodBuilder.Complete()
            this.ValueTaskSource.SetException(``exception``)
        member this.SetStateMachine(_) = ()

    interface IValueTaskSource<bool> with
        member this.GetResult(token) = this.ValueTaskSource.GetResult(token)
        member this.GetStatus(token) = this.ValueTaskSource.GetStatus(token)
        member this.OnCompleted(continuation, state, token, flags) = this.ValueTaskSource.OnCompleted(continuation, state, token, flags)

    interface IValueTaskSource with
        member this.GetResult(token) = this.ValueTaskSource.GetResult(token) |> ignore
        member this.GetStatus(token) = this.ValueTaskSource.GetStatus(token)
        member this.OnCompleted(continuation, state, token, flags) = this.ValueTaskSource.OnCompleted(continuation, state, token, flags)

    interface IAsyncStateMachine with
        member this.MoveNext() = this.MoveNext()
        member this.SetStateMachine(_) = ()

type internal GenericTaskSeqAsyncEnumerable<'TMethodBuilder, 'TResult, 'TStateMachine, 'TInitializer
    when 'TStateMachine :> IAsyncStateMachine
    and 'TMethodBuilder :> IAsyncIteratorMethodBuilder
    and 'TInitializer :> IStateMachineDataInitializer<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>, unit, IAsyncEnumerable<'TResult>>
    and 'TStateMachine :> IResumableStateMachine<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder,'TResult, 'TInitializer>>>() =
    inherit GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>()

    [<DefaultValue(false)>]
    val mutable StateMachine: 'TStateMachine

    override this.MoveNext() = this.StateMachine.MoveNext()

    interface IAsyncEnumerator<'TResult> with
        member this.MoveNextAsync() =
            if this.StateMachine.ResumptionPoint = StateMachineCodes.Finished then
                ValueTask.FromResult(false)
            else
                // UnsafeAccessor
                StateMachineData.moveNext (this :> GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>)

        member this.DisposeAsync() =
            if this.StateMachine.ResumptionPoint = StateMachineCodes.Finished then
                ValueTask()
            else
                // UnsafeAccessor
                StateMachineData.dispose (this :> GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>)

        member this.Current =
            // UnsafeAccessor
            StateMachineData.getResult (this :> GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>)

    interface IAsyncEnumerable<'TResult> with
        member this.GetAsyncEnumerator(_: CancellationToken) =
            let mutable stateMachineCopy = this.StateMachine
            let mutable data = stateMachineCopy.Data
            'TInitializer.Initialize(&stateMachineCopy, &data, ()) :?> IAsyncEnumerator<'TResult>

type [<Struct>] DefaultSeqStateMachineDataInitializer<'TResult, 'TMethodBuilder
    when 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TMethodBuilder :> IAsyncIteratorMethodBuilder> =

    interface IStateMachineDataInitializer<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, DefaultSeqStateMachineDataInitializer<'TResult, 'TMethodBuilder>>, unit, IAsyncEnumerable<'TResult>> with
        static member Initialize(stateMachine: byref<'TStateMachine>, _, _) =
            // Currently UnsafeAccessor doesn't allow extracting fields from unbounded generics. It requires a concrete type, sadly.
            // So for now we have to use class to mutate "Data" field
            let box = GenericTaskSeqAsyncEnumerable<'TMethodBuilder, 'TResult, 'TStateMachine, DefaultSeqStateMachineDataInitializer<'TResult, 'TMethodBuilder>>(
                MethodBuilder = 'TMethodBuilder.Create(),
                StateMachine = stateMachine
            )
            box.StateMachine.Data <- box
            box :> IAsyncEnumerable<'TResult>