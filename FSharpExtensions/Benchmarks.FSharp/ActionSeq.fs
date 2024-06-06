module Benchmarks.ActionSeq

open System.Collections.Generic
open System.Runtime.CompilerServices

type IAction<'T> =
    abstract Invoke: value: 'T -> unit

type IActionResult<'T> =
    abstract Result: 'T

type PipelineElement<'T, 'TPipeline when 'TPipeline :> IAction<'T> and 'TPipeline: struct> = 'TPipeline
type PipelineResult<'T, 'TPipeline when 'TPipeline :> IActionResult<'T> and 'TPipeline: struct> = 'TPipeline

[<Struct; NoComparison; NoEquality>]
type EnumeratorIntake<'T, 'TEnumerator, 'TNext when 'TNext :> IAction<'T> and 'TNext: struct and 'TEnumerator :> IEnumerator<'T>> =
    val mutable next: 'TNext

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (next) = {
        next = next
    }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.Invoke(value: 'TEnumerator) =
        let mutable v = value
        while v.MoveNext() do
            this.next.Invoke(v.Current)

    interface IAction<'TEnumerator> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.Invoke(value) = this.Invoke(value)

[<Struct; NoComparison; NoEquality>]
type ArrayIntake<'T, 'TNext when PipelineElement<'T, 'TNext>> =
    val mutable next: 'TNext

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (next) = {
        next = next
    }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.Invoke(value: 'T[]) =
        for value in value do
            this.next.Invoke(value)

    interface IAction<'T[]> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.Invoke(value) = this.Invoke(value)

[<Struct; NoComparison; NoEquality>]
type ForkAction<'T, 'TNext, 'TNext2 when 'TNext :> IAction<'T> and 'TNext: struct and 'TNext2 :> IAction<'T> and 'TNext2: struct> =
    val mutable next: 'TNext
    val mutable next2: 'TNext2

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (next, next2) = {
        next = next
        next2 = next2
    }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.Invoke(value: 'T) =
        this.next.Invoke(value)
        this.next2.Invoke(value)

    interface IAction<'T> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.Invoke(value) = this.Invoke(value)

[<Struct; NoComparison; NoEquality>]
type FilterAction<'T, 'TNext when PipelineElement<'T, 'TNext>> =
    val mutable next: 'TNext
    val mutable filter: 'T -> bool

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (next, filter) = {
        next = next
        filter = filter
    }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.Invoke(value: 'T) =
        if this.filter(value) then
            this.next.Invoke(value)

    interface IAction<'T> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.Invoke(value) = this.Invoke(value)

[<Struct; NoComparison; NoEquality>]
type MapAction<'T, 'TNext when PipelineElement<'T, 'TNext>> =
    val mutable next: 'TNext
    val mutable mapper: 'T -> 'T

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (next, mapper) = {
        next = next
        mapper = mapper
    }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.Invoke(value: 'T) =
        this.next.Invoke(this.mapper(value))

    interface IAction<'T> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.Invoke(value) = this.Invoke(value)

[<Struct; NoComparison; NoEquality>]
type FoldAction<'T, 'TAcc> =
    val folder: OptimizedClosures.FSharpFunc<'TAcc, 'T, 'TAcc>
    val mutable acc: 'TAcc

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (folder, acc) = {
        folder = OptimizedClosures.FSharpFunc<_,_,_>.Adapt folder
        acc = acc
    }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.Invoke(value: 'T) =
        this.acc <- this.folder.Invoke(this.acc, value)

    member this.Result
        with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = this.acc

    interface IAction<'T> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.Invoke(value) = this.Invoke(value)

    interface IActionResult<'TAcc> with
        member this.Result
            with [<MethodImpl(MethodImplOptions.AggressiveInlining)>] get() = this.acc

[<Struct; NoComparison; NoEquality>]
type SkipAction<'T, 'TNext when PipelineElement<'T, 'TNext>> =
    val mutable next: 'TNext
    val mutable skip: int

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (next, skip) = {
        next = next
        skip = skip
    }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.Invoke(value: 'T) =
        if this.skip > 0 then
            this.skip <- this.skip - 1
        else
            this.next.Invoke(value)

    interface IAction<'T> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.Invoke(value) = this.Invoke(value)

[<Struct; NoComparison; NoEquality>]
type IterAction<'T> =
    val mutable next: 'T -> unit

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    new (next) = {
        next = next
    }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    member this.Invoke(value: 'T) =
        this.next(value)

    interface IAction<'T> with
        [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
        member this.Invoke(value) = this.Invoke(value)

let filter filter next = FilterAction(next, filter)
let map mapper next = MapAction(next, mapper)
let fold acc folder = FoldAction(folder, acc)
let skip skip next = SkipAction(next, skip)
let iter thunk = IterAction(thunk)
let fromArray next = ArrayIntake(next)
let fromEnumerator next = EnumeratorIntake next
let inline getResult (result: PipelineResult<_,_>) = result.Result

// [<AutoOpen>]
// module CodeDefinition =
//
//     // type ResumableCode<'Data,'T> = delegate of byref<ResumableStateMachine<'Data>> -> boo
//     type ActionSeqActionCode<'T> = delegate of 'T * ('T -> unit) -> unit
//     type ActionSeqFuncCode<'T, 'U> = delegate of 'T * ('U -> unit) -> unit
//     type ActionSeqActionFinalCode<'T> = delegate of 'T -> unit
//     type ActionSeqFuncFinalCode<'T, 'U> = delegate of 'T -> 'U
//
//
// type ActionSeqBuilder() =
//     member inline _.Yield([<InlineIfLambda>] action: ActionSeqActionCode<_>) = ActionSeqActionCode(fun value next -> action.Invoke(value, next))
//     member inline _.Yield([<InlineIfLambda>] action: ActionSeqFuncCode<_,_>) = ActionSeqFuncCode(fun value next -> action.Invoke(value, next))
//     member inline _.Zero(): ActionSeqActionCode<_> = ActionSeqActionCode(fun _ _ -> ())
//     member inline _.Delay(code) = code()
//
//     // termination
//     member inline _.Combine(first: ActionSeqActionCode<_>, second: ActionSeqActionFinalCode<_>): ActionSeqActionFinalCode<_> =
//         ActionSeqActionFinalCode(fun value ->
//             let next2 = fun value -> second.Invoke(value)
//             first.Invoke(value, next2))
//
//     member inline _.Combine(first: ActionSeqFuncCode<_,_>, second: ActionSeqActionFinalCode<_>): ActionSeqActionFinalCode<_> =
//         ActionSeqActionFinalCode(fun value ->
//             let next2 = fun value -> second.Invoke(value)
//             first.Invoke(value, next2))
//
//     member inline _.Combine(first: ActionSeqActionCode<_>, second: ActionSeqFuncFinalCode<_,_>): ActionSeqFuncFinalCode<_,_> =
//         ActionSeqFuncFinalCode(fun value ->
//             let next2 = fun value -> second.Invoke(value)
//             first.Invoke(value, next2))
//
//     member inline _.Combine(first: ActionSeqFuncCode<_,_>, second: ActionSeqFuncFinalCode<_,_>): ActionSeqFuncFinalCode<_,_> =
//         ActionSeqFuncFinalCode(fun value ->
//             let next2 = fun value -> second.Invoke(value)
//             first.Invoke(value, next2))
//
//     // combining actions and funcs
//     member inline _.Combine(first: ActionSeqActionCode<_>, second: ActionSeqActionCode<_>): ActionSeqActionCode<_> =
//         ActionSeqActionCode(fun value next ->
//             let next2 = fun value -> second.Invoke(value, next)
//             first.Invoke(value, next2))
//
//     member inline _.Combine(first: ActionSeqFuncCode<'a,'b>, second: ActionSeqActionCode<'b>): ActionSeqFuncCode<'a,'b> =
//         ActionSeqFuncCode(fun value next ->
//             let next2 = fun value -> second.Invoke(value, next)
//             first.Invoke(value, next2))
//
//     member inline _.Run(code: ActionSeqFuncFinalCode<_,_>) =
//         fun value -> code.Invoke(value)
//
//     member inline _.Run(code: ActionSeqActionFinalCode<_>) =
//         fun value -> code.Invoke(value)
//
// module Actions =
//     let inline filter ([<InlineIfLambda>] filter) =
//         ActionSeqActionCode(fun value ([<InlineIfLambda>] next) ->
//             if filter value then next value else ())
//
//     let inline map ([<InlineIfLambda>] map) =
//         ActionSeqFuncCode(fun value ([<InlineIfLambda>] next) ->
//             next (map value))
//
//     let inline mapFinal ([<InlineIfLambda>] map) =
//         ActionSeqFuncFinalCode(fun value ->
//             map value)
//
//     let inline skip amount =
//         let mutable current = 0
//         ActionSeqActionCode(fun value ([<InlineIfLambda>] next) ->
//             if current = amount then
//                 next value
//             else
//                 current <- current + 1)
//
//     let builder = ActionSeqBuilder()
//
//
//     let test() = builder {
//         filter (fun x -> x > 5)
//         mapFinal (fun (x: int) -> x + 1 |> ignore)
//     }
//
// // fun f next -> f(next)
// //