module En3Tho.FSharp.ComputationExpressions.ResultBuilder

open System

type ResultCode<'a, 'b> = unit -> Result<'a, 'b>

// TODO: /v/task/e/result

type ResultBuilderUsingInlineIfLambdaBase() =

    member inline _.Zero _ : ResultCode<unit, 'b> =
        fun() -> Ok ()

    member inline _.Delay([<InlineIfLambda>] f: unit -> ResultCode<'a, 'b>) : ResultCode<'a, 'b> =
        fun () -> (f())()
        // Note, not "f()()" - the F# compiler optimizer likes arguments to match lambdas in order to preserve
        // argument evaluation order, so for "(f())()" the optimizer reduces one lambda then another, while "f()()" doesn't

    member inline _.Combine([<InlineIfLambda>] task1: ResultCode<unit, 'b>, [<InlineIfLambda>] task2: ResultCode<'a, 'b>) : ResultCode<'a, 'b> =
        fun () ->
            match task1() with
            | Error error -> Error error
            | Ok () -> task2()

    member inline _.Bind(res1: Result<'a, 'b>, [<InlineIfLambda>] task2: 'a -> ResultCode<'c, 'b>) : ResultCode<'c, 'b> =
        fun () ->
            match res1 with
            | Error error -> Error error
            | Ok v -> (task2 v)()

    member inline _.While([<InlineIfLambda>] condition : unit -> bool, [<InlineIfLambda>] body : ResultCode<'a, 'b>) : ResultCode<unit, 'b> =
        fun () ->
            let mutable proceed = true
            while proceed && condition() do
                match body() with
                | Error _ -> proceed <- false
                | Ok _ -> ()
            Ok(())

    member inline _.TryWith([<InlineIfLambda>] body: ResultCode<'a, 'b>, [<InlineIfLambda>] catch: exn -> ResultCode<'a, 'b>) : ResultCode<'a, 'b> =
        fun () ->
            try
                body()
            with exn ->
                (catch exn)()

    member inline _.TryFinally([<InlineIfLambda>] body: ResultCode<'a, 'b>, [<InlineIfLambda>] compensation: unit -> unit) : ResultCode<'a, 'b> =
        fun () ->
            try
                body()
            finally
                compensation()

    member inline this.Using(disp: #IDisposable, [<InlineIfLambda>] body: #IDisposable -> ResultCode<'a, 'b>) : ResultCode<'a, 'b> =
        // A using statement is just a try/finally with the finally block disposing if non-null.
        this.TryFinally(
            (fun () -> (body disp)()),
            (fun () -> if not (isNull (box disp)) then disp.Dispose()))

    member inline this.For(sequence: seq<'a>, [<InlineIfLambda>] body: 'a -> ResultCode<unit, 'b>) : ResultCode<unit, 'b> =
        this.Using (
            sequence.GetEnumerator(),
            (fun e -> this.While((fun () -> e.MoveNext()),
            (fun () -> (body e.Current)()))))

    member inline _.Return (value: 'a) : ResultCode<'a, 'b> =
        fun () ->
            Ok value

    member inline this.ReturnFrom (source: Result<'a, 'b>) : ResultCode<'a, 'b> =
        fun () ->
            source

type ResultBuilder() =
    inherit ResultBuilderUsingInlineIfLambdaBase()
    member inline this.Run([<InlineIfLambda>] code : ResultCode<'a, 'b>) = code()

type EResultBuilder() =
    inherit ResultBuilderUsingInlineIfLambdaBase()
    member inline this.Run([<InlineIfLambda>] code : ResultCode<'a, exn>) = code()
    member inline _.Return (value: 'a) : ResultCode<'a, exn> =
        fun () ->
            Ok value
    member inline _.Return (value: #Exception) : ResultCode<'a, exn> =
        fun () ->
            Error (value :> exn)
    member inline _.ReturnFrom (value: #Exception) : ResultCode<'a, exn> =
        fun () ->
            Error (value :> exn)

let result = ResultBuilder()
let eresult = EResultBuilder()