namespace En3Tho.FSharp.Extensions

open System.Collections.Generic
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.Tasks
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.Low
open En3Tho.FSharp.ComputationExpressions.GenericTaskBuilder.Extensions.High
open En3Tho.FSharp.ComputationExpressions.ArrayPoolBasedBuilders
open System.Runtime.CompilerServices

module GSeqEnumerators =

    type [<Struct; IsReadOnly>] AsyncEnumeratorCurrentValueProvider<'a>(wrapper: IAsyncEnumerator<'a>) =

        interface System.Collections.IEnumerator with
            member this.Current = wrapper.Current
            member this.MoveNext() = true
            member this.Reset() = ()

        interface IEnumerator<'a> with
            member this.Current = wrapper.Current
            member this.Dispose() = ()

module GSeq =

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let inline toArray<'i, 'e when 'e: struct and 'e :> IEnumerator<'i>>(enumerator: 'e) = arr {
        let mutable enumerator = enumerator
        while enumerator.MoveNext() do
            enumerator.Current
    }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let inline toResizeArray<'i, 'e when 'e: struct and 'e :> IEnumerator<'i>>(enumerator: 'e) = rsz {
        let mutable enumerator = enumerator
        while enumerator.MoveNext() do
            enumerator.Current
    }

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let inline toBlock<'i, 'e when 'e: struct and 'e :> IEnumerator<'i>>(enumerator: 'e) = block {
        let mutable enumerator = enumerator
        while enumerator.MoveNext() do
            enumerator.Current
    }

    open GSeqEnumerators

    // maybe AsyncEnumerable.withGSeq is better but I don't have AsyncEnumerable module now so whatever
    let inline withAsyncSeq<'a, 'i, 'e when 'e: struct and 'e :> IEnumerator<'i>>
        ([<InlineIfLambda>] gseqFactory: AsyncEnumeratorCurrentValueProvider<'a> -> 'e) (e: IAsyncEnumerable<'a>) =
        taskSeq {
            use enum = e.GetAsyncEnumerator()
            let mutable enumerator = gseqFactory (AsyncEnumeratorCurrentValueProvider(enum))
            while! enum.MoveNextAsync() do
                if enumerator.MoveNext() then
                    enumerator.Current
        }