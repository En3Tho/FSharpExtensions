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

type [<Struct; IsReadOnly>] NewCtorValidatorValidated<'value, 'validator when 'validator :> IValidator<'value>
                                                                and 'validator: (new: unit -> 'validator)
                                                                and 'validator :> IValidator<'value>>
#if DEBUG
    private (value: 'value, wasValidated: bool) =
    private new(value) = Validated10(value, true)
#else
    private (value: 'value, validator: 'validator) =
    member _.Value = value
#endif
    member internal _.Validator = validator

    override this.ToString() = value.ToString()

    static member Try value =
        let validator = (new 'validator())
        match validator.Validate value with
        | Ok value -> NewCtorValidatorValidated(value, validator) |> Ok
        | Error err -> Error err

    static member Make (value: 'value) =
        NewCtorValidatorValidated<'value, 'validator>.Try(value) |> EResult.unwrap

    member this.MapTry (map: 'value -> 'value) =
        NewCtorValidatorValidated<'value, 'validator>.Try(map this.Value)

    member this.MapMake (map: 'value -> 'value) =
        NewCtorValidatorValidated<'value, 'validator>.Make(map this.Value)

    static member Try (valueOption: 'value voption) =
        match valueOption with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match NewCtorValidatorValidated<'value, 'validator>.Try value with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member Make (valueOption: 'value voption) =
        NewCtorValidatorValidated<'value, 'validator>.Try(valueOption) |> EResult.unwrap

    static member TryAggregate value =
        let validator = (new 'validator())
        match validator.ValidateAggregate value with
        | Ok value -> NewCtorValidatorValidated(value, validator) |> Ok
        | Error err -> Error err

    static member MakeAggregate (value: 'value) =
        NewCtorValidatorValidated<'value, 'validator>.TryAggregate(value) |> EResult.unwrap

    member this.MapTryAggregate (map: 'value -> 'value) =
        NewCtorValidatorValidated<'value, 'validator>.TryAggregate(map this.Value)

    member this.MapMakeAggregate (map: 'value -> 'value) =
        NewCtorValidatorValidated<'value, 'validator>.MakeAggregate(map this.Value)

    static member TryAggregate (value: 'value voption) =
        match value with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match NewCtorValidatorValidated<'value, 'validator>.TryAggregate value with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member MakeAggregate (value: 'value voption) =
        NewCtorValidatorValidated<'value, 'validator>.TryAggregate(value) |> EResult.unwrap

type [<Struct; IsReadOnly>] InstanceValidatorValidated<'value, 'validator when 'validator :> IValidator<'value>>
#if DEBUG
    private (value: 'value, wasValidated: bool) =
    private new(value) = Validated10(value, true)
#else
    private (value: 'value, validator: 'validator) =
    member _.Value = value
#endif
    member internal _.Validator = validator

    override this.ToString() = value.ToString()

    static member Try(value: 'value, validator: 'validator) =
        match validator.Validate value with
        | Ok value -> InstanceValidatorValidated(value, validator) |> Ok
        | Error err -> Error err

    static member Make(value: 'value, validator: 'validator) =
        InstanceValidatorValidated<'value, 'validator>.Try(value, validator) |> EResult.unwrap

    member this.MapTry (map: 'value -> 'value) =
        InstanceValidatorValidated<'value, 'validator>.Try(map this.Value, this.Validator)

    member this.MapMake (map: 'value -> 'value) =
        InstanceValidatorValidated<'value, 'validator>.Make(map this.Value, this.Validator)

    static member Try(valueOption: 'value voption, validator: 'validator) =
        match valueOption with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match InstanceValidatorValidated<'value, 'validator>.Try(value, validator) with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member Make(valueOption: 'value voption, validator: 'validator) =
        InstanceValidatorValidated<'value, 'validator>.Try(valueOption, validator) |> EResult.unwrap

    static member TryAggregate(value: 'value, validator: 'validator) =
        match validator.ValidateAggregate value with
        | Ok value -> InstanceValidatorValidated(value, validator) |> Ok
        | Error err -> Error err

    static member MakeAggregate(value: 'value, validator: 'validator) =
        InstanceValidatorValidated<'value, 'validator>.TryAggregate(value, validator) |> EResult.unwrap

    member this.MapTryAggregate (map: 'value -> 'value) =
        InstanceValidatorValidated<'value, 'validator>.TryAggregate(map this.Value, this.Validator)

    member this.MapMakeAggregate (map: 'value -> 'value) =
        InstanceValidatorValidated<'value, 'validator>.MakeAggregate(map this.Value, this.Validator)

    static member TryAggregate(value: 'value voption, validator: 'validator) =
        match value with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match InstanceValidatorValidated<'value, 'validator>.TryAggregate(value, validator) with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member MakeAggregate(value: 'value voption, validator: 'validator) =
        InstanceValidatorValidated<'value, 'validator>.TryAggregate(value, validator) |> EResult.unwrap

#noward "0043"