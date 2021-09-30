[<AutoOpen>]
module En3Tho.FSharp.Extensions.Extensions

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