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
    
type GenericTaskBuilderBase() =

    member inline _.Delay([<InlineIfLambda>] generator: unit -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>) =
        ResumableCode.Delay(generator)

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

    member inline _.While (
        [<InlineIfLambda>] condition: unit -> bool,
        [<InlineIfLambda>] body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, unit>)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, unit> =
        ResumableCode.While(condition, body)

    member inline _.TryWith (
        [<InlineIfLambda>] body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>,
        [<InlineIfLambda>] catch: exn -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult> =
        ResumableCode.TryWith(body, catch)

    member inline _.TryFinally (
        [<InlineIfLambda>] body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> unit)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult> =
        ResumableCode.TryFinally(body, ResumableCode<_,_>(fun _sm -> compensation(); true))

    member inline _.For (
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

                // shortcut to continue immediately
                if awaiter.IsCompleted then
                    true
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false
                ))

    member inline this.Using<'Resource, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
        when 'Resource :> IAsyncDisposable
        and 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>
        and 'TAwaiter :> ITaskAwaiter<'TOverall>
        and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>> (
        resource: 'Resource,
        [<InlineIfLambda>] body: 'Resource -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>)
        : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall> =
        this.TryFinallyAsync(
            (fun sm -> (body resource).Invoke(&sm)),
            (fun () ->
                if not (isNull (box resource)) then
                    resource.DisposeAsync()
                else
                    ValueTask()))

type GenericTaskBuilder<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResultTask
    when 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>
    and 'TAwaiter :> ITaskAwaiter<'TOverall>
    and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>
    and 'TTask :> ITaskLikeTask<'TResultTask>>() =

    inherit GenericTaskBuilderBase()

    static member inline RunDynamic([<InlineIfLambda>] code: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>) : 'TTask =
        let mutable sm = GenericTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>()
        let initialResumptionFunc = GenericTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>(fun sm -> code.Invoke(&sm))
        let resumptionInfo =
            { new GenericTaskResumptionDynamicInfo<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>(initialResumptionFunc) with
                member info.MoveNext(sm) =
                    let mutable savedExn = null
                    try
                        sm.ResumptionDynamicInfo.ResumptionData <- null
                        let step = info.ResumptionFunc.Invoke(&sm)
                        if step then
                            sm.Data.MethodBuilder.SetResult(sm.Data.Result)
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

    static member inline Run([<InlineIfLambda>] code: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>) : 'TTask =
         if __useResumableCode then
            __stateMachine<GenericTaskStateMachineData<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>, 'TTask>
                (MoveNextMethodImpl<_>(fun sm ->
                    //-- RESUMABLE CODE START
                    __resumeAt sm.ResumptionPoint
                    let mutable __stack_exn: Exception = null
                    try
                        let __stack_code_fin = code.Invoke(&sm)
                        if __stack_code_fin then
                            sm.Data.MethodBuilder.SetResult(sm.Data.Result)
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
            GenericTaskBuilder.RunDynamic(code)

    member inline _.Run([<InlineIfLambda>] code: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult>) : 'TResultTask =
       GenericTaskBuilder.Run(code).Task