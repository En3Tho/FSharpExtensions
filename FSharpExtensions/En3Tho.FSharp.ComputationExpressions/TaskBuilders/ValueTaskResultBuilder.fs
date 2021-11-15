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

#nowarn "5313"

namespace En3Tho.FSharp.ComputationExpressions.Tasks

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators
open Microsoft.FSharp.Collections

type AsyncValueTaskResultMethodBuilder<'TOverall, 'TError> = AsyncValueTaskMethodBuilder<Result<'TOverall, 'TError>>
type ValueTaskResultStateMachineData<'TOverall, 'TError> = ValueTaskStateMachineData<Result<'TOverall, 'TError>>
type ValueTaskResultStateMachine<'TOverall, 'TError> = ResumableStateMachine<ValueTaskStateMachineData<Result<'TOverall, 'TError>>>
type ValueTaskResultResumptionFunc<'TOverall, 'TError> = ResumptionFunc<ValueTaskStateMachineData<Result<'TOverall, 'TError>>>
type ValueTaskResultResumptionDynamicInfo<'TOverall, 'TError> = ResumptionDynamicInfo<ValueTaskStateMachineData<Result<'TOverall, 'TError>>>
type ValueTaskResultCode<'TOverall, 'TError, 'T> = ResumableCode<ValueTaskStateMachineData<Result<'TOverall, 'TError>>, 'T>

