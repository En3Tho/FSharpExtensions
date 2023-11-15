module En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.ResumableCodeHelpers

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers

let rec WhileDynamic<'TData when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
    (
        sm: byref<ResumableStateMachine<'TData>>,
        condition: unit -> bool,
        body: ResumableCode<'TData, unit>
    ) : bool =
    if sm.Data.CheckCanContinueOrThrow() then
        if condition () then
            if body.Invoke(&sm) then
                WhileDynamic(&sm, condition, body)
            else
                let rf = sm.ResumptionDynamicInfo.ResumptionFunc

                sm.ResumptionDynamicInfo.ResumptionFunc <-
                    ResumptionFunc<'TData>(fun sm -> WhileBodyDynamicAux(&sm, condition, body, rf))

                false
        else
            true
    else
        true

and WhileBodyDynamicAux
    (
        sm: byref<ResumableStateMachine<'TData>>,
        condition: unit -> bool,
        body: ResumableCode<'TData, unit>,
        rf: ResumptionFunc<_>
    ) : bool =
    if rf.Invoke(&sm) then
        WhileDynamic(&sm, condition, body)
    else
        let rf = sm.ResumptionDynamicInfo.ResumptionFunc

        sm.ResumptionDynamicInfo.ResumptionFunc <-
            ResumptionFunc<'TData>(fun sm -> WhileBodyDynamicAux(&sm, condition, body, rf))

        false

let rec WhileDynamicAsync<'TData
        when 'TData :> IAsyncMethodBuilderBase
        and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
    (
        sm: byref<ResumableStateMachine<'TData>>,
        condition: unit -> ValueTask<bool>,
        body: ResumableCode<'TData, unit>
    ) : bool =

    if sm.Data.CheckCanContinueOrThrow() then

        let mutable vtTask = condition()
        let mutable awaiter = vtTask.GetAwaiter()

        let cont =
            ResumptionFunc(fun sm ->
                if awaiter.GetResult() then
                    if body.Invoke(&sm) then
                        WhileDynamicAsync(&sm, condition, body)
                    else
                        let rf = sm.ResumptionDynamicInfo.ResumptionFunc
                        sm.ResumptionDynamicInfo.ResumptionFunc <-
                            ResumptionFunc(fun sm -> WhileBodyDynamicAuxAsync(&sm, condition, body, rf))
                        false
                else
                    true
            )

        if awaiter.IsCompleted then
            cont.Invoke(&sm)
        else
            sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
            sm.ResumptionDynamicInfo.ResumptionFunc <- cont
            false
    else
        true

and WhileBodyDynamicAuxAsync
    (
        sm: byref<ResumableStateMachine<'TData>>,
        condition: unit -> ValueTask<bool>,
        body: ResumableCode<'TData, unit>,
        rf
    ) : bool =
    if rf.Invoke(&sm) then
        WhileDynamicAsync(&sm, condition, body)
    else
        let rf = sm.ResumptionDynamicInfo.ResumptionFunc
        sm.ResumptionDynamicInfo.ResumptionFunc <-
            ResumptionFunc(fun sm -> WhileBodyDynamicAuxAsync(&sm, condition, body, rf))
        false

let inline While<'TData when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
    (
        [<InlineIfLambda>] condition: unit -> bool,
        body: ResumableCode<'TData, unit>
    ) : ResumableCode<'TData, unit> =
    ResumableCode<'TData, unit>(fun sm ->
        if __useResumableCode then
            let mutable __stack_go = true

            while __stack_go && sm.Data.CheckCanContinueOrThrow() && condition() do
                let __stack_body_fin = body.Invoke(&sm)
                __stack_go <- __stack_body_fin

            __stack_go
        else
            WhileDynamic(&sm, condition, body))

