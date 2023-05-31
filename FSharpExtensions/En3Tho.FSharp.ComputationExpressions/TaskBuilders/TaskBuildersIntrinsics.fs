namespace En3Tho.FSharp.ComputationExpressions.Tasks

open System.Threading.Tasks

[<Struct>]
type VerbatimNonGenericTask(task: Task) =
    member _.Value = task
    member _.GetAwaiter() = task.GetAwaiter()

[<Struct>]
type VerbatimTask<'T>(task: Task<'T>) =
    member _.Value = task
    member _.GetAwaiter() = task.GetAwaiter()

[<Struct>]
type VerbatimNonGenericValueTask(valueTask: ValueTask) =
    member _.Value = valueTask
    member _.GetAwaiter() = valueTask.GetAwaiter()

[<Struct>]
type VerbatimValueTask<'T>(valueTask: ValueTask<'T>) =
    member _.Value = valueTask
    member _.GetAwaiter() = valueTask.GetAwaiter()

[<AbstractClass; Sealed; AutoOpen>]
type TaskBuildersIntrinsics() =
    static member verb(value) = VerbatimTask(value)
    static member verb(value) = VerbatimValueTask(value)
    static member verb(value) = VerbatimNonGenericTask(value)
    static member verb(value) = VerbatimNonGenericValueTask(value)