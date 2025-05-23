﻿[<AutoOpen>]
module En3Tho.FSharp.ComputationExpressions.Tasks.Tasks

open System.Diagnostics
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.CancellableTask
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionTask
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.LazyTask
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.Native
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.AsyncEnumerable
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ActivityTask
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.RepeatableTask
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.SemaphoreSlimTask
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.SynchronizationContextTask
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ResultTask
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionResultTask
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ValueOptionTask

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.Low")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.High")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ResultTask.TaskLikeLow")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ResultTask.TaskLikeHigh")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ResultTask.Low")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ResultTask.High")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionResultTask.TaskLikeLow")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionResultTask.TaskLikeHigh")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionResultTask.Low")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionResultTask.High")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionTask.Low")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionTask.High")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ValueOptionTask.TaskLikeLow")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ValueOptionTask.TaskLikeHighOption")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ValueOptionTask.TaskLikeHighValueOption")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ValueOptionTask.Low")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ValueOptionTask.High")>]
do()

let inline getState() = StateIntrinsic()

// expected warning for builders, should be only respected when actual state machine is not generated
#nowarn "3511"

let vtask = ValueTaskBuilder()
let uvtask = UnitValueTaskBuilder()
// it is safe to replace default task builder to get native IAsyncEnumerable consumption support
// but there is a considerable compiler perf drop using all these tricky ce things so let's no do that
let task2 = TaskBuilder()
let utask = UnitTaskBuilder()

let taskSeq = TaskSeq()

let synchronizationContextTask ctx = SynchronizationContextTaskBuilder(ctx)
let synchronizationContextValueTask ctx = SynchronizationContextValueTaskBuilder(ctx)

let exnResultTask = ExceptionResultTaskBuilder()
let exnResultValueTask = ExceptionResultValueTaskBuilder()

let exnTask = ExceptionTaskBuilder()
let exnValueTask = ExceptionValueTaskBuilder()

let etask = ExceptionTaskBuilder()
let evtask = ExceptionValueTaskBuilder()

let resultTask = ResultTaskBuilder()
let resultValueTask = ResultValueTaskBuilder()

let voptionTask = ValueOptionTaskBuilder()
let voptionValueTask = ValueOptionValueTaskBuilder()

let cancellableTask cancellationToken = CancellableTaskBuilder(cancellationToken)
let cancellableValueTask cancellationToken = CancellableValueTaskBuilder(cancellationToken)

let semaphoreSlimTask semaphore = SemaphoreSlimTaskBuilder(semaphore)
let semaphoreSlimValueTask semaphore = SemaphoreSlimValueTaskBuilder(semaphore)

let lazyTask = LazyTaskBuilder()
let lazyUnitTask = LazyUnitTaskBuilder()

// let repeatableTask = RepeatableTaskBuilder()
// let repeatableUnitTask = RepeatableUnitTaskBuilder()

[<AbstractClass; Sealed; AutoOpen>]
type ActivityBuilders() =

    static member activityTask(activity) = ActivityTaskBuilder(activity)

    static member activityTask(
        source: ActivitySource,
        [<Optional; DefaultParameterValue(ActivityKind.Internal)>] kind: ActivityKind,
        [<CallerMemberName; Optional; DefaultParameterValue("")>] activityName: string) =

        let activity = source.StartActivity(activityName, kind)
        activityTask(activity)

    static member activityTask(
        kind: ActivityKind,
        [<CallerMemberName; Optional; DefaultParameterValue("")>] activityName: string) =

        let activity =
            match Activity.Current with
            | null -> null
            | activity ->
                activity.Source.StartActivity(activityName, kind)

        activityTask(activity)

    static member activityTask(
        [<CallerMemberName; Optional; DefaultParameterValue("")>] activityName: string) =
        activityTask(ActivityKind.Internal, activityName)

    static member activityValueTask(activity) = ActivityValueTaskBuilder(activity)

    static member activityValueTask(
        source: ActivitySource,
        [<Optional; DefaultParameterValue(ActivityKind.Internal)>] kind: ActivityKind,
        [<CallerMemberName; Optional; DefaultParameterValue("")>] activityName: string) =

        let activity = source.StartActivity(activityName, kind)
        activityTask(activity)

    static member activityValueTask(
        kind: ActivityKind,
        [<CallerMemberName; Optional; DefaultParameterValue("")>] activityName: string) =

        let activity =
            match Activity.Current with
            | null -> null
            | activity ->
                activity.Source.StartActivity(activityName, kind)

        activityTask(activity)

    static member activityValueTask(
        [<CallerMemberName; Optional; DefaultParameterValue("")>] activityName: string) =
        activityTask(ActivityKind.Internal, activityName)