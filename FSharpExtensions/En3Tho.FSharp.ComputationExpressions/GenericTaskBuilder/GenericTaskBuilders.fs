[<AutoOpen>]
module En3Tho.FSharp.ComputationExpressions.Tasks2.GenericTaskBuilders

open System.Diagnostics
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.HighPriority")>]
do()

let inline getState() = StateIntrinsic()

let vtask = ValueTaskBuilder()
let unitvtask = UnitValueTaskBuilder()
let unittask = UnitTaskBuilder()

let taskSeq = TaskSeq()

let syncCtxTask ctx = SyncContextTask(ctx)
let syncCtxValueTask ctx = SyncContextValueTask(ctx)

let etask = ExnResultTaskBuilder()
let evtask = ExnResultValueTaskBuilder()

[<AbstractClass; Sealed; AutoOpen>]
type ActivityBuilders() =

    static member activityTask(activity) = ActivityValueTaskBuilder(activity)

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