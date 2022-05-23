[<AutoOpen>]
module En3Tho.FSharp.Extensions.Extensions

open System
open System.Threading.Tasks
open En3Tho.FSharp.ComputationExpressions.Tasks

let private unitTask = Task.FromResult()

type Task with
    static member inline RunSynchronously (task: Task) =
#if NETSTANDARD2_0
        if task.IsCompleted && not task.IsCanceled && not task.IsFaulted then () else
#else
        if task.IsCompletedSuccessfully then () else
#endif
        task.ConfigureAwait(false).GetAwaiter().GetResult()

    static member inline RunSynchronously (task: Task<'a>) =
#if NETSTANDARD2_0
        if task.IsCompleted && not task.IsCanceled && not task.IsFaulted then task.Result else
#else
        if task.IsCompletedSuccessfully then task.Result else
#endif
        task.ConfigureAwait(false).GetAwaiter().GetResult()

    static member CompletedUnitTask = unitTask

    member this.AsResult() =
#if NETSTANDARD2_0
        if this.IsCompleted && not this.IsCanceled && not this.IsFaulted then ValueTask<_>(result = Ok())
#else
        if this.IsCompletedSuccessfully then ValueTask<_>(result = Ok())
#endif
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
#if NETSTANDARD2_0
        if this.IsCompleted && not this.IsCanceled && not this.IsFaulted then ValueTask<_>(result = Ok this.Result)
#else
        if this.IsCompletedSuccessfully then ValueTask<_>(result = Ok this.Result)
#endif

        else
            vtask {
                try
                    let! result = this
                    return Ok result
                with e ->
                    return Error e
            }

type ValueTask with
    static member inline RunSynchronously (task: ValueTask) =
        if task.IsCompletedSuccessfully then () else
        task.ConfigureAwait(false).GetAwaiter().GetResult()

    static member inline RunSynchronously (task: ValueTask<'a>) =
        if task.IsCompletedSuccessfully then task.Result else
        task.ConfigureAwait(false).GetAwaiter().GetResult()

    static member inline FromTask value = ValueTask<_>(task = value)
    static member inline FromTask value = ValueTask(task = value)

    static member CompletedUnitTask = ValueTask<unit>()

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

type Async<'a> with
    static member inline AwaitValueTask (valueTask: ValueTask) =
        if valueTask.IsCompletedSuccessfully then async.Zero()
        else valueTask.AsTask() |> Async.AwaitTask

    static member inline AwaitValueTask (valueTask: ValueTask<_>) =
        if valueTask.IsCompletedSuccessfully then valueTask.Result |> Async.ofObj
        else valueTask.AsTask() |> Async.AwaitTask

    member this.AsResult() =
        async {
            try
                let! result = this
                return Ok result
            with e ->
                return Error e
        }

type Exception with
    member this.IsOrContains<'a when 'a :> exn>() =
        match this with
        | :? 'a as result -> ValueSome result
        | :? AggregateException as exn ->
            let mutable result = ValueNone
            let enum = exn.InnerExceptions.GetEnumerator()
            while result.IsNone && enum.MoveNext() do
                result <- enum.Current.IsOrContains<'a>()
            match result with
            | ValueSome _ -> result
            | _ -> exn.InnerException.IsOrContains<'a>()
        | _ ->
            match this.InnerException with
            | null -> ValueNone
            | exn -> exn.IsOrContains<'a>()

// Move this to functions when byrefs are available as generics
type Span<'a> with
    member this.SliceForward value =
        if uint value >= uint this.Length then
            Span<'a>()
        else
            this.Slice(value, this.Length - value)

type ReadOnlySpan<'a> with
    member this.SliceForward value =
        if uint value >= uint this.Length then
            ReadOnlySpan<'a>()
        else
            this.Slice(value, this.Length - value)