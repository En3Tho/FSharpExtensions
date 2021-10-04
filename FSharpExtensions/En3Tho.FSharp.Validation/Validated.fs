namespace En3Tho.FSharp.Validation

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.Extensions

// TODO: Investigate if only a single type is needed through validator combinations

type [<Struct; IsReadOnly>] Validated10<'value, 'validator when 'validator: struct
                                                            and 'validator: (new: unit -> 'validator)
                                                            and 'validator :> IValidator<'value>>
#if DEBUG
    private (value: 'value, wasValidated: bool) =
    private new(value) = Validated10(value, true)
    member _.Value =
        if not wasValidated then invalidOp "Value was not properly validated"
        else value
    #else
    private (value: 'value) =
    member _.Value = value
#endif

    override this.ToString() = this.Value.ToString()

    static member Try value : ExnResult<Validated10<'value, 'validator>> =
        match (new 'validator()).Validate value with
        | Ok value -> Validated10 value |> Ok
        | Error err -> Error err

type [<Struct; IsReadOnly>] Validated01<'value, 'validator when 'validator :> IValidator<'value>>
#if DEBUG
    private (value: 'value, wasValidated: bool) =
    private new(value) = Validated01(value, true)
    member _.Value =
        if not wasValidated then invalidOp "Value was not properly validated"
        else value
#else
    private (value: 'value, validator: 'validator) =
    member _.Value = value
    member internal _.Validator = validator
#endif

    static member Try (value, validator: 'validator) : ExnResult<Validated01<'value, 'validator>> =
        match validator.Validate value with
        | Ok value -> Validated01(value, validator) |> Ok
        | Error err -> Error err

type [<Struct; IsReadOnly>] Validated20<'value, 'validator, 'validator2 when 'validator: struct
                                                                         and 'validator: (new: unit -> 'validator)
                                                                         and 'validator :> IValidator<'value>
                                                                         and 'validator2: struct
                                                                         and 'validator2: (new: unit -> 'validator2)
                                                                         and 'validator2 :> IValidator<'value>>
#if DEBUG
    private (value: 'value, isValidated: bool) =
    private new (value) = Validated20(value, true)
    member _.Value =
        if not isValidated then invalidOp "Value was not properly validated"
        else value
#else
    private (value: 'value) =

    member _.Value = value
#endif
    static member Try value : ExnResult<Validated20<'value, 'validator, 'validator2>> =
        match (new 'validator()).Validate value with
        | Ok value ->
            match (new 'validator2()).Validate value with
            | Ok value -> Validated20(value) |> Ok
            | Error err -> Error err
        | Error err -> Error err

    static member TryAggregate value : EResult<Validated20<'value, 'validator, 'validator2>, _> =
        match (new 'validator()).Validate value, (new 'validator2()).Validate value with
        | Ok _, Ok _ ->
            Validated20(value) |> Ok
        | Error exn1, Error exn2 ->
            Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            Error (AggregateException(exn))

type [<Struct; IsReadOnly>] Validated02<'value, 'validator, 'validator2 when 'validator :> IValidator<'value>
                                                                         and 'validator2 :> IValidator<'value>>
#if DEBUG
    private (value: 'value, isValidated: bool) =
    private new (value) = Validated02(value, true)
    member _.Value =
        if not isValidated then invalidOp "Value was not properly validated"
        else value
#else
    private (value: 'value, validator: 'validator, validator2: 'validator2) =

    member _.Value = value
    member internal _.Validator = validator
    member internal _.Validator2 = validator2
#endif
    static member Try(value, validator: 'validator, validator2: 'validator2) : ExnResult<Validated02<'value, 'validator, 'validator2>> =
        match validator.Validate value with
        | Ok value ->
            match validator2.Validate value with
            | Ok value -> Validated02(value, validator, validator2) |> Ok
            | Error err -> Error err
        | Error err -> Error err

    static member TryAggregate(value, validator: 'validator, validator2: 'validator2) : EResult<Validated02<'value, 'validator, 'validator2>, _> =
        match validator.Validate value, validator2.Validate value with
        | Ok _, Ok _ ->
            Validated02(value, validator, validator2) |> Ok
        | Error exn1, Error exn2 ->
            Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            Error (AggregateException(exn))

type [<Struct; IsReadOnly>] Validated11<'value, 'validator, 'validator2 when 'validator: struct
                                                                         and 'validator: (new: unit -> 'validator)
                                                                         and 'validator :> IValidator<'value>
                                                                         and 'validator2 :> IValidator<'value>>
#if DEBUG
    private (value: 'value, wasValidated: bool) =
    private new value = Validated11(value, true)
    member _.Value =
        if not wasValidated then invalidOp "Value was not properly validated"
        else value
#else
    private (value: 'value, validator: 'validator2) =

    member _.Value = value
    member internal _.Validator = validator
#endif
    static member Try(value, validator2: 'validator2) : ExnResult<Validated11<'value, 'validator, 'validator2>> =
        match (new 'validator()).Validate value with
        | Ok value ->
            match validator2.Validate value with
            | Ok value -> Validated11(value, validator2) |> Ok
            | Error err -> Error err
        | Error err -> Error err

    static member TryAggregate(value, validator2: 'validator2) : EResult<Validated11<'value, 'validator, 'validator2>, _> =
        match (new 'validator()).Validate value, validator2.Validate value with
        | Ok _, Ok _ ->
            Validated11(value, validator2) |> Ok
        | Error exn1, Error exn2 ->
            Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            Error (AggregateException(exn))
            
type [<Struct; IsReadOnly>] Validated30<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                      and 'validator: (new: unit -> 'validator)
                                                                                      and 'validator :> IValidator<'value>
                                                                                      and 'validator2: struct
                                                                                      and 'validator2: (new: unit -> 'validator2)
                                                                                      and 'validator2 :> IValidator<'value>
                                                                                      and 'validator3: struct
                                                                                      and 'validator3: (new: unit -> 'validator3)
                                                                                      and 'validator3 :> IValidator<'value>>
#if DEBUG
    private (value: 'value, isValidated: bool) =
    private new (value) = Validated20(value, true)
    member _.Value =
        if not isValidated then invalidOp "Value was not properly validated"
        else value
#else
    private (value: 'value) =

    member _.Value = value
#endif
    static member Try value : ExnResult<Validated30<'value, 'validator, 'validator2, 'validator3>> =
        match (new 'validator()).Validate value with
        | Ok value ->
            match (new 'validator2()).Validate value with
            | Ok value ->
                match (new 'validator3()).Validate value with
                | Ok value -> Validated30(value) |> Ok
                | Error err -> Error err
            | Error err -> Error err
        | Error err -> Error err

    static member TryAggregate value : EResult<Validated30<'value, 'validator, 'validator2, 'validator3>, _> =
        match (new 'validator()).Validate value, (new 'validator2()).Validate value, (new 'validator3()).Validate value with
        | Ok _, Ok _, Ok _ ->
            Validated30(value) |> Ok
        | Error exn1, Error exn2, Error exn3 ->
            Error (AggregateException(exn1, exn2, exn3))
        | Error exn, Error exn2, _
        | Error exn, _, Error exn2
        | _, Error exn, Error exn2 ->
            Error (AggregateException(exn, exn2))
        | Error exn, _, _
        | _, Error exn, _
        | _, _, Error exn ->
            Error (AggregateException(exn))

type [<Struct; IsReadOnly>] Validated21<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                      and 'validator: (new: unit -> 'validator)
                                                                                      and 'validator :> IValidator<'value>
                                                                                      and 'validator2: struct
                                                                                      and 'validator2: (new: unit -> 'validator2)
                                                                                      and 'validator2 :> IValidator<'value>
                                                                                      and 'validator3 :> IValidator<'value>>
#if DEBUG
    private (value: 'value, isValidated: bool) =
    private new (value) = Validated20(value, true)
    member _.Value =
        if not isValidated then invalidOp "Value was not properly validated"
        else value
#else
    private (value: 'value, validator: 'validator3) =

    member _.Value = value
    member internal _.Validator = validator
#endif
    static member Try(value, validator3: 'validator3) : ExnResult<Validated21<'value, 'validator, 'validator2, 'validator3>> =
        match (new 'validator()).Validate value with
        | Ok value ->
            match (new 'validator2()).Validate value with
            | Ok value ->
                match validator3.Validate value with
                | Ok value -> Validated21(value, validator3) |> Ok
                | Error err -> Error err
            | Error err -> Error err
        | Error err -> Error err

    static member TryAggregate(value, validator3: 'validator3) : EResult<Validated21<'value, 'validator, 'validator2, 'validator3>, _> =
        match (new 'validator()).Validate value, (new 'validator2()).Validate value, validator3.Validate value with
        | Ok _, Ok _, Ok _ ->
            Validated21(value, validator3) |> Ok
        | Error exn1, Error exn2, Error exn3 ->
            Error (AggregateException(exn1, exn2, exn3))
        | Error exn, Error exn2, _
        | Error exn, _, Error exn2
        | _, Error exn, Error exn2 ->
            Error (AggregateException(exn, exn2))
        | Error exn, _, _
        | _, Error exn, _
        | _, _, Error exn ->
            Error (AggregateException(exn))

type [<Struct; IsReadOnly>] Validated12<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                      and 'validator: (new: unit -> 'validator)
                                                                                      and 'validator :> IValidator<'value>
                                                                                      and 'validator2 :> IValidator<'value>
                                                                                      and 'validator3 :> IValidator<'value>>
#if DEBUG
    private (value: 'value, isValidated: bool) =
    private new (value) = Validated20(value, true)
    member _.Value =
        if not isValidated then invalidOp "Value was not properly validated"
        else value
#else
    private (value: 'value, validator: 'validator2, validator2: 'validator3) =

    member _.Value = value
    member internal _.Validator = validator
    member internal _.Validator2 = validator2

#endif
    static member Try(value, validator2: 'validator2, validator3: 'validator3) : ExnResult<Validated12<'value, 'validator, 'validator2, 'validator3>> =
        match (new 'validator()).Validate value with
        | Ok value ->
            match validator2.Validate value with
            | Ok value ->
                match validator3.Validate value with
                | Ok value -> Validated12(value, validator2, validator3) |> Ok
                | Error err -> Error err
            | Error err -> Error err
        | Error err -> Error err

    static member TryAggregate(value, validator2: 'validator2, validator3: 'validator3) : EResult<Validated12<'value, 'validator, 'validator2, 'validator3>, _> =
        match (new 'validator()).Validate value, validator2.Validate value, validator3.Validate value with
        | Ok _, Ok _, Ok _ ->
            Validated12(value, validator2, validator3) |> Ok
        | Error exn1, Error exn2, Error exn3 ->
            Error (AggregateException(exn1, exn2, exn3))
        | Error exn, Error exn2, _
        | Error exn, _, Error exn2
        | _, Error exn, Error exn2 ->
            Error (AggregateException(exn, exn2))
        | Error exn, _, _
        | _, Error exn, _
        | _, _, Error exn ->
            Error (AggregateException(exn))

type [<Struct; IsReadOnly>] Validated03<'value, 'validator, 'validator2, 'validator3 when 'validator :> IValidator<'value>
                                                                                      and 'validator2 :> IValidator<'value>
                                                                                      and 'validator3 :> IValidator<'value>>
#if DEBUG
    private (value: 'value, isValidated: bool) =
    private new (value) = Validated20(value, true)
    member _.Value =
        if not isValidated then invalidOp "Value was not properly validated"
        else value
#else
    private (value: 'value, validator: 'validator, validator2: 'validator2, validator3: 'validator3) =

    member _.Value = value
    member internal _.Validator = validator
    member internal _.Validator2 = validator2
    member internal _.Validator3 = validator3

#endif
    static member Try(value, validator: 'validator, validator2: 'validator2, validator3: 'validator3) : ExnResult<Validated03<'value, 'validator, 'validator2, 'validator3>> =
        match validator.Validate value with
        | Ok value ->
            match validator2.Validate value with
            | Ok value ->
                match validator3.Validate value with
                | Ok value -> Validated03(value, validator, validator2, validator3) |> Ok
                | Error err -> Error err
            | Error err -> Error err
        | Error err -> Error err

    static member TryAggregate(value, validator: 'validator, validator2: 'validator2, validator3: 'validator3) : EResult<Validated03<'value, 'validator, 'validator2, 'validator3>, _> =
        match validator.Validate value, validator2.Validate value, validator3.Validate value with
        | Ok _, Ok _, Ok _ ->
            Validated03(value, validator, validator2, validator3) |> Ok
        | Error exn1, Error exn2, Error exn3 ->
            Error (AggregateException(exn1, exn2, exn3))
        | Error exn, Error exn2, _
        | Error exn, _, Error exn2
        | _, Error exn, Error exn2 ->
            Error (AggregateException(exn, exn2))
        | Error exn, _, _
        | _, Error exn, _
        | _, _, Error exn ->
            Error (AggregateException(exn))