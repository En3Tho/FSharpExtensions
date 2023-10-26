namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators
open Microsoft.FSharp.Collections
    
type GenericTaskBuilderBase() =

    member inline _.Delay([<InlineIfLambda>] generator: unit -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>) =
        GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>(fun sm -> (generator()).Invoke(&sm))

    [<DefaultValue>]
    member inline _.Zero() : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, unit> =
        ResumableCode.Zero()

    member inline _.Return (value: 'TResult) =
        GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TResult>(fun sm ->
            sm.Data.Result <- value
            true)

    member inline _.Combine(
        [<InlineIfLambda>] task1: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, unit>,
        [<InlineIfLambda>] task2: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult> =
        ResumableCode.Combine(task1, task2)

    member inline _.While(
        [<InlineIfLambda>] condition: unit -> bool,
        [<InlineIfLambda>] body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, unit>)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, unit> =
        ResumableCode.While(condition, body)

    member inline _.TryWith(
        [<InlineIfLambda>] body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>,
        [<InlineIfLambda>] catch: exn -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult> =
        ResumableCode.TryWith(body, catch)

    member inline _.TryFinally(
        [<InlineIfLambda>] body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> unit)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult> =
        ResumableCode.TryFinally(body, ResumableCode<_,_>(fun _sm -> compensation(); true))

    member inline _.For(
        sequence: seq<'T>,
        [<InlineIfLambda>] body: 'T -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, unit>)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, unit> =
        ResumableCode.For(sequence, body)

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

                    if not __stack_condition_fin then
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
                    true
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