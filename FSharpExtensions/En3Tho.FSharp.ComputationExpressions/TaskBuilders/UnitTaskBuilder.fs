﻿// Task builder for F# that compiles to allocation-free paths for synchronous code.
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

namespace Microsoft.FSharp.Core.CompilerServices

open System

[<AttributeUsage (AttributeTargets.Method, AllowMultiple=false)>]
[<Sealed>]
type NoEagerConstraintApplicationAttribute() =
    inherit Attribute()

namespace En3Tho.FSharp.ComputationExpressions.Tasks

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers

/// The extra data stored in ResumableStateMachine for tasks
    [<Struct; NoComparison; NoEquality>]
    type UnitTaskStateMachineData =
       
        [<DefaultValue(false)>]
        val mutable MethodBuilder : AsyncTaskMethodBuilder

    and UnitTaskStateMachine = ResumableStateMachine<UnitTaskStateMachineData>
    and UnitTaskResumptionFunc = ResumptionFunc<UnitTaskStateMachineData>
    and UnitTaskResumptionDynamicInfo = ResumptionDynamicInfo<UnitTaskStateMachineData>
    and UnitTaskCode<'T> = ResumableCode<UnitTaskStateMachineData, 'T>

type UnitTaskBuilderBase() =

    member inline _.Delay(generator : unit -> UnitTaskCode<'T>) : UnitTaskCode<'T> =
        UnitTaskCode<'T>(fun sm -> (generator()).Invoke(&sm))

    /// Used to represent no-ops like the implicit empty "else" branch of an "if" expression.
    //[<DefaultValue]
    member inline _.Zero() : UnitTaskCode<unit> = ResumableCode.Zero()

    member inline _.Return (value: 'T) : UnitTaskCode<'T> =
        UnitTaskCode<_>(fun sm ->
            true)

    /// Chains together a step with its following step.
    /// Note that this requires that the first step has no result.
    /// This prevents constructs like `task { return 1; return 2; }`.
    member inline _.Combine(task1: UnitTaskCode<unit>, task2: UnitTaskCode<'T>) : UnitTaskCode<'T> =
        ResumableCode.Combine(task1, task2)

    /// Builds a step that executes the body while the condition predicate is true.
    member inline _.While ([<InlineIfLambda>] condition : unit -> bool, body : UnitTaskCode<unit>) : UnitTaskCode<unit> =
        ResumableCode.While(condition, body)

    /// Wraps a step in a try/with. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryWith (body: UnitTaskCode<'T>, catch: exn -> UnitTaskCode<'T>) : UnitTaskCode<'T> =
        ResumableCode.TryWith(body, catch)

    /// Wraps a step in a try/finally. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryFinally (body: UnitTaskCode<'T>, [<InlineIfLambda>] compensation : unit -> unit) : UnitTaskCode<'T> =
        ResumableCode.TryFinally(body, ResumableCode<_,_>(fun _sm -> compensation(); true))

    member inline _.For (sequence : seq<'T>, body : 'T -> UnitTaskCode<unit>) : UnitTaskCode<unit> =
        ResumableCode.For(sequence, body)

    type UnitTaskBuilder() =

        inherit UnitTaskBuilderBase()

        static member RunDynamic(code: UnitTaskCode<'T>) : Task =
            let mutable sm = UnitTaskStateMachine()
            let initialResumptionFunc = UnitTaskResumptionFunc(fun sm -> code.Invoke(&sm))
            let resumptionInfo =
                { new UnitTaskResumptionDynamicInfo(initialResumptionFunc) with
                    member info.MoveNext(sm) =
                        let mutable savedExn = null
                        try
                            sm.ResumptionDynamicInfo.ResumptionData <- null
                            let step = info.ResumptionFunc.Invoke(&sm)
                            if step then
                                sm.Data.MethodBuilder.SetResult()
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
            sm.Data.MethodBuilder <- AsyncTaskMethodBuilder.Create()
            sm.Data.MethodBuilder.Start(&sm)
            sm.Data.MethodBuilder.Task

        static member inline Run(code: UnitTaskCode<'T>) : Task =
             if __useResumableCode then
                __stateMachine<UnitTaskStateMachineData, Task>
                    (MoveNextMethodImpl<_>(fun sm ->
                        //-- RESUMABLE CODE START
                        __resumeAt sm.ResumptionPoint
                        let mutable __stack_exn : Exception = null
                        try
                            let __stack_code_fin = code.Invoke(&sm)
                            if __stack_code_fin then
                                sm.Data.MethodBuilder.SetResult()
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
                        sm.Data.MethodBuilder <- AsyncTaskMethodBuilder.Create()
                        sm.Data.MethodBuilder.Start(&sm)
                        sm.Data.MethodBuilder.Task))
             else
                UnitTaskBuilder.RunDynamic(code)

        member inline _.Run(code: UnitTaskCode<unit>) : Task =
           UnitTaskBuilder.Run(code)

namespace En3Tho.FSharp.ComputationExpressions.Tasks.UnitTaskBuilderExtensions

open System
open En3Tho.FSharp.ComputationExpressions.Tasks
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

module LowPriority =
    // Low priority extensions
    type UnitTaskBuilderBase with

        [<NoEagerConstraintApplication>]
        static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter
                                            when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                            and ^Awaiter :> ICriticalNotifyCompletion
                                            and ^Awaiter: (member get_IsCompleted:  unit -> bool)
                                            and ^Awaiter: (member GetResult:  unit ->  'TResult1)>
                    (sm: byref<_>, task: ^TaskLike, continuation: ('TResult1 -> UnitTaskCode<'TResult2>)) : bool =

                let mutable awaiter = (^TaskLike: (member GetAwaiter : unit -> ^Awaiter)(task))

                let cont =
                    (UnitTaskResumptionFunc( fun sm ->
                        let result = (^Awaiter : (member GetResult : unit -> 'TResult1)(awaiter))
                        (continuation result).Invoke(&sm)))

                // shortcut to continue immediately
                if (^Awaiter : (member get_IsCompleted : unit -> bool)(awaiter)) then
                    cont.Invoke(&sm)
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false

        [<NoEagerConstraintApplication>]
        member inline _.Bind< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter
                                            when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                            and ^Awaiter :> ICriticalNotifyCompletion
                                            and ^Awaiter: (member get_IsCompleted:  unit -> bool)
                                            and ^Awaiter: (member GetResult:  unit ->  'TResult1)>
                    (task: ^TaskLike, continuation: ('TResult1 -> UnitTaskCode<'TResult2>)) : UnitTaskCode<'TResult2> =

            UnitTaskCode<_>(fun sm ->
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
                        let result = (^Awaiter : (member GetResult : unit -> 'TResult1)(awaiter))
                        (continuation result).Invoke(&sm)
                    else
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&awaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter>(&sm, task, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Using<'Resource, 'T when 'Resource :> IDisposable> (resource: 'Resource, body: 'Resource -> UnitTaskCode<'T>) =
            ResumableCode.Using(resource, body)

module HighPriority =
    // High priority extensions
    type UnitTaskBuilderBase with
        static member BindDynamic (sm: byref<_>, task: Task<'TResult1>, continuation: ('TResult1 -> UnitTaskCode<'TResult2>)) : bool =
            let mutable awaiter = task.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    (continuation result).Invoke(&sm)))

            // shortcut to continue immediately
            if awaiter.IsCompleted then
                cont.Invoke(&sm)
            else
                sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        member inline _.Bind (task: Task<'TResult1>, continuation: ('TResult1 -> UnitTaskCode<'TResult2>)) : UnitTaskCode<'TResult2> =

            UnitTaskCode<_>(fun sm ->
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
                    UnitTaskBuilderBase.BindDynamic(&sm, task, continuation)
                //-- RESUMABLE CODE END
            )

        member inline this.ReturnFrom (task: Task<'T>) : UnitTaskCode<'T> =
            this.Bind(task, (fun v -> this.Return v))

        static member BindDynamic (sm: byref<_>, task: ValueTask<'TResult1>, continuation: ('TResult1 -> UnitTaskCode<'TResult2>)) : bool =
            let mutable awaiter = task.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    (continuation result).Invoke(&sm)))

            // shortcut to continue immediately
            if awaiter.IsCompleted then
                cont.Invoke(&sm)
            else
                sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        member inline _.Bind (task: ValueTask<'TResult1>, continuation: ('TResult1 -> UnitTaskCode<'TResult2>)) : UnitTaskCode<'TResult2> =

            UnitTaskCode<_>(fun sm ->
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
                    UnitTaskBuilderBase.BindDynamic(&sm, task, continuation)
                //-- RESUMABLE CODE END
            )

        member inline this.ReturnFrom (task: ValueTask<'T>) : UnitTaskCode<'T> =
            this.Bind(task, (fun v -> this.Return v))
        
        static member BindDynamic2 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, continuation: ('TResult1 * 'TResult2 -> UnitTaskCode<'TResult3>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    (continuation(result, result2).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper2()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false
            
        static member BindDynamic3 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, continuation: ('TResult1 * 'TResult2 * 'TResult3 -> UnitTaskCode<'TResult4>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()
            let mutable awaiter3 = task3.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    let result3 = awaiter3.GetResult()
                    (continuation(result, result2, result3).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper3()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                whenAllHelper.Add task3
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        static member BindDynamic4 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 -> UnitTaskCode<'TResult5>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()
            let mutable awaiter3 = task3.GetAwaiter()
            let mutable awaiter4 = task4.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    let result3 = awaiter3.GetResult()
                    let result4 = awaiter4.GetResult()
                    (continuation(result, result2, result3, result4).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper4()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                whenAllHelper.Add task3
                whenAllHelper.Add task4
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        static member BindDynamic5 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 -> UnitTaskCode<'TResult6>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()
            let mutable awaiter3 = task3.GetAwaiter()
            let mutable awaiter4 = task4.GetAwaiter()
            let mutable awaiter5 = task5.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    let result3 = awaiter3.GetResult()
                    let result4 = awaiter4.GetResult()
                    let result5 = awaiter5.GetResult()
                    (continuation(result, result2, result3, result4, result5).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper5()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                whenAllHelper.Add task3
                whenAllHelper.Add task4
                whenAllHelper.Add task5
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        static member BindDynamic6 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, task6: Task<'TResult6>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 -> UnitTaskCode<'TResult7>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()
            let mutable awaiter3 = task3.GetAwaiter()
            let mutable awaiter4 = task4.GetAwaiter()
            let mutable awaiter5 = task5.GetAwaiter()
            let mutable awaiter6 = task6.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    let result3 = awaiter3.GetResult()
                    let result4 = awaiter4.GetResult()
                    let result5 = awaiter5.GetResult()
                    let result6 = awaiter6.GetResult()
                    (continuation(result, result2, result3, result4, result5, result6).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted && awaiter6.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper6()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                whenAllHelper.Add task3
                whenAllHelper.Add task4
                whenAllHelper.Add task5
                whenAllHelper.Add task6
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        static member BindDynamic7 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, task6: Task<'TResult6>, task7: Task<'TResult7>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 * 'TResult7 -> UnitTaskCode<'TResult8>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()
            let mutable awaiter3 = task3.GetAwaiter()
            let mutable awaiter4 = task4.GetAwaiter()
            let mutable awaiter5 = task5.GetAwaiter()
            let mutable awaiter6 = task6.GetAwaiter()
            let mutable awaiter7 = task7.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    let result3 = awaiter3.GetResult()
                    let result4 = awaiter4.GetResult()
                    let result5 = awaiter5.GetResult()
                    let result6 = awaiter6.GetResult()
                    let result7 = awaiter7.GetResult()
                    (continuation(result, result2, result3, result4, result5, result6, result7).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted && awaiter6.IsCompleted && awaiter7.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper7()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                whenAllHelper.Add task3
                whenAllHelper.Add task4
                whenAllHelper.Add task5
                whenAllHelper.Add task6
                whenAllHelper.Add task7
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        static member BindDynamic2 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, continuation: ('TResult1 * 'TResult2 -> UnitTaskCode<'TResult3>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    (continuation(result, result2).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper2()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        static member BindDynamic3 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, continuation: ('TResult1 * 'TResult2 * 'TResult3 -> UnitTaskCode<'TResult4>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()
            let mutable awaiter3 = task3.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    let result3 = awaiter3.GetResult()
                    (continuation(result, result2, result3).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper3()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                whenAllHelper.Add task3
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        static member BindDynamic4 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 -> UnitTaskCode<'TResult5>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()
            let mutable awaiter3 = task3.GetAwaiter()
            let mutable awaiter4 = task4.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    let result3 = awaiter3.GetResult()
                    let result4 = awaiter4.GetResult()
                    (continuation(result, result2, result3, result4).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper4()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                whenAllHelper.Add task3
                whenAllHelper.Add task4
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        static member BindDynamic5 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 -> UnitTaskCode<'TResult6>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()
            let mutable awaiter3 = task3.GetAwaiter()
            let mutable awaiter4 = task4.GetAwaiter()
            let mutable awaiter5 = task5.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    let result3 = awaiter3.GetResult()
                    let result4 = awaiter4.GetResult()
                    let result5 = awaiter5.GetResult()
                    (continuation(result, result2, result3, result4, result5).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper5()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                whenAllHelper.Add task3
                whenAllHelper.Add task4
                whenAllHelper.Add task5
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        static member BindDynamic6 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, task6: ValueTask<'TResult6>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 -> UnitTaskCode<'TResult7>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()
            let mutable awaiter3 = task3.GetAwaiter()
            let mutable awaiter4 = task4.GetAwaiter()
            let mutable awaiter5 = task5.GetAwaiter()
            let mutable awaiter6 = task6.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    let result3 = awaiter3.GetResult()
                    let result4 = awaiter4.GetResult()
                    let result5 = awaiter5.GetResult()
                    let result6 = awaiter6.GetResult()
                    (continuation(result, result2, result3, result4, result5, result6).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted && awaiter6.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper6()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                whenAllHelper.Add task3
                whenAllHelper.Add task4
                whenAllHelper.Add task5
                whenAllHelper.Add task6
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        static member BindDynamic7 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, task6: ValueTask<'TResult6>, task7: ValueTask<'TResult7>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 * 'TResult7 -> UnitTaskCode<'TResult8>)) : bool =
            let mutable awaiter = task.GetAwaiter()
            let mutable awaiter2 = task2.GetAwaiter()
            let mutable awaiter3 = task3.GetAwaiter()
            let mutable awaiter4 = task4.GetAwaiter()
            let mutable awaiter5 = task5.GetAwaiter()
            let mutable awaiter6 = task6.GetAwaiter()
            let mutable awaiter7 = task7.GetAwaiter()

            let cont =
                (UnitTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    let result2 = awaiter2.GetResult()
                    let result3 = awaiter3.GetResult()
                    let result4 = awaiter4.GetResult()
                    let result5 = awaiter5.GetResult()
                    let result6 = awaiter6.GetResult()
                    let result7 = awaiter7.GetResult()
                    (continuation(result, result2, result3, result4, result5, result6, result7).Invoke(&sm))))

            // shortcut to continue immediately
            if awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted && awaiter6.IsCompleted && awaiter7.IsCompleted then
                cont.Invoke(&sm)
            else
                let mutable whenAllHelper = WhenAllHelper7()
                whenAllHelper.Add task
                whenAllHelper.Add task2
                whenAllHelper.Add task3
                whenAllHelper.Add task4
                whenAllHelper.Add task5
                whenAllHelper.Add task6
                whenAllHelper.Add task7
                let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                sm.ResumptionDynamicInfo.ResumptionData <- (combinedAwaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false
                        
        member inline _.Bind2 (task: Task<'TResult1>, task2: Task<'TResult2>, continuation: ('TResult1 * 'TResult2 -> UnitTaskCode<'TResult3>)) : UnitTaskCode<'TResult3> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        (continuation(result, result2).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper2()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic2(&sm, task, task2, continuation)
                //-- RESUMABLE CODE END
            )
                    
        member inline _.Bind3 (task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, continuation: ('TResult1 * 'TResult2 * 'TResult3 -> UnitTaskCode<'TResult4>)) : UnitTaskCode<'TResult4> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()
                    let mutable awaiter3 = task3.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        let result3 = awaiter3.GetResult()
                        (continuation(result, result2, result3).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper3()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        whenAllHelper.Add task3
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic3(&sm, task, task2, task3, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Bind4 (task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 -> UnitTaskCode<'TResult5>)) : UnitTaskCode<'TResult5> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()
                    let mutable awaiter3 = task3.GetAwaiter()
                    let mutable awaiter4 = task4.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        let result3 = awaiter3.GetResult()
                        let result4 = awaiter4.GetResult()
                        (continuation(result, result2, result3, result4).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper4()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        whenAllHelper.Add task3
                        whenAllHelper.Add task4
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic4(&sm, task, task2, task3, task4, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Bind5 (task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 -> UnitTaskCode<'TResult6>)) : UnitTaskCode<'TResult6> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()
                    let mutable awaiter3 = task3.GetAwaiter()
                    let mutable awaiter4 = task4.GetAwaiter()
                    let mutable awaiter5 = task5.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        let result3 = awaiter3.GetResult()
                        let result4 = awaiter4.GetResult()
                        let result5 = awaiter5.GetResult()
                        (continuation(result, result2, result3, result4, result5).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper5()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        whenAllHelper.Add task3
                        whenAllHelper.Add task4
                        whenAllHelper.Add task5
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic5(&sm, task, task2, task3, task4, task5, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Bind6 (task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, task6: Task<'TResult6>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 -> UnitTaskCode<'TResult7>)) : UnitTaskCode<'TResult7> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()
                    let mutable awaiter3 = task3.GetAwaiter()
                    let mutable awaiter4 = task4.GetAwaiter()
                    let mutable awaiter5 = task5.GetAwaiter()
                    let mutable awaiter6 = task6.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted && awaiter6.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        let result3 = awaiter3.GetResult()
                        let result4 = awaiter4.GetResult()
                        let result5 = awaiter5.GetResult()
                        let result6 = awaiter6.GetResult()
                        (continuation(result, result2, result3, result4, result5, result6).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper6()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        whenAllHelper.Add task3
                        whenAllHelper.Add task4
                        whenAllHelper.Add task5
                        whenAllHelper.Add task6
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic6(&sm, task, task2, task3, task4, task5, task6, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Bind7 (task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, task6: Task<'TResult6>, task7: Task<'TResult7>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 * 'TResult7 -> UnitTaskCode<'TResult8>)) : UnitTaskCode<'TResult8> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()
                    let mutable awaiter3 = task3.GetAwaiter()
                    let mutable awaiter4 = task4.GetAwaiter()
                    let mutable awaiter5 = task5.GetAwaiter()
                    let mutable awaiter6 = task6.GetAwaiter()
                    let mutable awaiter7 = task7.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted && awaiter6.IsCompleted && awaiter7.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        let result3 = awaiter3.GetResult()
                        let result4 = awaiter4.GetResult()
                        let result5 = awaiter5.GetResult()
                        let result6 = awaiter6.GetResult()
                        let result7 = awaiter7.GetResult()
                        (continuation(result, result2, result3, result4, result5, result6, result7).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper7()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        whenAllHelper.Add task3
                        whenAllHelper.Add task4
                        whenAllHelper.Add task5
                        whenAllHelper.Add task6
                        whenAllHelper.Add task7
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic7(&sm, task, task2, task3, task4, task5, task6, task7, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Bind2 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, continuation: ('TResult1 * 'TResult2 -> UnitTaskCode<'TResult3>)) : UnitTaskCode<'TResult3> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        (continuation(result, result2).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper2()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic2(&sm, task, task2, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Bind3 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, continuation: ('TResult1 * 'TResult2 * 'TResult3 -> UnitTaskCode<'TResult4>)) : UnitTaskCode<'TResult4> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()
                    let mutable awaiter3 = task3.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        let result3 = awaiter3.GetResult()
                        (continuation(result, result2, result3).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper3()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        whenAllHelper.Add task3
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic3(&sm, task, task2, task3, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Bind4 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 -> UnitTaskCode<'TResult5>)) : UnitTaskCode<'TResult5> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()
                    let mutable awaiter3 = task3.GetAwaiter()
                    let mutable awaiter4 = task4.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        let result3 = awaiter3.GetResult()
                        let result4 = awaiter4.GetResult()
                        (continuation(result, result2, result3, result4).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper4()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        whenAllHelper.Add task3
                        whenAllHelper.Add task4
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic4(&sm, task, task2, task3, task4, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Bind5 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 -> UnitTaskCode<'TResult6>)) : UnitTaskCode<'TResult6> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()
                    let mutable awaiter3 = task3.GetAwaiter()
                    let mutable awaiter4 = task4.GetAwaiter()
                    let mutable awaiter5 = task5.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        let result3 = awaiter3.GetResult()
                        let result4 = awaiter4.GetResult()
                        let result5 = awaiter5.GetResult()
                        (continuation(result, result2, result3, result4, result5).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper5()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        whenAllHelper.Add task3
                        whenAllHelper.Add task4
                        whenAllHelper.Add task5
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic5(&sm, task, task2, task3, task4, task5, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Bind6 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, task6: ValueTask<'TResult6>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 -> UnitTaskCode<'TResult7>)) : UnitTaskCode<'TResult7> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()
                    let mutable awaiter3 = task3.GetAwaiter()
                    let mutable awaiter4 = task4.GetAwaiter()
                    let mutable awaiter5 = task5.GetAwaiter()
                    let mutable awaiter6 = task6.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted && awaiter6.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        let result3 = awaiter3.GetResult()
                        let result4 = awaiter4.GetResult()
                        let result5 = awaiter5.GetResult()
                        let result6 = awaiter6.GetResult()
                        (continuation(result, result2, result3, result4, result5, result6).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper6()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        whenAllHelper.Add task3
                        whenAllHelper.Add task4
                        whenAllHelper.Add task5
                        whenAllHelper.Add task6
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic6(&sm, task, task2, task3, task4, task5, task6, continuation)
                //-- RESUMABLE CODE END
            )

        member inline _.Bind7 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, task6: ValueTask<'TResult6>, task7: ValueTask<'TResult7>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 * 'TResult7 -> UnitTaskCode<'TResult8>)) : UnitTaskCode<'TResult8> =

            UnitTaskCode<_>(fun sm ->
                if __useResumableCode then
                    //-- RESUMABLE CODE START
                    // Get an awaiter from the task
                    let mutable awaiter = task.GetAwaiter()
                    let mutable awaiter2 = task2.GetAwaiter()
                    let mutable awaiter3 = task3.GetAwaiter()
                    let mutable awaiter4 = task4.GetAwaiter()
                    let mutable awaiter5 = task5.GetAwaiter()
                    let mutable awaiter6 = task6.GetAwaiter()
                    let mutable awaiter7 = task7.GetAwaiter()

                    let mutable __stack_fin = true
                    if not (awaiter.IsCompleted && awaiter2.IsCompleted && awaiter3.IsCompleted && awaiter4.IsCompleted && awaiter5.IsCompleted && awaiter6.IsCompleted && awaiter7.IsCompleted) then
                        // This will yield with __stack_yield_fin = false
                        // This will resume with __stack_yield_fin = true
                        let __stack_yield_fin = ResumableCode.Yield().Invoke(&sm)
                        __stack_fin <- __stack_yield_fin
                    if __stack_fin then
                        let result = awaiter.GetResult()
                        let result2 = awaiter2.GetResult()
                        let result3 = awaiter3.GetResult()
                        let result4 = awaiter4.GetResult()
                        let result5 = awaiter5.GetResult()
                        let result6 = awaiter6.GetResult()
                        let result7 = awaiter7.GetResult()
                        (continuation(result, result2, result3, result4, result5, result6, result7).Invoke(&sm))
                    else
                        let mutable whenAllHelper = WhenAllHelper7()
                        whenAllHelper.Add task
                        whenAllHelper.Add task2
                        whenAllHelper.Add task3
                        whenAllHelper.Add task4
                        whenAllHelper.Add task5
                        whenAllHelper.Add task6
                        whenAllHelper.Add task7
                        let mutable combinedAwaiter = whenAllHelper.WhenAll().GetAwaiter()
                        sm.Data.MethodBuilder.AwaitUnsafeOnCompleted(&combinedAwaiter, &sm)
                        false
                else
                    UnitTaskBuilderBase.BindDynamic7(&sm, task, task2, task3, task4, task5, task6, task7, continuation)
                //-- RESUMABLE CODE END
            )
        
module MediumPriority =
    open HighPriority

    // Medium priority extensions
    type UnitTaskBuilderBase with
        member inline this.Bind (computation: Async<'TResult1>, continuation: ('TResult1 -> UnitTaskCode<'TResult2>)) : UnitTaskCode<'TResult2> =
            this.Bind (Async.StartAsTask computation, continuation)
        
        member inline this.Bind2 (computation: Async<'TResult1>, computation2: Async<'TResult2>, continuation: ('TResult1 * 'TResult2 -> UnitTaskCode<'TResult3>)) : UnitTaskCode<'TResult3> =
            this.Bind2 (Async.StartAsTask computation, Async.StartAsTask computation2, continuation)

        member inline this.Bind3 (computation: Async<'TResult1>, computation2: Async<'TResult2>, computation3: Async<'TResult3>, continuation: ('TResult1 * 'TResult2 * 'TResult3 -> UnitTaskCode<'TResult4>)) : UnitTaskCode<'TResult4> =
            this.Bind3 (Async.StartAsTask computation, Async.StartAsTask computation2, Async.StartAsTask computation3, continuation)

        member inline this.Bind4 (computation: Async<'TResult1>, computation2: Async<'TResult2>, computation3: Async<'TResult3>, computation4: Async<'TResult4>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 -> UnitTaskCode<'TResult5>)) : UnitTaskCode<'TResult5> =
            this.Bind4 (Async.StartAsTask computation, Async.StartAsTask computation2, Async.StartAsTask computation3, Async.StartAsTask computation4, continuation)

        member inline this.Bind5 (computation: Async<'TResult1>, computation2: Async<'TResult2>, computation3: Async<'TResult3>, computation4: Async<'TResult4>, computation5: Async<'TResult5>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 -> UnitTaskCode<'TResult6>)) : UnitTaskCode<'TResult6> =
            this.Bind5 (Async.StartAsTask computation, Async.StartAsTask computation2, Async.StartAsTask computation3, Async.StartAsTask computation4, Async.StartAsTask computation5, continuation)

        member inline this.Bind6 (computation: Async<'TResult1>, computation2: Async<'TResult2>, computation3: Async<'TResult3>, computation4: Async<'TResult4>, computation5: Async<'TResult5>, computation6: Async<'TResult6>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 -> UnitTaskCode<'TResult7>)) : UnitTaskCode<'TResult7> =
            this.Bind6 (Async.StartAsTask computation, Async.StartAsTask computation2, Async.StartAsTask computation3, Async.StartAsTask computation4, Async.StartAsTask computation5, Async.StartAsTask computation6, continuation)

        member inline this.Bind7 (computation: Async<'TResult1>, computation2: Async<'TResult2>, computation3: Async<'TResult3>, computation4: Async<'TResult4>, computation5: Async<'TResult5>, computation6: Async<'TResult6>, computation7: Async<'TResult7>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 * 'TResult7 -> UnitTaskCode<'TResult8>)) : UnitTaskCode<'TResult8> =
            this.Bind7 (Async.StartAsTask computation, Async.StartAsTask computation2, Async.StartAsTask computation3, Async.StartAsTask computation4, Async.StartAsTask computation5, Async.StartAsTask computation6, Async.StartAsTask computation7, continuation)
        
        member inline this.ReturnFrom (computation: Async<'T>)  : UnitTaskCode<'T> =
            this.ReturnFrom (Async.StartAsTask computation)