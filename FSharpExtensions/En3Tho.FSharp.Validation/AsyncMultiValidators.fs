namespace En3Tho.FSharp.Validation

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.Extensions
open FSharp.Control.Tasks

type [<Struct; IsReadOnly>] AsyncMultiValidator20<'value, 'validator, 'validator2 when 'validator: struct
                                                                                   and 'validator: (new: unit -> 'validator)
                                                                                   and 'validator :> IAsyncValidator<'value>
                                                                                   and 'validator2: struct
                                                                                   and 'validator2: (new: unit -> 'validator2)
                                                                                   and 'validator2 :> IAsyncValidator<'value>> =

    member this.Validate value = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! (new 'validator2()).Validate value with
            | Ok value -> return value |> Ok
            | Error err -> return Error err
        | Error err -> return Error err
    }
    
    member this.ValidateAggregate value = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = (new 'validator2()).Validate value
        match result1, result2 with
        | Ok _, Ok _ ->
            return value |> Ok
        | Error exn1, Error exn2 ->
            return Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            return Error (AggregateException(exn))
    }

    interface IAsyncValidator<'value> with
        member this.Validate(value) = this.Validate(value)
        member this.ValidateAggregate(value) = this.ValidateAggregate(value)

type [<Struct; IsReadOnly>] AsyncMultiValidator02<'value, 'validator, 'validator2 when 'validator :> IAsyncValidator<'value>
                                                                              and 'validator2 :> IAsyncValidator<'value>>
    (validator: 'validator, validator2: 'validator2) =

    member this.Validate(value) =
        let validator = validator
        let validator2 = validator2
        vtask {
            match! validator.Validate value with
            | Ok value ->
                match! validator2.Validate value with
                | Ok value -> return value |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        }

    member this.ValidateAggregate(value) =
        let validator = validator
        let validator2 = validator2
        vtask {
            let! result1 = validator.Validate value
            let! result2 = validator2.Validate value
            match result1, result2 with
            | Ok _, Ok _ ->
                return value |> Ok
            | Error exn1, Error exn2 ->
                return Error (AggregateException(exn1, exn2))
            | Error exn, _
            | _, Error exn ->
                return Error (AggregateException(exn))
        }

    interface IAsyncValidator<'value> with
        member this.Validate(value) = this.Validate(value)
        member this.ValidateAggregate(value) = this.ValidateAggregate(value)

