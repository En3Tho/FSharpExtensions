module Benchmarks.ActionSeq

type IAction<'T> =
    abstract Invoke: value: 'T -> unit

type PipelineElement<'T, 'TPipeline when 'TPipeline :> IAction<'T> and 'TPipeline: struct> = 'TPipeline

[<Struct; NoComparison; NoEquality>]
type ArrayIntake<'T, 'TNext when PipelineElement<'T, 'TNext>> =
    val mutable private next: 'TNext

    new (next) = {
        next = next
    }

    member this.Invoke(value: 'T[]) =
        for value in value do
            this.next.Invoke(value)

    interface IAction<'T[]> with
        member this.Invoke(value) = this.Invoke(value)

[<Struct; NoComparison; NoEquality>]
type FilterAction<'T, 'TNext when PipelineElement<'T, 'TNext>> =
    val mutable private next: 'TNext
    val mutable filter: 'T -> bool

    new (next, filter) = {
        next = next
        filter = filter
    }

    member this.Invoke(value: 'T) =
        if this.filter(value) then
            this.next.Invoke(value)

    interface IAction<'T> with
        member this.Invoke(value) = this.Invoke(value)

[<Struct; NoComparison; NoEquality>]
type MapAction<'T, 'TNext when PipelineElement<'T, 'TNext>> =
    val mutable private next: 'TNext
    val mutable mapper: 'T -> 'T

    new (next, mapper) = {
        next = next
        mapper = mapper
    }

    member this.Invoke(value: 'T) =
        this.next.Invoke(this.mapper(value))

    interface IAction<'T> with
        member this.Invoke(value) = this.Invoke(value)

[<Struct; NoComparison; NoEquality>]
type FoldAction<'T, 'TAcc> =
    val mutable folder: 'TAcc -> 'T -> 'TAcc
    val mutable acc: 'TAcc

    new (folder, acc) = {
        folder = folder
        acc = acc
    }

    member this.Invoke(value: 'T) =
        this.acc <- this.folder this.acc value

    member this.Result() = this.acc

    interface IAction<'T> with
        member this.Invoke(value) = this.Invoke(value)

[<Struct; NoComparison; NoEquality>]
type SkipAction<'T, 'TNext when PipelineElement<'T, 'TNext>> =
    val mutable private next: 'TNext
    val mutable skip: int

    new (next, skip) = {
        next = next
        skip = skip
    }

    member this.Invoke(value: 'T) =
        if this.skip > 0 then
            this.skip <- this.skip - 1
        else
            this.next.Invoke(value)

    interface IAction<'T> with
        member this.Invoke(value) = this.Invoke(value)

let filter filter next = FilterAction(next, filter)
let map mapper next = MapAction(next, mapper)
let fold acc folder = FoldAction(folder, acc)
let skip skip next = SkipAction(next, skip)
let fromArray next = ArrayIntake(next)