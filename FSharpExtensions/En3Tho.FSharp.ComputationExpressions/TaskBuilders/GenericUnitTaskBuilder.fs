namespace En3Tho.FSharp.ComputationExpressions.Tasks

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators
open Microsoft.FSharp.Collections

type GenericUnitTaskBuilderBase() =

    member inline _.Delay(
        [<InlineIfLambda>] generator: unit -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>) =
        GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>(fun sm -> (generator()).Invoke(&sm))

    /// Used to represent no-ops like the implicit empty "else" branch of an "if" expression.
    [<DefaultValue>]
    member inline _.Zero() : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit> =
        ResumableCode.Zero()

    member inline _.Return() =
        GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>(fun sm ->
            true)

    /// Chains together a step with its following step.
    /// Note that this requires that the first step has no result.
    /// This prevents constructs like `task { return 1; return 2; }`.
    member inline _.Combine(
        [<InlineIfLambda>] task1: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>,
        [<InlineIfLambda>] task2: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult> =
        ResumableCode.Combine(task1, task2)

    /// Builds a step that executes the body while the condition predicate is true.
    member inline _.While (
        [<InlineIfLambda>] condition: unit -> bool,
        [<InlineIfLambda>] body: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit> =
        ResumableCode.While(condition, body)

    /// Wraps a step in a try/with. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryWith (
        [<InlineIfLambda>] body: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>,
        [<InlineIfLambda>] catch: exn -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult> =
        ResumableCode.TryWith(body, catch)

    /// Wraps a step in a try/finally. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryFinally (
        [<InlineIfLambda>] body: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> unit)
        : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult> =
        ResumableCode.TryFinally(body, ResumableCode<_,_>(fun _sm -> compensation(); true))

    member inline _.For (
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

                // shortcut to continue immediately
                if awaiter.IsCompleted then
                    true
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false
                ))

    member inline this.Using<'Resource, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult
        when 'Resource :> IAsyncDisposable
        and 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask>
        and 'TAwaiter :> ITaskAwaiter
        and 'TTask :> ITaskLike<'TAwaiter>> (
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

type GenericUnitTaskBuilder<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResultTask
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask>
    and 'TAwaiter :> ITaskAwaiter
    and 'TTask :> ITaskLike<'TAwaiter>
    and 'TTask :> ITaskLikeTask<'TResultTask>>() =

    inherit GenericUnitTaskBuilderBase()

    static member inline RunDynamic(
        [<InlineIfLambda>] code: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>)
        : 'TTask =
        let mutable sm = GenericUnitTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask>()
        let initialResumptionFunc = GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask>(fun sm -> code.Invoke(&sm))
        let resumptionInfo =
            { new GenericUnitTaskResumptionDynamicInfo<'TMethodBuilder, 'TAwaiter, 'TTask>(initialResumptionFunc) with
                member info.MoveNext(sm) =
                    let mutable savedExn = null
                    try
                        sm.ResumptionDynamicInfo.ResumptionData <- null
                        let step = info.ResumptionFunc.Invoke(&sm)
                        if step then
                            sm.Data.MethodBuilder.SetResult()
                        else
                            let mutable awaiter = sm.ResumptionDynamicInfo.ResumptionData :?> ICriticalNotifyCompletion
                            assert not (isNull awaiter)
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)

                    with exn ->
                        savedExn <- exn
                    // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                    match savedExn with
                    | null -> ()
                    | exn -> sm.Data.MethodBuilder.SetException exn

                member _.SetStateMachine(sm, state) =
                    sm.Data.MethodBuilder.SetStateMachine(state)
                }
        sm.ResumptionDynamicInfo <- resumptionInfo
        sm.Data.MethodBuilder <- 'TMethodBuilder.Create()
        sm.Data.MethodBuilder.Start(&sm)
        sm.Data.MethodBuilder.Task

    static member inline Run(
        [<InlineIfLambda>] code: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>)
        : 'TTask =
         if __useResumableCode then
            __stateMachine<GenericUnitTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask>, 'TTask>
                (MoveNextMethodImpl<_>(fun sm ->
                    //-- RESUMABLE CODE START
                    __resumeAt sm.ResumptionPoint
                    let mutable __stack_exn: Exception = null
                    try
                        let __stack_code_fin = code.Invoke(&sm)
                        if __stack_code_fin then
                            sm.Data.MethodBuilder.SetResult()
                    with exn ->
                        __stack_exn <- exn
                    // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                    match __stack_exn with
                    | null -> ()
                    | exn -> sm.Data.MethodBuilder.SetException exn
                    //-- RESUMABLE CODE END
                ))
                (SetStateMachineMethodImpl<_>(fun sm state -> sm.Data.MethodBuilder.SetStateMachine(state)))
                (AfterCode<_,_>(fun sm ->
                    sm.Data.MethodBuilder <- 'TMethodBuilder.Create()
                    sm.Data.MethodBuilder.Start(&sm)
                    sm.Data.MethodBuilder.Task))
         else
            GenericUnitTaskBuilder.RunDynamic(code)

    member inline _.Run([<InlineIfLambda>] code: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>) : 'TResultTask =
        GenericUnitTaskBuilder.Run(code).Task