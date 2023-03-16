[<AutoOpen>]
module En3Tho.FSharp.Extensions.ActivePatterns

open System
open Core

let inline (|Null|_|) value = if isNull value then Option.someObj else None
let inline (|NotNull|_|) value = if isNotNull value then Option.someObj else None

// TODO: bench value option vs option on pattern check
let [<return: Struct>] inline (|Eq|_|) with' what = what == with' |> ValueOption.ofBool
let [<return: Struct>] inline (|Neq|_|) with' what = what <> with' |> ValueOption.ofBool
let [<return: Struct>] inline (|Gt|_|) with' what = what > with' |> ValueOption.ofBool
let [<return: Struct>] inline (|GtEq|_|) with' what = what >= with' |> ValueOption.ofBool
let [<return: Struct>] inline (|Lt|_|) with' what = what < with' |> ValueOption.ofBool
let [<return: Struct>] inline (|LtEq|_|) with' what = what <= with' |> ValueOption.ofBool
let [<return: Struct>] inline (|RefEq|_|) with' what = referenceEquals with' what |> ValueOption.ofBool

let [<return: Struct>] inline (|Equals|_|) with' what = what == with' |> ValueOption.ofBool
let [<return: Struct>] inline (|NotEquals|_|) with' what = what <> with' |> ValueOption.ofBool
let [<return: Struct>] inline (|GreaterThan|_|) with' what = what > with' |> ValueOption.ofBool
let [<return: Struct>] inline (|GreaterThanEquals|_|) with' what = what >= with' |> ValueOption.ofBool
let [<return: Struct>] inline (|LessThan|_|) with' what = what < with' |> ValueOption.ofBool
let [<return: Struct>] inline (|LessThanEquals|_|) with' what = what <= with' |> ValueOption.ofBool
let [<return: Struct>] inline (|ReferenceEquals|_|) with' what = referenceEquals with' what |> ValueOption.ofBool

let [<return: Struct>] inline (|NullableSome|_|) (value: 'a Nullable) =
    if value.HasValue then ValueSome value.Value else ValueNone

let [<return: Struct>] inline (|NullableNone|_|) (value: 'a Nullable) =
    value.HasValue |> not |> ValueOption.ofBool

module Requires =
    let inline (|NotNull|) (obj: 'a when 'a: not struct) = if Object.ReferenceEquals(obj, null) then nullArg "Value cannot be null" else obj
    let inline (|Eq|) value obj = if obj == value then obj else invalidArg "" $"Value should be equal to {value}"
    let inline (|Neq|) value obj = if obj <> value then obj else invalidArg "" $"Value should not be equal to {value}"
    let inline (|Gt|) value obj = if obj > value then obj else invalidArg "" $"Value should be greater than {value}"
    let inline (|GtEq|) value obj = if obj >= value then obj else invalidArg "" $"Value should be greater than or equal to {value}"
    let inline (|Lt|) value obj = if obj < value then obj else invalidArg "" $"Value should be less than {value}"
    let inline (|LtEq|) value obj = if obj <= value then obj else invalidArg "" $"Value should be less than or equal to {value}"

module It =
    let inline (|Id|) a = (^a: (member Id: ^b) a)
    let inline (|Name|) a = (^a: (member Name: ^b) a)
    let inline (|Value|) a = (^a: (member Value: ^b) a)
    let inline (|Values|) a = (^a: (member Values: ^b) a)
    let inline (|Item|) a = (^a: (member Item: ^b) a)
    let inline (|Items|) a = (^a: (member Items: ^b) a)
    let inline (|Index|) a = (^a: (member Index: ^b) a)
    let inline (|Type|) a = (^a: (member Type: ^b) a)
    let inline (|Current|) a = (^a: (member Current: ^b) a)
    let inline (|Length|) a = (^a: (member Length: ^b) a)
    let inline (|Count|) a = (^a: (member Count: ^b) a)
    let inline (|Message|) a = (^a: (member Message: ^b) a)
    let inline (|Text|) a = (^a: (member Text: ^b) a)
    let inline (|Result|) a = (^a: (member Result: ^b) a)
    let inline (|GetHashCode|) a = (^a: (member GetHashCode: unit -> int) a)
    let inline (|ToString|) a = (^a: (member ToString: unit -> string) a)    

    let inline Id a = (^a: (member Id: ^b) a)
    let inline Name a = (^a: (member Name: ^b) a)
    let inline Value a = (^a: (member Value: ^b) a)
    let inline Values a = (^a: (member Values: ^b) a)
    let inline Item a = (^a: (member Item: ^b) a)
    let inline Items a = (^a: (member Items: ^b) a)
    let inline Index a = (^a: (member Index: ^b) a)
    let inline Type a = (^a: (member Type: ^b) a)
    let inline Current a = (^a: (member Current: ^b) a)
    let inline Length a = (^a: (member Length: ^b) a)
    let inline Count a = (^a: (member Count: ^b) a)
    let inline Message a = (^a: (member Message: ^b) a)
    let inline Text a = (^a: (member Text: ^b) a)
    let inline Result a = (^a: (member Result: ^b) a)

    let inline GetHashCode a = (^a: (member GetHashCode: unit -> int) a)
    let inline ToString a = (^a: (member ToString: unit -> string) a)
    let inline GetType a = (^a: (member GetType: unit -> Type) a)
    let inline Add b a = (^a: (member Add: ^b -> ^c) a, b)
    let inline TryAdd b a = (^a: (member TryAdd: ^b -> ^c) a, b)
    let inline Remove b a = (^a: (member Remove: ^b -> ^c) a, b)
    let inline TryRemove b a = (^a: (member TryRemove: ^b -> ^c) a, b)
    let inline Update b a = (^a: (member Update: ^b -> ^c) a, b)
    let inline TryUpdate b a = (^a: (member TryUpdate: ^b -> ^c) a, b)