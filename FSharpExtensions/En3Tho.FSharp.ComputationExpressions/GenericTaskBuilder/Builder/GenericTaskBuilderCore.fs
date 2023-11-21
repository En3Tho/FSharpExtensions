namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

type GenericTaskBuilderCore() =

    [<DefaultValue>]
    member inline _.Zero() : ResumableCode<'TData, unit> =
        ResumableCode.Zero()

    member inline _.Combine(
        [<InlineIfLambda>] task1: ResumableCode<'TData, unit>,
        [<InlineIfLambda>] task2: ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        ResumableCodeHelpers.Combine(task1, task2)

    member inline _.While(
        [<InlineIfLambda>] condition: unit -> bool,
        [<InlineIfLambda>] body: ResumableCode<'TData, unit>)
        : ResumableCode<'TData, unit> =
            ResumableCodeHelpers.While(condition, body)

    member inline _.For<'TData, 'T when 'TData :> IStateMachineDataWithCheck<'TData>>(
        sequence: seq<'T>,
        [<InlineIfLambda>] body: 'T -> ResumableCode<'TData, unit>)
        : ResumableCode<'TData, unit> =
            ResumableCodeHelpers.Using(
                sequence.GetEnumerator(),
                (fun e ->
                    ResumableCodeHelpers.While(
                        (fun () ->
                            __debugPoint "ForLoop.InOrToKeyword"
                            e.MoveNext()),
                        ResumableCode<'TData, unit>(fun sm -> (body e.Current).Invoke(&sm))
                    ))
            )

    member inline _.TryWith<'TData, 'TResult when 'TData :> IStateMachineDataWithCheck<'TData>>(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] catch: exn -> ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
            ResumableCodeHelpers.TryWith(body, catch)

    member inline _.TryFinally<'TData, 'TResult when 'TData :> IStateMachineDataWithCheck<'TData>>(
        [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
        [<InlineIfLambda>] compensation: unit -> unit)
        : ResumableCode<'TData, 'TResult> =
           ResumableCodeHelpers.TryFinally(body, ResumableCode(fun _sm -> compensation(); true))

    member inline this.Using<'TData, 'TResource, 'TResult
        when 'TData :> IAsyncMethodBuilderBase
        and 'TData :> IStateMachineDataWithCheck<'TData>
        and 'TResource :> IAsyncDisposable>(
        resource: 'TResource,
        [<InlineIfLambda>] body: 'TResource -> ResumableCode<'TData, 'TResult>)
        : ResumableCode<'TData, 'TResult> =
        ResumableCodeHelpers.TryFinallyAsyncUsing<'TData, 'TResult>(
            ResumableCode<'TData, 'TResult>(fun sm -> (body resource).Invoke(&sm)),
            (fun () ->
                if not (isNull (box resource)) then
                    resource.DisposeAsync()
                else
                    ValueTask()))

type GenericTaskBuilderCore<'TState>(state: 'TState) =
    inherit GenericTaskBuilderCore()

    member _.State = state

    member inline this.Bind<'TData, 'TResult when 'TData :> IStateMachineDataWithState<'TData, 'TState>>(_: StateIntrinsic, [<InlineIfLambda>] continuation: 'TState -> ResumableCode<'TData, 'TResult>) =
        ResumableCode<'TData, 'TResult>(fun sm ->
            let state: 'TState = sm.Data.State
            (continuation state).Invoke(&sm)
        )

    static member inline RunDynamic<'TData, 'TResult, 'TBuilderResult, 'TInitializer
        when 'TData :> IStateMachineData<'TData, 'TResult>
        and 'TInitializer :> IStateMachineDataInitializer<'TData, 'TState, 'TBuilderResult>>
        ([<InlineIfLambda>] code: ResumableCode<'TData, 'TResult>, state: 'TState) : 'TBuilderResult =

        let mutable sm = ResumableStateMachine<'TData>()
        let initialResumptionFunc = ResumptionFunc<'TData>(fun sm -> code.Invoke(&sm))
        let resumptionInfo =
            { new ResumptionDynamicInfo<'TData>(initialResumptionFunc) with
                member info.MoveNext(sm) =
                    let mutable savedExn = null
                    if sm.Data.CheckCanContinueOrThrow() then
                        try
                            sm.ResumptionDynamicInfo.ResumptionData <- null
                            let step = info.ResumptionFunc.Invoke(&sm)

                            if step then
                                sm.ResumptionPoint <- StateMachineCodes.Finished
                                sm.Data.Finish(&sm)
                            else
                                let mutable awaiter = sm.ResumptionDynamicInfo.ResumptionData :?> ICriticalNotifyCompletion
                                assert not (isNull awaiter)
                                sm.Data.AwaitUnsafeOnCompleted(&awaiter, &sm)
                        with exn ->
                            savedExn <- exn
                        // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                        match savedExn with
                        | null -> ()
                        | exn ->
                            sm.ResumptionPoint <- StateMachineCodes.Finished
                            sm.Data.SetException(exn)
                    else
                        sm.ResumptionPoint <- StateMachineCodes.Finished
                        sm.Data.Finish(&sm)

                member _.SetStateMachine(sm, state) =
                    sm.Data.SetStateMachine(state)
            }

        sm.ResumptionDynamicInfo <- resumptionInfo
        'TInitializer.Initialize<_, 'TData, 'TState>(&sm, &sm.Data, state)

    member inline this.RunInternal<'TData, 'TResult, 'TBuilderResult, 'TInitializer
        when 'TData :> IStateMachineData<'TData, 'TResult>
        and 'TInitializer :> IStateMachineDataInitializer<'TData, 'TState, 'TBuilderResult>>
        ([<InlineIfLambda>] code: ResumableCode<'TData, 'TResult>) : 'TBuilderResult =

        (if __useResumableCode then
            __stateMachine<'TData, 'TBuilderResult>
                (MoveNextMethodImpl<_>(fun sm ->
                    __resumeAt sm.ResumptionPoint
                    let mutable __stack_exn: Exception = null
                    try
                        if sm.Data.CheckCanContinueOrThrow() then
                            let __stack_code_fin = code.Invoke(&sm)
                            if __stack_code_fin then
                                sm.ResumptionPoint <- StateMachineCodes.Finished
                                sm.Data.Finish(&sm)
                        else
                            sm.ResumptionPoint <- StateMachineCodes.Finished
                            sm.Data.Finish(&sm)
                    with exn ->
                        __stack_exn <- exn
                    // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                    match __stack_exn with
                    | null -> ()
                    | exn ->
                        sm.ResumptionPoint <- StateMachineCodes.Finished
                        sm.Data.SetException(exn)
                ))
                (SetStateMachineMethodImpl<_>(fun sm state -> sm.Data.SetStateMachine(state)))
                (AfterCode<_,_>(fun sm ->
                    'TInitializer.Initialize<_, 'TData, 'TState>(&sm, &sm.Data, this.State)))
        else
            GenericTaskBuilderCore<'TState>.RunDynamic(code, this.State))

type  GenericTaskBuilderReturnCore<'TState>(state: 'TState) =
    inherit GenericTaskBuilderCore<'TState>(state)
    member inline _.Delay([<InlineIfLambda>] generator: unit -> ResumableCode<'TData, 'TResult>) =
        ResumableCode<'TData, 'TResult>(fun sm -> (generator()).Invoke(&sm))

type GenericTaskBuilderYieldCore<'TState>(state: 'TState) =
    inherit GenericTaskBuilderCore<'TState>(state)

    member inline _.Delay([<InlineIfLambda>] generator: unit -> ResumableCode<'TData, 'TResult>) =
        ResumableCode<'TData, _>(fun sm -> (generator()).Invoke(&sm))