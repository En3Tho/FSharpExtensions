namespace En3Tho.FSharp.Validation

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.Extensions

// TODO: ResourceManagement, Localization

// TODO: wait for ErasedUnions and experiment with better function signatures

/// Indicates an error when trying to map DTO to domain type. Maps to BadRequest in REST.
type ValidationException(message, innerException: Exception) =
    inherit Exception(message, innerException)
    new (message) = ValidationException(message, null)

/// Indicates a domain processing error. Maps to UnprocessableEntity in REST.
type ProcessingException(message, innerException: Exception) =
    inherit Exception(message, innerException)
    new (message) = ProcessingException(message, null)

type IAsyncValidator<'value> =
    abstract member Validate: 'value -> ExnResult<'value> ValueTask
    abstract member ValidateAggregate: 'value -> EResult<'value, AggregateException> ValueTask

type IValidator<'value> =
    inherit IAsyncValidator<'value>
    abstract member Validate: 'value -> ExnResult<'value>
    abstract member ValidateAggregate: 'value -> EResult<'value, AggregateException>

type [<Struct; IsReadOnly>] Validated<'value, 'validator when 'validator :> IValidator<'value>>
#if DEBUG
    private (value: 'value, wasValidated: bool) =
    private new(value) = Validated10(value, true)
#else
    private (value: 'value, validator: 'validator) =
    member _.Value = value
#endif
    member internal _.Validator = validator

    override this.ToString() = value.ToString()

    static member Try<'validator2 when 'validator2: struct
                                   and 'validator2: (new: unit -> 'validator2)
                                   and 'validator2 :> IValidator<'value>> value =
        let validator = (new 'validator2())
        match validator.Validate value with
        | Ok value -> Validated(value, validator) |> Ok
        | Error err -> Error err

    static member Try(value: 'value, validator: 'validator) =
        match validator.Validate value with
        | Ok value -> Validated(value, validator) |> Ok
        | Error err -> Error err

    static member Make<'validator2 when 'validator2: struct
                                    and 'validator2: (new: unit -> 'validator2)
                                    and 'validator2 :> IValidator<'value>> (value: 'value) =
        Validated.Try<'validator2>(value) |> EResult.unwrap

    static member Make(value: 'value, validator: 'validator) =
        Validated<'value, 'validator>.Try(value, validator) |> EResult.unwrap

    member this.MapTry (map: 'value -> 'value) =
        Validated<'value, 'validator>.Try(map this.Value, this.Validator)

    member this.MapMake (map: 'value -> 'value) =
        Validated<'value, 'validator>.Make(map this.Value, this.Validator)

    static member Try<'validator2 when 'validator2: struct
                                   and 'validator2: (new: unit -> 'validator2)
                                   and 'validator2 :> IValidator<'value>> (value: 'value voption) =
        match value with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match Validated.Try<'validator2> value with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member Make<'validator2 when 'validator2: struct
                                    and 'validator2: (new: unit -> 'validator2)
                                    and 'validator2 :> IValidator<'value>> (value: 'value voption) =
        Validated.Try<'validator2>(value) |> EResult.unwrap

    static member Try(value: 'value voption, validator: 'validator) =
        match value with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match Validated<'value, 'validator>.Try(value, validator) with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member Make(value: 'value voption, validator: 'validator) =
        Validated<'value, 'validator>.Try(value, validator) |> EResult.unwrap

    //

    static member TryAggregate<'validator2 when 'validator2: struct
                                            and 'validator2: (new: unit -> 'validator2)
                                            and 'validator2 :> IValidator<'value>> value =
        let validator = (new 'validator2())
        match validator.ValidateAggregate value with
        | Ok value -> Validated(value, validator) |> Ok
        | Error err -> Error err

    static member TryAggregate(value: 'value, validator: 'validator) =
        match validator.ValidateAggregate value with
        | Ok value -> Validated(value, validator) |> Ok
        | Error err -> Error err

    static member MakeAggregate<'validator2 when 'validator2: struct
                                             and 'validator2: (new: unit -> 'validator2)
                                             and 'validator2 :> IValidator<'value>> (value: 'value) =
        Validated.TryAggregate<'validator2>(value) |> EResult.unwrap

    static member MakeAggregate(value: 'value, validator: 'validator) =
        Validated<'value, 'validator>.TryAggregate(value, validator) |> EResult.unwrap

    member this.MapTryAggregate (map: 'value -> 'value) =
        Validated<'value, 'validator>.TryAggregate(map this.Value, this.Validator)

    member this.MapMakeAggregate (map: 'value -> 'value) =
        Validated<'value, 'validator>.MakeAggregate(map this.Value, this.Validator)

    static member TryAggregate<'validator2 when 'validator2: struct
                                            and 'validator2: (new: unit -> 'validator2)
                                            and 'validator2 :> IValidator<'value>> (value: 'value voption) =
        match value with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match Validated.TryAggregate<'validator2> value with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member MakeAggregate<'validator2 when 'validator2: struct
                                             and 'validator2: (new: unit -> 'validator2)
                                             and 'validator2 :> IValidator<'value>> (value: 'value voption) =
        Validated.TryAggregate<'validator2>(value) |> EResult.unwrap

    static member TryAggregate(value: 'value voption, validator: 'validator) =
        match value with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match Validated<'value, 'validator>.TryAggregate(value, validator) with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member MakeAggregate(value: 'value voption, validator: 'validator) =
        Validated<'value, 'validator>.TryAggregate(value, validator) |> EResult.unwrap