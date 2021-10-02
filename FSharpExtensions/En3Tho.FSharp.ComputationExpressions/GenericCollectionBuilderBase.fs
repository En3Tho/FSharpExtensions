namespace En3Tho.FSharp.Extensions

open System
open System.ComponentModel
open System.Runtime.CompilerServices

module internal EditorBrowsableState =
#if RELEASE
    let [<Literal>] Value = EditorBrowsableState.Never
#else
    let [<Literal>] Value = EditorBrowsableState.Always
#endif

type TryFinallyExpression = unit -> unit
type WhileExpression = unit -> unit
type ForExpression<'a> = 'a -> unit
type RunExpression = unit -> unit

type CollectionCode = unit -> unit

[<AbstractClass;Extension>]
type GenericTypeExtensions() =
    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline While(_, [<InlineIfLambda>] moveNext, [<InlineIfLambda>] whileExpr: WhileExpression) = while moveNext() do whileExpr()
    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline For(_, values, [<InlineIfLambda>] forExpr: ForExpression<_>) = for value in values do forExpr value
    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline Combine(_, _, [<InlineIfLambda>] cexpr) = cexpr()
    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline TryFinally(_, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: TryFinallyExpression) = try tryExpr() finally compensation()
    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline TryWith(_, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation) = try tryExpr() with e -> compensation e
    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline Using(_, resource: #IDisposable, [<InlineIfLambda>] tryExpr) = try tryExpr() finally resource.Dispose()
    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline Zero _ = ()
    [<Extension; EditorBrowsable(EditorBrowsableState.Value)>]
    static member inline Delay(_, beforeAdd) = beforeAdd