let inline WhileAsync<'TData
    when 'TData :> IAsyncMethodBuilderBase
    and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
    (
        [<InlineIfLambda>] condition: unit -> ValueTask<bool>,
        [<InlineIfLambda>] body: ResumableCode<'TData, unit>
    ) : ResumableCode<'TData, unit> =
    ResumableCode<'TData, unit>(fun sm ->
        if __useResumableCode then
            let mutable __stack_go = true
            let mutable __stack_result = false

            while __stack_go do
                if sm.Data.CheckCanContinueOrThrow() then

                    let mutable __stack_fin = true
                    let __stack_vt = condition()
                    let mutable awaiter = __stack_vt.GetAwaiter()

                    if not awaiter.IsCompleted then
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin

                    if __stack_fin then
                        if awaiter.GetResult() then
                            let __stack_body_fin = body.Invoke(&sm)
                            __stack_go <- __stack_body_fin
                        else
                            // finally finished
                            __stack_go <- false
                            __stack_result <- true
                    else
                        sm.Data.AwaitUnsafeOnCompleted(&awaiter, &sm)
                        __stack_go <- false
                else
                    __stack_go <- false
                    __stack_result <- true

            __stack_result
        else
            WhileDynamicAsync(&sm, condition, body))
    
let CombineDynamic<'TData, 'TResult when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
    (
        sm: byref<ResumableStateMachine<'TData>>,
        code1: ResumableCode<'TData, unit>,
        code2: ResumableCode<'TData, 'TResult>
    ) : bool =
        if code1.Invoke(&sm) then
            if sm.Data.CheckCanContinueOrThrow() then
                code2.Invoke(&sm)
            else
                true
        else
            let rec resume (mf: ResumptionFunc<'TData>) =
                ResumptionFunc<'TData>(fun sm ->
                    if mf.Invoke(&sm) then
                        code2.Invoke(&sm)
                    else
                        sm.ResumptionDynamicInfo.ResumptionFunc <- (resume (sm.ResumptionDynamicInfo.ResumptionFunc))
                        false)

            sm.ResumptionDynamicInfo.ResumptionFunc <- (resume (sm.ResumptionDynamicInfo.ResumptionFunc))
            false

