[<AutoOpen>]
module En3Tho.FSharp.Extensions.Extensions

open System
open System.Threading.Tasks

type Task with
    static member inline RunSynchronously (task: Task) =
        if task.IsCompletedSuccessfully then () else
        task.ConfigureAwait(false).GetAwaiter().GetResult()
    static member inline RunSynchronously (task: Task<'a>) =
        if task.IsCompletedSuccessfully then task.Result else
        task.ConfigureAwait(false).GetAwaiter().GetResult()

type ValueTask with
    static member inline RunSynchronously (task: ValueTask) =
        if task.IsCompletedSuccessfully then () else
        task.ConfigureAwait(false).GetAwaiter().GetResult()
    static member inline RunSynchronously (task: ValueTask<'a>) =
        if task.IsCompletedSuccessfully then task.Result else
        task.ConfigureAwait(false).GetAwaiter().GetResult()

type Async<'a> with
    static member inline AwaitValueTask (valueTask: ValueTask) =
        if valueTask.IsCompletedSuccessfully then async.Zero()
        else valueTask.AsTask() |> Async.AwaitTask

    static member inline AwaitValueTask (valueTask: ValueTask<_>) =
        if valueTask.IsCompletedSuccessfully then valueTask.Result |> Async.ofObj
        else valueTask.AsTask() |> Async.AwaitTask

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

type Span<'a> with
    member this.SliceForward value =
        if value >= this.Length then
            Span<'a>()
        else
            this.Slice(value, this.Length - value)

type ReadOnlySpan<'a> with
    member this.SliceForward value =
        if value >= this.Length then
            ReadOnlySpan<'a>()
        else
            this.Slice(value, this.Length - value)