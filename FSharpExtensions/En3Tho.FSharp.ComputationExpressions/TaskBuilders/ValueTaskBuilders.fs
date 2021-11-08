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

/// The extra data stored in ResumableStateMachine for tasks
[<Struct; NoComparison; NoEquality>]
type ValueTaskStateMachineData<'T> =

    [<DefaultValue(false)>]
    val mutable Result : 'T

    [<DefaultValue(false)>]
    val mutable MethodBuilder : AsyncValueTaskMethodBuilder<'T>

and ValueTaskStateMachine<'TOverall> = ResumableStateMachine<ValueTaskStateMachineData<'TOverall>>
and ValueTaskResumptionFunc<'TOverall> = ResumptionFunc<ValueTaskStateMachineData<'TOverall>>
and ValueTaskResumptionDynamicInfo<'TOverall> = ResumptionDynamicInfo<ValueTaskStateMachineData<'TOverall>>
and ValueTaskCode<'TOverall, 'T> = ResumableCode<ValueTaskStateMachineData<'TOverall>, 'T>

type ValueTaskBuilderBase() =

    member inline _.Delay(generator : unit -> ValueTaskCode<'TOverall, 'T>) : ValueTaskCode<'TOverall, 'T> =
        ValueTaskCode<'TOverall, 'T>(fun sm -> (generator()).Invoke(&sm))

    /// Used to represent no-ops like the implicit empty "else" branch of an "if" expression.
    //[<DefaultValue]
    member inline _.Zero() : ValueTaskCode<'TOverall, unit> = ResumableCode.Zero()

    member inline _.Return (value: 'T) : ValueTaskCode<'T, 'T> =
        ValueTaskCode<'T, _>(fun sm ->
            sm.Data.Result <- value
            true)

    /// Chains together a step with its following step.
    /// Note that this requires that the first step has no result.
    /// This prevents constructs like `task { return 1; return 2; }`.
    member inline _.Combine(task1: ValueTaskCode<'TOverall, unit>, task2: ValueTaskCode<'TOverall, 'T>) : ValueTaskCode<'TOverall, 'T> =
        ResumableCode.Combine(task1, task2)

    /// Builds a step that executes the body while the condition predicate is true.
    member inline _.While ([<InlineIfLambda>] condition : unit -> bool, body : ValueTaskCode<'TOverall, unit>) : ValueTaskCode<'TOverall, unit> =
        ResumableCode.While(condition, body)

    /// Wraps a step in a try/with. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryWith (body: ValueTaskCode<'TOverall, 'T>, catch: exn -> ValueTaskCode<'TOverall, 'T>) : ValueTaskCode<'TOverall, 'T> =
        ResumableCode.TryWith(body, catch)

    /// Wraps a step in a try/finally. This catches exceptions both in the evaluation of the function
    /// to retrieve the step, and in the continuation of the step (if any).
    member inline _.TryFinally (body: ValueTaskCode<'TOverall, 'T>, [<InlineIfLambda>] compensation : unit -> unit) : ValueTaskCode<'TOverall, 'T> =
        ResumableCode.TryFinally(body, ResumableCode<_,_>(fun _sm -> compensation(); true))

    member inline _.For (sequence : seq<'T>, body : 'T -> ValueTaskCode<'TOverall, unit>) : ValueTaskCode<'TOverall, unit> =
        ResumableCode.For(sequence, body)

    type ValueTaskBuilder() =

        inherit ValueTaskBuilderBase()

        static member RunDynamic(code: ValueTaskCode<'T, 'T>) : ValueTask<'T> =
            let mutable sm = ValueTaskStateMachine<'T>()
            let initialResumptionFunc = ValueTaskResumptionFunc<'T>(fun sm -> code.Invoke(&sm))
            let resumptionInfo =
                { new ValueTaskResumptionDynamicInfo<'T>(initialResumptionFunc) with
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
            sm.Data.MethodBuilder <- AsyncValueTaskMethodBuilder<'T>.Create()
            sm.Data.MethodBuilder.Start(&sm)
            sm.Data.MethodBuilder.Task

        static member inline Run(code : ValueTaskCode<'T, 'T>) : ValueTask<'T> =
             if __useResumableCode then
                __stateMachine<ValueTaskStateMachineData<'T>, ValueTask<'T>>
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
                        sm.Data.MethodBuilder <- AsyncValueTaskMethodBuilder<'T>.Create()
                        sm.Data.MethodBuilder.Start(&sm)
                        sm.Data.MethodBuilder.Task))
             else
                ValueTaskBuilder.RunDynamic(code)

        member inline _.Run(code : ValueTaskCode<'T, 'T>) : ValueTask<'T> =
           ValueTaskBuilder.Run(code)

namespace En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions

    open En3Tho.FSharp.ComputationExpressions.Tasks
    open System
    open System.Runtime.CompilerServices
    open System.Threading.Tasks
    open Microsoft.FSharp.Core
    open Microsoft.FSharp.Core.CompilerServices
    open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
    open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

    module LowPriority =

        type ValueTaskBuilderBase with

            [<NoEagerConstraintApplication>]
            static member inline BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall
                                                when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                and ^Awaiter :> ICriticalNotifyCompletion
                                                and ^Awaiter: (member get_IsCompleted:  unit -> bool)
                                                and ^Awaiter: (member GetResult:  unit ->  'TResult1)>
                        (sm: byref<_>, task: ^TaskLike, continuation: ('TResult1 -> ValueTaskCode<'TOverall, 'TResult2>)) : bool =

                    let mutable awaiter = (^TaskLike: (member GetAwaiter : unit -> ^Awaiter)(task))

                    let cont =
                        (ValueTaskResumptionFunc<'TOverall>( fun sm ->
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
            member inline _.Bind< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall
                                                when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                and ^Awaiter :> ICriticalNotifyCompletion
                                                and ^Awaiter: (member get_IsCompleted:  unit -> bool)
                                                and ^Awaiter: (member GetResult:  unit ->  'TResult1)>
                        (task: ^TaskLike, continuation: ('TResult1 -> ValueTaskCode<'TOverall, 'TResult2>)) : ValueTaskCode<'TOverall, 'TResult2> =

                ValueTaskCode<'TOverall, _>(fun sm ->
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
                        ValueTaskBuilderBase.BindDynamic< ^TaskLike, 'TResult1, 'TResult2, ^Awaiter , 'TOverall>(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            [<NoEagerConstraintApplication>]
            member inline this.ReturnFrom< ^TaskLike, ^Awaiter, 'T
                                                  when  ^TaskLike: (member GetAwaiter:  unit ->  ^Awaiter)
                                                  and ^Awaiter :> ICriticalNotifyCompletion
                                                  and ^Awaiter: (member get_IsCompleted: unit -> bool)
                                                  and ^Awaiter: (member GetResult: unit ->  'T)>
                    (task: ^TaskLike) : ValueTaskCode< 'T,  'T> =

                this.Bind(task, (fun v -> this.Return v))

            member inline _.Using<'Resource, 'TOverall, 'T when 'Resource :> IDisposable> (resource: 'Resource, body: 'Resource -> ValueTaskCode<'TOverall, 'T>) =
                ResumableCode.Using(resource, body)

    module HighPriority =
        // High priority extensions

        type ValueTaskBuilderBase with
            static member BindDynamic (sm: byref<_>, task: Task<'TResult1>, continuation: ('TResult1 -> ValueTaskCode<'TOverall, 'TResult2>)) : bool =
                let mutable awaiter = task.GetAwaiter()

                let cont =
                    (ValueTaskResumptionFunc<'TOverall>(fun sm ->
                        let result = awaiter.GetResult()
                        (continuation result).Invoke(&sm)))

                // shortcut to continue immediately
                if awaiter.IsCompleted then
                    cont.Invoke(&sm)
                else
                    sm.ResumptionDynamicInfo.ResumptionData <- (awaiter :> ICriticalNotifyCompletion)
                    sm.ResumptionDynamicInfo.ResumptionFunc <- cont
                    false

            member inline _.Bind (task: Task<'TResult1>, continuation: ('TResult1 -> ValueTaskCode<'TOverall, 'TResult2>)) : ValueTaskCode<'TOverall, 'TResult2> =

                ValueTaskCode<'TOverall, _>(fun sm ->
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
                        ValueTaskBuilderBase.BindDynamic(&sm, task, continuation)
                    //-- RESUMABLE CODE END
                )

            member inline this.ReturnFrom (task: Task<'T>) : ValueTaskCode<'T, 'T> =
                this.Bind(task, (fun v -> this.Return v))

    module MediumPriority =
        open HighPriority

        // Medium priority extensions
        type ValueTaskBuilderBase with
            member inline this.Bind (computation: Async<'TResult1>, continuation: ('TResult1 -> ValueTaskCode<'TOverall, 'TResult2>)) : ValueTaskCode<'TOverall, 'TResult2> =
                this.Bind (Async.StartAsTask computation, continuation)

            member inline this.ReturnFrom (computation: Async<'T>)  : ValueTaskCode<'T, 'T> =
                this.ReturnFrom (Async.StartAsTask computation)