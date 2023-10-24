namespace En3Tho.FSharp.ComputationExpressions.Tasks.GenericUnitTaskBuilderExtensions

open En3Tho.FSharp.ComputationExpressions.Tasks
open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

module LowPriority =

    type GenericUnitTaskBuilderBase with

        [<NoEagerConstraintApplication>]
        static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TAwaiter, 'TTask
            when 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask>
            and 'TAwaiter :> ITaskAwaiter
            and 'TTask :> ITaskLike<'TAwaiter>

            and ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
            and ^Awaiter :> ICriticalNotifyCompletion
            and ^Awaiter: (member get_IsCompleted: unit -> bool)
            and ^Awaiter: (member GetResult: unit -> 'TResult1)>
            (sm: byref<_>,
             task: ^TaskLike,
             [<InlineIfLambda>] continuation: 'TResult1 -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>) : bool =

            let mutable awaiter = (^TaskLike: (member GetAwaiter : unit -> ^Awaiter)(task))

            let cont =
                GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask>(fun sm ->
                   let result = (^Awaiter : (member GetResult: unit -> 'TResult1)(awaiter))
                   (continuation result).Invoke(&sm))

            // shortcut to continue immediately
            if (^Awaiter : (member get_IsCompleted : unit -> bool)(awaiter)) then
                cont.Invoke(&sm)
            else
                sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        [<NoEagerConstraintApplication>]
        member inline _.Bind< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TAwaiter, 'TTask
            when 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask>
            and 'TAwaiter :> ITaskAwaiter
            and 'TTask :> ITaskLike<'TAwaiter>

            and ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
            and ^Awaiter :> ICriticalNotifyCompletion
            and ^Awaiter: (member get_IsCompleted: unit -> bool)
            and ^Awaiter: (member GetResult: unit -> 'TResult1)>

            (task: ^TaskLike,
             [<InlineIfLambda>] continuation: 'TResult1 -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>)
            : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2> =

            GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the awaitable
                    let mutable awaiter = (^TaskLike: (member GetAwaiter : unit -> ^Awaiter)(task))

                    let mutable __stack_fin = true
                    if not (^Awaiter : (member get_IsCompleted : unit -> bool)(awaiter)) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin

                    if __stack_fin then
                        let result = (^Awaiter : (member GetResult: unit -> 'TResult1)(awaiter))
                        (continuation result).Invoke(&sm)
                    else
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                        false
                    //-- RESUMABLE CODE END
                else
                    GenericUnitTaskBuilderBase.BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TAwaiter, 'TTask>(&sm, task, continuation)
            )

        [<NoEagerConstraintApplication>]
        member inline this.ReturnFrom (task: ^TaskLike) =
            this.Bind(task, (fun v -> this.Return()))

        [<NoEagerConstraintApplication>]
        member inline _.Using<'Resource, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult
            when 'TMethodBuilder :> IAsyncMethodBuilder<'TMethodBuilder, 'TAwaiter, 'TTask>
            and 'TAwaiter :> ITaskAwaiter
            and 'TTask :> ITaskLike<'TAwaiter>
            and 'Resource :> IDisposable> (
                resource: 'Resource,
                [<InlineIfLambda>] body: 'Resource -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult>) =
            ResumableCode.Using(resource, body)

module HighPriority =

    type GenericUnitTaskBuilderBase with
        static member BindDynamic (sm: byref<_>, task: Task<'TResult1>, continuation: 'TResult1 -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>) : bool =
            let mutable awaiter = task.GetAwaiter()

            let cont =
                (GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask>(fun sm ->
                    let result = awaiter.GetResult()
                    (continuation result).Invoke(&sm)))

            // shortcut to continue immediately
            if awaiter.IsCompleted then
                cont.Invoke(&sm)
            else
                sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        member inline _.Bind (task: Task<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>) =
            GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, _>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()

                    let mutable __stack_fin = true
                    if not awaiter.IsCompleted then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        (continuation result).Invoke(&sm)
                    else
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                        false
                else
                    GenericUnitTaskBuilderBase.BindDynamic(&sm, task, continuation)
                //-- RESUMABLE CODE END
            )
        member inline this.ReturnFrom (task: Task<'TResult>) : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult> =
            this.Bind(task, (fun v -> this.Return()))

module MediumPriority =
    open HighPriority

    // Medium priority extensions
    type GenericUnitTaskBuilderBase with
        member inline this.Bind (computation: Async<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>) =
            this.Bind (Async.StartAsTask computation, continuation)

        member inline this.ReturnFrom (computation: Async<'TResult>) =
            this.ReturnFrom (Async.StartAsTask computation)