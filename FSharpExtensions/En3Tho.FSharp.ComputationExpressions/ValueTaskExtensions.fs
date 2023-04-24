namespace En3Tho.FSharp.Extensions

open En3Tho.FSharp.ComputationExpressions.Tasks
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.LowPriority
open En3Tho.FSharp.ComputationExpressions.Tasks.ValueTaskBuilderExtensions.HighPriority

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
                vtask {
                    try
                        do! this
                        return Ok()
                    with e ->
                        return Error e
                }

    type Task<'a> with
        member this.AsResult() =
            if this.IsCompletedSuccessfully then ValueTask<_>(result = Ok this.Result)

            else
                vtask {
                    try
                        let! result = this
                        return Ok result
                    with e ->
                        return Error e
                }

    type ValueTask with
        member this.AsResult() =
            if
                this.IsCompletedSuccessfully then ValueTask<_>(result = Ok())
            else
                vtask {
                    try
                        do! this
                        return Ok()
                    with e ->
                        return Error e
                }
    type ValueTask<'a> with
        member this.AsResult() =
            if
                this.IsCompletedSuccessfully then ValueTask<_>(result = Ok this.Result)
            else
                vtask {
                    try
                        let! result = this
                        return Ok result
                    with e ->
                        return Error e
                }