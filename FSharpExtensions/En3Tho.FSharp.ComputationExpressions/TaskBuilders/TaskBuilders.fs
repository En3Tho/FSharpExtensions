[<AutoOpen>]
module En3Tho.FSharp.ComputationExpressions.Tasks.TaskBuilders

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.TaskBuilderExtensions")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitTaskBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitTaskBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitTaskBuilderExtensions.HighPriority")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitValueTaskBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitValueTaskBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitValueTaskBuilderExtensions.HighPriority")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.HighPriority")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskValueOptionBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskValueOptionBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskValueOptionBuilderExtensions.HighPriority")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskResultBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskResultBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskResultBuilderExtensions.HighPriority")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskEResultBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskEResultBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskEResultBuilderExtensions.HighPriority")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskExnResultBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskExnResultBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskExnResultBuilderExtensions.HighPriority")>]

do()

let unittask = UnitTaskBuilder()
let vtask = ValueTaskBuilder()
let unitvtask = UnitValueTaskBuilder()
let voptionvtask = ValueTaskValueOptionBuilder()
let resultvtask = ValueTaskResultBuilder()
let eresultvtask = ValueTaskEResultBuilder()
let exnresultvtask = ValueTaskExnResultBuilder()