let inline Combine<'TData, 'TResult when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(code1: ResumableCode<'TData, unit>, code2: ResumableCode<'TData, 'TResult>) : ResumableCode<'TData, 'TResult> =
    ResumableCode<'TData, 'TResult>(fun sm ->
        if __useResumableCode then
            let __stack_fin = code1.Invoke(&sm)

            if __stack_fin then
                if sm.Data.CheckCanContinueOrThrow() then
                    code2.Invoke(&sm)
                else
                    true
            else
                false
        else
            CombineDynamic(&sm, code1, code2))

let rec TryFinallyCompensateDynamic
    (
        sm: byref<ResumableStateMachine<'TData>>,
        mf: ResumptionFunc<'TData>,
        savedExn: exn option
    ) : bool =
    let mutable fin = false
    fin <- mf.Invoke(&sm)

    if fin then
        match savedExn with
        | None -> true
        | Some exn -> raise exn
    else
        let rf = sm.ResumptionDynamicInfo.ResumptionFunc

        sm.ResumptionDynamicInfo.ResumptionFunc <-
            (ResumptionFunc<'TData>(fun sm -> TryFinallyCompensateDynamic(&sm, rf, savedExn)))

        false

let rec TryFinallyAsyncDynamic<'TData, 'TResult
    when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
        (
            sm: byref<ResumableStateMachine<'TData>>,
            body: ResumableCode<'TData, 'TResult>,
            compensation: ResumableCode<'TData, unit>
        ) : bool =
        if sm.Data.CheckCanContinueOrThrow() then
            let mutable fin = false
            let mutable savedExn = None

            try
                fin <- body.Invoke(&sm)
            with exn ->
                savedExn <- Some exn
                fin <- true

            if fin then
                TryFinallyCompensateDynamic(&sm, ResumptionFunc<'TData>(fun sm -> compensation.Invoke(&sm)), savedExn)
            else
                let rf = sm.ResumptionDynamicInfo.ResumptionFunc

                sm.ResumptionDynamicInfo.ResumptionFunc <-
                    (ResumptionFunc<'TData>(fun sm ->
                        TryFinallyAsyncDynamic(&sm, ResumableCode<'TData, 'TResult>(fun sm -> rf.Invoke(&sm)), compensation)))

                false
        else
            true

let inline TryFinallyAsyncEx<'TData, 'TResult
    when 'TData :> IAsyncMethodBuilderBase
    and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
    (
        body: ResumableCode<'TData, 'TResult>,
        compensation: ResumableCode<'TData, unit>
    ) : ResumableCode<'TData, 'TResult> =
    ResumableCode<'TData, 'TResult>(fun sm ->
        if __useResumableCode then
            if sm.Data.CheckCanContinueOrThrow() then
                let mutable __stack_fin = false
                let mutable __stack_exn = Unchecked.defaultof<_>

                try
                    let __stack_body_fin = body.Invoke(&sm)
                    __stack_fin <- __stack_body_fin
                with exn ->
                    __stack_exn <- exn
                    __stack_fin <- true

                if __stack_fin then
                    let __stack_compensation_fin = compensation.Invoke(&sm)
                    __stack_fin <- __stack_compensation_fin

                if __stack_fin then
                    match __stack_exn with
                    | null -> ()
                    | exn -> raise exn

                __stack_fin
            else
                true
        else
            TryFinallyAsyncDynamic(&sm, body, compensation))

let inline TryFinallyAsync<'TData, 'TResult
    when 'TData :> IAsyncMethodBuilderBase
    and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(
    [<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>,
    [<InlineIfLambda>] compensation: unit -> ValueTask)
    : ResumableCode<'TData, 'TResult> =
    TryFinallyAsyncEx(body, ResumableCode(fun sm ->
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

let inline TryFinally<'TData, 'TResult when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
    ([<InlineIfLambda>] body: ResumableCode<'TData, 'TResult>, [<InlineIfLambda>] compensation: ResumableCode<'TData, unit>) =
    ResumableCode<'TData, 'TResult>(fun sm ->
        if __useResumableCode then
            if sm.Data.CheckCanContinueOrThrow() then
                let mutable __stack_fin = false

                try
                    let __stack_body_fin = body.Invoke(&sm)
                    __stack_fin <- __stack_body_fin
                with _exn ->
                    let __stack_ignore = compensation.Invoke(&sm)
                    reraise ()

                if __stack_fin then
                    let __stack_ignore = compensation.Invoke(&sm)
                    ()

                __stack_fin
            else
                true
        else
            TryFinallyAsyncDynamic(&sm, body, ResumableCode<_, _>(fun sm -> compensation.Invoke(&sm))))

let inline Using
    (
        resource: 'Resource,
        [<InlineIfLambda>] body: 'Resource -> ResumableCode<'TData, 'TResult>
    ) : ResumableCode<'TData, 'TResult> when 'Resource :> IDisposable =
    TryFinally(
        ResumableCode<'TData, 'TResult>(fun sm -> (body resource).Invoke(&sm)),
        ResumableCode<'TData, unit>(fun sm ->
            if not (isNull (box resource)) then
                resource.Dispose()

            true)
    )
    
let rec TryWithDynamic<'TData, 'TResult when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
    (
        sm: byref<ResumableStateMachine<'TData>>,
        body: ResumableCode<'TData, 'TResult>,
        handler: exn -> ResumableCode<'TData, 'TResult>
    ) : bool =
        if sm.Data.CheckCanContinueOrThrow() then
            try
                if body.Invoke(&sm) then
                    true
                else
                    let rf = sm.ResumptionDynamicInfo.ResumptionFunc

                    sm.ResumptionDynamicInfo.ResumptionFunc <-
                        (ResumptionFunc<'TData>(fun sm ->
                            TryWithDynamic(&sm, ResumableCode<'TData, 'TResult>(fun sm -> rf.Invoke(&sm)), handler)))

                    false
            with exn ->
                (handler exn).Invoke(&sm)
        else
            true

let inline TryWith<'TData, 'TResult when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
    (
        body: ResumableCode<'TData, 'TResult>,
        catch: exn -> ResumableCode<'TData, 'TResult>
    ) : ResumableCode<'TData, 'TResult> =
    ResumableCode<'TData, 'TResult>(fun sm ->
        if __useResumableCode then
            if sm.Data.CheckCanContinueOrThrow() then
                //-- RESUMABLE CODE START
                let mutable __stack_fin = false
                let mutable __stack_caught = false
                let mutable __stack_savedExn = Unchecked.defaultof<_>

                try
                    let __stack_body_fin = body.Invoke(&sm)
                    __stack_fin <- __stack_body_fin
                with exn ->
                    __stack_caught <- true
                    __stack_savedExn <- exn

                if __stack_caught then
                    (catch __stack_savedExn).Invoke(&sm)
                else
                    __stack_fin
            else
                true

        else
            TryWithDynamic(&sm, body, catch))