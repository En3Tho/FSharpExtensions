module En3Tho.FSharp.Extensions.GSeq

open System.Collections.Generic
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.ArrayPoolBasedBuilders
open System.Runtime.CompilerServices

type private SStructEnumerator<'i, 'e when 'e: struct and 'e :> IEnumerator<'i>> = 'e

[<MethodImpl(MethodImplOptions.AggressiveInlining)>]
let toArray (enumerator: SStructEnumerator<'i,'e>) = arr {
    let mutable enumerator = enumerator
    while enumerator.MoveNext() do
        enumerator.Current
}

[<MethodImpl(MethodImplOptions.AggressiveInlining)>]
let toResizeArray (enumerator: SStructEnumerator<'i,'e>) = rsz {
    let mutable enumerator = enumerator
    while enumerator.MoveNext() do
        enumerator.Current
}

[<MethodImpl(MethodImplOptions.AggressiveInlining)>]
let toBlock (enumerator: SStructEnumerator<'i,'e>) = block {
    let mutable enumerator = enumerator
    while enumerator.MoveNext() do
        enumerator.Current
}