namespace En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions

open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder

module LowPriority =

    [<NoEagerConstraintApplication>]
    let inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TData
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

    type GenericTaskBuilderCore with
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

        [<NoEagerConstraintApplication>]
        member inline _.Bind<^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TMethodBuilder, 'TData
            when 'TData :> IAsyncMethodBuilderBase
            and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>
            and ^TaskLike: (member GetAwaiter: unit -> ^Awaiter)
            and ^Awaiter :> ICriticalNotifyCompletion
            and ^Awaiter: (member get_IsCompleted: unit -> bool)
            and ^Awaiter: (member GetResult: unit -> 'TResult1)>
            (task: ^TaskLike,
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
                    BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter, 'TData>(&sm, task, continuation)
            )

    [<AbstractClass; Sealed; Extension>]
    type LowPriorityImpl() =

        [<Extension>]
        static member inline Return<'TData, 'TResult
            when 'TData :> IGenericTaskBuilderStateMachineDataSetResult<'TResult>
            and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(_: GenericTaskBuilderCore<ReturnExtensions>, value: 'TResult) =
            ResumableCode<'TData, 'TResult>(fun sm ->
                sm.Data.SetResult(value)
                true)

        [<NoEagerConstraintApplication; Extension>]
        static member inline ReturnFrom (this: GenericTaskBuilderCore<ReturnExtensions>, task: ^TaskLike) =
            this.Bind(task, (fun v -> this.Return v))

        [<Extension>]
        static member inline Yield<'TData, 'TResult
            when 'TData :> IGenericTaskBuilderStateMachineDataYield<'TData, 'TResult>
            and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>(_: GenericTaskBuilderCore<YieldExtensions>, value: 'TResult) =
                ResumableCode<'TData, unit>(fun sm ->
                    if sm.Data.CheckCanContinueOrThrow() then
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        if not __stack_yield_fin then
                            sm.Data.SetResult(value)
                        __stack_yield_fin
                    else
                        true)

        [<NoEagerConstraintApplication; Extension>]
        static member inline YieldFrom (this: GenericTaskBuilderCore<YieldExtensions>, task: ^TaskLike) =
            this.Bind(task, (fun v -> this.Yield v))

module HighPriority =
    open LowPriority

    [<AbstractClass; Sealed; Extension>]
    type HighPriorityImpl() =

        [<Extension>]
        static member inline Bind<'TData, 'TResult1, 'TResult2
            when 'TData :> IAsyncMethodBuilderBase
            and 'TData :> IGenericTaskBuilderStateMachineDataWithCheck<'TData>>
            (this: GenericTaskBuilderCore, task: Task<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
                this.Bind(TaskBindWrapper(task), continuation)

        [<Extension>]
        static member inline ReturnFrom(this: GenericTaskBuilderCore<ReturnExtensions>, task: Task<'TResult>) =
            this.Bind(task, (fun v -> this.Return v))

        [<NoEagerConstraintApplication; Extension>]
        static member inline YieldFrom (this: GenericTaskBuilderCore<YieldExtensions>, task: Task<'TResult>) =
            this.Bind(task, (fun v -> this.Yield v))

module MediumPriority =
    open HighPriority

    [<AbstractClass; Sealed; Extension>]
    type MediumPriorityImpl() =
        [<Extension>]
        static member inline Bind(this: GenericTaskBuilderCore, computation: Async<'TResult1>, [<InlineIfLambda>] continuation: 'TResult1 -> ResumableCode<'TData, 'TResult2>) =
            this.Bind (Async.StartAsTask computation, continuation)

        [<Extension>]
        static member inline ReturnFrom(this: GenericTaskBuilderCore<ReturnExtensions>, computation: Async<'TResult>) =
            this.ReturnFrom (Async.StartAsTask computation)

        [<Extension>]
        static member inline YieldFrom (this: GenericTaskBuilderCore<YieldExtensions>, computation: Async<'TResult>) =
            this.YieldFrom (Async.StartAsTask computation)