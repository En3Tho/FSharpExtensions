namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2

open System.Collections.Generic
open System.Runtime.CompilerServices
open System.Threading
open System.Threading.Tasks
open System.Threading.Tasks.Sources
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core.CompilerServices

#if StructData
[<Struct>]
type GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer
    when 'TInitializer :> IGenericTaskBuilderStateMachineDataInitializer<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>, unit, IAsyncEnumerable<'TResult>>
    and 'TMethodBuilder :> IAsyncIteratorMethodBuilder> =

    [<DefaultValue(false)>]
    val mutable Data: GenericTaskSeqAsyncEnumerableBase<'TMethodBuilder, 'TResult>

    interface IGenericTaskBuilderStateMachineDataYield<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>, 'TResult> with
        member this.Dispose() =
            this.Data.DisposeMode <- true
            this.Data.ValueTaskSource.Reset()
            this.Data.MethodBuilder.MoveNext(&this.Data)
            ValueTask(this.Data, this.Data.ValueTaskSource.Version)

        member this.MoveNextAsync() =
            this.Data.ValueTaskSource.Reset()
            this.Data.MethodBuilder.MoveNext(&this.Data)

            let version = this.Data.ValueTaskSource.Version
            if (this.Data.ValueTaskSource.GetStatus(version) = ValueTaskSourceStatus.Succeeded) then
                ValueTask.FromResult(this.Data.ValueTaskSource.GetResult(version))
            else
                ValueTask<bool>(this.Data, version)

        member this.GetResult() =
            this.Data.Current

        member this.SetResult(result) =
            this.Data.Current <- result
            this.Data.ValueTaskSource.SetResult(true)

    interface IGenericTaskStateMachineData<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>> with

        member this.CheckCanContinueOrThrow() = not this.Data.DisposeMode
        member this.AwaitOnCompleted(awaiter, _) =
            this.Data.MethodBuilder.AwaitOnCompleted(&awaiter, &this.Data)
        member this.AwaitUnsafeOnCompleted(awaiter, _) =
            this.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &this.Data)

        member this.Finish(sm) =
            this.Data.MethodBuilder.Complete()
            this.Data.ValueTaskSource.SetResult(false)

        member this.SetException(``exception``: exn) =
            this.Data.MethodBuilder.Complete()
            this.Data.ValueTaskSource.SetException(``exception``)
        member this.SetStateMachine(_) = ()

and [<AbstractClass>] GenericTaskSeqAsyncEnumerableBase<'TMethodBuilder, 'TResult
    when 'TMethodBuilder :> IAsyncIteratorMethodBuilder>() =
#else
type [<AbstractClass>] GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer
    when 'TInitializer :> IGenericTaskBuilderStateMachineDataInitializer<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>, unit, IAsyncEnumerable<'TResult>>
    and 'TMethodBuilder :> IAsyncIteratorMethodBuilder>() =
#endif

    [<DefaultValue(false)>]
    val mutable Current: 'TResult

    [<DefaultValue(false)>]
    val mutable MethodBuilder: 'TMethodBuilder

    [<DefaultValue(false)>]
    val mutable ValueTaskSource: ManualResetValueTaskSourceCore<bool>

    [<DefaultValue(false)>]
    val mutable DisposeMode: bool
    abstract member MoveNext: unit -> unit

#if !StructData

    interface IGenericTaskBuilderStateMachineDataYield<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>, 'TResult> with
        member this.Dispose() =
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

    interface IGenericTaskStateMachineData<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>> with

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

#endif
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
#if StructData
    and 'TInitializer :> IGenericTaskBuilderStateMachineDataInitializer<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>, unit, IAsyncEnumerable<'TResult>>
    and 'TStateMachine :> IResumableStateMachine<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder,'TResult, 'TInitializer>>>() =
    inherit GenericTaskSeqAsyncEnumerableBase<'TMethodBuilder, 'TResult>()
#else
    and 'TInitializer :> IGenericTaskBuilderStateMachineDataInitializer<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>, unit, IAsyncEnumerable<'TResult>>
    and 'TStateMachine :> IResumableStateMachine<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder,'TResult, 'TInitializer>>>() =

    inherit GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>()
#endif

    [<DefaultValue(false)>]
    val mutable StateMachine: 'TStateMachine

    override this.MoveNext() = this.StateMachine.MoveNext()

    interface IAsyncEnumerator<'TResult> with
        member this.MoveNextAsync() =
            if this.StateMachine.ResumptionPoint = -1 then
                ValueTask.FromResult(false)
            else
#if StructData
                GenericTaskBuilderStateMachineDataYield.moveNext this.StateMachine.Data
#else
                GenericTaskBuilderStateMachineDataYield.moveNext (this :> GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>)
#endif

        member this.DisposeAsync() =
            if this.StateMachine.ResumptionPoint = -1 then
                ValueTask()
            else
#if StructData
                GenericTaskBuilderStateMachineDataYield.dispose this.StateMachine.Data
#else
                GenericTaskBuilderStateMachineDataYield.dispose (this :> GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>)
#endif

        member this.Current =
#if StructData
                GenericTaskBuilderStateMachineDataYield.getResult this.StateMachine.Data
#else
                GenericTaskBuilderStateMachineDataYield.getResult (this :> GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, 'TInitializer>)
#endif

    interface IAsyncEnumerable<'TResult> with
        member this.GetAsyncEnumerator(_: CancellationToken) =
            let mutable stateMachineCopy = this.StateMachine
            let mutable data = stateMachineCopy.Data
            'TInitializer.Initialize(&stateMachineCopy, &data, ()) :?> IAsyncEnumerator<'TResult>

type [<Struct>] DefaultSeqStateMachineDataInitializer<'TResult, 'TMethodBuilder
    when 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TMethodBuilder :> IAsyncIteratorMethodBuilder> =

    interface IGenericTaskBuilderStateMachineDataInitializer<GenericTaskSeqAsyncEnumerableData<'TMethodBuilder, 'TResult, DefaultSeqStateMachineDataInitializer<'TResult, 'TMethodBuilder>>, unit, IAsyncEnumerable<'TResult>> with
        static member Initialize(stateMachine: byref<'TStateMachine>, _, _) =
            let box = GenericTaskSeqAsyncEnumerable<'TMethodBuilder, 'TResult, 'TStateMachine, DefaultSeqStateMachineDataInitializer<'TResult, 'TMethodBuilder>>(
                MethodBuilder = 'TMethodBuilder.Create(),
                StateMachine = stateMachine
            )
#if StructData
            box.StateMachine.Data <- GenericTaskSeqAsyncEnumerableData(Data = box)
#else
            box.StateMachine.Data <- box
#endif
            box :> IAsyncEnumerable<'TResult>