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
                                                                              and 'validator2 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncMultiValidator: bool) =
    private new (value) = AsyncMultiValidator20(value, true)
    member _.Value =
        if not isAsyncMultiValidator then invalidOp "Value was not properly AsyncMultiValidator"
        else value
#else
    private (value: 'value) =

    member _.Value = value
#endif
    member this.Validate value : AsyncExnResult<AsyncMultiValidator20<'value, 'validator, 'validator2>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! (new 'validator2()).Validate value with
            | Ok value -> return AsyncMultiValidator20(value) |> Ok
            | Error err -> return Error err
        | Error err -> return Error err
    }
    
    member this.ValidateAggregate value : AsyncEResult<AsyncMultiValidator20<'value, 'validator, 'validator2>, _> = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = (new 'validator2()).Validate value
        match result1, result2 with
        | Ok _, Ok _ ->
            return AsyncMultiValidator20(value) |> Ok
        | Error exn1, Error exn2 ->
            return Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            return Error (AggregateException(exn))
    }

type [<Struct; IsReadOnly>] AsyncMultiValidator02<'value, 'validator, 'validator2 when 'validator :> IAsyncValidator<'value>
                                                                              and 'validator2 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncMultiValidator: bool) =
    private new (value) = AsyncMultiValidator02(value, true)
    member _.Value =
        if not isAsyncMultiValidator then invalidOp "Value was not properly AsyncMultiValidator"
        else value
#else
    private (value: 'value, validator: 'validator, validator2: 'validator2) =

    member _.Value = value
    member internal _.Validator = validator
    member internal _.Validator2 = validator2
#endif
    member this.Validate(value, validator: 'validator, validator2: 'validator2) : AsyncExnResult<AsyncMultiValidator02<'value, 'validator, 'validator2>> = vtask {
        match! validator.Validate value with
        | Ok value ->
            match! validator2.Validate value with
            | Ok value -> return AsyncMultiValidator02(value, validator, validator2) |> Ok
            | Error err -> return Error err
        | Error err -> return Error err
    }

    member this.ValidateAggregate(value, validator: 'validator, validator2: 'validator2) : AsyncEResult<AsyncMultiValidator02<'value, 'validator, 'validator2>, _> = vtask {
        let! result1 = validator.Validate value
        let! result2 = validator2.Validate value
        match result1, result2 with
        | Ok _, Ok _ ->
            return AsyncMultiValidator02(value, validator, validator2) |> Ok
        | Error exn1, Error exn2 ->
            return Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            return Error (AggregateException(exn))
    }

type [<Struct; IsReadOnly>] AsyncMultiValidator11<'value, 'validator, 'validator2 when 'validator: struct
                                                                              and 'validator: (new: unit -> 'validator)
                                                                              and 'validator :> IAsyncValidator<'value>
                                                                              and 'validator2 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, wasAsyncMultiValidator: bool) =
    private new value = AsyncMultiValidator11(value, true)
    member _.Value =
        if not wasAsyncMultiValidator then invalidOp "Value was not properly AsyncMultiValidator"
        else value
#else
    private (value: 'value, validator: 'validator2) =

    member _.Value = value
    member internal _.Validator = validator
#endif
    member this.Validate(value, validator2: 'validator2) : AsyncExnResult<AsyncMultiValidator11<'value, 'validator, 'validator2>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! validator2.Validate value with
            | Ok value -> return AsyncMultiValidator11(value, validator2) |> Ok
            | Error err -> return Error err
        | Error err -> return Error err
    }

    member this.ValidateAggregate(value, validator2: 'validator2) : AsyncEResult<AsyncMultiValidator11<'value, 'validator, 'validator2>, _> = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = validator2.Validate value
        match result1, result2 with
        | Ok _, Ok _ ->
            return AsyncMultiValidator11(value, validator2) |> Ok
        | Error exn1, Error exn2 ->
            return Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            return Error (AggregateException(exn))
    }
            
type [<Struct; IsReadOnly>] AsyncMultiValidator30<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2: struct
                                                                                           and 'validator2: (new: unit -> 'validator2)
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3: struct
                                                                                           and 'validator3: (new: unit -> 'validator3)
                                                                                           and 'validator3 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncMultiValidator: bool) =
    private new (value) = AsyncMultiValidator20(value, true)
    member _.Value =
        if not isAsyncMultiValidator then invalidOp "Value was not properly AsyncMultiValidator"
        else value
