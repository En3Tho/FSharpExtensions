namespace En3Tho.FSharp.Extensions

open System
open System.Runtime.CompilerServices

type TryFinallyExpression = unit -> unit
type WhileExpression = unit -> unit
type ForExpression<'a> = 'a -> unit
type RunExpression = unit -> unit

type CollectionCode = unit -> unit

[<AbstractClass;Extension>]
type GenericTypeExtensions() = // TODO: F# 6 InlineIfLambda
    [<Extension>]
    static member inline While(_, [<InlineIfLambda>] moveNext, [<InlineIfLambda>] whileExpr: WhileExpression) = while moveNext() do whileExpr()
    [<Extension>]
    static member inline For(_, values, [<InlineIfLambda>] forExpr: ForExpression<_>) = for value in values do forExpr value
    [<Extension>]
    static member inline Combine(_, _, [<InlineIfLambda>] cexpr) = cexpr()
    [<Extension>]
    static member inline TryFinally(_, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: TryFinallyExpression) = try tryExpr() finally compensation()
    [<Extension>]
    static member inline TryWith(_, [<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation) = try tryExpr() with e -> compensation e
    [<Extension>]
    static member inline Using(_, resource: #IDisposable, [<InlineIfLambda>] tryExpr) = try tryExpr() finally resource.Dispose()
    [<Extension>]
    static member inline Zero _ = ()
    [<Extension>]
    static member inline Delay(_, beforeAdd) = beforeAdd