namespace En3Tho.FSharp.ComputationExpressions

open System
open System.Collections.Generic
open System.ComponentModel
open System.Runtime.CompilerServices
open En3Tho.FSharp.Extensions

type CollectionCode = UnitBuilderCode<unit>

[<AbstractClass>]
type UnitLikeCodeExtensions() =

    [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
    static member inline While(_, [<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: CollectionCode) : CollectionCode =
        fun () -> while moveNext() do (whileExpr())

    [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
    static member inline Combine(_, [<InlineIfLambda>] first: CollectionCode, [<InlineIfLambda>] second) : CollectionCode =
        fun() ->
            first()
            second()

    [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
    static member inline TryFinally(_, [<InlineIfLambda>] tryExpr: CollectionCode, [<InlineIfLambda>] compensation: CollectionCode) =
        fun() ->
            try
                tryExpr()
            finally
                compensation()

    [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
    static member inline TryWith(_, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> CollectionCode) : CollectionCode =
        fun() ->
            try
                tryExpr()
            with e ->
                (compensation e)()

    [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
    static member inline Using(this, resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> CollectionCode) : CollectionCode =
        this.TryFinally(
            (fun() -> (tryExpr(resource)())),
            (fun() -> if not (isNull (box resource)) then resource.Dispose()))


    [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
    static member inline For(this, values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> CollectionCode) : CollectionCode =
        this.Using(
            values.GetEnumerator(), (fun e ->
                this.While((fun () -> e.MoveNext()), (fun () ->
                    (forExpr e.Current)())
                )
            )
        )

    // This one is for GSeq mainly
    [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
    static member inline For<'T, 'TEnumerator when 'TEnumerator :> IEnumerator<'T>>(_, enumerator: 'TEnumerator, [<InlineIfLambda>] forExpr: 'T -> CollectionCode) : CollectionCode =
        fun () ->
            let mutable enumerator = enumerator
            while enumerator.MoveNext() do
                (forExpr enumerator.Current)()

    [<Extension; EditorBrowsable(EditorBrowsableState.Never)>]
    static member inline Delay(_, [<InlineIfLambda>] delay: unit -> CollectionCode) =
        fun () -> (delay())()