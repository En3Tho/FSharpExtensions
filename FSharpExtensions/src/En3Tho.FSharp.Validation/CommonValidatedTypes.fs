﻿module En3Tho.FSharp.Validation.CommonValidatedTypes

open System
open System.Collections.Generic
open System.Collections.Immutable
open System.Threading.Tasks
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.Validation

module PlainValue =
    type [<Struct>] Validator<'a> =
         member this.Validate(value) = Ok value

         interface IValidator<'a> with
             member this.Validate value : ExnResult<'a> = this.Validate value
             member this.Validate value : AsyncExnResult<'a> = this.Validate value |> ValueTask.FromResult
             member this.ValidateAggregate value: EResult<'a, AggregateException> = this.Validate value
             member this.ValidateAggregate value: ValueTask<EResult<'a, AggregateException>> = this.Validate value |> ValueTask.FromResult

type PlainValue<'a> = NewCtorValidatorValidated<'a, PlainValue.Validator<'a>>
let inline (|PlainValue|) (value: PlainValue<'a>) = value.Value
let [<return: Struct>] inline (|IsPlainValue|_|) (value: 'a) = PlainValue<'a>.Try value

module NonNullValue =
    type ValueIsNullException() = inherit ValidationException(nameof(ValueIsNullException)) // TODO: argument null exception?
        
    type [<Struct>] Validator<'a when 'a: not struct> =
        member this.Validate(value) =
            if Object.ReferenceEquals(value, null) then Error (ValueIsNullException() :> exn)
            else Ok value

        interface IValidator<'a> with
            member this.Validate value : ExnResult<'a> = this.Validate value
            member this.Validate value : AsyncExnResult<'a> = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value: EResult<'a, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value: ValueTask<EResult<'a, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type NonNullValue<'a when 'a: not struct> = NewCtorValidatorValidated<'a, NonNullValue.Validator<'a>>
let inline (|NonNullValue|) (value: NonNullValue<'a>) = value.Value
let [<return: Struct>] inline (|IsNonNullValue|_|) (value: 'a) = NonNullValue<'a>.Try value

module NonDefaultValue =
    type ValueIsDefaultException() = inherit ValidationException(nameof(ValueIsDefaultException))

    type [<Struct>] Validator<'a when 'a: struct and 'a :> IEquatable<'a>> =
        member this.Validate(value: 'a) =
            if value.Equals(Unchecked.defaultof<'a>) then Error (ValueIsDefaultException() :> exn)
            else Ok value

        interface IValidator<'a> with
            member this.Validate value : ExnResult<'a> = this.Validate value
            member this.Validate value : AsyncExnResult<'a> = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<'a, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<'a, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type NonDefaultValue<'a when 'a: struct and 'a :> IEquatable<'a>> = NewCtorValidatorValidated<'a, NonDefaultValue.Validator<'a>>
let inline (|NonDefaultValue|) (value: NonDefaultValue<'a>) = value.Value
let [<return: Struct>] inline (|IsNonDefaultValue|_|) (value: 'a) = NonDefaultValue<'a>.Try value

module NonNegativeValue =
    type ValueIsNegativeException() = inherit ValidationException(nameof(ValueIsNegativeException))
        
    type [<Struct>] Validator<'a when 'a :> IComparable<'a> and 'a: struct> =
        member this.Validate(value: 'a) =
            if value.CompareTo(Unchecked.defaultof<'a>) == -1 then Error (ValueIsNegativeException() :> exn)
            else Ok value
        interface IValidator<'a> with
            member this.Validate value : ExnResult<'a> = this.Validate value
            member this.Validate value : AsyncExnResult<'a> = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<'a, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<'a, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

// this can be of INumber
type NonNegativeValue<'a when 'a :> IComparable<'a> and 'a: struct> = NewCtorValidatorValidated<'a, NonNegativeValue.Validator<'a>>
let inline (|NonNegativeValue|) (value: NonNegativeValue<'a>) = value.Value
let [<return: Struct>] inline (|IsNonNegativeValue|_|) (value: 'a) = NonNegativeValue<'a>.Try value

module NegativeValue =
    type ValueIsNotNegativeException() = inherit ValidationException(nameof(ValueIsNotNegativeException))

    type [<Struct>] Validator<'a when 'a :> IComparable<'a> and 'a: struct> =
        member this.Validate(value: 'a) =
            if value.CompareTo(Unchecked.defaultof<'a>) == 1 then Ok value
            else Error (ValueIsNotNegativeException() :> exn)
        interface IValidator<'a> with
            member this.Validate value : ExnResult<'a> = this.Validate value
            member this.Validate value : AsyncExnResult<'a> = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<'a, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<'a, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type NegativeValue<'a when 'a :> IComparable<'a> and 'a: struct> = NewCtorValidatorValidated<'a, NonNegativeValue.Validator<'a>>
let inline (|NegativeValue|) (value: NonNegativeValue<'a>) = value.Value
let [<return: Struct>] inline (|IsNegativeValue|_|) (value: 'a) = NegativeValue<'a>.Try value

module NonEmptyString =
    type StringIsNullException() = inherit ValidationException(nameof(StringIsNullException))
    type StringIsEmptyException() = inherit ValidationException(nameof(StringIsEmptyException))

    type [<Struct>] Validator =
        member this.Validate(value) =
                if Object.ReferenceEquals(value, null) then Error (StringIsNullException() :> exn)
                elif String.IsNullOrEmpty value then Error (StringIsEmptyException() :> exn)
                else Ok value
        interface IValidator<string> with
            member this.Validate(value) = this.Validate value
            member this.Validate(value) = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<string, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<string, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type NonEmptyString = NewCtorValidatorValidated<string, NonEmptyString.Validator>
let inline (|NonEmptyString|) (value: NonEmptyString) = value.Value
let [<return: Struct>] inline (|IsNonEmptyString|_|) (value: 'a) = NonEmptyString.Try value

module NonEmptyGuid =
    type GuidIsEmptyException() = inherit ValidationException(nameof(GuidIsEmptyException))

    type [<Struct>] Validator =
        member this.Validate(value) =
            if Guid.Empty = value then Error (GuidIsEmptyException() :> exn)
            else Ok value

        interface IValidator<Guid> with
            member this.Validate(value) = this.Validate value
            member this.Validate(value) = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<Guid, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<Guid, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type NonEmptyGuid = NewCtorValidatorValidated<Guid, NonEmptyGuid.Validator>
let inline (|NonEmptyGuid|) (value: NonEmptyGuid) = value.Value
let [<return: Struct>] inline (|IsNonEmptyGuid|_|) (value: 'a) = NonEmptyGuid.Try value

module NonEmptyArray =
    type ArrayIsNullException() = inherit ValidationException(nameof(ArrayIsNullException))
    type ArrayIsEmptyException() = inherit ValidationException(nameof(ArrayIsEmptyException))

    type [<Struct>] Validator<'a> =
        member this.Validate(value) =
            if isNull value then Error (ArrayIsNullException() :> exn)
            elif Array.isEmpty value then Error (ArrayIsEmptyException() :> exn)
            else Ok value

        interface IValidator<'a array> with
            member this.Validate value : ExnResult<'a array> = this.Validate value
            member this.Validate value : AsyncExnResult<'a array> = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<'a array, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<'a array, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type NonEmptyArray<'a> = NewCtorValidatorValidated<'a array, NonEmptyArray.Validator<'a>>
let inline (|NonEmptyArray|) (value: NonEmptyArray<'a>) = value.Value
let [<return: Struct>] inline (|IsNonEmptyArray|_|) (value: 'a) = NonEmptyArray<'a>.Try value

module NonEmptyBlock =
    type ArrayIsNullException() = inherit ValidationException(nameof(ArrayIsNullException))
    type ArrayIsEmptyException() = inherit ValidationException(nameof(ArrayIsEmptyException))

    type [<Struct>] Validator<'a> =
        member this.Validate(value: ImmutableArray<'a>) =
            if value.IsDefault then Error (ArrayIsNullException() :> exn)
            elif value.IsEmpty then Error (ArrayIsEmptyException() :> exn)
            else Ok value

        interface IValidator<'a ImmutableArray> with
            member this.Validate value : ExnResult<'a ImmutableArray> = this.Validate value
            member this.Validate value : AsyncExnResult<'a ImmutableArray> = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<'a ImmutableArray, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<'a ImmutableArray, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type NonEmptyBlock<'a> = NewCtorValidatorValidated<'a ImmutableArray, NonEmptyBlock.Validator<'a>>
let inline (|NonEmptyBlock|) (value: NonEmptyArray<'a>) = value.Value
let [<return: Struct>] inline (|IsNonEmptyBlock|_|) (value: 'a) = NonEmptyBlock<'a>.Try value

module NonEmptyList =
    type ListIsNullException() = inherit ValidationException(nameof(ListIsNullException))
    type ListIsEmptyException() = inherit ValidationException(nameof(ListIsEmptyException))

    type [<Struct>] Validator<'a> =
        member this.Validate(value) =
            if Object.ReferenceEquals(value, null) then Error (ListIsNullException() :> exn)
            elif List.isEmpty value then Error (ListIsEmptyException() :> exn)
            else Ok value

        interface IValidator<'a list> with
            member this.Validate value : ExnResult<'a list> = this.Validate value
            member this.Validate value : AsyncExnResult<'a list> = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<'a list, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<'a list, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type NonEmptyList<'a> = NewCtorValidatorValidated<'a list, NonEmptyList.Validator<'a>>
let inline (|NonEmptyList|) (value: NonEmptyList<'a>) = value.Value
let [<return: Struct>] inline (|IsNonEmptyList|_|) (value: 'a) = NonEmptyList<'a>.Try value

module NonEmptyCollection =
    type CollectionIsNullException() = inherit ValidationException(nameof(CollectionIsNullException))
    type CollectionIsEmptyException() = inherit ValidationException(nameof(CollectionIsEmptyException))

    type [<Struct>] Validator<'a, 'b when 'a :> ICollection<'b>> =
        member this.Validate(value: 'a) =
            if Object.ReferenceEquals(value, null) then Error (CollectionIsNullException() :> exn)
            elif value.Count = 0 then Error (CollectionIsEmptyException() :> exn)
            else Ok value

        interface IValidator<'a> with
            member this.Validate(value) = this.Validate value
            member this.Validate(value) = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<'a, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<'a, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type NonEmptyCollection<'a, 'b when 'a :> ICollection<'b>> = NewCtorValidatorValidated<'a, NonEmptyCollection.Validator<'a, 'b>>
let inline (|NonEmptyCollection|) (value: NonEmptyCollection<'a, 'b>) = value.Value
let [<return: Struct>] inline (|IsNonEmptyCollection|_|) (value: 'a) = NonEmptyCollection<'a, 'b>.Try value

module GuidString =
    type StringIsNotGuidParseableException() = inherit ValidationException(nameof(StringIsNotGuidParseableException))

    type [<Struct>] Validator =
        member this.Validate(value: string) =
            match Guid.TryParse value with
            | true, _ -> Ok value
            | _ -> Error (StringIsNotGuidParseableException() :> exn)

        interface IValidator<string> with
            member this.Validate(value) = this.Validate value
            member this.Validate(value) = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<string, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<string, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type GuidString = NewCtorValidatorValidated<string, MultiValidator20<string, NonEmptyString.Validator, GuidString.Validator>>
let inline (|GuidString|) (value: GuidString) = value.Value
let [<return: Struct>] inline (|IsGuidString|_|) (value: 'a) = GuidString.Try value

module ValidEnumValue =
    type ValueIsNotDefinedException() = inherit ValidationException(nameof(ValueIsNotDefinedException))

    type [<Struct>] Validator<'a when 'a: struct and 'a :> Enum and 'a: (new: unit -> 'a)> =
        member this.Validate(value) =
            if Enum.IsDefined value then Ok value
            else Error (ValueIsNotDefinedException() :> exn)

        interface IValidator<'a> with
            member this.Validate value : ExnResult<'a> = this.Validate value
            member this.Validate value : AsyncExnResult<'a> = this.Validate value |> ValueTask.FromResult
            member this.ValidateAggregate value : EResult<'a, AggregateException> = this.Validate value |> Result.mapError AggregateException
            member this.ValidateAggregate value : ValueTask<EResult<'a, AggregateException>> = this.Validate value |> Result.mapError AggregateException |> ValueTask.FromResult

type ValidEnum<'a when 'a: struct and 'a :> Enum and 'a: (new: unit -> 'a)> = NewCtorValidatorValidated<'a, ValidEnumValue.Validator<'a>>
let inline (|ValidEnum|) (value: ValidEnum<'a>) = value.Value
let [<return: Struct>] inline (|IsValidEnum|_|) (value: 'a) = ValidEnum<'a>.Try value