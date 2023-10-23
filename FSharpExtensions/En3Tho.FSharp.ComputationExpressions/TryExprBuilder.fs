namespace En3Tho.FSharp.ComputationExpressions

open System.Runtime.CompilerServices

module TryExprBuilderImpl =

    type TryExprBuilderExpression<'a> = unit -> 'a

    type TryExprBuilderBase() =
        member inline _.Delay([<InlineIfLambda>] delay: unit -> TryExprBuilderExpression<'a>) = fun builder -> (delay())(builder)
        member inline _.Yield(value: 'a) : TryExprBuilderExpression<'a> = fun () -> value
        member inline _.Zero() : TryExprBuilderExpression<unit> = fun () -> ()

    type TryExprBuilderDefault() =
        inherit TryExprBuilderBase()
        member inline _.Run(expr: TryExprBuilderExpression<'a>) =
            try
                expr()
            with _ ->
                Unchecked.defaultof<'a>

    [<Struct; IsReadOnly>]
    type TryExprBuilderOrElse<'a>(value: 'a) =
        member _.Value = value
        member inline _.Delay([<InlineIfLambda>] delay: unit -> TryExprBuilderExpression<'a>) = fun builder -> (delay())(builder)
        member inline _.Yield(value: 'a) : TryExprBuilderExpression<'a> = fun () -> value
        member inline _.Zero() : TryExprBuilderExpression<unit> = fun () -> ()
        member inline this.Run(expr: TryExprBuilderExpression<'a>) =
            try
                expr()
            with _ ->
                this.Value

[<AutoOpen>]
module TryExprBuilder =
    let try' = TryExprBuilderImpl.TryExprBuilderDefault()
    let tryWith value = TryExprBuilderImpl.TryExprBuilderOrElse(value)