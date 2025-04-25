module En3Tho.FSharp.ComputationExpressions.ResultBuilder

open System

type ResultCode<'a, 'b> = unit -> Result<'a, 'b>

type ResultBuilderBase() =

    member inline _.Zero() : ResultCode<unit, 'b> =
        fun() -> Ok ()

    member inline _.Delay([<InlineIfLambda>] f: unit -> ResultCode<'a, 'b>) : ResultCode<'a, 'b> =
        fun () -> (f())()

    member inline _.Combine([<InlineIfLambda>] task1: ResultCode<unit, 'b>, [<InlineIfLambda>] task2: ResultCode<'a, 'b>) : ResultCode<'a, 'b> =
        fun () ->
            match task1() with
            | Error error -> Error error
            | Ok () -> task2()

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
        this.TryFinally(
            (fun () -> (body disp)()),
            (fun () -> if not (isNull (box disp)) then disp.Dispose()))

    member inline _.Return(value: 'a) : ResultCode<'a, 'b> =
        fun () ->
            Ok value

    member inline this.ReturnFrom(source: Result<'a, 'b>) : ResultCode<'a, 'b> =
        fun () ->
            source

type ResultBuilder() =
    inherit ResultBuilderBase()

    member inline _.Bind(res1: Result<'a, 'b>, [<InlineIfLambda>] task2: 'a -> ResultCode<'c, 'b>) : ResultCode<'c, 'b> =
        fun () ->
            match res1 with
            | Error error -> Error error
            | Ok v -> (task2 v)()

    member inline this.Run([<InlineIfLambda>] code: ResultCode<'a, 'b>) = code()

type EResultBuilder() =
    inherit ResultBuilderBase()

    member inline _.Bind(res1: Result<'a, #exn>, [<InlineIfLambda>] task2: 'a -> ResultCode<'c, #exn>) : ResultCode<'c, #exn> =
        fun () ->
            match res1 with
            | Error error -> Error error
            | Ok v -> (task2 v)()

    member inline this.Run<'a, 'b when 'b :> exn>([<InlineIfLambda>] code: ResultCode<'a, 'b>) = code()

    // TODO: A less allocating way

    member inline this.Bind2(value: Result<'a, #exn>, value2: Result<'b, #exn>, [<InlineIfLambda>] next: 'a * 'b -> ResultCode<'c, AggregateException>) =
        fun() ->
            match value, value2 with
            | Ok value, Ok value2 -> (next (value, value2))()
            | Error exn, Error exn2 -> AggregateException(exn :> exn, exn2 :> exn) |> Error
            | Error exn, _ -> Error (AggregateException(exn :> exn))
            | _, Error exn -> Error (AggregateException(exn :> exn))
        
    member inline this.Bind3(value: Result<'a, #exn>, value2: Result<'b, #exn>, value3: Result<'c, #exn>, [<InlineIfLambda>] next: 'a * 'b * 'c -> ResultCode<'d, AggregateException>) =
        fun() ->
            match value, value2, value3 with
            | Ok value, Ok value2, Ok value3 -> (next (value, value2, value3))()
            | _ -> ([
                match value with Error exn -> exn :> exn | _ -> ()
                match value2 with Error exn -> exn :> exn | _ -> ()
                match value3 with Error exn -> exn :> exn | _ -> ()
                ] |> AggregateException |> Error)
        
    member inline this.Bind4(value: Result<'a, #exn>, value2: Result<'b, #exn>, value3: Result<'c, #exn>, value4: Result<'d, #exn>, [<InlineIfLambda>] next: 'a * 'b * 'c * 'd -> ResultCode<'e, AggregateException>) =
        fun() ->
            match value, value2, value3, value4 with
            | Ok value, Ok value2, Ok value3, Ok value4 -> (next (value, value2, value3, value4))()
            | _ -> ([
                match value with Error exn -> exn :> exn | _ -> ()
                match value2 with Error exn -> exn :> exn | _ -> ()
                match value3 with Error exn -> exn :> exn | _ -> ()
                match value4 with Error exn -> exn :> exn | _ -> ()
                ] |> AggregateException |> Error)
    
    member inline this.Bind5(value: Result<'a, #exn>, value2: Result<'b, #exn>, value3: Result<'c, #exn>, value4: Result<'d, #exn>, value5: Result<'e, #exn>,
                             [<InlineIfLambda>] next: 'a * 'b * 'c * 'd * 'e -> ResultCode<'f, AggregateException>) =
        fun() ->
            match value, value2, value3, value4, value5 with
            | Ok value, Ok value2, Ok value3, Ok value4, Ok value5 -> (next (value, value2, value3, value4, value5))()
            | _ -> ([
                match value with Error exn -> exn :> exn | _ -> ()
                match value2 with Error exn -> exn :> exn | _ -> ()
                match value3 with Error exn -> exn :> exn | _ -> ()
                match value4 with Error exn -> exn :> exn | _ -> ()
                match value5 with Error exn -> exn :> exn | _ -> ()
            ] |> AggregateException |> Error)
    
    member inline this.Bind6(value: Result<'a, #exn>, value2: Result<'b, #exn>, value3: Result<'c, #exn>, value4: Result<'d, #exn>, value5: Result<'e, #exn>,
                             value6: Result<'f, #exn>, [<InlineIfLambda>] next: 'a * 'b * 'c * 'd * 'e * 'f -> ResultCode<'g, AggregateException>) =
        fun() ->
            match value, value2, value3, value4, value5, value6 with
            | Ok value, Ok value2, Ok value3, Ok value4, Ok value5, Ok value6 -> (next (value, value2, value3, value4, value5, value6))()
            | _ -> ([
                match value with Error exn -> exn :> exn | _ -> ()
                match value2 with Error exn -> exn :> exn | _ -> ()
                match value3 with Error exn -> exn :> exn | _ -> ()
                match value4 with Error exn -> exn :> exn | _ -> ()
                match value5 with Error exn -> exn :> exn | _ -> ()
                match value6 with Error exn -> exn :> exn | _ -> ()
            ] |> AggregateException |> Error)
    
    member inline this.Bind7(value: Result<'a, #exn>, value2: Result<'b, #exn>, value3: Result<'c, #exn>, value4: Result<'d, #exn>, value5: Result<'e, #exn>,
                             value6: Result<'f, #exn>, value7: Result<'g, #exn>, [<InlineIfLambda>] next: 'a * 'b * 'c * 'd * 'e * 'f * 'g -> ResultCode<'h, AggregateException>) =
        fun() ->
            match value, value2, value3, value4, value5, value6, value7 with
            | Ok value, Ok value2, Ok value3, Ok value4, Ok value5, Ok value6, Ok value7 -> (next (value, value2, value3, value4, value5, value6, value7))()
            | _ -> ([
                match value with Error exn -> exn :> exn | _ -> ()
                match value2 with Error exn -> exn :> exn | _ -> ()
                match value3 with Error exn -> exn :> exn | _ -> ()
                match value4 with Error exn -> exn :> exn | _ -> ()
                match value5 with Error exn -> exn :> exn | _ -> ()
                match value6 with Error exn -> exn :> exn | _ -> ()
                match value7 with Error exn -> exn :> exn | _ -> ()
            ] |> AggregateException |> Error)

type ExnResultBuilder() =
    inherit ResultBuilderBase()
    member inline this.Run<'a>([<InlineIfLambda>] code: ResultCode<'a, exn>) =
        try
            code()
        with e ->
            Error e

    // TODO: A less allocating way

    member inline _.Bind(res1: Result<'a, #exn>, [<InlineIfLambda>] task2: 'a -> ResultCode<'c, exn>) : ResultCode<'c, exn> =
        fun () ->
            match res1 with
            | Error error -> Error (error :> exn)
            | Ok v -> (task2 v)()

    member inline this.Bind2(value: Result<'a, #exn>, value2: Result<'b, #exn>, [<InlineIfLambda>] next: 'a * 'b -> ResultCode<'c, exn>) =
        fun() ->
            match value, value2 with
            | Ok value, Ok value2 -> (next (value, value2))()
            | Error exn, Error exn2 -> AggregateException(exn :> exn, exn2 :> exn) :> exn |> Error
            | Error exn, _ -> Error (AggregateException(exn :> exn) :> exn)
            | _, Error exn -> Error (AggregateException(exn :> exn) :> exn)

    member inline this.Bind3(value: Result<'a, #exn>, value2: Result<'b, #exn>, value3: Result<'c, #exn>, [<InlineIfLambda>] next: 'a * 'b * 'c -> ResultCode<'d, exn>) =
        fun() ->
            match value, value2, value3 with
            | Ok value, Ok value2, Ok value3 -> (next (value, value2, value3))()
            | _ -> ([
                match value with Error exn -> exn :> exn | _ -> ()
                match value2 with Error exn -> exn :> exn | _ -> ()
                match value3 with Error exn -> exn :> exn | _ -> ()
                ] |> AggregateException :> exn |> Error)

    member inline this.Bind4(value: Result<'a, #exn>, value2: Result<'b, #exn>, value3: Result<'c, #exn>, value4: Result<'d, #exn>, [<InlineIfLambda>] next: 'a * 'b * 'c * 'd -> ResultCode<'e, exn>) =
        fun() ->
            match value, value2, value3, value4 with
            | Ok value, Ok value2, Ok value3, Ok value4 -> (next (value, value2, value3, value4))()
            | _ -> ([
                match value with Error exn -> exn :> exn | _ -> ()
                match value2 with Error exn -> exn :> exn | _ -> ()
                match value3 with Error exn -> exn :> exn | _ -> ()
                match value4 with Error exn -> exn :> exn | _ -> ()
                ] |> AggregateException :> exn |> Error)

    member inline this.Bind5(value: Result<'a, #exn>, value2: Result<'b, #exn>, value3: Result<'c, #exn>, value4: Result<'d, #exn>, value5: Result<'e, #exn>,
                             [<InlineIfLambda>] next: 'a * 'b * 'c * 'd * 'e -> ResultCode<'f, exn>) =
        fun() ->
            match value, value2, value3, value4, value5 with
            | Ok value, Ok value2, Ok value3, Ok value4, Ok value5 -> (next (value, value2, value3, value4, value5))()
            | _ -> ([
                match value with Error exn -> exn :> exn | _ -> ()
                match value2 with Error exn -> exn :> exn | _ -> ()
                match value3 with Error exn -> exn :> exn | _ -> ()
                match value4 with Error exn -> exn :> exn | _ -> ()
                match value5 with Error exn -> exn :> exn | _ -> ()
            ] |> AggregateException :> exn |> Error)

    member inline this.Bind6(value: Result<'a, #exn>, value2: Result<'b, #exn>, value3: Result<'c, #exn>, value4: Result<'d, #exn>, value5: Result<'e, #exn>,
                             value6: Result<'f, #exn>, [<InlineIfLambda>] next: 'a * 'b * 'c * 'd * 'e * 'f -> ResultCode<'g, exn>) =
        fun() ->
            match value, value2, value3, value4, value5, value6 with
            | Ok value, Ok value2, Ok value3, Ok value4, Ok value5, Ok value6 -> (next (value, value2, value3, value4, value5, value6))()
            | _ -> ([
                match value with Error exn -> exn :> exn | _ -> ()
                match value2 with Error exn -> exn :> exn | _ -> ()
                match value3 with Error exn -> exn :> exn | _ -> ()
                match value4 with Error exn -> exn :> exn | _ -> ()
                match value5 with Error exn -> exn :> exn | _ -> ()
                match value6 with Error exn -> exn :> exn | _ -> ()
            ] |> AggregateException :> exn |> Error)

    member inline this.Bind7(value: Result<'a, #exn>, value2: Result<'b, #exn>, value3: Result<'c, #exn>, value4: Result<'d, #exn>, value5: Result<'e, #exn>,
                             value6: Result<'f, #exn>, value7: Result<'g, #exn>, [<InlineIfLambda>] next: 'a * 'b * 'c * 'd * 'e * 'f * 'g -> ResultCode<'h, exn>) =
        fun() ->
            match value, value2, value3, value4, value5, value6, value7 with
            | Ok value, Ok value2, Ok value3, Ok value4, Ok value5, Ok value6, Ok value7 -> (next (value, value2, value3, value4, value5, value6, value7))()
            | _ -> ([
                match value with Error exn -> exn :> exn | _ -> ()
                match value2 with Error exn -> exn :> exn | _ -> ()
                match value3 with Error exn -> exn :> exn | _ -> ()
                match value4 with Error exn -> exn :> exn | _ -> ()
                match value5 with Error exn -> exn :> exn | _ -> ()
                match value6 with Error exn -> exn :> exn | _ -> ()
                match value7 with Error exn -> exn :> exn | _ -> ()
            ] |> AggregateException :> exn |> Error)

let result = ResultBuilder()
let eresult = EResultBuilder()
let exnresult = ExnResultBuilder()