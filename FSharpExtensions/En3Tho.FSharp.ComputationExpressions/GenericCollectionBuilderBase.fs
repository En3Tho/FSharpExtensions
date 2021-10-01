namespace En3Tho.FSharp.Extensions

open System
open System.Runtime.CompilerServices

type CollectionCode = unit -> unit

type TryFinallyExpression = unit -> CollectionCode
type WhileExpression = unit -> CollectionCode
type ForExpression<'a> = 'a -> CollectionCode
type RunExpression = unit -> unit


[<AbstractClass;Extension>]
type GenericTypeExtensions() =
    [<Extension>]
    static member inline While(_, [<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: unit -> unit) : CollectionCode =
        fun () -> while moveNext() do (whileExpr())

    [<Extension>]
    static member inline Combine(_, first: CollectionCode, [<InlineIfLambda>] second) : CollectionCode =
        fun() ->
            first()
            second()

    [<Extension>]
    static member inline TryFinally(_, [<InlineIfLambda>] tryExpr: CollectionCode, [<InlineIfLambda>] compensation: CollectionCode) =
        fun() ->
            try
                tryExpr()
            finally
                compensation()

    [<Extension>]
    static member inline TryWith(_, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> CollectionCode) : CollectionCode =
        fun() ->
            try
                tryExpr()
            with e ->
                (compensation e)()

    [<Extension>]
    static member inline Using(this, resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> CollectionCode) : CollectionCode =
        this.TryFinally(
            (fun() -> (tryExpr(resource)())),
            (fun() -> resource.Dispose()))


    [<Extension>]
    static member inline For(this, values: 'a seq, [<InlineIfLambda>] forExpr: ForExpression<'a>) : CollectionCode =
        this.Using (
            values.GetEnumerator(),
            (fun e -> this.While((fun () -> e.MoveNext()),
            (fun () -> (forExpr e.Current)()))))

    [<Extension>]
    static member inline Zero _ : CollectionCode = fun() -> ()

    [<Extension>]
    static member inline Delay(_, [<InlineIfLambda>] delay: unit -> CollectionCode) =
        (fun () -> (delay())())