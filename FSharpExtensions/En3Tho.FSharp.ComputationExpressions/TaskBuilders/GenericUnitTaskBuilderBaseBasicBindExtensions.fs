namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

type IGenericUnitTaskBuilderBasicBindExtensions = interface end

module GenericUnitTaskBuilderBasicBindExtensionsLowPriority =

    [<AbstractClass; Sealed; Extension>]
    type GenericUnitTaskBuilderBasicBindExtensionsLowPriorityImpl() =

        [<NoEagerConstraintApplication>]
        static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TAwaiter, 'TTask
            when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
            and 'TAwaiter :> ITaskAwaiter
            and 'TTask :> ITaskLike<'TAwaiter>

            and ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
            and ^Awaiter :> ICriticalNotifyCompletion
            and ^Awaiter: (member get_IsCompleted: unit -> bool)
            and ^Awaiter: (member GetResult: unit -> 'TResult1)>
            (sm: byref<_>,
             task: ^TaskLike,
             [<InlineIfLambda>] continuation: 'TResult1 -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>)
            : bool =

            let mutable awaiter = (^TaskLike: (member GetAwaiter: unit -> ^Awaiter) task)

            let cont =
                GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask>(fun sm ->
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
            static member inline Bind< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TAwaiter, 'TTask
                when 'TMethodBuilder :> IAsyncMethodBuilder<'TAwaiter, 'TTask>
                and 'TAwaiter :> ITaskAwaiter
                and 'TTask :> ITaskLike<'TAwaiter>

                and ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
                and ^Awaiter :> ICriticalNotifyCompletion
                and ^Awaiter: (member get_IsCompleted: unit -> bool)
                and ^Awaiter: (member GetResult: unit -> 'TResult1)>

                (_: GenericUnitTaskBuilderBase<IGenericUnitTaskBuilderBasicBindExtensions>, task: ^TaskLike,
                 [<InlineIfLambda>] continuation: 'TResult1 -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>)
                : GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2> =

                GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>(fun sm ->
                    if __useResumableCode then
                        let mutable awaiter = (^TaskLike: (member GetAwaiter: unit -> ^Awaiter) task)

                        let mutable __stack_fin = true
                        if not (^Awaiter: (member get_IsCompleted: unit -> bool) awaiter) then
                            let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                            __stack_fin <- __stack_yield_fin

                        if __stack_fin then
                            let result = (^Awaiter: (member GetResult: unit -> 'TResult1) awaiter)
                            (continuation result).Invoke(&sm)
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        GenericUnitTaskBuilderBasicBindExtensionsLowPriorityImpl.BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TAwaiter, 'TTask>(&sm, task, continuation)
                )

module GenericUnitTaskBuilderBasicBindExtensionsHighPriority =

    [<AbstractClass; Sealed; Extension>]
    type GenericUnitTaskBuilderBasicBindExtensionsHighPriorityImpl() =

        static member BindDynamic(sm: byref<_>, task: Task<'TResult1>, continuation: 'TResult1 -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>) : bool =
            let mutable awaiter = task.GetAwaiter()

            let cont =
                GenericUnitTaskResumptionFunc<'TMethodBuilder, 'TAwaiter, 'TTask>(fun sm ->
                   let result = awaiter.GetResult()
                   (continuation result).Invoke(&sm))

            if awaiter.IsCompleted then
                cont.Invoke(&sm)
            else
                sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        [<Extension>]
        static member inline Bind(_: GenericUnitTaskBuilderBase<IGenericUnitTaskBuilderBasicBindExtensions>, task: Task<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>) =
            GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, _>(fun sm ->
                if __useResumableCode then
                    let mutable awaiter = task.GetAwaiter()

                    let mutable __stack_fin = true
                    if not awaiter.IsCompleted then
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        (continuation result).Invoke(&sm)
                    else
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                        false
                else
                    GenericUnitTaskBuilderBasicBindExtensionsHighPriorityImpl.BindDynamic(&sm, task, continuation)
            )

module GenericUnitTaskBuilderBasicBindExtensionsMediumPriority =
    open GenericUnitTaskBuilderBasicBindExtensionsHighPriority

    [<AbstractClass; Sealed; Extension>]
    type GenericUnitTaskBuilderBasicBindExtensionsMediumPriorityImpl() =
        [<Extension>]
        static member inline Bind(this: GenericUnitTaskBuilderBase<IGenericUnitTaskBuilderBasicBindExtensions>, computation: Async<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> GenericUnitTaskCode<'TMethodBuilder, 'TAwaiter, 'TTask, 'TResult2>) =
            this.Bind (Async.StartAsTask computation, continuation)