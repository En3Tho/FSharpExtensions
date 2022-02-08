// Task builder for F# that compiles to allocation-free paths for synchronous code.
//
// Originally written in 2016 by Robert Peele (humbobst@gmail.com)
// New operator-based overload resolution for F# 4.0 compatibility by Gustavo Leon in 2018.
// Revised for insertion into FSHarp.Core by Microsoft, 2019.
//
// Original notice:
// To the extent possible under law, the author(s) have dedicated all copyright and related and neighboring rights
// to this software to the public domain worldwide. This software is distributed without any warranty.
//
// Updates:
// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

#nowarn "3513"

namespace En3Tho.FSharp.ComputationExpressions.Tasks

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators
open Microsoft.FSharp.Collections

type AsyncValueTaskExnResultMethodBuilder<'TOverall> = AsyncValueTaskMethodBuilder<Result<'TOverall, exn>>
type ValueTaskExnResultStateMachineData<'TOverall> = ValueTaskStateMachineData<Result<'TOverall, exn>>
type ValueTaskExnResultStateMachine<'TOverall> = ResumableStateMachine<ValueTaskStateMachineData<Result<'TOverall, exn>>>
type ValueTaskExnResultResumptionFunc<'TOverall> = ResumptionFunc<ValueTaskStateMachineData<Result<'TOverall, exn>>>
type ValueTaskExnResultResumptionDynamicInfo<'TOverall> = ResumptionDynamicInfo<ValueTaskStateMachineData<Result<'TOverall, exn>>>
type ValueTaskExnResultCode<'TOverall, 'T> = ResumableCode<ValueTaskStateMachineData<Result<'TOverall, exn>>, 'T>

