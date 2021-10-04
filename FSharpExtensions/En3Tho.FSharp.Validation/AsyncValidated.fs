namespace En3Tho.FSharp.Validation

open System
open System.Runtime.CompilerServices
open En3Tho.FSharp.Extensions
open FSharp.Control.Tasks

// TODO: Investigate if only a single type is needed through validator combinations

type [<Struct; IsReadOnly>] AsyncValidated10<'value, 'validator when 'validator: struct
                                                                 and 'validator: (new: unit -> 'validator)
                                                                 and 'validator :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, wasAsyncValidated: bool) =
    private new(value) = AsyncValidated10(value, true)
    member _.Value =
        if not wasAsyncValidated then invalidOp "Value was not properly AsyncValidated"
        else value
    #else
    private (value: 'value) =
    member _.Value = value
#endif

    override this.ToString() = this.Value.ToString()

    static member Try value : AsyncExnResult<AsyncValidated10<'value, 'validator>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value -> return AsyncValidated10 value |> Ok
        | Error err -> return Error err
    }
        

type [<Struct; IsReadOnly>] AsyncValidated01<'value, 'validator when 'validator :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, wasAsyncValidated: bool) =
    private new(value) = AsyncValidated01(value, true)
    member _.Value =
        if not wasAsyncValidated then invalidOp "Value was not properly AsyncValidated"
        else value
#else
    private (value: 'value, validator: 'validator) =
    member _.Value = value
    member internal _.Validator = validator
#endif

    static member Try (value, validator: 'validator) : AsyncExnResult<AsyncValidated01<'value, 'validator>> = vtask {
        match! validator.Validate value with
        | Ok value -> return AsyncValidated01(value, validator) |> Ok
        | Error err -> return Error err
    }

type [<Struct; IsReadOnly>] AsyncValidated20<'value, 'validator, 'validator2 when 'validator: struct
                                                                              and 'validator: (new: unit -> 'validator)
                                                                              and 'validator :> IAsyncValidator<'value>
                                                                              and 'validator2: struct
                                                                              and 'validator2: (new: unit -> 'validator2)
                                                                              and 'validator2 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncValidated: bool) =
    private new (value) = AsyncValidated20(value, true)
    member _.Value =
        if not isAsyncValidated then invalidOp "Value was not properly AsyncValidated"
        else value
#else
    private (value: 'value) =

    member _.Value = value
#endif
    static member Try value : AsyncExnResult<AsyncValidated20<'value, 'validator, 'validator2>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! (new 'validator2()).Validate value with
            | Ok value -> return AsyncValidated20(value) |> Ok
            | Error err -> return Error err
        | Error err -> return Error err
    }
    
    static member TryAggregate value : AsyncEResult<AsyncValidated20<'value, 'validator, 'validator2>, _> = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = (new 'validator2()).Validate value
        match result1, result2 with
        | Ok _, Ok _ ->
            return AsyncValidated20(value) |> Ok
        | Error exn1, Error exn2 ->
            return Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            return Error (AggregateException(exn))
    }

type [<Struct; IsReadOnly>] AsyncValidated02<'value, 'validator, 'validator2 when 'validator :> IAsyncValidator<'value>
                                                                              and 'validator2 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncValidated: bool) =
    private new (value) = AsyncValidated02(value, true)
    member _.Value =
        if not isAsyncValidated then invalidOp "Value was not properly AsyncValidated"
        else value
