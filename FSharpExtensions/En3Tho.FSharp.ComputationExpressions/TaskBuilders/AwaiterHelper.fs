namespace En3Tho.FSharp.ComputationExpressions.Tasks

open System.Diagnostics
open System.Runtime.InteropServices
open System.Threading.Tasks

type [<Struct>] ValueArray2<'a> =
    [<DefaultValue(false)>]
    val mutable Item1: 'a

    [<DefaultValue(false)>]
    val mutable Item2: 'a

    [<DefaultValue(false)>]
    val mutable Item3: 'a

type [<Struct>] WhenAllHelper2 =

    [<DefaultValue>]
    val mutable private taskCount: int

    [<DefaultValue>]
    val mutable private tasks: ValueArray2<Task>

    member private this.PushInternal(task: Task) =
        Debug.Assert(this.taskCount + 1 <= 2)

        match this.taskCount with
        | 0 -> this.tasks.Item1 <- task
        | 1 -> this.tasks.Item2 <- task
        | _ -> ()

        this.taskCount <- this.taskCount + 1

    member this.Add(task: Task) =
        if not task.IsCompleted then
            this.PushInternal(task)

    member this.Add(task: Task<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.Add(task: ValueTask) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask())

    member this.Add(task: ValueTask<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask() :> Task)

    member this.Add(task: Async<'a>) =
        let task = Async.StartAsTask task
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.WhenAll() =
        match this.taskCount with
        | 0 ->
            Task.CompletedTask
        | 1 ->
            this.tasks.Item1
        | taskCount ->
            let span = MemoryMarshal.CreateSpan(&this.tasks.Item1, taskCount)
            Task.WhenAll(span.ToArray())

type [<Struct>] ValueArray3<'a> =
    [<DefaultValue(false)>]
    val mutable Item1: 'a

    [<DefaultValue(false)>]
    val mutable Item2: 'a

    [<DefaultValue(false)>]
    val mutable Item3: 'a

type [<Struct>] WhenAllHelper3 =

    [<DefaultValue>]
    val mutable private taskCount: int

    [<DefaultValue>]
    val mutable private tasks: ValueArray3<Task>

    member private this.PushInternal(task: Task) =
        Debug.Assert(this.taskCount + 1 <= 3)

        match this.taskCount with
        | 0 -> this.tasks.Item1 <- task
        | 1 -> this.tasks.Item2 <- task
        | 2 -> this.tasks.Item3 <- task
        | _ -> ()

        this.taskCount <- this.taskCount + 1

    member this.Add(task: Task) =
        if not task.IsCompleted then
            this.PushInternal(task)

    member this.Add(task: Task<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.Add(task: ValueTask) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask())

    member this.Add(task: ValueTask<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask() :> Task)

    member this.Add(task: Async<'a>) =
        let task = Async.StartAsTask task
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.WhenAll() =
        match this.taskCount with
        | 0 ->
            Task.CompletedTask
        | 1 ->
            this.tasks.Item1
        | taskCount ->
            let span = MemoryMarshal.CreateSpan(&this.tasks.Item1, taskCount)
            Task.WhenAll(span.ToArray())

type [<Struct>] ValueArray4<'a> =
    [<DefaultValue(false)>]
    val mutable Item1: 'a

    [<DefaultValue(false)>]
    val mutable Item2: 'a

    [<DefaultValue(false)>]
    val mutable Item3: 'a

    [<DefaultValue(false)>]
    val mutable Item4: 'a

type [<Struct>] WhenAllHelper4 =

    [<DefaultValue>]
    val mutable private taskCount: int

    [<DefaultValue>]
    val mutable private tasks: ValueArray4<Task>

    member private this.PushInternal(task: Task) =
        Debug.Assert(this.taskCount + 1 <= 4)

        match this.taskCount with
        | 0 -> this.tasks.Item1 <- task
        | 1 -> this.tasks.Item2 <- task
        | 2 -> this.tasks.Item3 <- task
        | 3 -> this.tasks.Item4 <- task
        | _ -> ()

        this.taskCount <- this.taskCount + 1

    member this.Add(task: Task) =
        if not task.IsCompleted then
            this.PushInternal(task)

    member this.Add(task: Task<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.Add(task: ValueTask) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask())

    member this.Add(task: ValueTask<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask() :> Task)

    member this.Add(task: Async<'a>) =
        let task = Async.StartAsTask task
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.WhenAll() =
        match this.taskCount with
        | 0 ->
            Task.CompletedTask
        | 1 ->
            this.tasks.Item1
        | taskCount ->
            let span = MemoryMarshal.CreateSpan(&this.tasks.Item1, taskCount)
            Task.WhenAll(span.ToArray())

type [<Struct>] ValueArray5<'a> =
    [<DefaultValue(false)>]
    val mutable Item1: 'a

    [<DefaultValue(false)>]
    val mutable Item2: 'a

    [<DefaultValue(false)>]
    val mutable Item3: 'a

    [<DefaultValue(false)>]
    val mutable Item4: 'a

    [<DefaultValue(false)>]
    val mutable Item5: 'a

type [<Struct>] WhenAllHelper5 =

    [<DefaultValue>]
    val mutable private taskCount: int

    [<DefaultValue>]
    val mutable private tasks: ValueArray5<Task>

    member private this.PushInternal(task: Task) =
        Debug.Assert(this.taskCount + 1 <= 5)

        match this.taskCount with
        | 0 -> this.tasks.Item1 <- task
        | 1 -> this.tasks.Item2 <- task
        | 2 -> this.tasks.Item3 <- task
        | 3 -> this.tasks.Item4 <- task
        | 4 -> this.tasks.Item5 <- task
        | _ -> ()

        this.taskCount <- this.taskCount + 1

    member this.Add(task: Task) =
        if not task.IsCompleted then
            this.PushInternal(task)

    member this.Add(task: Task<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.Add(task: ValueTask) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask())

    member this.Add(task: ValueTask<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask() :> Task)

    member this.Add(task: Async<'a>) =
        let task = Async.StartAsTask task
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.WhenAll() =
        match this.taskCount with
        | 0 ->
            Task.CompletedTask
        | 1 ->
            this.tasks.Item1
        | taskCount ->
            let span = MemoryMarshal.CreateSpan(&this.tasks.Item1, taskCount)
            Task.WhenAll(span.ToArray())

type [<Struct>] ValueArray6<'a> =
    [<DefaultValue(false)>]
    val mutable Item1: 'a

    [<DefaultValue(false)>]
    val mutable Item2: 'a

    [<DefaultValue(false)>]
    val mutable Item3: 'a

    [<DefaultValue(false)>]
    val mutable Item4: 'a

    [<DefaultValue(false)>]
    val mutable Item5: 'a

    [<DefaultValue(false)>]
    val mutable Item6: 'a

type [<Struct>] WhenAllHelper6 =

    [<DefaultValue>]
    val mutable private taskCount: int

    [<DefaultValue>]
    val mutable private tasks: ValueArray6<Task>

    member private this.PushInternal(task: Task) =
        Debug.Assert(this.taskCount + 1 <= 6)

        match this.taskCount with
        | 0 -> this.tasks.Item1 <- task
        | 1 -> this.tasks.Item2 <- task
        | 2 -> this.tasks.Item3 <- task
        | 3 -> this.tasks.Item4 <- task
        | 4 -> this.tasks.Item5 <- task
        | 5 -> this.tasks.Item6 <- task
        | _ -> ()

        this.taskCount <- this.taskCount + 1

    member this.Add(task: Task) =
        if not task.IsCompleted then
            this.PushInternal(task)

    member this.Add(task: Task<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.Add(task: ValueTask) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask())

    member this.Add(task: ValueTask<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask() :> Task)

    member this.Add(task: Async<'a>) =
        let task = Async.StartAsTask task
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.WhenAll() =
        match this.taskCount with
        | 0 ->
            Task.CompletedTask
        | 1 ->
            this.tasks.Item1
        | taskCount ->
            let span = MemoryMarshal.CreateSpan(&this.tasks.Item1, taskCount)
            Task.WhenAll(span.ToArray())

type [<Struct>] ValueArray7<'a> =
    [<DefaultValue(false)>]
    val mutable Item1: 'a

    [<DefaultValue(false)>]
    val mutable Item2: 'a

    [<DefaultValue(false)>]
    val mutable Item3: 'a

    [<DefaultValue(false)>]
    val mutable Item4: 'a

    [<DefaultValue(false)>]
    val mutable Item5: 'a

    [<DefaultValue(false)>]
    val mutable Item6: 'a

    [<DefaultValue(false)>]
    val mutable Item7: 'a

type [<Struct>] WhenAllHelper7 =

    [<DefaultValue>]
    val mutable private taskCount: int

    [<DefaultValue>]
    val mutable private tasks: ValueArray7<Task>

    member private this.PushInternal(task: Task) =
        Debug.Assert(this.taskCount + 1 <= 7)

        match this.taskCount with
        | 0 -> this.tasks.Item1 <- task
        | 1 -> this.tasks.Item2 <- task
        | 2 -> this.tasks.Item3 <- task
        | 3 -> this.tasks.Item4 <- task
        | 4 -> this.tasks.Item5 <- task
        | 5 -> this.tasks.Item6 <- task
        | 6 -> this.tasks.Item7 <- task
        | _ -> ()

        this.taskCount <- this.taskCount + 1

    member this.Add(task: Task) =
        if not task.IsCompleted then
            this.PushInternal(task)

    member this.Add(task: Task<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.Add(task: ValueTask) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask())

    member this.Add(task: ValueTask<'a>) =
        if not task.IsCompleted then
            this.PushInternal(task.AsTask() :> Task)

    member this.Add(task: Async<'a>) =
        let task = Async.StartAsTask task
        if not task.IsCompleted then
            this.PushInternal(task :> Task)

    member this.WhenAll() =
        match this.taskCount with
        | 0 ->
            Task.CompletedTask
        | 1 ->
            this.tasks.Item1
        | taskCount ->
            let span = MemoryMarshal.CreateSpan(&this.tasks.Item1, taskCount)
            Task.WhenAll(span.ToArray())