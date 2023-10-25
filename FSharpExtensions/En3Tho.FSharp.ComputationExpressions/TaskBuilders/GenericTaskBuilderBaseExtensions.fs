namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers

module rec GenericTaskBuilderExtensionsLowPriority =

    type GenericTaskBuilderBase with
        [<NoEagerConstraintApplication>]
        member inline _.Using<'Resource, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
            when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
            and 'TAwaiter :> ITaskAwaiter<'TOverall>
            and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>
            and 'Resource :> IDisposable>(resource: 'Resource,
                [<InlineIfLambda>] body: 'Resource -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>) =
            ResumableCode.Using(resource, body)

        member this.WhileDynamicBodyAsync
            (
                sm: byref<GenericTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>>,
                awaiter: byref<ValueTaskAwaiter<bool>>,
                condition: unit -> ValueTask<bool>,
                body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>
            ) : bool =

            if awaiter.GetResult() then
                if body.Invoke(&sm) then
                    this.WhileDynamicAsync(&sm, condition, body)
                else
                    let rf = sm.ResumptionDynamicInfo.ResumptionFunc
                    sm.ResumptionDynamicInfo.ResumptionFunc <-
                        GenericTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>(fun sm -> this.WhileBodyDynamicAuxAsync(&sm, condition, body, rf))
                    false
            else
                true

        member this.WhileDynamicAsync
            (
                sm: byref<GenericTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>>,
                condition: unit -> ValueTask<bool>,
                body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>
            ) : bool =
            let mutable vtTask = condition()
            let mutable awaiter = vtTask.GetAwaiter()

            let cont =
                GenericTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>(fun sm ->
                    this.WhileDynamicBodyAsync(&sm, &awaiter, condition, body)
                )

            if awaiter.IsCompleted then
                this.WhileDynamicBodyAsync(&sm, &awaiter, condition, body)
            else
                sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        member this.WhileBodyDynamicAuxAsync
            (
                sm: byref<GenericTaskStateMachine<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>>,
                condition: unit -> ValueTask<bool>,
                body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>,
                rf: GenericTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>
            ) : bool =
            if rf.Invoke(&sm) then
                this.WhileDynamicAsync(&sm, condition, body)
            else
                let rf = sm.ResumptionDynamicInfo.ResumptionFunc

                sm.ResumptionDynamicInfo.ResumptionFunc <-
                    GenericTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>(fun sm -> this.WhileBodyDynamicAuxAsync(&sm, condition, body, rf))

                false

        member inline this.WhileAsync
            (
                [<InlineIfLambda>] condition: unit -> ValueTask<bool>,
                [<InlineIfLambda>] body: GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>
            ) : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall> =
            GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>(fun sm ->
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
                                __stack_go <- false
                                __stack_result <- true // finally finished
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            __stack_go <- false

                    __stack_result
                //-- RESUMABLE CODE END
                else
                    this.WhileDynamicAsync(&sm, condition, body))

        member inline this.For(sequence: IAsyncEnumerable<'T>, [<InlineIfLambda>] body: 'T -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>) : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall> =
            this.Using(
                sequence.GetAsyncEnumerator(),
                (fun e ->
                    this.WhileAsync(
                        (fun () ->
                            __debugPoint "ForLoop.InOrToKeyword"
                            e.MoveNextAsync()),
                        GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TOverall>(fun sm -> (body e.Current).Invoke(&sm))
                    ))
            )