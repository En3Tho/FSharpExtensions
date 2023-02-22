namespace En3Tho.FSharp.ComputationExpressions.Tasks

open System.Threading.Tasks

[<Struct>]
type VerbatimTaskResult<'T, 'U>(task: Task<Result<'T, 'U>>) =
    member _.Value = task

[<Struct>]
type VerbatimValueTaskResult<'T, 'U>(valueTask: ValueTask<Result<'T, 'U>>) =
    member _.Value = valueTask

[<Struct>]
type VerbatimResult<'T, 'U>(result: Result<'T, 'U>) =
    member _.Value = result

[<AbstractClass; Sealed; AutoOpen>]
type TaskBuildersIntrinsics() =
    static member verb(value) = VerbatimTaskResult(value)
    static member verb(value) = VerbatimValueTaskResult(value)
    static member verb(value) = VerbatimResult(value)