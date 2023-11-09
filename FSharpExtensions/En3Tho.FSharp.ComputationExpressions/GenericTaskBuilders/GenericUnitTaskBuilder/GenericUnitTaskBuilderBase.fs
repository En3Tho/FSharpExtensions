namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

type GenericUnitTaskBuilderBase() =
    inherit TaskLikeBuilderBase()

    member inline _.Return() =
        GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>(fun sm ->
            true)

    member inline internal this.TryFinallyAsync(
        [<InlineIfLambda>] body: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> ValueTask)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult> =
        ResumableCode.TryFinallyAsync(body, GenericUnitTaskCode(fun sm ->
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
                    GenericUnitTaskResumptionFunc(fun sm ->
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

    member inline this.Using<'Resource, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult
        when 'Resource :> IAsyncDisposable
        and 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
        and 'TAwaiter :> ITaskAwaiter
        and 'TTask :> ITaskLike<'TAwaiter>>(
        resource: 'Resource,
        [<InlineIfLambda>] body: 'Resource -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult> =
        this.TryFinallyAsync(
            (fun sm -> (body resource).Invoke(&sm)),
            (fun () ->
                if not (isNull (box resource)) then
                    resource.DisposeAsync()
                else
                    ValueTask()))

type GenericUnitTaskBuilderBase<'TExtensionMarker>() =
    inherit GenericUnitTaskBuilderBase()