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

type AsyncValueTaskValueOptionMethodBuilder<'TOverall> = AsyncValueTaskMethodBuilder<'TOverall voption>
type ValueTaskValueOptionStateMachineData<'TOverall> = ValueTaskStateMachineData<'TOverall voption>
type ValueTaskValueOptionStateMachine<'TOverall> = ResumableStateMachine<ValueTaskStateMachineData<'TOverall voption>>
type ValueTaskValueOptionResumptionFunc<'TOverall> = ResumptionFunc<ValueTaskStateMachineData<'TOverall voption>>
type ValueTaskValueOptionResumptionDynamicInfo<'TOverall> = ResumptionDynamicInfo<ValueTaskStateMachineData<'TOverall voption>>
type ValueTaskValueOptionCode<'TOverall, 'T> = ResumableCode<ValueTaskStateMachineData<'TOverall voption>, 'T>

type ValueTaskValueOptionBuilderBase() =

    member inline _.Delay(generator : unit -> ValueTaskValueOptionCode<'TOverall, 'T>) : ValueTaskValueOptionCode<'TOverall, 'T> =
        ValueTaskValueOptionCode<'TOverall, 'T>(fun sm -> (generator()).Invoke(&sm))

    /// Used to represent no-ops like the implicit empty "else" branch of an "if" expression.
    //[<DefaultValue]
    member inline _.Zero() : ValueTaskValueOptionCode<'TOverall, unit> = ResumableCode.Zero()

    member inline _.Return (value: 'T) : ValueTaskValueOptionCode<'T, 'T> =
        ValueTaskValueOptionCode<'T, _>(fun sm ->
            sm.Data.Result <- ValueSome value
            true)

    /// Chains together a step with its following step.
    /// Note that this requires that the first step has no result.
    /// This prevents constructs like `task { return 1; return 2; }`.
    member inline _.Combine(task1: ValueTaskValueOptionCode<'TOverall, unit>, task2: ValueTaskValueOptionCode<'TOverall, 'T>) : ValueTaskValueOptionCode<'TOverall, 'T> =
        ResumableCode.Combine(task1, ValueTaskValueOptionCode<_,_>(fun sm ->
            if sm.Data.Result.IsNone then true else task2.Invoke(&sm)))

    /// Builds a step that executes the body while the condition predicate is true.
    member inline _.While ([<InlineIfLambda>] condition : unit -> bool, body : ValueTaskValueOptionCode<'TOverall, unit>) : ValueTaskValueOptionCode<'TOverall, unit> =
        ResumableCode.While(condition, body) // TODO: OptionResumableCode

    /// Wraps a step in a try/with. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryWith (body: ValueTaskValueOptionCode<'TOverall, 'T>, catch: exn -> ValueTaskValueOptionCode<'TOverall, 'T>) : ValueTaskValueOptionCode<'TOverall, 'T> =
        ResumableCode.TryWith(body, catch)

    /// Wraps a step in a try/finally. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryFinally (body: ValueTaskValueOptionCode<'TOverall, 'T>, [<InlineIfLambda>] compensation : unit -> unit) : ValueTaskValueOptionCode<'TOverall, 'T> =
        ResumableCode.TryFinally(body, ResumableCode<_,_>(fun _sm -> compensation(); true))

    member inline _.For (sequence : seq<'T>, body : 'T -> ValueTaskValueOptionCode<'TOverall, unit>) : ValueTaskValueOptionCode<'TOverall, unit> =
        ResumableCode.For(sequence, body)

    type ValueTaskValueOptionBuilder() =

        inherit ValueTaskValueOptionBuilderBase()

        static member RunDynamic(code: ValueTaskValueOptionCode<'T, 'T>) : ValueTask<'T voption> =
            let mutable sm = ValueTaskValueOptionStateMachine<'T>()
            let initialResumptionFunc = ValueTaskValueOptionResumptionFunc<'T>(fun sm -> code.Invoke(&sm))
            let resumptionInfo =
                { new ValueTaskValueOptionResumptionDynamicInfo<'T>(initialResumptionFunc) with
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
            sm.Data.MethodBuilder <- AsyncValueTaskValueOptionMethodBuilder<'T>.Create()
            sm.Data.MethodBuilder.Start(&sm)
            sm.Data.MethodBuilder.Task

        static member inline Run(code : ValueTaskValueOptionCode<'T, 'T>) : ValueTask<'T voption> =
             if __useResumableCode then
                __stateMachine<ValueTaskValueOptionStateMachineData<'T>, ValueTask<'T voption>>
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
                        sm.Data.MethodBuilder <- AsyncValueTaskValueOptionMethodBuilder<'T>.Create()
                        sm.Data.MethodBuilder.Start(&sm)
                        sm.Data.MethodBuilder.Task))
             else
                ValueTaskValueOptionBuilder.RunDynamic(code)

        member inline _.Run(code : ValueTaskValueOptionCode<'T, 'T>) : ValueTask<'T voption> =
           ValueTaskValueOptionBuilder.Run(code)

namespace En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskValueOptionBuilderExtensions

    open En3Tho.FSharp.ComputationExpressions.Tasks
    open System
    open System.Runtime.CompilerServices
    open System.Threading.Tasks
    open Microsoft.FSharp.Core
    open Microsoft.FSharp.Core.CompilerServices
    open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
    open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

    module LowPriority =

        type ValueTaskValueOptionBuilderBase with

            [<NoEagerConstraintApplication>]
            static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall
                                                when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                and ^Awaiter :> ICriticalNotifyCompletion
                                                and ^Awaiter: (member get_IsCompleted:  unit -> bool)
                                                and ^Awaiter: (member GetResult:  unit ->  'TResult1 voption)>
                        (sm: byref<_>, task: ^TaskLike, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : bool =

                    let mutable awaiter = (^TaskLike: (member GetAwaiter : unit -> ^Awaiter)(task))

                    let cont =
                        (ValueTaskValueOptionResumptionFunc<'TOverall>( fun sm ->
                            let result = (^Awaiter : (member GetResult : unit -> 'TResult1 voption)(awaiter))
                            match result with
                            | ValueSome result ->
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
                                                and ^Awaiter: (member GetResult:  unit ->  'TResult1 voption)>
                        (task: ^TaskLike, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : ValueTaskValueOptionCode<'TOverall, 'TResult2> =

                ValueTaskValueOptionCode<'TOverall, _>(fun sm ->
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
                            let result = (^Awaiter : (member GetResult : unit -> 'TResult1 voption)(awaiter))
                            match result with
                            | ValueSome result ->
                                (continuation result).Invoke(&sm)
                            | _ -> true
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        ValueTaskValueOptionBuilderBase.BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall>(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            [<NoEagerConstraintApplication>]
            member inline this.ReturnFrom< ^TaskLike, ^Awaiter, 'T
                                                  when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                  and ^Awaiter :> ICriticalNotifyCompletion
                                                  and ^Awaiter: (member get_IsCompleted: unit -> bool)
                                                  and ^Awaiter: (member GetResult: unit ->  'T voption)>
                    (task: ^TaskLike) : ValueTaskValueOptionCode< 'T,  'T> =

                this.Bind(task, (fun v -> this.Return v))

            member inline _.Using<'Resource, 'TOverall, 'T when 'Resource :> IDisposable> (resource: 'Resource, body: 'Resource -> ValueTaskValueOptionCode<'TOverall, 'T>) =
                ResumableCode.Using(resource, body)

            static member BindTaskDynamic (sm: byref<_>, task: Task<'TResult1>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskValueOptionResumptionFunc<'TOverall>(fun sm ->
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

            static member BindDynamic (sm: byref<_>, task: ValueTask<'TResult1>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskValueOptionResumptionFunc<'TOverall>(fun sm ->
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

            member inline _.Bind (task: Task<'TResult1>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : ValueTaskValueOptionCode<'TOverall, 'TResult2> =

                ValueTaskValueOptionCode<'TOverall, _>(fun sm ->
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
                        ValueTaskValueOptionBuilderBase.BindTaskDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline _.Bind (task: ValueTask<'TResult1>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : ValueTaskValueOptionCode<'TOverall, 'TResult2> =
                ValueTaskValueOptionCode<'TOverall, _>(fun sm ->
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
                        ValueTaskValueOptionBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline this.Bind (task: Option<'TResult1>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : ValueTaskValueOptionCode<'TOverall, 'TResult2> =
                this.Bind(ValueTask.FromResult (match task with Some x -> ValueSome x | _ -> ValueNone), continuation)

            member inline this.Bind (task: ValueOption<'TResult1>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : ValueTaskValueOptionCode<'TOverall, 'TResult2> =
                this.Bind(ValueTask.FromResult task, continuation)

            member inline this.ReturnFrom (task: Option<'TResult1>) : ValueTaskValueOptionCode<'TResult1, 'TResult1> =
                this.ReturnFrom(ValueTask.FromResult (match task with Some x -> ValueSome x | _ -> ValueNone))

            member inline this.ReturnFrom (task: ValueOption<'TResult1>) : ValueTaskValueOptionCode<'TResult1, 'TResult1> =
                this.ReturnFrom(ValueTask.FromResult task)

    module HighPriority =
        // High priority extensions

        type ValueTaskValueOptionBuilderBase with
            static member BindDynamic (sm: byref<_>, task: Task<'TResult1 voption>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskValueOptionResumptionFunc<'TOverall>(fun sm ->
                        let result = awaiter.GetResult()
                        match result with
                        | ValueSome result ->
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

            static member BindDynamic (sm: byref<_>, task: ValueTask<'TResult1 voption>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskValueOptionResumptionFunc<'TOverall>(fun sm ->
                        let result = awaiter.GetResult()
                        match result with
                        | ValueSome result ->
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

            member inline _.Bind (task: Task<'TResult1 voption>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : ValueTaskValueOptionCode<'TOverall, 'TResult2> =

                ValueTaskValueOptionCode<'TOverall, _>(fun sm ->
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
                            | ValueSome result ->
                                (continuation result).Invoke(&sm)
                            | _ -> true
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        ValueTaskValueOptionBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline _.Bind (task: ValueTask<'TResult1 voption>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : ValueTaskValueOptionCode<'TOverall, 'TResult2> =
                ValueTaskValueOptionCode<'TOverall, _>(fun sm ->
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
                            | ValueSome result ->
                                (continuation result).Invoke(&sm)
                            | _ ->
                                true
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        ValueTaskValueOptionBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline this.ReturnFrom (task: Task<'T voption>) : ValueTaskValueOptionCode<'T, 'T> =
                this.Bind(task, (fun v -> this.Return v))

            member inline this.ReturnFrom (task: ValueTask<'T voption>) : ValueTaskValueOptionCode<'T, 'T> =
                this.Bind(task, (fun v -> this.Return v))

            static member BindDynamic (sm: byref<_>, task: Task<'TResult1 option>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskValueOptionResumptionFunc<'TOverall>(fun sm ->
                        let result = awaiter.GetResult()
                        match result with
                        | Some result ->
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

            static member BindDynamic (sm: byref<_>, task: ValueTask<'TResult1 option>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskValueOptionResumptionFunc<'TOverall>(fun sm ->
                        let result = awaiter.GetResult()
                        match result with
                        | Some result ->
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

            member inline _.Bind (task: Task<'TResult1 option>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : ValueTaskValueOptionCode<'TOverall, 'TResult2> =

                ValueTaskValueOptionCode<'TOverall, _>(fun sm ->
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
                            | Some result ->
                                (continuation result).Invoke(&sm)
                            | _ -> true
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        ValueTaskValueOptionBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline _.Bind (task: ValueTask<'TResult1 option>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : ValueTaskValueOptionCode<'TOverall, 'TResult2> =
                ValueTaskValueOptionCode<'TOverall, _>(fun sm ->
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
                            | Some result ->
                                (continuation result).Invoke(&sm)
                            | _ ->
                                true
                        else
                            sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                            false
                    else
                        ValueTaskValueOptionBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline this.ReturnFrom (task: Task<'T option>) : ValueTaskValueOptionCode<'T, 'T> =
                this.Bind(task, (fun v -> this.Return v))

            member inline this.ReturnFrom (task: ValueTask<'T option>) : ValueTaskValueOptionCode<'T, 'T> =
                this.Bind(task, (fun v -> this.Return v))

    module MediumPriority =
        open HighPriority

        // Medium priority extensions
        type ValueTaskValueOptionBuilderBase with
            member inline this.Bind (computation: Async<'TResult1 voption>, continuation: ('TResult1 -> ValueTaskValueOptionCode<'TOverall, 'TResult2>)) : ValueTaskValueOptionCode<'TOverall, 'TResult2> =
                this.Bind (Async.StartAsTask computation, continuation)

            member inline this.ReturnFrom (computation: Async<'T voption>)  : ValueTaskValueOptionCode<'T, 'T> =
                this.ReturnFrom (Async.StartAsTask computation)