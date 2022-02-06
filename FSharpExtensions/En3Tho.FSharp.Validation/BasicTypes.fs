namespace En3Tho.FSharp.Validation

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.Extensions
open FSharp.Control.Tasks

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

type [<Struct; IsReadOnly>] NewCtorValidatorValidated<'value, 'validator when 'validator: (new: unit -> 'validator)
                                                                          and 'validator :> IValidator<'value>>
#if DEBUG || CHECKED
    private (value: 'value, validator: 'validator, wasValidated: bool) =
    private new(value, validator) = NewCtorValidatorValidated(value, validator, true)
    member _.Value = if not wasValidated then invalidOp "Value was not properly validated" else value
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

    static member TryAggregate (valueOption: 'value voption) =
        match valueOption with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match NewCtorValidatorValidated<'value, 'validator>.TryAggregate value with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member MakeAggregate (valueOption: 'value voption) =
        NewCtorValidatorValidated<'value, 'validator>.TryAggregate(valueOption) |> EResult.unwrap

    static member op_Implicit (validated: NewCtorValidatorValidated<'value, 'validator>) : 'value = validated.Value

type [<Struct; IsReadOnly>] InstanceValidatorValidated<'value, 'validator when 'validator :> IValidator<'value>>
#if DEBUG || CHECKED
    private (value: 'value, validator: 'validator, wasValidated: bool) =
    private new(value, validator) = InstanceValidatorValidated(value, validator, true)
    member _.Value = if not wasValidated then invalidOp "Value was not properly validated" else value
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

    static member TryAggregate(valueOption: 'value voption, validator: 'validator) =
        match valueOption with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match InstanceValidatorValidated<'value, 'validator>.TryAggregate(value, validator) with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member MakeAggregate(valueOption: 'value voption, validator: 'validator) =
        InstanceValidatorValidated<'value, 'validator>.TryAggregate(valueOption, validator) |> EResult.unwrap

    static member op_Implicit (validated: InstanceValidatorValidated<'value, 'validator>) : 'value = validated.Value

type [<Struct; IsReadOnly>] NewCtorAsyncValidatorValidated<'value, 'validator when 'validator: (new: unit -> 'validator)
                                                                               and 'validator :> IAsyncValidator<'value>>
#if DEBUG || CHECKED
    private (value: 'value, validator: 'validator, wasValidated: bool) =
    private new(value, validator) = NewCtorAsyncValidatorValidated(value, validator, true)
    member _.Value = if not wasValidated then invalidOp "Value was not properly validated" else value
#else
    private (value: 'value, validator: 'validator) =
    member _.Value = value
#endif
    member internal _.Validator = validator

    override this.ToString() = value.ToString()

    static member Try value = vtask {
        let validator = (new 'validator())
        match! validator.Validate value with
        | Ok value -> return NewCtorAsyncValidatorValidated(value, validator) |> Ok
        | Error err -> return Error err
    }

    static member Make (value: 'value) = vtask {
        let! result = NewCtorAsyncValidatorValidated<'value, 'validator>.Try(value)
        return result |> EResult.unwrap
    }

    member this.MapTry (map: 'value -> 'value) =
        NewCtorAsyncValidatorValidated<'value, 'validator>.Try(map this.Value)

    member this.MapMake (map: 'value -> 'value) =
        NewCtorAsyncValidatorValidated<'value, 'validator>.Make(map this.Value)

    static member Try (valueOption: 'value voption) = vtask {
        match valueOption with
        | ValueNone -> return ValueNone |> Ok
        | ValueSome value ->
            match! NewCtorAsyncValidatorValidated<'value, 'validator>.Try value with
            | Ok value -> return value |> ValueSome |> Ok
            | Error err -> return Error err
    }

    static member Make (valueOption: 'value voption) = vtask {
        let! result = NewCtorAsyncValidatorValidated<'value, 'validator>.Try(valueOption)
        return result |> EResult.unwrap
    }

    static member TryAggregate value = vtask {
        let validator = (new 'validator())
        match! validator.ValidateAggregate value with
        | Ok value -> return NewCtorAsyncValidatorValidated(value, validator) |> Ok
        | Error err -> return Error err
    }

    static member MakeAggregate (value: 'value) = vtask {
        let! result = NewCtorAsyncValidatorValidated<'value, 'validator>.TryAggregate(value)
        return result |> EResult.unwrap
    }

    member this.MapTryAggregate (map: 'value -> 'value) =
        NewCtorAsyncValidatorValidated<'value, 'validator>.TryAggregate(map this.Value)

    member this.MapMakeAggregate (map: 'value -> 'value) =
        NewCtorAsyncValidatorValidated<'value, 'validator>.MakeAggregate(map this.Value)

    static member TryAggregate (valueOption: 'value voption) = vtask {
        match valueOption with
        | ValueNone -> return ValueNone |> Ok
        | ValueSome value ->
            match! NewCtorAsyncValidatorValidated<'value, 'validator>.TryAggregate value with
            | Ok value -> return value |> ValueSome |> Ok
            | Error err -> return Error err
    }

    static member MakeAggregate (valueOption: 'value voption) = vtask {
        let! result = NewCtorAsyncValidatorValidated<'value, 'validator>.TryAggregate(valueOption)
        return result |> EResult.unwrap
    }

    static member op_Implicit (validated: NewCtorAsyncValidatorValidated<'value, 'validator>) : 'value = validated.Value

type [<Struct; IsReadOnly>] InstanceAsyncValidatorValidated<'value, 'validator when 'validator :> IAsyncValidator<'value>>
#if DEBUG || CHECKED
    private (value: 'value, validator: 'validator, wasValidated: bool) =
    private new(value, validator) = InstanceAsyncValidatorValidated(value, validator, true)
    member _.Value = if not wasValidated then invalidOp "Value was not properly validated" else value
#else
    private (value: 'value, validator: 'validator) =
    member _.Value = value
#endif
    member internal _.Validator = validator

    override this.ToString() = value.ToString()

    static member Try(value: 'value, validator: 'validator) = vtask {
        match! validator.Validate value with
        | Ok value -> return InstanceAsyncValidatorValidated(value, validator) |> Ok
        | Error err -> return Error err
    }

    static member Make(value: 'value, validator: 'validator) = vtask {
        let! result = InstanceAsyncValidatorValidated<'value, 'validator>.Try(value, validator)
        return result  |> EResult.unwrap
    }

    member this.MapTry (map: 'value -> 'value) =
        InstanceAsyncValidatorValidated<'value, 'validator>.Try(map this.Value, this.Validator)

    member this.MapMake (map: 'value -> 'value) =
        InstanceAsyncValidatorValidated<'value, 'validator>.Make(map this.Value, this.Validator)

    static member Try(valueOption: 'value voption, validator: 'validator) = vtask {
        match valueOption with
        | ValueNone -> return ValueNone |> Ok
        | ValueSome value ->
            match! InstanceAsyncValidatorValidated<'value, 'validator>.Try(value, validator) with
            | Ok value -> return value |> ValueSome |> Ok
            | Error err -> return Error err
    }

    static member Make(valueOption: 'value voption, validator: 'validator) = vtask {
        let! result = InstanceAsyncValidatorValidated<'value, 'validator>.Try(valueOption, validator)
        return result |> EResult.unwrap
    }

    static member TryAggregate(value: 'value, validator: 'validator) = vtask {
        match! validator.ValidateAggregate value with
        | Ok value -> return InstanceAsyncValidatorValidated(value, validator) |> Ok
        | Error err -> return Error err
    }

    static member MakeAggregate(value: 'value, validator: 'validator) = vtask {
        let! result = InstanceAsyncValidatorValidated<'value, 'validator>.TryAggregate(value, validator)
        return result |> EResult.unwrap
    }

    member this.MapTryAggregate (map: 'value -> 'value) =
        InstanceAsyncValidatorValidated<'value, 'validator>.TryAggregate(map this.Value, this.Validator)

    member this.MapMakeAggregate (map: 'value -> 'value) =
        InstanceAsyncValidatorValidated<'value, 'validator>.MakeAggregate(map this.Value, this.Validator)

    static member TryAggregate(valueOption: 'value voption, validator: 'validator) = vtask {
        match valueOption with
        | ValueNone -> return ValueNone |> Ok
        | ValueSome value ->
            match! InstanceAsyncValidatorValidated<'value, 'validator>.TryAggregate(value, validator) with
            | Ok value -> return value |> ValueSome |> Ok
            | Error err -> return Error err
    }

    static member MakeAggregate(valueOption: 'value voption, validator: 'validator) =vtask {
        let! result = InstanceAsyncValidatorValidated<'value, 'validator>.TryAggregate(valueOption, validator)
        return result |> EResult.unwrap
    }

    static member op_Implicit (validated: InstanceAsyncValidatorValidated<'value, 'validator>) : 'value = validated.Value

#noward "0043"