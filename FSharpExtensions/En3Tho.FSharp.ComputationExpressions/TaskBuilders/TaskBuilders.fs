[<AutoOpen>]
module En3Tho.FSharp.ComputationExpressions.Tasks.TaskBuilders

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitTaskBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitTaskBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitTaskBuilderExtensions.HighPriority")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitValueTaskBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitValueTaskBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.UnitValueTaskBuilderExtensions.HighPriority")>]

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.HighPriority")>]

#if VALUE_OPTION_BUILDER

[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskValueOptionBuilderExtensions.LowPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskValueOptionBuilderExtensions.MediumPriority")>]
[<assembly: AutoOpen("En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskValueOptionBuilderExtensions.HighPriority")>]

#endif

do()

let unittask = UnitTaskBuilder()
let vtask = ValueTaskBuilder()
let unitvtask = UnitValueTaskBuilder()

#if VALUE_OPTION_BUILDER

let voptionvtask = ValueTaskValueOptionBuilder()

#endif