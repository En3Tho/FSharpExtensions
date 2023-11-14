namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2.Tasks

open System
open System.Runtime.CompilerServices
open System.Threading
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder2
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilders.GenericTaskBuilder2
open Microsoft.FSharp.Core

open UnsafeEx

type [<Struct>] SyncContextTaskStateMachineDataInitializer<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TResult>
    and 'TMethodBuilder :> IAsyncMethodBuilderCreator<'TMethodBuilder>
    and 'TAwaiter :> ITaskAwaiter<'TResult>
    and 'TTask :> ITaskLike<'TAwaiter, 'TResult>
    and 'TTask :> ITaskLikeTask<'TBuilderResult>> =
    interface IGenericTaskBuilderStateMachineDataInitializer<StateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>, SynchronizationContext, 'TBuilderResult> with
        static member Initialize(sm: byref<'a>, data, state) =
            data.MethodBuilder <- 'TMethodBuilder.Create()

            let ctx = SynchronizationContext.Current
            if Object.ReferenceEquals(state, ctx) then
                data.MethodBuilder.Start(&sm)
                data.MethodBuilder.Task.Task
            else
                let fieldOffset = Unsafe.ByteOffset(&sm, &data)
                let box = StateMachineBox(StateMachine = sm, DataFieldOffset = fieldOffset)
                state.Post((fun o -> // TODO: static callback
                    let mutable box = o :?> StateMachineBox<'a>
                    let mutable data = &Unsafe.As<'a, StateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>>(&box.StateMachine, box.DataFieldOffset)
                    data.MethodBuilder.Start(&box)
                ), box)
                let data = &Unsafe.As<'a, StateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TBuilderResult>>(&box.StateMachine, fieldOffset)
                data.MethodBuilder.Task.Task

type SyncContextTask(state) =
    inherit GenericTaskBuilder2WithStateBase<SynchronizationContext>(state)
    member inline this.Run([<InlineIfLambda>] code) =
        this.RunInternal<StateMachineData<AsyncTaskMethodBuilderWrapper<'a>,_,_,_,_>,_,_,SyncContextTaskStateMachineDataInitializer<_,_,_,_,_>>(code)