#else
    private (value: 'value, validator: 'validator, validator2: 'validator2) =

    member _.Value = value
    member internal _.Validator = validator
    member internal _.Validator2 = validator2
#endif
    static member Try(value, validator: 'validator, validator2: 'validator2) : AsyncExnResult<AsyncValidated02<'value, 'validator, 'validator2>> = vtask {
        match! validator.Validate value with
        | Ok value ->
            match! validator2.Validate value with
            | Ok value -> return AsyncValidated02(value, validator, validator2) |> Ok
            | Error err -> return Error err
        | Error err -> return Error err
    }

    static member TryAggregate(value, validator: 'validator, validator2: 'validator2) : AsyncEResult<AsyncValidated02<'value, 'validator, 'validator2>, _> = vtask {
        let! result1 = validator.Validate value
        let! result2 = validator2.Validate value
        match result1, result2 with
        | Ok _, Ok _ ->
            return AsyncValidated02(value, validator, validator2) |> Ok
        | Error exn1, Error exn2 ->
            return Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            return Error (AggregateException(exn))
    }

type [<Struct; IsReadOnly>] AsyncValidated11<'value, 'validator, 'validator2 when 'validator: struct
                                                                              and 'validator: (new: unit -> 'validator)
                                                                              and 'validator :> IAsyncValidator<'value>
                                                                              and 'validator2 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, wasAsyncValidated: bool) =
    private new value = AsyncValidated11(value, true)
    member _.Value =
        if not wasAsyncValidated then invalidOp "Value was not properly AsyncValidated"
        else value
#else
    private (value: 'value, validator: 'validator2) =

    member _.Value = value
    member internal _.Validator = validator
#endif
    static member Try(value, validator2: 'validator2) : AsyncExnResult<AsyncValidated11<'value, 'validator, 'validator2>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! validator2.Validate value with
            | Ok value -> return AsyncValidated11(value, validator2) |> Ok
            | Error err -> return Error err
        | Error err -> return Error err
    }

    static member TryAggregate(value, validator2: 'validator2) : AsyncEResult<AsyncValidated11<'value, 'validator, 'validator2>, _> = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = validator2.Validate value
        match result1, result2 with
        | Ok _, Ok _ ->
            return AsyncValidated11(value, validator2) |> Ok
        | Error exn1, Error exn2 ->
            return Error (AggregateException(exn1, exn2))
        | Error exn, _
        | _, Error exn ->
            return Error (AggregateException(exn))
    }
            
type [<Struct; IsReadOnly>] AsyncValidated30<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2: struct
                                                                                           and 'validator2: (new: unit -> 'validator2)
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3: struct
                                                                                           and 'validator3: (new: unit -> 'validator3)
                                                                                           and 'validator3 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncValidated: bool) =
    private new (value) = AsyncValidated20(value, true)
    member _.Value =
        if not isAsyncValidated then invalidOp "Value was not properly AsyncValidated"
        else value
#else
    private (value: 'value) =

    member _.Value = value
#endif
    static member Try value : AsyncExnResult<AsyncValidated30<'value, 'validator, 'validator2, 'validator3>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! (new 'validator2()).Validate value with
            | Ok value ->
                match! (new 'validator3()).Validate value with
                | Ok value -> return AsyncValidated30(value) |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        | Error err -> return Error err
    }


    static member TryAggregate value : AsyncEResult<AsyncValidated30<'value, 'validator, 'validator2, 'validator3>, _> = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = (new 'validator2()).Validate value
        let! result3 = (new 'validator3()).Validate value
        match result1, result2, result3 with
        | Ok _, Ok _, Ok _ ->
            return AsyncValidated30(value) |> Ok
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


type [<Struct; IsReadOnly>] AsyncValidated21<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2: struct
                                                                                           and 'validator2: (new: unit -> 'validator2)
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncValidated: bool) =
    private new (value) = AsyncValidated20(value, true)
    member _.Value =
        if not isAsyncValidated then invalidOp "Value was not properly AsyncValidated"
        else value
#else
    private (value: 'value, validator: 'validator3) =

    member _.Value = value
    member internal _.Validator = validator
#endif
    static member Try(value, validator3: 'validator3) : AsyncExnResult<AsyncValidated21<'value, 'validator, 'validator2, 'validator3>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! (new 'validator2()).Validate value with
            | Ok value ->
                match! validator3.Validate value with
                | Ok value -> return AsyncValidated21(value, validator3) |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        | Error err -> return Error err
    }

    static member TryAggregate(value, validator3: 'validator3) : AsyncEResult<AsyncValidated21<'value, 'validator, 'validator2, 'validator3>, _> = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = (new 'validator2()).Validate value
        let! result3 = validator3.Validate value
        match result1, result2, result3 with
        | Ok _, Ok _, Ok _ ->
            return AsyncValidated21(value, validator3) |> Ok
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

type [<Struct; IsReadOnly>] AsyncValidated12<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
                                                                                           and 'validator: (new: unit -> 'validator)
                                                                                           and 'validator :> IAsyncValidator<'value>
                                                                                           and 'validator2 :> IAsyncValidator<'value>
                                                                                           and 'validator3 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncValidated: bool) =
    private new (value) = AsyncValidated20(value, true)
    member _.Value =
        if not isAsyncValidated then invalidOp "Value was not properly AsyncValidated"
        else value
