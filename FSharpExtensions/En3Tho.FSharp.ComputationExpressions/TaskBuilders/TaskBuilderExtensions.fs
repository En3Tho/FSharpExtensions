module En3Tho.FSharp.ComputationExpressions.Tasks.TaskBuilderExtensions

open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.FSharp.Core.CompilerServices
open Microsoft.FSharp.Core.CompilerServices.StateMachineHelpers
open Microsoft.FSharp.Core.LanguagePrimitives.IntrinsicOperators

#nowarn "1204"
#nowarn "3513"

type TaskBuilder with
    static member BindDynamic2 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, continuation: ('TResult1 * 'TResult2 -> TaskCode<'TOverall, 'TResult3>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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
            
    static member BindDynamic3 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, continuation: ('TResult1 * 'TResult2 * 'TResult3 -> TaskCode<'TOverall, 'TResult4>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()
        let mutable awaiter3 = task3.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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

    static member BindDynamic4 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 -> TaskCode<'TOverall, 'TResult5>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()
        let mutable awaiter3 = task3.GetAwaiter()
        let mutable awaiter4 = task4.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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

    static member BindDynamic5 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 -> TaskCode<'TOverall, 'TResult6>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()
        let mutable awaiter3 = task3.GetAwaiter()
        let mutable awaiter4 = task4.GetAwaiter()
        let mutable awaiter5 = task5.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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

    static member BindDynamic6 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, task6: Task<'TResult6>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 -> TaskCode<'TOverall, 'TResult7>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()
        let mutable awaiter3 = task3.GetAwaiter()
        let mutable awaiter4 = task4.GetAwaiter()
        let mutable awaiter5 = task5.GetAwaiter()
        let mutable awaiter6 = task6.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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

    static member BindDynamic7 (sm: byref<_>, task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, task6: Task<'TResult6>, task7: Task<'TResult7>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 * 'TResult7 -> TaskCode<'TOverall, 'TResult8>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()
        let mutable awaiter3 = task3.GetAwaiter()
        let mutable awaiter4 = task4.GetAwaiter()
        let mutable awaiter5 = task5.GetAwaiter()
        let mutable awaiter6 = task6.GetAwaiter()
        let mutable awaiter7 = task7.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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

    static member BindDynamic2 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, continuation: ('TResult1 * 'TResult2 -> TaskCode<'TOverall, 'TResult3>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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

    static member BindDynamic3 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, continuation: ('TResult1 * 'TResult2 * 'TResult3 -> TaskCode<'TOverall, 'TResult4>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()
        let mutable awaiter3 = task3.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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

    static member BindDynamic4 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 -> TaskCode<'TOverall, 'TResult5>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()
        let mutable awaiter3 = task3.GetAwaiter()
        let mutable awaiter4 = task4.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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

    static member BindDynamic5 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 -> TaskCode<'TOverall, 'TResult6>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()
        let mutable awaiter3 = task3.GetAwaiter()
        let mutable awaiter4 = task4.GetAwaiter()
        let mutable awaiter5 = task5.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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

    static member BindDynamic6 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, task6: ValueTask<'TResult6>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 -> TaskCode<'TOverall, 'TResult7>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()
        let mutable awaiter3 = task3.GetAwaiter()
        let mutable awaiter4 = task4.GetAwaiter()
        let mutable awaiter5 = task5.GetAwaiter()
        let mutable awaiter6 = task6.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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

    static member BindDynamic7 (sm: byref<_>, task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, task6: ValueTask<'TResult6>, task7: ValueTask<'TResult7>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 * 'TResult7 -> TaskCode<'TOverall, 'TResult8>)) : bool =
        let mutable awaiter = task.GetAwaiter()
        let mutable awaiter2 = task2.GetAwaiter()
        let mutable awaiter3 = task3.GetAwaiter()
        let mutable awaiter4 = task4.GetAwaiter()
        let mutable awaiter5 = task5.GetAwaiter()
        let mutable awaiter6 = task6.GetAwaiter()
        let mutable awaiter7 = task7.GetAwaiter()

        let cont =
            (TaskResumptionFunc<'TOverall>(fun sm ->
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
                    
    member inline _.Bind2 (task: Task<'TResult1>, task2: Task<'TResult2>, continuation: ('TResult1 * 'TResult2 -> TaskCode<'TOverall, 'TResult3>)) : TaskCode<'TOverall, 'TResult3> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic2(&sm, task, task2, continuation)
            //-- RESUMABLE CODE END
        )
                
    member inline _.Bind3 (task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, continuation: ('TResult1 * 'TResult2 * 'TResult3 -> TaskCode<'TOverall, 'TResult4>)) : TaskCode<'TOverall, 'TResult4> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic3(&sm, task, task2, task3, continuation)
            //-- RESUMABLE CODE END
        )

    member inline _.Bind4 (task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 -> TaskCode<'TOverall, 'TResult5>)) : TaskCode<'TOverall, 'TResult5> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic4(&sm, task, task2, task3, task4, continuation)
            //-- RESUMABLE CODE END
        )

    member inline _.Bind5 (task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 -> TaskCode<'TOverall, 'TResult6>)) : TaskCode<'TOverall, 'TResult6> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic5(&sm, task, task2, task3, task4, task5, continuation)
            //-- RESUMABLE CODE END
        )

    member inline _.Bind6 (task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, task6: Task<'TResult6>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 -> TaskCode<'TOverall, 'TResult7>)) : TaskCode<'TOverall, 'TResult7> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic6(&sm, task, task2, task3, task4, task5, task6, continuation)
            //-- RESUMABLE CODE END
        )

    member inline _.Bind7 (task: Task<'TResult1>, task2: Task<'TResult2>, task3: Task<'TResult3>, task4: Task<'TResult4>, task5: Task<'TResult5>, task6: Task<'TResult6>, task7: Task<'TResult7>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 * 'TResult7 -> TaskCode<'TOverall, 'TResult8>)) : TaskCode<'TOverall, 'TResult8> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic7(&sm, task, task2, task3, task4, task5, task6, task7, continuation)
            //-- RESUMABLE CODE END
        )

    member inline _.Bind2 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, continuation: ('TResult1 * 'TResult2 -> TaskCode<'TOverall, 'TResult3>)) : TaskCode<'TOverall, 'TResult3> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic2(&sm, task, task2, continuation)
            //-- RESUMABLE CODE END
        )

    member inline _.Bind3 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, continuation: ('TResult1 * 'TResult2 * 'TResult3 -> TaskCode<'TOverall, 'TResult4>)) : TaskCode<'TOverall, 'TResult4> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic3(&sm, task, task2, task3, continuation)
            //-- RESUMABLE CODE END
        )

    member inline _.Bind4 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 -> TaskCode<'TOverall, 'TResult5>)) : TaskCode<'TOverall, 'TResult5> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic4(&sm, task, task2, task3, task4, continuation)
            //-- RESUMABLE CODE END
        )

    member inline _.Bind5 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 -> TaskCode<'TOverall, 'TResult6>)) : TaskCode<'TOverall, 'TResult6> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic5(&sm, task, task2, task3, task4, task5, continuation)
            //-- RESUMABLE CODE END
        )

    member inline _.Bind6 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, task6: ValueTask<'TResult6>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 -> TaskCode<'TOverall, 'TResult7>)) : TaskCode<'TOverall, 'TResult7> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic6(&sm, task, task2, task3, task4, task5, task6, continuation)
            //-- RESUMABLE CODE END
        )

    member inline _.Bind7 (task: ValueTask<'TResult1>, task2: ValueTask<'TResult2>, task3: ValueTask<'TResult3>, task4: ValueTask<'TResult4>, task5: ValueTask<'TResult5>, task6: ValueTask<'TResult6>, task7: ValueTask<'TResult7>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 * 'TResult7 -> TaskCode<'TOverall, 'TResult8>)) : TaskCode<'TOverall, 'TResult8> =

        TaskCode<'TOverall, _>(fun sm ->
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
                TaskBuilder.BindDynamic7(&sm, task, task2, task3, task4, task5, task6, task7, continuation)
            //-- RESUMABLE CODE END
        )
    
    member inline this.Bind2 (computation: Async<'TResult1>, computation2: Async<'TResult2>, continuation: ('TResult1 * 'TResult2 -> TaskCode<'TOverall, 'TResult3>)) : TaskCode<'TOverall, 'TResult3> =
        this.Bind2 (Async.StartAsTask computation, Async.StartAsTask computation2, continuation)

    member inline this.Bind3 (computation: Async<'TResult1>, computation2: Async<'TResult2>, computation3: Async<'TResult3>, continuation: ('TResult1 * 'TResult2 * 'TResult3 -> TaskCode<'TOverall, 'TResult4>)) : TaskCode<'TOverall, 'TResult4> =
        this.Bind3 (Async.StartAsTask computation, Async.StartAsTask computation2, Async.StartAsTask computation3, continuation)

    member inline this.Bind4 (computation: Async<'TResult1>, computation2: Async<'TResult2>, computation3: Async<'TResult3>, computation4: Async<'TResult4>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 -> TaskCode<'TOverall, 'TResult5>)) : TaskCode<'TOverall, 'TResult5> =
        this.Bind4 (Async.StartAsTask computation, Async.StartAsTask computation2, Async.StartAsTask computation3, Async.StartAsTask computation4, continuation)

    member inline this.Bind5 (computation: Async<'TResult1>, computation2: Async<'TResult2>, computation3: Async<'TResult3>, computation4: Async<'TResult4>, computation5: Async<'TResult5>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 -> TaskCode<'TOverall, 'TResult6>)) : TaskCode<'TOverall, 'TResult6> =
        this.Bind5 (Async.StartAsTask computation, Async.StartAsTask computation2, Async.StartAsTask computation3, Async.StartAsTask computation4, Async.StartAsTask computation5, continuation)

    member inline this.Bind6 (computation: Async<'TResult1>, computation2: Async<'TResult2>, computation3: Async<'TResult3>, computation4: Async<'TResult4>, computation5: Async<'TResult5>, computation6: Async<'TResult6>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 -> TaskCode<'TOverall, 'TResult7>)) : TaskCode<'TOverall, 'TResult7> =
        this.Bind6 (Async.StartAsTask computation, Async.StartAsTask computation2, Async.StartAsTask computation3, Async.StartAsTask computation4, Async.StartAsTask computation5, Async.StartAsTask computation6, continuation)

    member inline this.Bind7 (computation: Async<'TResult1>, computation2: Async<'TResult2>, computation3: Async<'TResult3>, computation4: Async<'TResult4>, computation5: Async<'TResult5>, computation6: Async<'TResult6>, computation7: Async<'TResult7>, continuation: ('TResult1 * 'TResult2 * 'TResult3 * 'TResult4 * 'TResult5 * 'TResult6 * 'TResult7 -> TaskCode<'TOverall, 'TResult8>)) : TaskCode<'TOverall, 'TResult8> =
        this.Bind7 (Async.StartAsTask computation, Async.StartAsTask computation2, Async.StartAsTask computation3, Async.StartAsTask computation4, Async.StartAsTask computation5, Async.StartAsTask computation6, Async.StartAsTask computation7, continuation)