type ValueTaskExnResultBuilderBase() =

    member inline _.Delay(generator : unit -> ValueTaskExnResultCode<'TOverall, 'T>) : ValueTaskExnResultCode<'TOverall, 'T> =
        ValueTaskExnResultCode<'TOverall, 'T>(fun sm -> (generator()).Invoke(&sm))

    /// Used to represent no-ops like the implicit empty "else" branch of an "if" expression.
    [<DefaultValue>]
    member inline _.Zero() : ValueTaskExnResultCode<'TOverall, unit> = ResumableCode.Zero()

    member inline _.Return (value: 'T) : ValueTaskExnResultCode<'T, 'T> =
        ValueTaskExnResultCode<'T, _>(fun sm ->
            sm.Data.Result <- Ok value
            true)

    /// Chains together a step with its following step.
    /// Note that this requires that the first step has no result.
    /// This prevents constructs like `task { return 1; return 2; }`.
    member inline _.Combine(task1: ValueTaskExnResultCode<'TOverall, unit>, task2: ValueTaskExnResultCode<'TOverall, 'T>) : ValueTaskExnResultCode<'TOverall, 'T> =
        ResumableCode.Combine(task1, ValueTaskExnResultCode<_,_>(fun sm ->
            match sm.Data.Result with
            | Ok _ -> task2.Invoke(&sm)
            | _ -> true
        ))

    /// Builds a step that executes the body while the condition predicate is true.
    member inline _.While ([<InlineIfLambda>] condition : unit -> bool, body : ValueTaskExnResultCode<'TOverall, unit>) : ValueTaskExnResultCode<'TOverall, unit> =
        ResumableCode.While(condition, body) // TODO: ResultResumableCode

    /// Wraps a step in a try/with. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryWith (body: ValueTaskExnResultCode<'TOverall, 'T>, catch: exn -> ValueTaskExnResultCode<'TOverall, 'T>) : ValueTaskExnResultCode<'TOverall, 'T> =
        ResumableCode.TryWith(body, catch)

    /// Wraps a step in a try/finally. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryFinally (body: ValueTaskExnResultCode<'TOverall, 'T>, [<InlineIfLambda>] compensation : unit -> unit) : ValueTaskExnResultCode<'TOverall, 'T> =
        ResumableCode.TryFinally(body, ResumableCode<_,_>(fun _sm -> compensation(); true))

    member inline _.For (sequence : seq<'T>, body : 'T -> ValueTaskExnResultCode<'TOverall, unit>) : ValueTaskExnResultCode<'TOverall, unit> =
        ResumableCode.For(sequence, body)

    type ValueTaskExnResultBuilder() =

        inherit ValueTaskExnResultBuilderBase()

        static member RunDynamic(code: ValueTaskExnResultCode<'T, 'T>) : ValueTask<Result<'T, exn>> =
            let mutable sm = ValueTaskExnResultStateMachine<'T>()
            let initialResumptionFunc = ValueTaskExnResultResumptionFunc<'T>(fun sm -> code.Invoke(&sm))
            let resumptionInfo =
                { new ValueTaskExnResultResumptionDynamicInfo<'T>(initialResumptionFunc) with
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
                        | exn -> sm.Data.MethodBuilder.SetResult (Error exn)

                    member _.SetStateMachine(sm, state) =
                        sm.Data.MethodBuilder.SetStateMachine(state)
                    }
            sm.ResumptionDynamicInfo <- resumptionInfo
            sm.Data.MethodBuilder <- AsyncValueTaskExnResultMethodBuilder<'T>.Create()
            sm.Data.MethodBuilder.Start(&sm)
            sm.Data.MethodBuilder.Task

        static member inline Run(code: ValueTaskExnResultCode<'T, 'T>) : ValueTask<Result<'T, exn>> =
             if __useResumableCode then
                __stateMachine<ValueTaskExnResultStateMachineData<'T>, ValueTask<Result<'T, exn>>>
                    (MoveNextMethodImpl<_>(fun sm ->
                        //-- RESUMABLE CODE START
                        __resumeAt sm.ResumptionPoint
                        let mutable __stack_exn : Exception = null
                        try
                            let __stack_code_fin = code.Invoke(&sm)
                            if __stack_code_fin then
                                sm.Data.MethodBuilder.SetResult(sm.Data.Result)
                        with exn ->
                            __stack_exn <- exn
                        // Run SetException outside the stack unwind, see https://github.com/dotnet/roslyn/issues/26567
                        match __stack_exn with
                        | null -> ()
                        | exn -> sm.Data.MethodBuilder.SetResult (Error exn)
                        //-- RESUMABLE CODE END
                    ))
                    (SetStateMachineMethodImpl<_>(fun sm state -> sm.Data.MethodBuilder.SetStateMachine(state)))
                    (AfterCode<_,_>(fun sm ->
                        sm.Data.MethodBuilder <- AsyncValueTaskExnResultMethodBuilder<'T>.Create()
                        sm.Data.MethodBuilder.Start(&sm)
                        sm.Data.MethodBuilder.Task))
             else
                ValueTaskExnResultBuilder.RunDynamic(code)

        member inline _.Run(code: ValueTaskExnResultCode<'T, 'T>) : ValueTask<Result<'T, exn>> =
           ValueTaskExnResultBuilder.Run(code)

namespace En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskExnResultBuilderExtensions

    open En3Tho.FSharp.ComputationExpressions.Tasks
    open System
    open System.Runtime.CompilerServices
    open System.Threading.Tasks
    open Microsoft.FSharp.Core
    open Microsoft.FSharp.Core.CompilerServices
    open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
    open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

    module LowPriority =

        type ValueTaskExnResultBuilderBase with

            [<NoEagerConstraintApplication>]
            static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall
                                                when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                and ^Awaiter :> ICriticalNotifyCompletion
                                                and ^Awaiter: (member get_IsCompleted:  unit -> bool)
                                                and ^Awaiter: (member GetResult:  unit -> Result<'TResult1, exn>)>
                        (sm: byref<_>, task: ^TaskLike, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : bool =

                    let mutable awaiter = (^TaskLike: (member GetAwaiter : unit -> ^Awaiter)(task))

                    let cont =
                        (ValueTaskExnResultResumptionFunc<'TOverall>( fun sm ->
                            let result = (^Awaiter : (member GetResult : unit -> Result<'TResult1, exn>)(awaiter))
                            match result with
                            | Ok result ->
                                (continuation result).Invoke(&sm)
                            | _ -> true))

                    // shortcut to continue immediately
                    if (^Awaiter : (member get_IsCompleted : unit -> bool)(awaiter)) then
                        cont.Invoke(&sm)
                    else
                        sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                        sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                        false

            [<NoEagerConstraintApplication>]
            member inline _.Bind< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall
                                                when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                and ^Awaiter :> ICriticalNotifyCompletion
                                                and ^Awaiter: (member get_IsCompleted:  unit -> bool)
                                                and ^Awaiter: (member GetResult:  unit -> Result<'TResult1, exn>)>
                        (task: ^TaskLike, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : ValueTaskExnResultCode<'TOverall, 'TResult2> =

                ValueTaskExnResultCode<'TOverall, _>(fun sm ->
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
                            let result = (^Awaiter : (member GetResult : unit -> Result<'TResult1, exn>)(awaiter))
                            match result with
                            | Ok result ->
                                (continuation result).Invoke(&sm)
                            | _ -> true
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        ValueTaskExnResultBuilderBase.BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall>(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            [<NoEagerConstraintApplication>]
            member inline this.ReturnFrom< ^TaskLike, ^Awaiter, 'T
                                                  when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                  and ^Awaiter :> ICriticalNotifyCompletion
                                                  and ^Awaiter: (member get_IsCompleted: unit -> bool)
                                                  and ^Awaiter: (member GetResult: unit -> Result<'T, exn>)>
                    (task: ^TaskLike) : ValueTaskExnResultCode< 'T, 'T> =

                this.Bind(task, (fun v -> this.Return v))

            member inline _.Using<'Resource, 'TOverall, 'T when 'Resource :> IDisposable> (resource: 'Resource, body: 'Resource -> ValueTaskExnResultCode<'TOverall, 'T>) =
                ResumableCode.Using(resource, body)

            static member BindTaskDynamic (sm: byref<_>, task: Task<'TResult1>, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskExnResultResumptionFunc<'TOverall>(fun sm ->
                        let result = awaiter.GetResult()
                        (continuation result).Invoke(&sm)
                        ))

                // shortcut to continue immediately
                if awaiter.IsCompleted then
                    cont.Invoke(&sm)
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false

            static member BindDynamic (sm: byref<_>, task: ValueTask<'TResult1>, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskExnResultResumptionFunc<'TOverall>(fun sm ->
                        let result = awaiter.GetResult()
                        (continuation result).Invoke(&sm)
                        ))

                // shortcut to continue immediately
                if awaiter.IsCompleted then
                    cont.Invoke(&sm)
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false

            member inline _.Bind (task: Task<'TResult1>, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : ValueTaskExnResultCode<'TOverall, 'TResult2> =

                ValueTaskExnResultCode<'TOverall, _>(fun sm ->
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
                        ValueTaskExnResultBuilderBase.BindTaskDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline _.Bind (task: ValueTask<'TResult1>, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : ValueTaskExnResultCode<'TOverall, 'TResult2> =
                ValueTaskExnResultCode<'TOverall, _>(fun sm ->
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
                        ValueTaskExnResultBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline this.Bind (task: Result<'TResult1, exn>, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : ValueTaskExnResultCode<'TOverall, 'TResult2> =
                this.Bind(ValueTask.FromResult task, continuation)

            member inline this.ReturnFrom (value: Result<'T, exn>)  : ValueTaskExnResultCode<'T, 'T> =
                this.ReturnFrom (ValueTask.FromResult value)

    module HighPriority =
        // High priority extensions

        type ValueTaskExnResultBuilderBase with
            static member BindDynamic (sm: byref<_>, task: Task<Result<'TResult1, exn>>, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskExnResultResumptionFunc<'TOverall>(fun sm ->
                        let result = awaiter.GetResult()
                        match result with
                        | Ok result ->
                            (continuation result).Invoke(&sm)
                        | _ -> true
                        ))

                // shortcut to continue immediately
                if awaiter.IsCompleted then
                    cont.Invoke(&sm)
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false

            static member BindDynamic (sm: byref<_>, task: ValueTask<Result<'TResult1, exn>>, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskExnResultResumptionFunc<'TOverall>(fun sm ->
                        let result = awaiter.GetResult()
                        match result with
                        | Ok result ->
                            (continuation result).Invoke(&sm)
                        | _ -> true
                        ))

                // shortcut to continue immediately
                if awaiter.IsCompleted then
                    cont.Invoke(&sm)
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false

            member inline _.Bind (task: Task<Result<'TResult1, exn>>, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : ValueTaskExnResultCode<'TOverall, 'TResult2> =

                ValueTaskExnResultCode<'TOverall, _>(fun sm ->
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
                            match result with
                            | Ok result ->
                                (continuation result).Invoke(&sm)
                            | _ -> true
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        ValueTaskExnResultBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline _.Bind (task: ValueTask<Result<'TResult1, exn>>, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : ValueTaskExnResultCode<'TOverall, 'TResult2> =
                ValueTaskExnResultCode<'TOverall, _>(fun sm ->
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
                            match result with
                            | Ok result ->
                                (continuation result).Invoke(&sm)
                            | _ ->
                                true
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        ValueTaskExnResultBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline this.ReturnFrom (task: Task<Result<'T, exn>>) : ValueTaskExnResultCode<'T, 'T> =
                this.Bind(task, (fun v -> this.Return v))

            member inline this.ReturnFrom (task: ValueTask<Result<'T, exn>>) : ValueTaskExnResultCode<'T, 'T> =
                this.Bind(task, (fun v -> this.Return v))

    module MediumPriority =
        open HighPriority

        // Medium priority extensions
        type ValueTaskExnResultBuilderBase with
            member inline this.Bind (computation: Async<Result<'TResult1, exn>>, continuation: ('TResult1 -> ValueTaskExnResultCode<'TOverall, 'TResult2>)) : ValueTaskExnResultCode<'TOverall, 'TResult2> =
                this.Bind (Async.StartAsTask computation, continuation)

            member inline this.ReturnFrom (computation: Async<Result<'T, exn>>)  : ValueTaskExnResultCode<'T, 'T> =
                this.ReturnFrom (Async.StartAsTask computation)