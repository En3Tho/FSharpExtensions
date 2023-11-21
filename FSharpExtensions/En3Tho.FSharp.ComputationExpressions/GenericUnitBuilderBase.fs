namespace En3Tho.FSharp.Extensions

open System

[<AutoOpen>]
module CodeDefinition =
    type UnitBuilderCode<'Builder> = 'Builder -> unit

type UnitBuilderBase<'Builder>() =

    member inline _.While([<InlineIfLambda>] moveNext: unit -> bool, [<InlineIfLambda>] whileExpr: UnitBuilderCode<'Builder>) : UnitBuilderCode<'Builder> =
        fun builder -> while moveNext() do (whileExpr(builder))

    member inline _.Combine([<InlineIfLambda>] first: UnitBuilderCode<'Builder>, [<InlineIfLambda>] second: UnitBuilderCode<'Builder>) : UnitBuilderCode<'Builder> =
        fun(builder) ->
            first(builder)
            second(builder)

    member inline _.TryFinally([<InlineIfLambda>] tryExpr: UnitBuilderCode<'Builder>, [<InlineIfLambda>] compensation: UnitBuilderCode<'Builder>) =
        fun (builder) ->
            try
                tryExpr(builder)
            finally
                compensation(builder)

    member inline _.TryWith([<InlineIfLambda>] tryExpr, [<InlineIfLambda>] compensation: exn -> UnitBuilderCode<'Builder>) : UnitBuilderCode<'Builder> =
        fun (builder) ->
            try
                tryExpr()
            with e ->
                (compensation e)(builder)

    member inline this.Using(resource: #IDisposable, [<InlineIfLambda>] tryExpr: #IDisposable -> UnitBuilderCode<'Builder>) : UnitBuilderCode<'Builder> =
        this.TryFinally(
            (fun (builder) -> (tryExpr(resource)(builder))),
            (fun (builder) -> if not (isNull (box resource)) then resource.Dispose()))

    member inline this.For(values: 'a seq, [<InlineIfLambda>] forExpr: 'a -> UnitBuilderCode<'Builder>) : UnitBuilderCode<'Builder> =
        this.Using (
            values.GetEnumerator(), (fun e ->
                this.While((fun () -> e.MoveNext()), (fun (builder) ->
                    (forExpr e.Current)(builder))
                )
            )
        )

    member inline _.Zero() : UnitBuilderCode<'Builder> = fun _ -> ()

    member inline _.Delay([<InlineIfLambda>] delay: unit -> UnitBuilderCode<'Builder>) =
        fun (builder) -> (delay())(builder)