type ValueTaskResultBuilderBase() =

    member inline _.Delay(generator : unit -> ValueTaskResultCode<'TOverall, 'TError, 'T>) : ValueTaskResultCode<'TOverall, 'TError, 'T> =
        ValueTaskResultCode<'TOverall, 'TError, 'T>(fun sm -> (generator()).Invoke(&sm))

    /// Used to represent no-ops like the implicit empty "else" branch of an "if" expression.
    //[<DefaultValue]
    member inline _.Zero() : ValueTaskResultCode<'TOverall, 'TError, unit> = ResumableCode.Zero()

    member inline _.Return (value: 'T) : ValueTaskResultCode<'T, 'TError, 'T> =
        ValueTaskResultCode<'T, _, _>(fun sm ->
            sm.Data.Result <- Ok value
            true)

    /// Chains together a step with its following step.
    /// Note that this requires that the first step has no result.
    /// This prevents constructs like `task { return 1; return 2; }`.
    member inline _.Combine(task1: ValueTaskResultCode<'TOverall, 'TError, unit>, task2: ValueTaskResultCode<'TOverall, 'TError, 'T>) : ValueTaskResultCode<'TOverall, 'TError, 'T> =
        ResumableCode.Combine(task1, ValueTaskResultCode<_,_,_>(fun sm ->
            match sm.Data.Result with
            | Ok _ -> task2.Invoke(&sm)
            | _ -> true
        ))

    /// Builds a step that executes the body while the condition predicate is true.
    member inline _.While ([<InlineIfLambda>] condition : unit -> bool, body : ValueTaskResultCode<'TOverall, 'TError, unit>) : ValueTaskResultCode<'TOverall, 'TError, unit> =
        ResumableCode.While(condition, body) // TODO: ResultResumableCode

    /// Wraps a step in a try/with. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryWith (body: ValueTaskResultCode<'TOverall, 'TError, 'T>, catch: exn -> ValueTaskResultCode<'TOverall, 'TError, 'T>) : ValueTaskResultCode<'TOverall, 'TError, 'T> =
        ResumableCode.TryWith(body, catch)

    /// Wraps a step in a try/finally. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryFinally (body: ValueTaskResultCode<'TOverall, 'TError, 'T>, [<InlineIfLambda>] compensation : unit -> unit) : ValueTaskResultCode<'TOverall, 'TError, 'T> =
        ResumableCode.TryFinally(body, ResumableCode<_,_>(fun _sm -> compensation(); true))

    member inline _.For (sequence : seq<'T>, body : 'T -> ValueTaskResultCode<'TOverall, 'TError, unit>) : ValueTaskResultCode<'TOverall, 'TError, unit> =
        ResumableCode.For(sequence, body)

    type ValueTaskResultBuilder() =

        inherit ValueTaskResultBuilderBase()

        static member RunDynamic(code: ValueTaskResultCode<'T, 'TError, 'T>) : ValueTask<Result<'T, 'TError>> =
            let mutable sm = ValueTaskResultStateMachine<'T, 'TError>()
            let initialResumptionFunc = ValueTaskResultResumptionFunc<'T, 'TError>(fun sm -> code.Invoke(&sm))
            let resumptionInfo =
                { new ValueTaskResultResumptionDynamicInfo<'T, 'TError>(initialResumptionFunc) with
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
                        | exn -> sm.Data.MethodBuilder.SetException exn

                    member _.SetStateMachine(sm, state) =
                        sm.Data.MethodBuilder.SetStateMachine(state)
                    }
            sm.ResumptionDynamicInfo <- resumptionInfo
            sm.Data.MethodBuilder <- AsyncValueTaskResultMethodBuilder<'T, 'TError>.Create()
            sm.Data.MethodBuilder.Start(&sm)
            sm.Data.MethodBuilder.Task

        static member inline Run(code : ValueTaskResultCode<'T, 'TError, 'T>) : ValueTask<Result<'T, 'TError>> =
             if __useResumableCode then
                __stateMachine<ValueTaskResultStateMachineData<'T, 'TError>, ValueTask<Result<'T, 'TError>>>
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
                        | exn -> sm.Data.MethodBuilder.SetException exn
                        //-- RESUMABLE CODE END
                    ))
                    (SetStateMachineMethodImpl<_>(fun sm state -> sm.Data.MethodBuilder.SetStateMachine(state)))
                    (AfterCode<_,_>(fun sm ->
                        sm.Data.MethodBuilder <- AsyncValueTaskResultMethodBuilder<'T, 'TError>.Create()
                        sm.Data.MethodBuilder.Start(&sm)
                        sm.Data.MethodBuilder.Task))
             else
                ValueTaskResultBuilder.RunDynamic(code)

        member inline _.Run(code : ValueTaskResultCode<'T, 'TError, 'T>) : ValueTask<Result<'T, 'TError>> =
           ValueTaskResultBuilder.Run(code)

namespace En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskResultBuilderExtensions

    open En3Tho.FSharp.ComputationExpressions.Tasks
    open System
    open System.Runtime.CompilerServices
    open System.Threading.Tasks
    open Microsoft.FSharp.Core
    open Microsoft.FSharp.Core.CompilerServices
    open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
    open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

    module LowPriority =

        type ValueTaskResultBuilderBase with

            [<NoEagerConstraintApplication>]
            static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall, 'TError
                                                when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                and ^Awaiter :> ICriticalNotifyCompletion
                                                and ^Awaiter: (member get_IsCompleted:  unit -> bool)
                                                and ^Awaiter: (member GetResult:  unit -> Result<'TResult1, 'TError>)>
                        (sm: byref<_>, task: ^TaskLike, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : bool =

                    let mutable awaiter = (^TaskLike: (member GetAwaiter : unit -> ^Awaiter)(task))

                    let cont =
                        (ValueTaskResultResumptionFunc<'TOverall, 'TError>( fun sm ->
                            let result = (^Awaiter : (member GetResult : unit -> Result<'TResult1, 'TError>)(awaiter))
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
            member inline _.Bind< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall, 'TError
                                                when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                and ^Awaiter :> ICriticalNotifyCompletion
                                                and ^Awaiter: (member get_IsCompleted:  unit -> bool)
                                                and ^Awaiter: (member GetResult:  unit -> Result<'TResult1, 'TError>)>
                        (task: ^TaskLike, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : ValueTaskResultCode<'TOverall, 'TError, 'TResult2> =

                ValueTaskResultCode<'TOverall, _, _>(fun sm ->
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
                            let result = (^Awaiter : (member GetResult : unit -> Result<'TResult1, 'TError>)(awaiter))
                            match result with
                            | Ok result ->
                                (continuation result).Invoke(&sm)
                            | _ -> true
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        ValueTaskResultBuilderBase.BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall, 'TError>(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            [<NoEagerConstraintApplication>]
            member inline this.ReturnFrom< ^TaskLike, ^Awaiter, 'T, 'TError
                                                  when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                  and ^Awaiter :> ICriticalNotifyCompletion
                                                  and ^Awaiter: (member get_IsCompleted: unit -> bool)
                                                  and ^Awaiter: (member GetResult: unit -> Result<'T, 'TError>)>
                    (task: ^TaskLike) : ValueTaskResultCode< 'T, _,  'T> =

                this.Bind(task, (fun v -> this.Return v))

            member inline _.Using<'Resource, 'TOverall, 'T when 'Resource :> IDisposable> (resource: 'Resource, body: 'Resource -> ValueTaskResultCode<'TOverall, 'TError, 'T>) =
                ResumableCode.Using(resource, body)

            static member BindTaskDynamic (sm: byref<_>, task: Task<'TResult1>, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskResultResumptionFunc<'TOverall, 'TError>(fun sm ->
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

            static member BindDynamic (sm: byref<_>, task: ValueTask<'TResult1>, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskResultResumptionFunc<'TOverall, 'TError>(fun sm ->
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

            member inline _.Bind (task: Task<'TResult1>, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : ValueTaskResultCode<'TOverall, 'TError, 'TResult2> =

                ValueTaskResultCode<'TOverall, _, _>(fun sm ->
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
                        ValueTaskResultBuilderBase.BindTaskDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline _.Bind (task: ValueTask<'TResult1>, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : ValueTaskResultCode<'TOverall, 'TError, 'TResult2> =
                ValueTaskResultCode<'TOverall, _, _>(fun sm ->
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
                        ValueTaskResultBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline this.Bind (task: Result<'TResult1, 'TError>, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : ValueTaskResultCode<'TOverall, 'TError, 'TResult2> =
                this.Bind(ValueTask.FromResult task, continuation)

            member inline this.ReturnFrom (value: Result<'T, 'TError>)  : ValueTaskResultCode<'T, 'TError, 'T> =
                this.ReturnFrom (ValueTask.FromResult value)

    module HighPriority =
        // High priority extensions

        type ValueTaskResultBuilderBase with
            static member BindDynamic (sm: byref<_>, task: Task<Result<'TResult1, 'TError>>, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskResultResumptionFunc<'TOverall, 'TError>(fun sm ->
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

            static member BindDynamic (sm: byref<_>, task: ValueTask<Result<'TResult1, 'TError>>, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskResultResumptionFunc<'TOverall, 'TError>(fun sm ->
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

            member inline _.Bind (task: Task<Result<'TResult1, 'TError>>, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : ValueTaskResultCode<'TOverall, 'TError, 'TResult2> =

                ValueTaskResultCode<'TOverall, _, _>(fun sm ->
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
                        ValueTaskResultBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline _.Bind (task: ValueTask<Result<'TResult1, 'TError>>, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : ValueTaskResultCode<'TOverall, 'TError, 'TResult2> =
                ValueTaskResultCode<'TOverall, _, _>(fun sm ->
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
                        ValueTaskResultBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline this.ReturnFrom (task: Task<Result<'T, 'TError>>) : ValueTaskResultCode<'T, 'TError, 'T> =
                this.Bind(task, (fun v -> this.Return v))

            member inline this.ReturnFrom (task: ValueTask<Result<'T, 'TError>>) : ValueTaskResultCode<'T, 'TError, 'T> =
                this.Bind(task, (fun v -> this.Return v))

    module MediumPriority =
        open HighPriority

        // Medium priority extensions
        type ValueTaskResultBuilderBase with
            member inline this.Bind (computation: Async<Result<'TResult1, 'TError>>, continuation: ('TResult1 -> ValueTaskResultCode<'TOverall, 'TError, 'TResult2>)) : ValueTaskResultCode<'TOverall, 'TError, 'TResult2> =
                this.Bind (Async.StartAsTask computation, continuation)

            member inline this.ReturnFrom (computation: Async<Result<'T, 'TError>>)  : ValueTaskResultCode<'T, 'TError, 'T> =
                this.ReturnFrom (Async.StartAsTask computation)