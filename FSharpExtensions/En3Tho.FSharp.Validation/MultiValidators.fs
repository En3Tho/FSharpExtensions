namespace En3Tho.FSharp.Validation

open System
open System.Runtime.CompilerServices
open System.Threading.Tasks
open En3Tho.FSharp.Extensions

module Validator =
    let inline validate validator value =
        (^validator: (member Validate: 'value -> ExnResult<'value>) validator, value)

    let inline validateAggregate validator value =
        (^validator: (member ValidateAggregate: 'value -> EResult<'value, AggregateException>) validator, value)

    let inline validateAsync validator value =
        (^validator: (member Validate: 'value -> ExnResult<'value>) validator, value)
        |> ValueTask.FromResult

    let inline validateAggregateAsync validator value =
        (^validator: (member ValidateAggregate: 'value -> EResult<'value, AggregateException>) validator, value)
        |> ValueTask.FromResult
        


type [<Struct; IsReadOnly>] MultiValidator20<'value, 'validator, 'validator2 when 'validator: struct
                                                                              and 'validator: (new: unit -> 'validator)
                                                                              and 'validator :> IValidator<'value>
                                                                              and 'validator2: struct
                                                                              and 'validator2: (new: unit -> 'validator2)
                                                                              and 'validator2 :> IValidator<'value>> =
    member this.Validate(value) =
        match (new 'validator()).Validate(value) with
        | Ok value ->
            match (new 'validator2()).Validate(value) with
            | Ok value -> value |> Ok
            | Error err -> Error err
        | Error err -> Error err

    member this.ValidateAggregate(value) =
        match (new 'validator()).Validate(value), (new 'validator2()).Validate(value) with
        | Ok _, Ok _ ->
            value |> Ok
        | Error exn1, Error exn2 ->
            Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            Error (AggregateException(exn))
            
    interface IValidator<'value> with
        member this.Validate(value) = Validator.validate this value
        member this.Validate(value) = Validator.validateAsync this value
        member this.ValidateAggregate(value) = Validator.validateAggregate this value
        member this.ValidateAggregate(value) = Validator.validateAggregateAsync this value

type [<Struct; IsReadOnly>] MultiValidator02<'value, 'validator, 'validator2 when 'validator :> IValidator<'value>
                                                                              and 'validator2 :> IValidator<'value>>
    (validator: 'validator, validator2: 'validator2) =
    member this.Validate(value)  =
        match validator.Validate(value) with
        | Ok value ->
            match validator2.Validate(value) with
            | Ok value -> value |> Ok
            | Error err -> Error err
        | Error err -> Error err

    member this.ValidateAggregate(value) =
        match validator.Validate(value), validator2.Validate(value) with
        | Ok _, Ok _ ->
            value |> Ok
        | Error exn1, Error exn2 ->
            Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            Error (AggregateException(exn))

    interface IValidator<'value> with
        member this.Validate(value) = Validator.validate this value
        member this.Validate(value) = Validator.validateAsync this value
        member this.ValidateAggregate(value) = Validator.validateAggregate this value
        member this.ValidateAggregate(value) = Validator.validateAggregateAsync this value

type [<Struct; IsReadOnly>] MultiValidator11<'value, 'validator, 'validator2 when 'validator: struct
                                                                              and 'validator: (new: unit -> 'validator)
                                                                              and 'validator :> IValidator<'value>
                                                                              and 'validator2 :> IValidator<'value>>
    (validator: 'validator2) =
                                                                             
    member this.Validate(value) =
        match (new 'validator()).Validate(value) with
        | Ok value ->
            match validator.Validate value with
            | Ok value -> value |> Ok
            | Error err -> Error err
        | Error err -> Error err

    member this.ValidateAggregate(value) =
        match (new 'validator()).Validate value, validator.Validate value with
        | Ok _, Ok _ ->
            value |> Ok
        | Error exn1, Error exn2 ->
            Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            Error (AggregateException(exn))

    interface IValidator<'value> with
        member this.Validate(value) = Validator.validate this value
        member this.Validate(value) = Validator.validateAsync this value
        member this.ValidateAggregate(value) = Validator.validateAggregate this value
        member this.ValidateAggregate(value) = Validator.validateAggregateAsync this value
            
type [<Struct; IsReadOnly>] MultiValidator30<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IValidator<'value>
                                                                                           and 'validator2: struct
                                                                                           and 'validator2: (new: unit -> 'validator2)
                                                                                           and 'validator2 :> IValidator<'value>
                                                                                           and 'validator3: struct
                                                                                           and 'validator3: (new: unit -> 'validator3)
                                                                                           and 'validator3 :> IValidator<'value>> =
    member this.Validate(value) =
        match (new 'validator()).Validate value with
        | Ok value ->
            match (new 'validator2()).Validate value with
            | Ok value ->
                match (new 'validator3()).Validate value with
                | Ok value -> value |> Ok
                | Error err -> Error err
            | Error err -> Error err
        | Error err -> Error err

    member this.ValidateAggregate(value) =
        match (new 'validator()).Validate value, (new 'validator2()).Validate value, (new 'validator3()).Validate value with
        | Ok _, Ok _, Ok _ ->
            value |> Ok
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

    interface IValidator<'value> with
        member this.Validate(value) = Validator.validate this value
        member this.Validate(value) = Validator.validateAsync this value
        member this.ValidateAggregate(value) = Validator.validateAggregate this value
        member this.ValidateAggregate(value) = Validator.validateAggregateAsync this value

type [<Struct; IsReadOnly>] MultiValidator21<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IValidator<'value>
                                                                                           and 'validator2: struct
                                                                                           and 'validator2: (new: unit -> 'validator2)
                                                                                           and 'validator2 :> IValidator<'value>
                                                                                           and 'validator3 :> IValidator<'value>>
    (validator: 'validator3) =
    member this.Validate(value) =
        match (new 'validator()).Validate value with
        | Ok value ->
            match (new 'validator2()).Validate value with
            | Ok value ->
                match validator.Validate value with
                | Ok value -> value |> Ok
                | Error err -> Error err
            | Error err -> Error err
        | Error err -> Error err

    member this.ValidateAggregate(value) =
        match (new 'validator()).Validate value, (new 'validator2()).Validate value, validator.Validate value with
        | Ok _, Ok _, Ok _ ->
            value |> Ok
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

    interface IValidator<'value> with
        member this.Validate(value) = Validator.validate this value
        member this.Validate(value) = Validator.validateAsync this value
        member this.ValidateAggregate(value) = Validator.validateAggregate this value
        member this.ValidateAggregate(value) = Validator.validateAggregateAsync this value

type [<Struct; IsReadOnly>] MultiValidator12<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IValidator<'value>
                                                                                           and 'validator2 :> IValidator<'value>
                                                                                           and 'validator3 :> IValidator<'value>>
    (validator: 'validator2, validator2: 'validator3) =

    member this.Validate(value) =
        match (new 'validator()).Validate value with
        | Ok value ->
            match validator2.Validate value with
            | Ok value ->
                match validator.Validate value with
                | Ok value -> value |> Ok
                | Error err -> Error err
            | Error err -> Error err
        | Error err -> Error err

    member this.ValidateAggregate(value) =
        match (new 'validator()).Validate value, validator.Validate value, validator2.Validate value with
        | Ok _, Ok _, Ok _ ->
            value |> Ok
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

    interface IValidator<'value> with
        member this.Validate(value) = Validator.validate this value
        member this.Validate(value) = Validator.validateAsync this value
        member this.ValidateAggregate(value) = Validator.validateAggregate this value
        member this.ValidateAggregate(value) = Validator.validateAggregateAsync this value

type [<Struct; IsReadOnly>] MultiValidator03<'value, 'validator, 'validator2, 'validator3 when 'validator :> IValidator<'value>
                                                                                           and 'validator2 :> IValidator<'value>
                                                                                           and 'validator3 :> IValidator<'value>>
    (validator: 'validator, validator2: 'validator2, validator3: 'validator3) =

    member this.Validate(value) =
        match validator.Validate value with
        | Ok value ->
            match validator2.Validate value with
            | Ok value ->
                match validator3.Validate value with
                | Ok value -> value |> Ok
                | Error err -> Error err
            | Error err -> Error err
        | Error err -> Error err

    member this.ValidateAggregate(value) =
        match validator.Validate value, validator2.Validate value, validator3.Validate value with
        | Ok _, Ok _, Ok _ ->
            value |> Ok
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

    interface IValidator<'value> with
        member this.Validate(value) = Validator.validate this value
        member this.Validate(value) = Validator.validateAsync this value
        member this.ValidateAggregate(value) = Validator.validateAggregate this value
        member this.ValidateAggregate(value) = Validator.validateAggregateAsync this value