namespace En3Tho.FSharp.ComputationExpressions

open System.Runtime.CompilerServices

module TryExprBuilderImpl =

    type TryExprBuilderExpression<'a> = unit -> 'a

    type TryExprBuilderBase() =
        member inline _.Delay([<InlineIfLambda>] delay: unit -> TryExprBuilderExpression<'a>) = fun () -> (delay())()
        member inline _.Yield(value: 'a) : TryExprBuilderExpression<'a> = fun () -> value
        member inline _.Zero() : TryExprBuilderExpression<unit> = fun () -> ()

    [<Sealed>]
    type TryExprBuilder() =
        inherit TryExprBuilderBase()
        member inline _.Run(expr: TryExprBuilderExpression<'a>) =
            try
                expr()
            with _ ->
                Unchecked.defaultof<'a>

    [<Sealed>]
    type TryExprBuilderToException() =
        inherit TryExprBuilderBase()
        member inline _.Run(expr: TryExprBuilderExpression<unit>) =
            try
                expr()
                null
            with e ->
                e

    [<Sealed>]
    type TryExprBuilderToResult() =
        inherit TryExprBuilderBase()
        member inline _.Run(expr: TryExprBuilderExpression<'a>) =
            try
                Ok(expr())
            with e ->
                Error(e)

    [<Struct; IsReadOnly>]
    type TryWithExprBuilder<'a>(value: 'a) =
        member _.Value = value
        member inline _.Delay([<InlineIfLambda>] delay: unit -> TryExprBuilderExpression<'a>) = fun () -> (delay())()
        member inline _.Yield(value: 'a) : TryExprBuilderExpression<'a> = fun () -> value
        member inline _.Zero() : TryExprBuilderExpression<unit> = fun () -> ()
        member inline this.Run(expr: TryExprBuilderExpression<'a>) =
            try
                expr()
            with _ ->
                this.Value

[<AutoOpen>]
module TryExprBuilder =
    let try' = TryExprBuilderImpl.TryExprBuilder()
    let tryE = TryExprBuilderImpl.TryExprBuilderToException()
    let tryR = TryExprBuilderImpl.TryExprBuilderToResult()
    let tryWith value = TryExprBuilderImpl.TryWithExprBuilder(value)