#else
    private (value: 'value) =

    member _.Value = value
#endif
    member this.Validate value : AsyncExnResult<AsyncMultiValidator30<'value, 'validator, 'validator2, 'validator3>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! (new 'validator2()).Validate value with
            | Ok value ->
                match! (new 'validator3()).Validate value with
                | Ok value -> return AsyncMultiValidator30(value) |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        | Error err -> return Error err
    }


    member this.ValidateAggregate value : AsyncEResult<AsyncMultiValidator30<'value, 'validator, 'validator2, 'validator3>, _> = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = (new 'validator2()).Validate value
        let! result3 = (new 'validator3()).Validate value
        match result1, result2, result3 with
        | Ok _, Ok _, Ok _ ->
            return AsyncMultiValidator30(value) |> Ok
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


type [<Struct; IsReadOnly>] AsyncMultiValidator21<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2: struct
                                                                                           and 'validator2: (new: unit -> 'validator2)
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncMultiValidator: bool) =
    private new (value) = AsyncMultiValidator20(value, true)
    member _.Value =
        if not isAsyncMultiValidator then invalidOp "Value was not properly AsyncMultiValidator"
        else value
#else
    private (value: 'value, validator: 'validator3) =

    member _.Value = value
    member internal _.Validator = validator
#endif
    member this.Validate(value, validator3: 'validator3) : AsyncExnResult<AsyncMultiValidator21<'value, 'validator, 'validator2, 'validator3>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! (new 'validator2()).Validate value with
            | Ok value ->
                match! validator3.Validate value with
                | Ok value -> return AsyncMultiValidator21(value, validator3) |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        | Error err -> return Error err
    }

    member this.ValidateAggregate(value, validator3: 'validator3) : AsyncEResult<AsyncMultiValidator21<'value, 'validator, 'validator2, 'validator3>, _> = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = (new 'validator2()).Validate value
        let! result3 = validator3.Validate value
        match result1, result2, result3 with
        | Ok _, Ok _, Ok _ ->
            return AsyncMultiValidator21(value, validator3) |> Ok
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

type [<Struct; IsReadOnly>] AsyncMultiValidator12<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncMultiValidator: bool) =
    private new (value) = AsyncMultiValidator20(value, true)
    member _.Value =
        if not isAsyncMultiValidator then invalidOp "Value was not properly AsyncMultiValidator"
        else value
#else
    private (value: 'value, validator: 'validator2, validator2: 'validator3) =

    member _.Value = value
    member internal _.Validator = validator
    member internal _.Validator2 = validator2
    
#endif
    member this.Validate(value, validator2: 'validator2, validator3: 'validator3) : AsyncExnResult<AsyncMultiValidator12<'value, 'validator, 'validator2, 'validator3>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! validator2.Validate value with
            | Ok value ->
                match! validator3.Validate value with
                | Ok value -> return AsyncMultiValidator12(value, validator2, validator3) |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        | Error err -> return Error err
    }

    member this.ValidateAggregate(value, validator2: 'validator2, validator3: 'validator3) : AsyncEResult<AsyncMultiValidator12<'value, 'validator, 'validator2, 'validator3>, _> = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = validator2.Validate value
        let! result3 = validator3.Validate value
        match result1, result2, result3 with
        | Ok _, Ok _, Ok _ ->
            return AsyncMultiValidator12(value, validator2, validator3) |> Ok
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

type [<Struct; IsReadOnly>] AsyncMultiValidator03<'value, 'validator, 'validator2, 'validator3 when 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncMultiValidator: bool) =
    private new (value) = AsyncMultiValidator20(value, true)
    member _.Value =
        if not isAsyncMultiValidator then invalidOp "Value was not properly AsyncMultiValidator"
        else value
#else
    private (value: 'value, validator: 'validator, validator2: 'validator2, validator3: 'validator3) =

    member _.Value = value
    member internal _.Validator = validator
    member internal _.Validator2 = validator2
    member internal _.Validator3 = validator3

#endif
    member this.Validate(value, validator: 'validator, validator2: 'validator2, validator3: 'validator3) : AsyncExnResult<AsyncMultiValidator03<'value, 'validator, 'validator2, 'validator3>> = vtask {
        match! validator.Validate value with
        | Ok value ->
            match! validator2.Validate value with
            | Ok value ->
                match! validator3.Validate value with
                | Ok value -> return AsyncMultiValidator03(value, validator, validator2, validator3) |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        | Error err -> return Error err
    }

    member this.ValidateAggregate(value, validator: 'validator, validator2: 'validator2, validator3: 'validator3) : AsyncEResult<AsyncMultiValidator03<'value, 'validator, 'validator2, 'validator3>, _> = vtask {
        let! result1 = validator.Validate value
        let! result2 = validator2.Validate value
        let! result3 = validator3.Validate value
        match result1, result2, result3 with
        | Ok _, Ok _, Ok _ ->
            return AsyncMultiValidator03(value, validator, validator2, validator3) |> Ok
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
