namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers

module GenericUnitTaskBuilderExtensionsLowPriority =
    
    let rec WhileDynamicAsync
        (
            sm: byref<GenericUnitTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask>>,
            condition: unit -> ValueTask<bool>,
            body: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>
        ) : bool =
        let mutable vtTask = condition()
        let mutable awaiter = vtTask.GetAwaiter()

        let cont =
            GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask>(fun sm ->
                if awaiter.GetResult() then
                    if body.Invoke(&sm) then
                        WhileDynamicAsync(&sm, condition, body)
                    else
                        let rf = sm.ResumptionDynamicInfo.ResumptionFunc
                        sm.ResumptionDynamicInfo.ResumptionFunc <-
                            GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask>(fun sm -> WhileBodyDynamicAuxAsync(&sm, condition, body, rf))
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

    and WhileBodyDynamicAuxAsync
        (
            sm: byref<GenericUnitTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask>>,
            condition: unit -> ValueTask<bool>,
            body: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>,
            rf: GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask>
        ) : bool =
        if rf.Invoke(&sm) then
            WhileDynamicAsync(&sm, condition, body)
        else
            let rf = sm.ResumptionDynamicInfo.ResumptionFunc
            sm.ResumptionDynamicInfo.ResumptionFunc <-
                GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask>(fun sm -> WhileBodyDynamicAuxAsync(&sm, condition, body, rf))
            false
    
    type GenericUnitTaskBuilderBase with
        [<NoEagerConstraintApplication>]
        member inline _.Using<'Resource, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult
            when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
            and 'TAwaiter :> ITaskAwaiter
            and 'TTask :> ITaskLike<'TAwaiter>
            and 'Resource :> IDisposable>(
                resource: 'Resource,
                [<InlineIfLambda>] body: 'Resource -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>) =
            ResumableCode.Using(resource, body)
            
        member inline this.WhileAsync
            (
                [<InlineIfLambda>] condition: unit -> ValueTask<bool>,
                [<InlineIfLambda>] body: GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>
            ) : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit> =
            GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    let mutable __stack_go = true
                    let mutable __stack_result = false

                    while __stack_go do
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
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            __stack_go <- false

                    __stack_result
                //-- RESUMABLE CODE END
                else
                    WhileDynamicAsync(&sm, condition, body))

        member inline this.For(sequence: IAsyncEnumerable<'T>, [<InlineIfLambda>] body: 'T -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>) : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit> =
            this.Using(
                sequence.GetAsyncEnumerator(),
                (fun e ->
                    this.WhileAsync(
                        (fun () ->
                            __debugPoint "ForLoop.InOrToKeyword"
                            e.MoveNextAsync()),
                        GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, unit>(fun sm -> (body e.Current).Invoke(&sm))
                    ))
            )