type [<Struct; IsReadOnly>] AsyncMultiValidator11<'value, 'validator, 'validator2 when 'validator: struct
                                                                              and 'validator: (new: unit -> 'validator)
                                                                              and 'validator :> IAsyncValidator<'value>
                                                                              and 'validator2 :> IAsyncValidator<'value>>

    (validator: 'validator2) =

    member this.Validate(value) =
        let validator = validator
        vtask {
            match! (new 'validator()).Validate value with
            | Ok value ->
                match! validator.Validate value with
                | Ok value -> return value |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        }

    member this.ValidateAggregate(value) =
        let validator = validator
        vtask {
            let! result1 = (new 'validator()).Validate value
            let! result2 = validator.Validate value
            match result1, result2 with
            | Ok _, Ok _ ->
                return value |> Ok
            | Error exn1, Error exn2 ->
                return Error (AggregateException(exn1, exn2))
            | Error exn, _
            | _, Error exn ->
                return Error (AggregateException(exn))
        }

    interface IAsyncValidator<'value> with
        member this.Validate(value) = this.Validate(value)
        member this.ValidateAggregate(value) = this.ValidateAggregate(value)
            
type [<Struct; IsReadOnly>] AsyncMultiValidator30<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2: struct
                                                                                           and 'validator2: (new: unit -> 'validator2)
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3: struct
                                                                                           and 'validator3: (new: unit -> 'validator3)
                                                                                           and 'validator3 :> IAsyncValidator<'value>> =
    member this.Validate value = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! (new 'validator2()).Validate value with
            | Ok value ->
                match! (new 'validator3()).Validate value with
                | Ok value -> return value |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        | Error err -> return Error err
    }


    member this.ValidateAggregate value = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = (new 'validator2()).Validate value
        let! result3 = (new 'validator3()).Validate value
        match result1, result2, result3 with
        | Ok _, Ok _, Ok _ ->
            return value |> Ok
        | Error exn1, Error exn2, Error exn3 ->
            return Error (AggregateException(exn1, exn2, exn3))
        | Error exn, Error exn2, _
        | Error exn, _, Error exn2
        | _, Error exn, Error exn2 ->
            return Error (AggregateException(exn, exn2))
        | Error exn, _, _
        | _, Error exn, _
        | _, _, Error exn ->
            return Error (AggregateException(exn))
    }

    interface IAsyncValidator<'value> with
        member this.Validate(value) = this.Validate(value)
        member this.ValidateAggregate(value) = this.ValidateAggregate(value)

type [<Struct; IsReadOnly>] AsyncMultiValidator21<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2: struct
                                                                                           and 'validator2: (new: unit -> 'validator2)
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3 :> IAsyncValidator<'value>>

    (validator: 'validator3) =

    member this.Validate(value) =
        let validator = validator
        vtask {
            match! (new 'validator()).Validate value with
            | Ok value ->
                match! (new 'validator2()).Validate value with
                | Ok value ->
                    match! validator.Validate value with
                    | Ok value -> return value |> Ok
                    | Error err -> return Error err
                | Error err -> return Error err
            | Error err -> return Error err
        }

    member this.ValidateAggregate(value) =
        let validator = validator
        vtask {
            let! result1 = (new 'validator()).Validate value
            let! result2 = (new 'validator2()).Validate value
            let! result3 = validator.Validate value
            match result1, result2, result3 with
            | Ok _, Ok _, Ok _ ->
                return value |> Ok
            | Error exn1, Error exn2, Error exn3 ->
                return Error (AggregateException(exn1, exn2, exn3))
            | Error exn, Error exn2, _
            | Error exn, _, Error exn2
            | _, Error exn, Error exn2 ->
                return Error (AggregateException(exn, exn2))
            | Error exn, _, _
            | _, Error exn, _
            | _, _, Error exn ->
                return Error (AggregateException(exn))
        }

    interface IAsyncValidator<'value> with
        member this.Validate(value) = this.Validate(value)
        member this.ValidateAggregate(value) = this.ValidateAggregate(value)

type [<Struct; IsReadOnly>] AsyncMultiValidator12<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3 :> IAsyncValidator<'value>>

    (validator: 'validator2, validator2: 'validator3) =

    member this.Validate(value) =
        let validator = validator
        let validator2 = validator2
        vtask {
            match! (new 'validator()).Validate value with
            | Ok value ->
                match! validator.Validate value with
                | Ok value ->
                    match! validator2.Validate value with
                    | Ok value -> return value |> Ok
                    | Error err -> return Error err
                | Error err -> return Error err
            | Error err -> return Error err
        }

    member this.ValidateAggregate(value) =
        let validator = validator
        let validator2 = validator2
        vtask {
            let! result1 = (new 'validator()).Validate value
            let! result2 = validator.Validate value
            let! result3 = validator2.Validate value
            match result1, result2, result3 with
            | Ok _, Ok _, Ok _ ->
                return value |> Ok
            | Error exn1, Error exn2, Error exn3 ->
                return Error (AggregateException(exn1, exn2, exn3))
            | Error exn, Error exn2, _
            | Error exn, _, Error exn2
            | _, Error exn, Error exn2 ->
                return Error (AggregateException(exn, exn2))
            | Error exn, _, _
            | _, Error exn, _
            | _, _, Error exn ->
                return Error (AggregateException(exn))
        }

    interface IAsyncValidator<'value> with
        member this.Validate(value) = this.Validate(value)
        member this.ValidateAggregate(value) = this.ValidateAggregate(value)

type [<Struct; IsReadOnly>] AsyncMultiValidator03<'value, 'validator, 'validator2, 'validator3 when 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3 :> IAsyncValidator<'value>>

    (validator: 'validator, validator2: 'validator2, validator3: 'validator3) =

    member this.Validate(value) =
        let validator = validator
        let validator2 = validator2
        let validator3 = validator3
        vtask {
            match! validator.Validate value with
            | Ok value ->
                match! validator2.Validate value with
                | Ok value ->
                    match! validator3.Validate value with
                    | Ok value -> return value |> Ok
                    | Error err -> return Error err
                | Error err -> return Error err
            | Error err -> return Error err
        }

    member this.ValidateAggregate(value) =
        let validator = validator
        let validator2 = validator2
        let validator3 = validator3
        vtask {
            let! result1 = validator.Validate value
            let! result2 = validator2.Validate value
            let! result3 = validator3.Validate value
            match result1, result2, result3 with
            | Ok _, Ok _, Ok _ ->
                return value |> Ok
            | Error exn1, Error exn2, Error exn3 ->
                return Error (AggregateException(exn1, exn2, exn3))
            | Error exn, Error exn2, _
            | Error exn, _, Error exn2
            | _, Error exn, Error exn2 ->
                return Error (AggregateException(exn, exn2))
            | Error exn, _, _
            | _, Error exn, _
            | _, _, Error exn ->
                return Error (AggregateException(exn))
        }

    interface IAsyncValidator<'value> with
        member this.Validate(value) = this.Validate(value)
        member this.ValidateAggregate(value) = this.ValidateAggregate(value)