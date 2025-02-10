namespace En3Tho.FSharp.ComputationExpressions

open System.Collections.Concurrent
open System.Collections.Generic
open System.ComponentModel
open System.Runtime.CompilerServices
open System.Text

module SCollectionBuilder =

    let inline add value collection =
        ( ^a: (member Add: ^b -> ^c) collection, value)

    [<AbstractClass>]
    type SCollectionExtensions() =
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Yield(collection, value: 'b) : CollectionCode = fun() -> add value collection |> ignore
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom(collection, values: 'b seq) : CollectionCode = fun() -> for value in values do add value collection |> ignore

module ICollectionBuilder =

    [<AbstractClass>]
    type ICollectionExtensions() =
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Yield(collection: #ICollection<'a>, value: 'a) : CollectionCode = fun() -> collection.Add(value)
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom(collection: #ICollection<'a>, values: 'a seq) : CollectionCode = fun() -> for value in values do collection.Add(value)

    type ICollection<'a> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

    type List<'a> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

    type HashSet<'a> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

    type LinkedList<'a> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

    type Stack<'a> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

    type Queue<'a> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

    type SortedSet<'a> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

module IDictionaryBuilder =
    [<AbstractClass>]
    type IDictionaryExtensions() =
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Yield(dictionary: #IDictionary<_,_>, (key, value): struct ('a * 'b)) : CollectionCode = fun() -> dictionary[key] <- value
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Yield(dictionary: #IDictionary<_,_>, kvp: KeyValuePair<_,_>) : CollectionCode = fun() -> dictionary[kvp.Key] <- kvp.Value
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Yield(dictionary: #IDictionary<_,_>, (key, value): 'a * 'b) : CollectionCode = fun() -> dictionary[key] <- value
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom(dictionary: #IDictionary<_,_>, seq: ('a * 'b) seq) : CollectionCode = fun() -> for key, value in seq do dictionary[key] <- value
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom(dictionary: #IDictionary<_,_>, seq: KeyValuePair<_,_> seq) : CollectionCode = fun() -> for value in seq do dictionary[value.Key] <- value.Value
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom(dictionary: #IDictionary<_,_>, seq: struct ('a * 'b) seq) : CollectionCode = fun() -> for key, value in seq do dictionary[key] <- value

    type IDictionary<'a, 'b> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

    type Dictionary<'a, 'b> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

    type SortedList<'a, 'b> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

    type ConcurrentDictionary<'a, 'b> with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this

module SStringBuilderBuilder =

    let inline append value stringBuilder =
        ( ^a: (member Append: ^b -> ^c) stringBuilder, value) |> ignore

    [<AbstractClass>]
    type StringBuilderExtensions() =
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline Yield(builder, value: 'b) : CollectionCode = fun() -> append value builder
        [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
        static member inline YieldFrom(builder, values: 'b seq) : CollectionCode = fun() -> for value in values do append value builder

    type StringBuilder with
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Zero() : CollectionCode = fun() -> ()
        [<EditorBrowsable(EditorBrowsableState.Never)>]
        member inline this.Run([<InlineIfLambda>] runExpr: CollectionCode) = runExpr(); this