#else
    private (value: 'value, validator: 'validator2, validator2: 'validator3) =

    member _.Value = value
    member internal _.Validator = validator
    member internal _.Validator2 = validator2
    
#endif
    static member Try(value, validator2: 'validator2, validator3: 'validator3) : AsyncExnResult<AsyncValidated12<'value, 'validator, 'validator2, 'validator3>> = vtask {
        match! (new 'validator()).Validate value with
        | Ok value ->
            match! validator2.Validate value with
            | Ok value ->
                match! validator3.Validate value with
                | Ok value -> return AsyncValidated12(value, validator2, validator3) |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        | Error err -> return Error err
    }

    static member TryAggregate(value, validator2: 'validator2, validator3: 'validator3) : AsyncEResult<AsyncValidated12<'value, 'validator, 'validator2, 'validator3>, _> = vtask {
        let! result1 = (new 'validator()).Validate value
        let! result2 = validator2.Validate value
        let! result3 = validator3.Validate value
        match result1, result2, result3 with
        | Ok _, Ok _, Ok _ ->
            return AsyncValidated12(value, validator2, validator3) |> Ok
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

type [<Struct; IsReadOnly>] AsyncValidated03<'value, 'validator, 'validator2, 'validator3 when 'validator :> IAsyncValidator<'value>
                                                                                          and 'validator2 :> IAsyncValidator<'value>
                                                                                          and 'validator3 :> IAsyncValidator<'value>>
#if DEBUG
    private (value: 'value, isAsyncValidated: bool) =
    private new (value) = AsyncValidated20(value, true)
    member _.Value =
        if not isAsyncValidated then invalidOp "Value was not properly AsyncValidated"
        else value
#else
    private (value: 'value, validator: 'validator, validator2: 'validator2, validator3: 'validator3) =

    member _.Value = value
    member internal _.Validator = validator
    member internal _.Validator2 = validator2
    member internal _.Validator3 = validator3

#endif
    static member Try(value, validator: 'validator, validator2: 'validator2, validator3: 'validator3) : AsyncExnResult<AsyncValidated03<'value, 'validator, 'validator2, 'validator3>> = vtask {
        match! validator.Validate value with
        | Ok value ->
            match! validator2.Validate value with
            | Ok value ->
                match! validator3.Validate value with
                | Ok value -> return AsyncValidated03(value, validator, validator2, validator3) |> Ok
                | Error err -> return Error err
            | Error err -> return Error err
        | Error err -> return Error err
    }

    static member TryAggregate(value, validator: 'validator, validator2: 'validator2, validator3: 'validator3) : AsyncEResult<AsyncValidated03<'value, 'validator, 'validator2, 'validator3>, _> = vtask {
        let! result1 = validator.Validate value
        let! result2 = validator2.Validate value
        let! result3 = validator3.Validate value
        match result1, result2, result3 with
        | Ok _, Ok _, Ok _ ->
            return AsyncValidated03(value, validator, validator2, validator3) |> Ok
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
