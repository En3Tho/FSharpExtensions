module En3Tho.FSharp.Extensions.GenericBuilderBase

open System

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

    member inline _.Zero() : UnitBuilderCode<'Builder> = fun (builder) -> ()

    member inline _.Delay([<InlineIfLambda>] delay: unit -> UnitBuilderCode<'Builder>) =
        fun (builder) -> (delay())(builder)
        // Note, not "f()()" - the F# compiler optimizer likes arguments to match lambdas in order to preserve
        // argument evaluation order, so for "(f())()" the optimizer reduces one lambda then another, while "f()()" doesn't