module En3Tho.FSharp.ComputationExpressions.OptionBuilder

open System

type OptionCode<'T> = unit -> 'T voption

type OptionBuilderBase() =

    member inline _.Zero() : OptionCode<unit> =
        fun() -> ValueSome ()

    member inline _.Delay([<InlineIfLambda>] f: unit -> OptionCode<'T>) : OptionCode<'T> =
        fun () -> (f())()

    member inline _.Combine([<InlineIfLambda>] task1: OptionCode<unit>, [<InlineIfLambda>] task2: OptionCode<'T>) : OptionCode<'T> =
        fun () ->
            match task1() with
            | ValueNone -> ValueNone
            | ValueSome() -> task2()

    member inline _.Bind(res1: 'T1 option, [<InlineIfLambda>] task2: 'T1 -> OptionCode<'T>) : OptionCode<'T> =
        fun () ->
            match res1 with
            | None -> ValueNone
            | Some v -> (task2 v)()

    member inline _.Bind(res1: 'T1 voption, [<InlineIfLambda>] task2: 'T1 -> OptionCode<'T>) : OptionCode<'T> =
        fun () ->
            match res1 with
            | ValueNone -> ValueNone
            | ValueSome v -> (task2 v)()

    member inline _.Bind(res1: 'T1 Nullable, [<InlineIfLambda>] task2: 'T1 -> OptionCode<'T>) : OptionCode<'T> =
        fun () ->
            if res1.HasValue then
                (task2 res1.Value)()
            else
                ValueNone

    member inline _.TryWith([<InlineIfLambda>] body: OptionCode<'T>, [<InlineIfLambda>] catch: exn -> OptionCode<'T>) : OptionCode<'T> =
        fun () ->
            try
                body()
            with exn ->
                (catch exn)()

    member inline _.TryFinally([<InlineIfLambda>] body: OptionCode<'T>, [<InlineIfLambda>] compensation: unit -> unit) : OptionCode<'T> =
        fun () ->
            try
                body()
            finally
                compensation()

    member inline this.Using(disp: #IDisposable, [<InlineIfLambda>] body: #IDisposable -> OptionCode<'T>) : OptionCode<'T> =
        this.TryFinally(
            (fun () -> (body disp)()),
            (fun () -> if not (isNull (box disp)) then disp.Dispose()))

    member inline _.Return(value: 'T) : OptionCode<'T> =
        fun () ->
            ValueSome value

    member inline this.ReturnFrom(source: option<'T>) : OptionCode<'T> =
        fun () ->
            match source with Some x -> ValueSome x | None -> ValueNone

    member inline this.ReturnFrom(source: voption<'T>) : OptionCode<'T> =
        fun () ->
            source

    member inline this.ReturnFrom(source: Nullable<'T>) : OptionCode<'T> =
        fun () ->
            if source.HasValue then ValueSome source.Value else ValueNone

type OptionBuilder() =
    inherit OptionBuilderBase()

    member inline _.Run([<InlineIfLambda>] code: OptionCode<'T>) : 'T option =
         match code() with
         | ValueNone -> None
         | ValueSome v -> Some v

type ValueOptionBuilder() =
    inherit OptionBuilderBase()

    member inline _.Run([<InlineIfLambda>] code: OptionCode<'T>) : 'T voption =
        code()

let option = OptionBuilder()
let voption = ValueOptionBuilder()