[<AutoOpen>]
module En3Tho.FSharp.Extensions.ActivePatterns

open System
open Core

let inline (|Null|_|) value = isNull value
let inline (|NotNull|_|) value = isNotNull value

let inline (|Eq|_|) with' what = what == with'
let inline (|Neq|_|) with' what = what <> with'
let inline (|Gt|_|) with' what = what > with'
let inline (|GtEq|_|) with' what = what >= with'
let inline (|Lt|_|) with' what = what < with'
let inline (|LtEq|_|) with' what = what <= with'
let inline (|RefEq|_|) with' what = referenceEquals with' what

let inline (|Equals|_|) with' what = what == with'
let inline (|NotEquals|_|) with' what = what <> with'
let inline (|GreaterThan|_|) with' what = what > with'
let inline (|GreaterThanEquals|_|) with' what = what >= with'
let inline (|LessThan|_|) with' what = what < with'
let inline (|LessThanEquals|_|) with' what = what <= with'
let inline (|ReferenceEquals|_|) with' what = referenceEquals with' what

let inline (|NullableSome|_|) (value: 'a Nullable) =
    if value.HasValue then ValueSome value.Value else ValueNone

let inline (|NullableNone|_|) (value: 'a Nullable) =
    value.HasValue |> not

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