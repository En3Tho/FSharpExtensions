namespace En3Tho.FSharp.Validation

open System
open System.ComponentModel
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

    static member Of value =
        let validator = (new 'validator())
        match validator.Validate value with
        | Ok value -> NewCtorValidatorValidated(value, validator) |> Ok
        | Error err -> Error err

    static member Make (value: 'value) =
        NewCtorValidatorValidated<'value, 'validator>.Of(value) |> EResult.unwrap

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.MapOf ([<InlineIfLambda>] map: 'value -> 'value) =
        NewCtorValidatorValidated<'value, 'validator>.Of(map this.Value)

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member inline this.MapMake ([<InlineIfLambda>] map: 'value -> 'value) =
        NewCtorValidatorValidated<'value, 'validator>.Make(map this.Value)

    static member OfOption (valueOption: 'value voption) =
        match valueOption with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match NewCtorValidatorValidated<'value, 'validator>.Of value with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member MakeOption (valueOption: 'value voption) =
        NewCtorValidatorValidated<'value, 'validator>.OfOption(valueOption) |> EResult.unwrap

    static member OfAggregate value =
        let validator = (new 'validator())
        match validator.ValidateAggregate value with
        | Ok value -> NewCtorValidatorValidated(value, validator) |> Ok
        | Error err -> Error err

    static member MakeAggregate (value: 'value) =
        NewCtorValidatorValidated<'value, 'validator>.OfAggregate(value) |> EResult.unwrap

    member this.MapTryAggregate (map: 'value -> 'value) =
        NewCtorValidatorValidated<'value, 'validator>.OfAggregate(map this.Value)

    member inline this.MapMakeAggregate ([<InlineIfLambda>] map: 'value -> 'value) =
        NewCtorValidatorValidated<'value, 'validator>.MakeAggregate(map this.Value)

    static member OfOptionAggregate (valueOption: 'value voption) =
        match valueOption with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match NewCtorValidatorValidated<'value, 'validator>.OfAggregate value with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member MakeAggregateV (valueOption: 'value voption) =
        NewCtorValidatorValidated<'value, 'validator>.OfOptionAggregate(valueOption) |> EResult.unwrap

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

    static member Of(value: 'value, validator: 'validator) =
        match validator.Validate value with
        | Ok value -> InstanceValidatorValidated(value, validator) |> Ok
        | Error err -> Error err

    static member Make(value: 'value, validator: 'validator) =
        InstanceValidatorValidated<'value, 'validator>.Of(value, validator) |> EResult.unwrap

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member this.TryValue(value: 'value) =
        InstanceValidatorValidated<'value, 'validator>.Of(value, this.Validator)

    member inline this.MapOf ([<InlineIfLambda>] map: 'value -> 'value) =
        this.TryValue(map this.Value)

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member this.MakeValue(value: 'value) =
        InstanceValidatorValidated<'value, 'validator>.Make(value, this.Validator)

    member inline this.MapMake ([<InlineIfLambda>] map: 'value -> 'value) =
        this.MakeValue(map this.Value)

    static member OfOption(valueOption: 'value voption, validator: 'validator) =
        match valueOption with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match InstanceValidatorValidated<'value, 'validator>.Of(value, validator) with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member MakeOption(valueOption: 'value voption, validator: 'validator) =
        InstanceValidatorValidated<'value, 'validator>.OfOption(valueOption, validator) |> EResult.unwrap

    static member OfAggregate(value: 'value, validator: 'validator) =
        match validator.ValidateAggregate value with
        | Ok value -> InstanceValidatorValidated(value, validator) |> Ok
        | Error err -> Error err

    static member MakeAggregate(value: 'value, validator: 'validator) =
        InstanceValidatorValidated<'value, 'validator>.OfAggregate(value, validator) |> EResult.unwrap

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member this.TryValueAggregate(value: 'value) =
        InstanceValidatorValidated<'value, 'validator>.OfAggregate(value, this.Validator)

    member inline this.MapOfAggregate ([<InlineIfLambda>] map: 'value -> 'value) =
        this.TryValueAggregate(map this.Value)

     [<EditorBrowsable(EditorBrowsableState.Never)>]
     member this.MakeValueAggregate(value: 'value) =
        InstanceValidatorValidated<'value, 'validator>.MakeAggregate(value, this.Validator)

    member inline this.MapMakeAggregate ([<InlineIfLambda>] map: 'value -> 'value) =
        this.MakeValueAggregate(map this.Value)

    static member OfOptionAggregate(valueOption: 'value voption, validator: 'validator) =
        match valueOption with
        | ValueNone -> ValueNone |> Ok
        | ValueSome value ->
            match InstanceValidatorValidated<'value, 'validator>.OfAggregate(value, validator) with
            | Ok value -> value |> ValueSome |> Ok
            | Error err -> Error err

    static member MakeOptionAggregate(valueOption: 'value voption, validator: 'validator) =
        InstanceValidatorValidated<'value, 'validator>.OfOptionAggregate(valueOption, validator) |> EResult.unwrap

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

    static member Of value = vtask {
        let validator = (new 'validator())
        match! validator.Validate value with
        | Ok value -> return NewCtorAsyncValidatorValidated(value, validator) |> Ok
        | Error err -> return Error err
    }

    static member Make (value: 'value) = vtask {
        let! result = NewCtorAsyncValidatorValidated<'value, 'validator>.Of(value)
        return result |> EResult.unwrap
    }

    member inline this.MapOf ([<InlineIfLambda>] map: 'value -> 'value) =
        NewCtorAsyncValidatorValidated<'value, 'validator>.Of(map this.Value)

    member inline this.MapMake ([<InlineIfLambda>] map: 'value -> 'value) =
        NewCtorAsyncValidatorValidated<'value, 'validator>.Make(map this.Value)

    static member OfOption (valueOption: 'value voption) = vtask {
        match valueOption with
        | ValueNone -> return ValueNone |> Ok
        | ValueSome value ->
            match! NewCtorAsyncValidatorValidated<'value, 'validator>.Of value with
            | Ok value -> return value |> ValueSome |> Ok
            | Error err -> return Error err
    }

    static member MakeOption (valueOption: 'value voption) = vtask {
        let! result = NewCtorAsyncValidatorValidated<'value, 'validator>.OfOption(valueOption)
        return result |> EResult.unwrap
    }

    static member OfAggregate value = vtask {
        let validator = (new 'validator())
        match! validator.ValidateAggregate value with
        | Ok value -> return NewCtorAsyncValidatorValidated(value, validator) |> Ok
        | Error err -> return Error err
    }

    static member MakeAggregate (value: 'value) = vtask {
        let! result = NewCtorAsyncValidatorValidated<'value, 'validator>.OfAggregate(value)
        return result |> EResult.unwrap
    }

    member inline this.MapOfAggregate ([<InlineIfLambda>] map: 'value -> 'value) =
        NewCtorAsyncValidatorValidated<'value, 'validator>.OfAggregate(map this.Value)

    member inline this.MapMakeAggregate ([<InlineIfLambda>] map: 'value -> 'value) =
        NewCtorAsyncValidatorValidated<'value, 'validator>.MakeAggregate(map this.Value)

    static member OfOptionAggregate (valueOption: 'value voption) = vtask {
        match valueOption with
        | ValueNone -> return ValueNone |> Ok
        | ValueSome value ->
            match! NewCtorAsyncValidatorValidated<'value, 'validator>.OfAggregate value with
            | Ok value -> return value |> ValueSome |> Ok
            | Error err -> return Error err
    }

    static member MakeOptionAggregate (valueOption: 'value voption) = vtask {
        let! result = NewCtorAsyncValidatorValidated<'value, 'validator>.OfOptionAggregate(valueOption)
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

    static member Of(value: 'value, validator: 'validator) = vtask {
        match! validator.Validate value with
        | Ok value -> return InstanceAsyncValidatorValidated(value, validator) |> Ok
        | Error err -> return Error err
    }

    static member Make(value: 'value, validator: 'validator) = vtask {
        let! result = InstanceAsyncValidatorValidated<'value, 'validator>.Of(value, validator)
        return result  |> EResult.unwrap
    }

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member this.OfValue(value: 'value) =
        InstanceAsyncValidatorValidated<'value, 'validator>.Of(value, this.Validator)

    member inline this.MapOf ([<InlineIfLambda>] map: 'value -> 'value) =
        this.OfValue(map this.Value)

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member this.MakeValue(value: 'value) =
        InstanceAsyncValidatorValidated<'value, 'validator>.Of(value, this.Validator)

    member inline this.MapMake ([<InlineIfLambda>] map: 'value -> 'value) =
        this.MakeValue(map this.Value)

    static member OfOption(valueOption: 'value voption, validator: 'validator) = vtask {
        match valueOption with
        | ValueNone -> return ValueNone |> Ok
        | ValueSome value ->
            match! InstanceAsyncValidatorValidated<'value, 'validator>.Of(value, validator) with
            | Ok value -> return value |> ValueSome |> Ok
            | Error err -> return Error err
    }

    static member MakeOption(valueOption: 'value voption, validator: 'validator) = vtask {
        let! result = InstanceAsyncValidatorValidated<'value, 'validator>.OfOption(valueOption, validator)
        return result |> EResult.unwrap
    }

    static member OfAggregate(value: 'value, validator: 'validator) = vtask {
        match! validator.ValidateAggregate value with
        | Ok value -> return InstanceAsyncValidatorValidated(value, validator) |> Ok
        | Error err -> return Error err
    }

    static member MakeAggregate(value: 'value, validator: 'validator) = vtask {
        let! result = InstanceAsyncValidatorValidated<'value, 'validator>.OfAggregate(value, validator)
        return result |> EResult.unwrap
    }

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member this.OfValueAggregate(value: 'value) =
        InstanceAsyncValidatorValidated<'value, 'validator>.OfAggregate(value, this.Validator)

    member inline this.MapOfAggregate ([<InlineIfLambda>] map: 'value -> 'value) =
        this.OfValueAggregate(map this.Value)

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member this.MakeValueAggregate(value: 'value) =
        InstanceAsyncValidatorValidated<'value, 'validator>.OfAggregate(value, this.Validator)

    member inline this.MapMakeAggregate ([<InlineIfLambda>] map: 'value -> 'value) =
        this.MakeValueAggregate(map this.Value)

    static member OfOptionAggregate(valueOption: 'value voption, validator: 'validator) = vtask {
        match valueOption with
        | ValueNone -> return ValueNone |> Ok
        | ValueSome value ->
            match! InstanceAsyncValidatorValidated<'value, 'validator>.OfAggregate(value, validator) with
            | Ok value -> return value |> ValueSome |> Ok
            | Error err -> return Error err
    }

    static member MakeOptionAggregate(valueOption: 'value voption, validator: 'validator) =vtask {
        let! result = InstanceAsyncValidatorValidated<'value, 'validator>.OfOptionAggregate(valueOption, validator)
        return result |> EResult.unwrap
    }

    static member op_Implicit (validated: InstanceAsyncValidatorValidated<'value, 'validator>) : 'value = validated.Value

#noward "0043"