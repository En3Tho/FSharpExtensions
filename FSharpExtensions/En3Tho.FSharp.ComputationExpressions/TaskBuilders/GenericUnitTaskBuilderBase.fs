namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators
open Microsoft.FSharp.Collections

type GenericUnitTaskBuilderBase() =

    member inline _.Delay([<InlineIfLambda>] generator: unit -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>) =
        GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>(fun sm -> (generator()).Invoke(&sm))

    [<DefaultValue>]
    member inline _.Zero() : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit> =
        ResumableCode.Zero()

    member inline _.Return() =
        GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>(fun sm ->
            true)

    member inline _.Combine(
        [<InlineIfLambda>] task1: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>,
        [<InlineIfLambda>] task2: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult> =
        ResumableCode.Combine(task1, task2)

    member inline _.While(
        [<InlineIfLambda>] condition: unit -> bool,
        [<InlineIfLambda>] body: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit> =
        ResumableCode.While(condition, body)

    member inline _.TryWith(
        [<InlineIfLambda>] body: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>,
        [<InlineIfLambda>] catch: exn -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult> =
        ResumableCode.TryWith(body, catch)

    member inline _.TryFinally(
        [<InlineIfLambda>] body: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> unit)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult> =
        ResumableCode.TryFinally(body, ResumableCode<_,_>(fun _sm -> compensation(); true))

    member inline _.For(
        sequence: seq<'T>,
        [<InlineIfLambda>] body: 'T -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit> =
        ResumableCode.For(sequence, body)

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

                    if not __stack_condition_fin then
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
                    true
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