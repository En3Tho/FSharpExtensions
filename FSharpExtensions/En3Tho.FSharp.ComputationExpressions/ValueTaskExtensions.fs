namespace En3Tho.FSharp.Extensions

open En3Tho.FSharp.ComputationExpressions.Tasks
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.LowPriority
open En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.HighPriority
open En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskExnResultBuilderExtensions.TaskLikeLowPriority
open En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskExnResultBuilderExtensions.TaskLikeHighPriority
open En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskExnResultBuilderExtensions.LowPriority

module ValueTask =
    let inline map ([<InlineIfLambda>] mapper) (job: ValueTask<'a>) =
        if job.IsCompleted then
            ValueTask<_>(result = mapper job.Result)
        else vtask {
            let! result = job
            return mapper result
        }

    let inline bind ([<InlineIfLambda>] mapper) (job: ValueTask<'a>) =
        if job.IsCompleted then
            mapper job.Result
        else vtask {
            let! result = job
            return! mapper result
        }

[<AutoOpen>]
module TaskExtensions =
    type Task with
        member this.AsResult() =
            if this.IsCompletedSuccessfully then ValueTask<_>(result = Ok())
            else
                evtask {
                    do! this
                }

    type Task<'a> with
        member this.AsResult() =
            if this.IsCompletedSuccessfully then ValueTask<_>(result = Ok this.Result)
            else
                evtask {
                    let! (v: 'a) = this
                    return v
                }

    type ValueTask with
        member this.AsResult() =
            if
                this.IsCompletedSuccessfully then ValueTask<_>(result = Ok())
            else
                evtask {
                    do! this
                }
    type ValueTask<'a> with
        member this.AsResult() =
            if
                this.IsCompletedSuccessfully then ValueTask<_>(result = Ok this.Result)
            else
                evtask {
                    return! this
                }