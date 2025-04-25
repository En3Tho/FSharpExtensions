namespace En3Tho.FSharp.Extensions

open En3Tho.FSharp.ComputationExpressions.Tasks
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.Low
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.High

open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionResultTask.TaskLikeLow
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionResultTask.Low
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Tasks.ExceptionResultTask.High

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
        member inline this.AsResult() =
            if this.IsCompletedSuccessfully then ValueTask<_>(result = Ok())
            else
                exnResultValueTask {
                    do! this
                    return ()
                }

    type Task<'a> with
        member inline this.AsResult() =
            if this.IsCompletedSuccessfully then ValueTask<_>(result = Ok this.Result)
            else
                exnResultValueTask {
                    let! (v: 'a) = this
                    return v
                }

    type ValueTask with
        member inline this.AsResult() =
            if
                this.IsCompletedSuccessfully then ValueTask<_>(result = Ok())
            else
                exnResultValueTask {
                    do! this
                    return ()
                }
    type ValueTask<'a> with
        member inline this.AsResult() =
            if
                this.IsCompletedSuccessfully then ValueTask<_>(result = Ok this.Result)
            else
                exnResultValueTask {
                    return! this
                }