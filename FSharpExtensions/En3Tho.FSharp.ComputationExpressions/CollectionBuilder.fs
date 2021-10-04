namespace En3Tho.FSharp.ComputationExpressions

open System.Collections.Concurrent
open System.Collections.Generic
open System.ComponentModel
open System.Runtime.CompilerServices
open System.Text
open En3Tho.FSharp.Extensions

module SCollectionBuilder =

    let inline add value collection =
        ( ^a: (member Add: ^b -> ^c) collection, value)

    [<AbstractClass;Extension>]
    type SCollectionExtensions() =
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline Yield(collection, value: 'b) : CollectionCode = fun() -> add value collection
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline YieldFrom(collection, values: 'b seq) : CollectionCode = fun() -> for value in values do (add value collection)()

module ICollectionBuilder =

    [<AbstractClass;Extension>]
    type ICollectionExtensions() =
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline Yield(collection: #ICollection<'a>, value: 'a) : CollectionCode = fun() -> collection.Add value
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline YieldFrom(collection: #ICollection<'a>, values: 'a seq) : CollectionCode = fun() -> for value in values do collection.Add value

    type ICollection<'a> with
        [<EditorBrowsable(EditorBrowsableState.Value)>]
        member inline this.Run([<InlineIfLambda>] runExpr: RunExpression) = runExpr(); this

    type List<'a> with
        [<EditorBrowsable(EditorBrowsableState.Value)>]
        member inline this.Run([<InlineIfLambda>] runExpr: RunExpression) = runExpr(); this

    type HashSet<'a> with
        [<EditorBrowsable(EditorBrowsableState.Value)>]
        member inline this.Run([<InlineIfLambda>] runExpr: RunExpression) = runExpr(); this

    type LinkedList<'a> with
        [<EditorBrowsable(EditorBrowsableState.Value)>]
        member inline this.Run([<InlineIfLambda>] runExpr: RunExpression) = runExpr(); this

module IDictionaryBuilder =
    [<AbstractClass;Extension>]
    type IDictionaryExtensions() =
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline Yield(dictionary: #IDictionary<_,_>, (key, value): struct ('a * 'b)) : CollectionCode = fun() -> dictionary.Add(key, value)
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline Yield(dictionary: #IDictionary<_,_>, kvp: KeyValuePair<_,_>) : CollectionCode = fun() -> dictionary.Add(kvp.Key, kvp.Value)
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline Yield(dictionary: #IDictionary<_,_>, (key, value): 'a * 'b) : CollectionCode = fun() -> dictionary.Add(key, value)
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline YieldFrom(dictionary: #IDictionary<_,_>, seq: ('a * 'b) seq) : CollectionCode = fun() -> for key, value in seq do dictionary.Add(key, value)
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline YieldFrom(dictionary: #IDictionary<_,_>, seq: KeyValuePair<_,_> seq) : CollectionCode = fun() -> for value in seq do dictionary.Add(value.Key, value.Value)
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline YieldFrom(dictionary: #IDictionary<_,_>, seq: struct ('a * 'b) seq) : CollectionCode = fun() -> for key, value in seq do dictionary.Add(key, value)

    type IDictionary<'a, 'b> with
        [<EditorBrowsable(EditorBrowsableState.Value)>]
        member inline this.Run([<InlineIfLambda>] runExpr: RunExpression) = runExpr(); this

    type Dictionary<'a, 'b> with
        [<EditorBrowsable(EditorBrowsableState.Value)>]
        member inline this.Run([<InlineIfLambda>] runExpr: RunExpression) = runExpr(); this

    type ConcurrentDictionary<'a, 'b> with
        [<EditorBrowsable(EditorBrowsableState.Value)>]
        member inline this.Run([<InlineIfLambda>] runExpr: RunExpression) = runExpr(); this

module SStringBuilderBuilder =

    let inline append value stringBuilder =
        ( ^a: (member Append: ^b -> ^c) stringBuilder, value) |> ignore

    [<AbstractClass;Extension>]
    type StringBuilderExtensions() =
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline Yield(builder, value: 'b) : CollectionCode = fun() -> append value builder
        [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
        static member inline YieldFrom(builder, values: 'b seq) : CollectionCode = fun() -> for value in values do append value builder

    type StringBuilder with
        [<EditorBrowsable(EditorBrowsableState.Value)>]
        member inline this.Run([<InlineIfLambda>] runExpr: RunExpression) = runExpr(); this