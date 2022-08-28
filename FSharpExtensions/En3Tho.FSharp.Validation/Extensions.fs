[<AutoOpen>]
module En3Tho.FSharp.Validation.Extensions

type NewCtorValidatorValidated<'value, 'validator when 'validator: (new: unit -> 'validator) and 'validator :> IValidator<'value>> with
    static member Try (value: 'value) =
        match NewCtorValidatorValidated<'value, 'validator>.Of value with
        | Ok result -> ValueSome result
        | _ -> ValueNone

    static member TryO (value: 'value) =
        match NewCtorValidatorValidated<'value, 'validator>.Of value with
        | Ok result -> Some result
        | _ -> None

    member inline this.MapTry ([<InlineIfLambda>] map: 'value -> 'value) =
        match this.Map map with
        | Ok result -> ValueSome result
        | _ -> ValueNone

    member inline this.MapTryO ([<InlineIfLambda>] map: 'value -> 'value) =
        match this.Map map with
        | Ok result -> Some result
        | _ -> None

type InstanceValidatorValidated<'value, 'validator when 'validator :> IValidator<'value>> with
    static member Try (value: 'value, validator) =
        match InstanceValidatorValidated<'value, 'validator>.Of(value, validator) with
        | Ok result -> ValueSome result
        | _ -> ValueNone

    static member TryO (value: 'value, validator) =
        match InstanceValidatorValidated<'value, 'validator>.Of(value, validator) with
        | Ok result -> Some result
        | _ -> None

    member inline this.MapTry ([<InlineIfLambda>] map: 'value -> 'value) =
        match this.Map map with
        | Ok result -> ValueSome result
        | _ -> ValueNone

    member inline this.MapTryO ([<InlineIfLambda>] map: 'value -> 'value) =
        match this.Map map with
        | Ok result -> Some result
        | _ -> None