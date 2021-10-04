namespace En3Tho.FSharp.Validation

open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
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

/// Indicates an error which didn't come from domain. Maps to InternalServerError in REST.
type InternalException(message, innerException: Exception) =
    inherit Exception(message, innerException)
    new (message) = InternalException(message, null)
    new (innerException) = InternalException("Fatal exception occured", innerException)

type IAsyncValidator<'value> =
    abstract member Validate: 'value -> ExnResult<'value> ValueTask
    abstract member ValidateAggregate: 'value -> EResult<'value, AggregateException> ValueTask

type IValidator<'value> =
    inherit IAsyncValidator<'value>
    abstract member Validate: 'value -> ExnResult<'value>
    abstract member ValidateAggregate: 'value -> EResult<'value, AggregateException>

//type [<Struct>] MultiValidator20<'value, 'validator, 'validator2 when 'validator: struct
//                                                                  and 'validator: (new: unit -> 'validator)
//                                                                  and 'validator :> IValidator<'value>
//                                                                  and 'validator2: struct
//                                                                  and 'validator2: (new: unit -> 'validator2)
//                                                                  and 'validator2 :> IValidator<'value>> =
//    member this.Validate(value: 'value): ExnResult<'value> =
//            match (new 'validator()).Validate value with
//            | Ok value ->
//                match (new 'validator2()).Validate value with
//                | Ok value ->
//                    value |> Ok
//                | Error err -> Error err
//            | Error err -> Error err
//
//    member this.ValidateAggregate value : EResult<'value, AggregateException> =
//        match (new 'validator()).Validate value, (new 'validator2()).Validate value with
//        | Ok _, Ok _ ->
//            value |> Ok
//        | Error exn1, Error exn2 ->
//            Error (AggregateException(exn1, exn2))
//        | Error exn, _
//        | _, Error exn ->
//            Error (AggregateException(exn))
//
//    interface IValidator<'value> with
//        member this.Validate(value: 'value): ExnResult<'value> = this.Validate value
//        member this.Validate(value: 'value): ValueTask<ExnResult<'value>> = this.Validate value |> ValueTask.FromResult
//        member this.ValidateAggregate(value: 'value): EResult<'value, AggregateException> = this.ValidateAggregate value
//        member this.ValidateAggregate(value: 'value): ValueTask<EResult<'value,AggregateException>> = this.ValidateAggregate value |> ValueTask.FromResult
//
//type [<Struct>] MultiValidator30<'value, 'validator, 'validator2, 'validator3 when 'validator: struct
//                                                                               and 'validator: (new: unit -> 'validator)
//                                                                               and 'validator :> IValidator<'value>
//                                                                               and 'validator2: struct
//                                                                               and 'validator2: (new: unit -> 'validator2)
//                                                                               and 'validator2 :> IValidator<'value>
//                                                                               and 'validator3: struct
//                                                                               and 'validator3: (new: unit -> 'validator3)
//                                                                               and 'validator3 :> IValidator<'value>> =
//    member this.Validate(value: 'value): ExnResult<'value> =
//            match (new 'validator()).Validate value with
//            | Ok value ->
//                match (new 'validator2()).Validate value with
//                | Ok value ->
//                    match (new 'validator3()).Validate value with
//                    | Ok value -> value |> Ok
//                    | Error err -> Error err
//                | Error err -> Error err
//            | Error err -> Error err
//
//    member this.ValidateAggregate value : EResult<'value, AggregateException> =
//        match (new 'validator()).Validate value, (new 'validator2()).Validate value, (new 'validator3()).Validate value with
//        | Ok _, Ok _, Ok _ ->
//            value |> Ok
//        | Error exn1, Error exn2, Error exn3 ->
//            Error (AggregateException(exn1, exn2, exn3))
//        | Error exn, Error exn2, _
//        | Error exn, _, Error exn2
//        | _, Error exn, Error exn2 ->
//            Error (AggregateException(exn, exn2))
//        | Error exn, _, _
//        | _, Error exn, _
//        | _, _, Error exn ->
//            Error (AggregateException(exn))
//
//    interface IValidator<'value> with
//        member this.Validate(value: 'value): ExnResult<'value> = this.Validate value
//        member this.Validate(value: 'value): ValueTask<ExnResult<'value>> = this.Validate value |> ValueTask.FromResult
//        member this.ValidateAggregate(value: 'value): EResult<'value, AggregateException> = this.ValidateAggregate value
//        member this.ValidateAggregate(value: 'value): ValueTask<EResult<'value,AggregateException>> = this.ValidateAggregate value |> ValueTask.FromResult

type [<Struct>] internal ExnBag7 =
    val mutable private count: int
    val mutable private exn1: exn
    val mutable private exn2: exn
    val mutable private exn3: exn
    val mutable private exn4: exn
    val mutable private exn5: exn
    val mutable private exn6: exn
    val mutable private exn7: exn
    member this.Add(exn) =
        match this.count with
        | offset when offset < 7 ->
            Unsafe.Add(&this.exn1, offset) <- exn
            this.count <- this.count + 1
        | _ -> ()

    member this.IsEmpty = this.count = 0

    member this.ToArray() =
        match this.count with
        | 0 -> [||]
        | arrayLength ->
            let span = MemoryMarshal.CreateSpan(&this.exn1, arrayLength)
            span.ToArray()