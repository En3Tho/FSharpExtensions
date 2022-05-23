namespace En3Tho.FSharp.Extensions

open System
open System.ComponentModel
open System.Runtime.CompilerServices
open GenericBuilderBase


module EditorBrowsableState =
#if SHOW_COMPEXPR_EXTENSIONS
    let [<Literal>] Value = EditorBrowsableState.Always
#else
    let [<Literal>] Value = EditorBrowsableState.Never
#endif

type CollectionCode = UnitBuilderCode<unit>


[<AbstractClass;Extension>]
type CollectionCodeExtensions() =

    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline While(_, [<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: CollectionCode) : CollectionCode =
        fun () -> while moveNext() do (whileExpr())

    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline Combine(_, [<InlineIfLambda>] first: CollectionCode, [<InlineIfLambda>] second) : CollectionCode =
        fun() ->
            first()
            second()

    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline TryFinally(_, [<InlineIfLambda>] tryExpr: CollectionCode, [<InlineIfLambda>] compensation: CollectionCode) =
        fun() ->
            try
                tryExpr()
            finally
                compensation()

    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline TryWith(_, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> CollectionCode) : CollectionCode =
        fun() ->
            try
                tryExpr()
            with e ->
                (compensation e)()

    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline Using(this, resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> CollectionCode) : CollectionCode =
        this.TryFinally(
            (fun() -> (tryExpr(resource)())),
            (fun() -> if not (isNull (box resource)) then resource.Dispose()))


    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline For(this, values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> CollectionCode) : CollectionCode =
        this.Using (
            values.GetEnumerator(), (fun e ->
                this.While((fun () -> e.MoveNext()), (fun () ->
                    (forExpr e.Current)())
                )
            )
        )

    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline Zero _ : CollectionCode = fun() -> ()

    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline Delay(_, [<InlineIfLambda>] delay: unit -> CollectionCode) =
        fun () -> (delay())()
        // Note, not "f()()" - the F# compiler optimizer likes arguments to match lambdas in order to preserve
        // argument evaluation order, so for "(f())()" the optimizer reduces one lambda then another, while "f()()" doesn't