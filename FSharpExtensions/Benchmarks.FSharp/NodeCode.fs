module Benchmarks.NodeCode

open System.Diagnostics
open Benchmarks

[<NoEquality;NoComparison>]
type NodeCode<'T> = Node of Async<'T>

[<Sealed>]
type NodeCodeBuilder() =

    static let zero = Node(async.Zero())

    [<DebuggerHidden;DebuggerStepThrough>]
    member _.Zero() : NodeCode<unit> = zero

    [<DebuggerHidden;DebuggerStepThrough>]
    member _.Delay(f: unit -> NodeCode<'T>) =
        Node(async.Delay(fun () -> match f() with Node(p) -> p))

    [<DebuggerHidden;DebuggerStepThrough>]
    member _.Return(value) = Node(async.Return(value))

    [<DebuggerHidden;DebuggerStepThrough>]
    member _.ReturnFrom(computation: NodeCode<_>) = computation

    [<DebuggerHidden;DebuggerStepThrough>]
    member _.Bind(Node(p): NodeCode<'a>, binder: 'a -> NodeCode<'b>) : NodeCode<'b> =
        Node(async.Bind(p, fun x -> match binder x with Node p -> p))

    [<DebuggerHidden;DebuggerStepThrough>]
    member _.TryWith(Node(p): NodeCode<'T>, binder: exn -> NodeCode<'T>) : NodeCode<'T> =
        Node(async.TryWith(p, fun ex -> match binder ex with Node p -> p))

    [<DebuggerHidden;DebuggerStepThrough>]
    member _.TryFinally(Node(p): NodeCode<'T>, binder: unit -> unit) : NodeCode<'T> =
        Node(async.TryFinally(p, binder))

    [<DebuggerHidden;DebuggerStepThrough>]
    member _.For(xs: 'T seq, binder: 'T -> NodeCode<unit>) : NodeCode<unit> =
        Node(async.For(xs, fun x -> match binder x with Node p -> p))

    [<DebuggerHidden;DebuggerStepThrough>]
    member _.Combine(Node(p1): NodeCode<unit>, Node(p2): NodeCode<'T>) : NodeCode<'T> =
        Node(async.Combine(p1, p2))

let node = NodeCodeBuilder()

open BenchmarkDotNet.Attributes

[<MemoryDiagnoser>]
[<MarkdownExporterAttribute.GitHub>]
type ReturnBenchmark() =
    let value = 1

    let directReturned = node.Return(value)
    let ceReturned = node { return value }

    [<Benchmark>]
    member _.DirectReturn() =
        node.Return(value)

    [<Benchmark>]
    member _.CompExprReturn() =
        node { return value }

    [<Benchmark>]
    member _.AwaitDirectReturned() =
        match directReturned with
        | Node computation ->
        Async.RunSynchronously(computation) |> ignore

    [<Benchmark>]
    member _.AwaitCEReturned() =
        match ceReturned with
        | Node computation ->
        Async.RunSynchronously(computation) |> ignore