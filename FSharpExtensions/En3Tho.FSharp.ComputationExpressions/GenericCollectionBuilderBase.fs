[<AutoOpen>]
module En3Tho.FSharp.Extensions.GenericCollectionBuilderBase

open System
open System.Runtime.CompilerServices

type RunExpression = unit -> unit
type WhileExpression = unit -> unit
type ForExpression<'a> = 'a -> unit

[<AbstractClass;Extension>]
type GenericTypeExtensions() = // TODO: TryWith, TryFinally and F# 6 InlineIfLambda
    [<Extension>]
    static member inline While(_, moveNext, whileExpr: WhileExpression) = while moveNext() do whileExpr()
    [<Extension>]
    static member inline For(_, values, forExpr: ForExpression<_>) = for value in values do forExpr value
    [<Extension>]
    static member inline Combine(_, _, cexpr) = cexpr()
    [<Extension>]
    static member inline TryFinally(_, tryExpr, compensation: RunExpression) = try tryExpr() finally compensation()
    [<Extension>]
    static member inline TryWith(_, tryExpr, compensation) = try tryExpr() with e -> compensation e
    [<Extension>]
    static member inline Using(_, resource: #IDisposable, tryExpr) = try tryExpr() finally resource.Dispose()
    [<Extension>]
    static member inline Zero _ = ()
    [<Extension>]
    static member inline Delay(_, beforeAdd) = beforeAdd
    [<Extension>]
    static member inline Run(collection, runExpr: RunExpression) = runExpr(); collection