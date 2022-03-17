[<AutoOpen>]
module En3Tho.FSharp.Validation.Extensions

type NewCtorValidatorValidated<'value, 'validator when 'validator: (new: unit -> 'validator) and 'validator :> IValidator<'value>> with
    static member TryV (value: 'value) =
        match NewCtorValidatorValidated<'value, 'validator>.Try value with
        | Ok result -> ValueSome result
        | _ -> ValueNone

    static member TryO (value: 'value) =
        match NewCtorValidatorValidated<'value, 'validator>.Try value with
        | Ok result -> Some result
        | _ -> None

    member this.MapTryV (map: 'value -> 'value) =
        match this.MapTry map with
        | Ok result -> ValueSome result
        | _ -> ValueNone

    member this.MapTryO (map: 'value -> 'value) =
        match this.MapTry map with
        | Ok result -> Some result
        | _ -> None

type InstanceValidatorValidated<'value, 'validator when 'validator :> IValidator<'value>> with
    static member TryV (value: 'value, validator) =
        match InstanceValidatorValidated<'value, 'validator>.Try(value, validator) with
        | Ok result -> ValueSome result
        | _ -> ValueNone

    static member TryO (value: 'value, validator) =
        match InstanceValidatorValidated<'value, 'validator>.Try(value, validator) with
        | Ok result -> Some result
        | _ -> None

    member this.MapTryV (map: 'value -> 'value) =
        match this.MapTry map with
        | Ok result -> ValueSome result
        | _ -> ValueNone

    member this.MapTryO (map: 'value -> 'value) =
        match this.MapTry map with
        | Ok result -> Some result
        | _ -> None