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
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers

/// The extra data stored in ResumableStateMachine for tasks
    [<Struct; NoComparison; NoEquality>]
    type UnitValueTaskStateMachineData =
       
        [<DefaultValue(false)>]
        val mutable MethodBuilder : AsyncValueTaskMethodBuilder

    and UnitValueTaskStateMachine = ResumableStateMachine<UnitValueTaskStateMachineData>
    and UnitValueTaskResumptionFunc = ResumptionFunc<UnitValueTaskStateMachineData>
    and UnitValueTaskResumptionDynamicInfo = ResumptionDynamicInfo<UnitValueTaskStateMachineData>
    and UnitValueTaskCode<'T> = ResumableCode<UnitValueTaskStateMachineData, 'T>

type UnitValueTaskBuilderBase() =

    member inline _.Delay(generator : unit -> UnitValueTaskCode<'T>) : UnitValueTaskCode<'T> =
        UnitValueTaskCode<'T>(fun sm -> (generator()).Invoke(&sm))

    /// Used to represent no-ops like the implicit empty "else" branch of an "if" expression.
    //[<DefaultValue]
    member inline _.Zero() : UnitValueTaskCode<unit> = ResumableCode.Zero()

    member inline _.Return (value: 'T) : UnitValueTaskCode<'T> =
        UnitValueTaskCode<_>(fun sm ->
            true)

    /// Chains together a step with its following step.
    /// Note that this requires that the first step has no result.
    /// This prevents constructs like `task { return 1; return 2; }`.
    member inline _.Combine(task1: UnitValueTaskCode<unit>, task2: UnitValueTaskCode<'T>) : UnitValueTaskCode<'T> =
        ResumableCode.Combine(task1, task2)

    /// Builds a step that executes the body while the condition predicate is true.
    member inline _.While ([<InlineIfLambda>] condition : unit -> bool, body : UnitValueTaskCode<unit>) : UnitValueTaskCode<unit> =
        ResumableCode.While(condition, body)

    /// Wraps a step in a try/with. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryWith (body: UnitValueTaskCode<'T>, catch: exn -> UnitValueTaskCode<'T>) : UnitValueTaskCode<'T> =
        ResumableCode.TryWith(body, catch)

    /// Wraps a step in a try/finally. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryFinally (body: UnitValueTaskCode<'T>, [<InlineIfLambda>] compensation : unit -> unit) : UnitValueTaskCode<'T> =
        ResumableCode.TryFinally(body, ResumableCode<_,_>(fun _sm -> compensation(); true))

    member inline _.For (sequence : seq<'T>, body : 'T -> UnitValueTaskCode<unit>) : UnitValueTaskCode<unit> =
        ResumableCode.For(sequence, body)

    type UnitValueTaskBuilder() =

        inherit UnitValueTaskBuilderBase()

        static member RunDynamic(code: UnitValueTaskCode<'T>) : ValueTask =
            let mutable sm = UnitValueTaskStateMachine()
            let initialResumptionFunc = UnitValueTaskResumptionFunc(fun sm -> code.Invoke(&sm))
            let resumptionInfo =
                { new UnitValueTaskResumptionDynamicInfo(initialResumptionFunc) with
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
            sm.Data.MethodBuilder <- AsyncValueTaskMethodBuilder.Create()
            sm.Data.MethodBuilder.Start(&sm)
            sm.Data.MethodBuilder.Task

        static member inline Run(code : UnitValueTaskCode<'T>) : ValueTask =
             if __useResumableCode then
                __stateMachine<UnitValueTaskStateMachineData, ValueTask>
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
                        sm.Data.MethodBuilder <- AsyncValueTaskMethodBuilder.Create()
                        sm.Data.MethodBuilder.Start(&sm)
                        sm.Data.MethodBuilder.Task))
             else
                UnitValueTaskBuilder.RunDynamic(code)

        member inline _.Run(code : UnitValueTaskCode<unit>) : ValueTask =
           UnitValueTaskBuilder.Run(code)

namespace En3Tho.FSharp.ComputationExpressions.Tasks.UnitValueTaskBuilderExtensions

open En3Tho.FSharp.ComputationExpressions.Tasks
open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

module LowPriority =
    // Low priority extensions
    type UnitValueTaskBuilderBase with

        [<NoEagerConstraintApplication>]
        static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall
                                            when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                            and ^Awaiter :> ICriticalNotifyCompletion
                                            and ^Awaiter: (member get_IsCompleted:  unit -> bool)
                                            and ^Awaiter: (member GetResult:  unit ->  'TResult1)>
                    (sm: byref<_>, task: ^TaskLike, continuation: ('TResult1 -> UnitValueTaskCode<'TResult2>)) : bool =

                let mutable awaiter = (^TaskLike: (member GetAwaiter : unit -> ^Awaiter)(task))

                let cont =
                    (UnitValueTaskResumptionFunc( fun sm ->
                        let result = (^Awaiter : (member GetResult : unit -> 'TResult1)(awaiter))
                        (continuation result).Invoke(&sm)))

                // shortcut to continue immediately
                if (^Awaiter : (member get_IsCompleted : unit -> bool)(awaiter)) then
                    cont.Invoke(&sm)
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false

module HighPriority =
    // High priority extensions
    type UnitValueTaskBuilderBase with
        static member BindDynamic (sm: byref<_>, task: Task<'TResult1>, continuation: ('TResult1 -> UnitValueTaskCode<'TResult2>)) : bool =
            let mutable awaiter = task.GetAwaiter()

            let cont =
                (UnitValueTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    (continuation result).Invoke(&sm)))

            // shortcut to continue immediately
            if awaiter.IsCompleted then
                cont.Invoke(&sm)
            else
                sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        member inline _.Bind (task: Task<'TResult1>, continuation: ('TResult1 -> UnitValueTaskCode<'TResult2>)) : UnitValueTaskCode<'TResult2> =

            UnitValueTaskCode<_>(fun sm ->
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
                    UnitValueTaskBuilderBase.BindDynamic(&sm, task, continuation)
                //-- RESUMABLE CODE END
            )

        member inline this.ReturnFrom (task: Task<'T>) : UnitValueTaskCode<'T> =
            this.Bind(task, (fun v -> this.Return v))

        static member BindDynamic (sm: byref<_>, task: ValueTask<'TResult1>, continuation: ('TResult1 -> UnitValueTaskCode<'TResult2>)) : bool =
            let mutable awaiter = task.GetAwaiter()

            let cont =
                (UnitValueTaskResumptionFunc(fun sm ->
                    let result = awaiter.GetResult()
                    (continuation result).Invoke(&sm)))

            // shortcut to continue immediately
            if awaiter.IsCompleted then
                cont.Invoke(&sm)
            else
                sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                false

        member inline _.Bind (task: ValueTask<'TResult1>, continuation: ('TResult1 -> UnitValueTaskCode<'TResult2>)) : UnitValueTaskCode<'TResult2> =

            UnitValueTaskCode<_>(fun sm ->
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
                    UnitValueTaskBuilderBase.BindDynamic(&sm, task, continuation)
                //-- RESUMABLE CODE END
            )

        member inline this.ReturnFrom (task: ValueTask<'T>) : UnitValueTaskCode<'T> =
            this.Bind(task, (fun v -> this.Return v))

module MediumPriority =
    open HighPriority

    // Medium priority extensions
    type UnitValueTaskBuilderBase with
        member inline this.Bind (computation: Async<'TResult1>, continuation: ('TResult1 -> UnitValueTaskCode<'TResult2>)) : UnitValueTaskCode<'TResult2> =
            this.Bind (Async.StartAsTask computation, continuation)

        member inline this.ReturnFrom (computation: Async<'T>)  : UnitValueTaskCode<'T> =
            this.ReturnFrom (Async.StartAsTask computation)