namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder2

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

module GenericTaskBuilderExtensionsLowPriority =

    type GenericTaskBuilder2Core with
        member inline this.Using<'TData, 'TResult, 'TResource
            when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>
            and 'TResource :> IDisposable>
            (resource: 'TResource, [<InlineIfLambda>] body: 'TResource -> ResumableCode<'TData, 'TResult>) =
            ResumableCode.Using(resource, (fun resource -> ResumableCode<'TData, 'TResult>(fun sm ->
                if sm.Data.CheckCanContinueOrThrow() then
                    (body(resource)).Invoke(&sm)
                else
                    true)))

        member inline this.For(sequence: IAsyncEnumerable<'T>, [<InlineIfLambda>] body: 'T -> ResumableCode<'TData, unit>) : ResumableCode<'TData, unit> =
            this.Using(
                sequence.GetAsyncEnumerator(),
                (fun e ->
                    ResumableCodeHelpers.WhileAsync(
                        (fun () ->
                            __debugPoint "ForLoop.InOrToKeyword"
                            e.MoveNextAsync()),
                        ResumableCode<'TData, unit>(fun sm -> (body e.Current).Invoke(&sm))
                    ))
            )

    [<AbstractClass; Sealed; Extension>]
    type GenericTaskBuilder2BasicBindExtensionsLowPriorityImpl() =

        [<NoEagerConstraintApplication>]
        static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TData
            when ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
            and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>
            and ^Awaiter :> ICriticalNotifyCompletion
            and ^Awaiter: (member get_IsCompleted: unit -> bool)
            and ^Awaiter: (member GetResult: unit -> 'TResult1)>
            (sm: byref<ResumableStateMachine<'TData>>,
             task: ^TaskLike,
             [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>)
            : bool =
            
            if sm.Data.CheckCanContinueOrThrow() then
            
                let mutable awaiter = (^TaskLike: (member GetAwaiter: unit -> ^Awaiter) task)

                let cont =
                    ResumptionFunc<'TData>(fun sm ->
                       let result = (^Awaiter: (member GetResult: unit -> 'TResult1) awaiter)
                       (continuation result).Invoke(&sm))

                // shortcut to continue immediately
                if (^Awaiter: (member get_IsCompleted: unit -> bool) awaiter) then
                    cont.Invoke(&sm)
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false
            else
                true

        [<NoEagerConstraintApplication; Extension>]
        static member inline Bind< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TData
            when 'TData :> IAsyncMethodBuilderBase
            and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>
            and ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
            and ^Awaiter :> ICriticalNotifyCompletion
            and ^Awaiter: (member get_IsCompleted: unit -> bool)
            and ^Awaiter: (member GetResult: unit -> 'TResult1)>
            (_: GenericTaskBuilder2Core, task: ^TaskLike,
             [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>)
            : ResumableCode<'TData, 'TResult2> =

            ResumableCode<'TData, 'TResult2>(fun sm ->
                if __useResumableCode then
                    if sm.Data.CheckCanContinueOrThrow() then
                        let mutable awaiter = (^TaskLike: (member GetAwaiter: unit -> ^Awaiter) task)

                        let mutable __stack_fin = true
                        if not (^Awaiter: (member get_IsCompleted: unit -> bool) awaiter) then
                            let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                            __stack_fin <- __stack_yield_fin

                        if __stack_fin then
                            let result = (^Awaiter: (member GetResult: unit -> 'TResult1) awaiter)
                            (continuation result).Invoke(&sm)
                        else
                            sm.Data.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        true
                else
                    GenericTaskBuilder2BasicBindExtensionsLowPriorityImpl.BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TData>(&sm, task, continuation)
            )

        [<Extension>]
        static member inline Return<'TData, 'TResult
            when 'TData :> IGenericTaskBuilderStateMachineDataResult<'TResult>
            and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(_: GenericTaskBuilder2Core<ReturnExtensions>, value: 'TResult) =
            ResumableCode<'TData, 'TResult>(fun sm ->
                sm.Data.SetResult(value)
                true)

        [<NoEagerConstraintApplication; Extension>]
        static member inline ReturnFrom (this: GenericTaskBuilder2Core<ReturnExtensions>, task: ^TaskLike) =
            this.Bind(task, (fun v -> this.Return v))

        [<Extension>]
        static member inline Yield<'TData, 'TResult
            when 'TData :> IGenericTaskBuilderStateMachineDataYield<'TData, 'TResult>
            and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(_: GenericTaskBuilder2Core<YieldExtensions>, value: 'TResult) =
                ResumableCode<'TData, unit>(fun sm ->
                    if sm.Data.CheckCanContinueOrThrow() then
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        if not __stack_yield_fin then
                            sm.Data.SetResult(value)
                        __stack_yield_fin
                    else
                        true)

        [<NoEagerConstraintApplication; Extension>]
        static member inline YieldFrom (this: GenericTaskBuilder2Core<YieldExtensions>, task: ^TaskLike) =
            this.Bind(task, (fun v -> this.Yield v))

module GenericTaskBuilder2ExtensionsHighPriority =
    open GenericTaskBuilderExtensionsLowPriority

    [<AbstractClass; Sealed; Extension>]
    type GenericTaskBuilder2BasicBindExtensionsHighPriorityImpl() =

        static member BindDynamic<'TData, 'TResult1, 'TResult2 when 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
            (sm: byref<ResumableStateMachine<'TData>>, task: Task<'TResult1>, continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) : bool =
            if sm.Data.CheckCanContinueOrThrow() then
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    ResumptionFunc<'TData>(fun sm ->
                       let result = awaiter.GetResult()
                       (continuation result).Invoke(&sm))

                if awaiter.IsCompleted then
                    cont.Invoke(&sm)
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false
            else
                true

        [<Extension>]
        static member inline Bind<'TData, 'TResult1, 'TResult2
            when 'TData :> IAsyncMethodBuilderBase
            and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
            (_: GenericTaskBuilder2Core, task: Task<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            ResumableCode<'TData, 'TResult2>(fun sm ->
                if __useResumableCode then
                    if sm.Data.CheckCanContinueOrThrow() then
                        let mutable awaiter = task.GetAwaiter()

                        let mutable __stack_fin = true
                        if not awaiter.IsCompleted then
                            let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                            __stack_fin <- __stack_yield_fin

                        if __stack_fin then
                            let result = awaiter.GetResult()
                            (continuation result).Invoke(&sm)
                        else
                            sm.Data.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        true
                else
                    GenericTaskBuilder2BasicBindExtensionsHighPriorityImpl.BindDynamic(&sm, task, continuation)
            )

        [<Extension>]
        static member inline ReturnFrom(this: GenericTaskBuilder2Core<ReturnExtensions>, task: Task<'TResult>) =
            this.Bind(task, (fun v -> this.Return v))

        [<NoEagerConstraintApplication; Extension>]
        static member inline YieldFrom (this: GenericTaskBuilder2Core<YieldExtensions>, task: Task<'TResult>) =
            this.Bind(task, (fun v -> this.Yield v))

module GenericTaskBuilder2ExtensionsMediumPriority =
    open GenericTaskBuilder2ExtensionsHighPriority

    [<AbstractClass; Sealed; Extension>]
    type GenericTaskBuilder2BasicBindExtensionsMediumPriorityImpl() =
        [<Extension>]
        static member inline Bind(this: GenericTaskBuilder2Core, computation: Async<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            this.Bind (Async.StartAsTask computation, continuation)

        [<Extension>]
        static member inline ReturnFrom(this: GenericTaskBuilder2Core<ReturnExtensions>, computation: Async<'TResult>) =
            this.ReturnFrom (Async.StartAsTask computation)

        [<Extension>]
        static member inline YieldFrom (this: GenericTaskBuilder2Core<YieldExtensions>, computation: Async<'TResult>) =
            this.YieldFrom (Async.StartAsTask computation)