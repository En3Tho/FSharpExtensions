// Push Stream from mrange:
// https://gist.github.com/mrange/fbefd946dba6725a0b727b7d3fd81d6f

module Benchmarks.FSharp.Lib.PushStream

open System
open BenchmarkDotNet.Attributes

// 'T PushStream is an alternative syntax for PushStream<'T>
type 'T PushStream = ('T -> bool) -> bool


module PushStream =
    // Generates a range of ints in b..e
    //  Note the use of [<InlineIfLambda>] to inline the receiver function r
    let inline ofRange b e : int PushStream =
        fun ([<InlineIfLambda>] r) ->
            // This easy to implement in that we loop over the range b..e and
            //  call the receiver function r until either it returns false
            //  or we reach the end of the range
            //  Thanks to InlineIfLambda r should be inlined
            let mutable i = b

            while i <= e && r i do
                i <- i + 1

            i > e

    // Filters a PushStream using a filter function
    //  Note the use of [<InlineIfLambda>] to inline both the filter function f and the PushStream function ps
    let inline filter ([<InlineIfLambda>] f) ([<InlineIfLambda>] ps: _ PushStream) : _ PushStream =
        fun ([<InlineIfLambda>] r) ->
            // ps is the previous push stream which we invoke with our receiver lambda
            //  Our receiver lambda checks if each received value passes filter function f
            //  If it does we pass the value to r, otherwise we return true to continue
            //  f, ps and r are lambdas that should be inlined due to InlineIfLambda
            ps (fun v -> if f v then r v else true)

    // Maps a PushStream using a mapping function
    let inline map ([<InlineIfLambda>] f) ([<InlineIfLambda>] ps: _ PushStream) : _ PushStream =
        fun ([<InlineIfLambda>] r) ->
            // ps is the previous push stream which we invoke with our receiver lambda
            //  Our receiver lambda maps each received value with map function f and
            //  pass the mapped value to r
            //  If it does we pass the value to r, otherwise we return true to continue
            //  f, ps and r are lambdas that should be inlined due to InlineIfLambda
            ps (fun v -> r (f v))

    // Folds a PushStream using a folder function f and an initial value z
    let inline fold ([<InlineIfLambda>] f) z ([<InlineIfLambda>] ps: _ PushStream) =
        let mutable s = z
        // ps is the previous push stream which we invoke with our receiver lambda
        //  Our receiver lambda folds the state and value with folder function f
        //  Returns true to continue looping
        //  f and ps are lambdas that should be inlined due to InlineIfLambda
        //  This also means that s should not need to be a ref cell which avoids
        //  some memory pressure
        ps
            (fun v ->
                s <- f s v
                true)
        |> ignore

        s

    // It turns out that if we pipe using |> the F# compiler don't inline
    //  the lambdas as we like it to.
    //  So define a more restrictive version of |> that applies function f
    //  to a function v
    //  As both f and v are restricted to lambas we can apply InlineIfLambda
    let inline (|>>) ([<InlineIfLambda>] v: _ -> _) ([<InlineIfLambda>] f: _ -> _) = f v

open PushStream

[<MemoryDiagnoser; DisassemblyDiagnoser>]
type Benchmark() =

    [<Benchmark>]
    member x.PushStreamWithPipe() =
        ofRange 0 10000
        |> map ((+) 1)
        |> filter (fun v -> (v &&& 1) = 0)
        |> map int64
        |> fold (+) 0L

    member x.PushStreamWithComposition() =
        10000
        |> (ofRange 0
        >> map ((+) 1)
        >> filter (fun v -> (v &&& 1) = 0)
        >> map int64
        >> fold (+) 0L)

    [<Benchmark>]
    member x.PushStreamWithCustomOperator() =
        ofRange 0 10000
        |>> map ((+) 1)
        |>> filter (fun v -> (v &&& 1) = 0)
        |>> map int64
        |>> fold (+) 0L

open En3Tho.FSharp.Extensions.Experimental.PipeAndCompositionOperatorEx
[<MemoryDiagnoser; DisassemblyDiagnoser>]
type Benchmark2() =

//    [<Benchmark>]
//    member x.PushStreamWithExperimentalPipe() =
//        ofRange 0 10000
//        |> map ((+) 1)
//        |> filter (fun v -> (v &&& 1) = 0)
//        |> map int64
//        |> fold (+) 0L

    [<Benchmark>]
    member x.PushStreamWithExperimentalComposition() =
        10000
        |> (ofRange 0
        >> map ((+) 1)
        >> filter (fun v -> (v &&& 1) = 0)
        >> map int64
        >> fold (+) 0L)

module InlinePipe =

    // works
    let inline (|>) ([<InlineIfLambda>] x: 'a -> 'b) ([<InlineIfLambda>] f) = f x
    let inline (>>) ([<InlineIfLambda>] x) ([<InlineIfLambda>] f) = fun ([<InlineIfLambda>] v: 'a -> 'b) -> f (x v)
//    let inline (|>) ([<InlineIfLambda>] x: 'a -> 'b) = fun ([<InlineIfLambda>] f) -> f x
//    let inline (|>) ([<InlineIfLambda>] x: 'a when 'a :> FSharpFunc<_,_>) = fun ([<InlineIfLambda>] f) -> f x
//    let inline (|>) ([<InlineIfLambda>] x: #FSharpFunc<_,_>) = fun ([<InlineIfLambda>] f) -> f x

    // doesn't work
//    let inline (|>) x f = f x
//    let (|>) = (fun ([<InlineIfLambda>] x) ([<InlineIfLambda>] f) -> f x)
//    let inline (|>) x = fun ([<InlineIfLambda>] f) -> (fun ([<InlineIfLambda>] x)  -> f x) x

open InlinePipe
[<MemoryDiagnoser; DisassemblyDiagnoser>]
type Benchmark3() =

//    [<Benchmark>]
//    member x.PushStreamWithInlinePipe() =
//        ofRange 0 10000
//        |> map ((+) 1)
//        |> filter (fun v -> (v &&& 1) = 0)
//        |> map int64
//        |> fold (+) 0L

    [<Benchmark>]
    member x.PushStreamWithInlineComposition() =
        ofRange 0 10000
        |> (map ((+) 1)
            >> filter (fun v -> (v &&& 1) = 0)
            >> map int64
            >> fold (+) 0L)