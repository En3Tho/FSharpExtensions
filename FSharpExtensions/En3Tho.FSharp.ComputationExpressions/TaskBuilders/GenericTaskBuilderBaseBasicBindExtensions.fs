﻿namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

type IGenericTaskBuilderBasicBindExtensions = interface end

module GenericTaskBuilderBasicBindExtensionsLowPriority =

    [<AbstractClass; Sealed; Extension>]
    type GenericTaskBuilderBasicBindExtensionsLowPriorityImpl() =

        [<NoEagerConstraintApplication>]
        static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
            when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
            and 'TAwaiter :> ITaskAwaiter<'TOverall>
            and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>

            and ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
            and ^Awaiter :> ICriticalNotifyCompletion
            and ^Awaiter: (member get_IsCompleted: unit -> bool)
            and ^Awaiter: (member GetResult: unit -> 'TResult1)>
            (sm: byref<_>,
             task: ^TaskLike,
             [<InlineIfLambda>] continuation: 'TResult1 -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult2>)
            : bool =

            let mutable awaiter = (^TaskLike: (member GetAwaiter: unit -> ^Awaiter) task)

            let cont =
                GenericTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>(fun sm ->
                   let result = (^Awaiter: (member GetResult: unit -> 'TResult1) awaiter)
                   (continuation result).Invoke(&sm))

            // shortcut to continue immediately
            if (^Awaiter: (member get_IsCompleted: unit -> bool) awaiter) then
                cont.Invoke(&sm)
            else
                sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        [<NoEagerConstraintApplication; Extension>]
            static member inline Bind< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall
                when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask, 'TOverall>
                and 'TAwaiter :> ITaskAwaiter<'TOverall>
                and 'TTask :> ITaskLike<'TAwaiter, 'TOverall>

                and ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
                and ^Awaiter :> ICriticalNotifyCompletion
                and ^Awaiter: (member get_IsCompleted: unit -> bool)
                and ^Awaiter: (member GetResult: unit -> 'TResult1)>
                (_: GenericTaskBuilderBase<IGenericTaskBuilderBasicBindExtensions>, task: ^TaskLike,
                 [<InlineIfLambda>] continuation: 'TResult1 -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult2>)
                : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult2> =

                GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult2>(fun sm ->
                    if __useResumableCode then
                        //-- RESUMABLE CODE START
                        // Get an awaiter from the awaitable
                        let mutable awaiter = (^TaskLike: (member GetAwaiter: unit -> ^Awaiter) task)

                        let mutable __stack_fin = true
                        if not (^Awaiter: (member get_IsCompleted: unit -> bool) awaiter) then
                            // This will yield with __stack_yield_fin = false
                            // This will resume with __stack_yield_fin = true
                            let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                            __stack_fin <- __stack_yield_fin

                        if __stack_fin then
                            let result = (^Awaiter: (member GetResult: unit -> 'TResult1) awaiter)
                            (continuation result).Invoke(&sm)
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                        //-- RESUMABLE CODE END
                    else
                        GenericTaskBuilderBasicBindExtensionsLowPriorityImpl.BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>(&sm, task, continuation)
                )

            [<NoEagerConstraintApplication; Extension>]
            static member inline ReturnFrom (this: GenericTaskBuilderBase<IGenericTaskBuilderBasicBindExtensions>, task: ^TaskLike) =
                this.Bind(task, (fun v -> this.Return v))

module GenericTaskBuilderBasicBindExtensionsHighPriority =

    [<AbstractClass; Sealed; Extension>]
    type GenericTaskBuilderBasicBindExtensionsHighPriorityImpl() =

        static member BindDynamic(sm: byref<_>, task: Task<'TResult1>, continuation: 'TResult1 -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult2>) : bool =
            let mutable awaiter = task.GetAwaiter()

            let cont =
                GenericTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall>(fun sm ->
                   let result = awaiter.GetResult()
                   (continuation result).Invoke(&sm))

            // shortcut to continue immediately
            if awaiter.IsCompleted then
                cont.Invoke(&sm)
            else
                sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        [<Extension>]
        static member inline Bind(_: GenericTaskBuilderBase<IGenericTaskBuilderBasicBindExtensions>, task: Task<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult2>) =
            GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, _>(fun sm ->
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
                    GenericTaskBuilderBasicBindExtensionsHighPriorityImpl.BindDynamic(&sm, task, continuation)
                //-- RESUMABLE CODE END
            )

        [<Extension>]
        static member inline ReturnFrom(this: GenericTaskBuilderBase<IGenericTaskBuilderBasicBindExtensions>, task: Task<'TResult>) : GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult, 'TResult> =
            this.Bind(task, (fun v -> this.Return v))

module GenericTaskBuilderBasicBindExtensionsMediumPriority =
    open GenericTaskBuilderBasicBindExtensionsHighPriority

    [<AbstractClass; Sealed; Extension>]
    type GenericTaskBuilderBasicBindExtensionsMediumPriorityImpl() =
        [<Extension>]
        static member inline Bind(this: GenericTaskBuilderBase<IGenericTaskBuilderBasicBindExtensions>, computation: Async<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> GenericTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TOverall, 'TResult2>) =
            this.Bind (Async.StartAsTask computation, continuation)

        [<Extension>]
        static member inline ReturnFrom(this: GenericTaskBuilderBase<IGenericTaskBuilderBasicBindExtensions>, computation: Async<'TResult>) =
            this.ReturnFrom (Async.StartAsTask computation)