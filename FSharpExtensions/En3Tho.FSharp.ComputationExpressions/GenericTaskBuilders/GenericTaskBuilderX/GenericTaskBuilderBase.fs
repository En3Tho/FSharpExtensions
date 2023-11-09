namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder2

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type GenericTaskBuilder2Core() =

    member inline _.Delay([<InlineIfLambda>] generator: unit -> ResumableCode<'TData, 'TResult>) =
        ResumableCode(fun sm -> (generator()).Invoke(&sm))

    [<DefaultValue>]
    member inline _.Zero() : ResumableCode<'TData, unit> =
        ResumableCode.Zero()

    member inline _.Combine(
        [<InlineIfLambda>] task1: ResumableCode<'TData, unit>,
        [<InlineIfLambda>] task2: ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        ResumableCode.Combine(task1, task2)

    member inline _.While<'TData when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(
        [<InlineIfLambda>] condition: unit -> bool,
        [<InlineIfLambda>] body: ResumableCode<'TData, unit>)
        : ResumableCode<'TData, unit> =
            ResumableCode.While(condition, ResumableCode(fun sm -> if sm.Data.CheckCanContinueOrThrow() then body.Invoke(&sm) else true))

    member inline _.TryWith(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] catch: exn -> ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        ResumableCode.TryWith(body, catch)

    member inline _.TryFinally(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> unit)
        : ResumableCode<'TData, 'TResult> =
        ResumableCode.TryFinally(body, ResumableCode(fun _sm -> compensation(); true))

    member inline _.For<'TData, 'T when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(
        sequence: seq<'T>,
        [<InlineIfLambda>] body: 'T -> ResumableCode<'TData, unit>)
        : ResumableCode<'TData, unit> =
        ResumableCode.For(sequence, fun value -> ResumableCode(fun sm -> if sm.Data.CheckCanContinueOrThrow() then (body(value)).Invoke(&sm) else true))

    member inline internal this.TryFinallyAsync<'TData, 'TResult
        when 'TData :> IAsyncMethodBuilderBase>(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> ValueTask)
        : ResumableCode<'TData, 'TResult> =
        ResumableCode.TryFinallyAsync(body, ResumableCode(fun sm ->
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
                    sm.Data.AwaitUnsafeOnCompleted(&awaiter, &sm)

                __stack_condition_fin
            else
                let vtask = compensation()
                let mutable awaiter = vtask.GetAwaiter()

                let cont =
                    ResumptionFunc(fun sm ->
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

    member inline this.Using<'TData, 'TResource, 'TResult
        when 'TData :> IAsyncMethodBuilderBase
        and 'TResource :> IAsyncDisposable>(
        resource: 'TResource,
        [<InlineIfLambda>] body: 'TResource -> ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        this.TryFinallyAsync<'TData, 'TResult>(
            (fun sm -> (body resource).Invoke(&sm)),
            (fun () ->
                if not (isNull (box resource)) then
                    resource.DisposeAsync()
                else
                    ValueTask()))

type GenericTaskBuilder2Core<'TExtensionsMarker>() =
    inherit GenericTaskBuilder2Core()