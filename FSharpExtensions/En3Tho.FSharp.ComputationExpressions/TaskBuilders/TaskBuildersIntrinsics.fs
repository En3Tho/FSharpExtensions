namespace En3Tho.FSharp.ComputationExpressions.Tasks

open System.Threading.Tasks

[<Struct>]
type VerbatimNonGenericTask(task: Task) =
    member _.Value = task

[<Struct>]
type VerbatimTask<'T>(task: Task<'T>) =
    member _.Value = task

[<Struct>]
type VerbatimNonGenericValueTask(valueTask: ValueTask) =
    member _.Value = valueTask

[<Struct>]
type VerbatimValueTask<'T>(valueTask: ValueTask<'T>) =
    member _.Value = valueTask

[<AbstractClass; Sealed; AutoOpen>]
type TaskBuildersIntrinsics() =
    static member verb(value) = VerbatimTask(value)
    static member verb(value) = VerbatimValueTask(value)
    static member verb(value) = VerbatimNonGenericTask(value)
    static member verb(value) = VerbatimNonGenericValueTask(value)