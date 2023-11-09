namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

type GenericTaskBuilderBase() =
    inherit TaskLikeBuilderBase()

    member inline _.Return (value: 'TResult) =
        GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TResult>(fun sm ->
            sm.Data.Result <- value
            true)

    member inline internal this.TryFinallyAsync(
        [<InlineIfLambda>] body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> ValueTask)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult> =
        ResumableCode.TryFinallyAsync(body, GenericTaskCode(fun sm ->
            if __useResumableCode then
                let mutable __stack_condition_fin = true
                let __stack_vtask = compensation()
                let mutable awaiter = __stack_vtask.GetAwaiter()

                if not awaiter.IsCompleted then
                    let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                    __stack_condition_fin <- __stack_yield_fin

                if __stack_condition_fin then
                    awaiter.GetResult()
                else
                    sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)

                __stack_condition_fin
            else
                let vtask = compensation()
                let mutable awaiter = vtask.GetAwaiter()

                let cont =
                    GenericTaskResumptionFunc(fun sm ->
                        awaiter.GetResult()
                        true
                    )

                if awaiter.IsCompleted then
                    cont.Invoke(&sm)
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false
                ))

    member inline this.Using<'Resource, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult
        when 'Resource :> IAsyncDisposable
        and 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
        and 'TAwaiter :> ITaskAwaiter<'TOverall>
        and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>> (
        resource: 'Resource,
        [<InlineIfLambda>] body: 'Resource -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult> =
        this.TryFinallyAsync(
            (fun sm -> (body resource).Invoke(&sm)),
            (fun () ->
                if not (isNull (box resource)) then
                    resource.DisposeAsync()
                else
                    ValueTask()))

type GenericTaskBuilderBase<'TExtensionMarker>() =
    inherit GenericTaskBuilderBase()