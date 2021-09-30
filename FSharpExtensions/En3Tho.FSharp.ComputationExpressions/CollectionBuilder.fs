namespace En3Tho.FSharp.ComputationExpressions

open System.Collections.Generic
open System.Runtime.CompilerServices
open En3Tho.FSharp.Extensions

module SCollectionBuilder =
    type SCollection< ^a, ^b, ^c when ^a: (member Add: ^b -> ^c)> = ^a

    let inline add value (collection: SCollection<_,_,_>) =
        ( ^a: (member Add: ^b -> ^c) collection, value)

    [<AbstractClass;Extension>]
    type ICollectionExtensions() = // TODO: TryWith, TryFinally and F# 6 InlineIfLambda
        [<Extension>]
        static member inline Yield(collection, value: 'b) = add value collection
        [<Extension>]
        static member inline YieldFrom(collection, values: 'b seq) = for value in values do add value collection

module IDictionaryBuilder =
    type IDictionary<'a, 'b> with
        member inline this.Yield((key, value): struct ('a * 'b)) = this.Add(key, value)
        member inline this.Yield(kvp: KeyValuePair<_,_>) = this.Add(kvp.Key, kvp.Value)
        member inline this.Yield((key, value): 'a * 'b) = this.Add(key, value)
        member inline this.YieldFrom(seq: ('a * 'b) seq) = for value in seq do this.Yield value
        member inline this.YieldFrom(seq: KeyValuePair<_,_> seq) = for value in seq do this.Yield value
        member inline this.YieldFrom(seq: struct ('a * 'b) seq) = for value in seq do this.Yield value

module SStringBuilderBuilder =
    type SStringBuilder< ^a, ^b, ^c when ^a: (member Append: ^b -> ^c)> = ^a

    let inline append value (stringBuilder: SStringBuilder<_,_,_>) =
        ( ^a: (member Append: ^b -> ^c) stringBuilder, value) |> ignore

    [<AbstractClass;Extension>]
    type StringBuilderExtensions() =
        [<Extension>]
        static member inline Yield(collection, value: 'b) = append value collection
        [<Extension>]
        static member inline YieldFrom(collection, values: 'b seq) = for value in values do append value collection