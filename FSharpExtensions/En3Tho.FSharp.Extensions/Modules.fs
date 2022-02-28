namespace En3Tho.FSharp.Extensions

open System
open System.Collections
open System.Collections.Concurrent
open System.Collections.Generic
open System.Collections.Immutable
open System.Linq
open System.Reflection
open System.Reflection.Emit
open System.Runtime.ExceptionServices
open System.Runtime.InteropServices
open System.Threading.Tasks
open FSharp.NativeInterop
open Microsoft.FSharp.Reflection
open En3Tho.FSharp.Extensions.Disposables
open En3Tho.FSharp.ComputationExpressions.Tasks

#nowarn "0077"
#nowarn "0042"

type block<'a> = ImmutableArray<'a>

[<AutoOpen; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
module PipeAndCompositionOperatorEx =

    [<AutoOpen; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    module Pipe1 =

        [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
        type T = T with
            static member inline ($) (_, invokable) = fun value -> (^a: (member Invoke: ^b -> ^c) invokable, value)
            static member inline ($) (_, [<InlineIfLambda>] invokable: 'a -> 'b) = fun value -> invokable value

        let inline (|>) value invokable = (T $ invokable) value
        let inline (>>) f1 f2 = fun value -> (T $ f2) ((T $ f1) value)
        let inline (<<) f1 f2 = fun value -> (T $ f1) ((T $ f2) value)

[<AutoOpen; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
module Pipe2 =

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    type T = T with
        static member inline ($) (T, invokable: ^a) = fun (value1, value2) -> (^a: (member Invoke: 'b * 'c -> 'd) invokable, value1, value2)
        static member inline ($) (T, [<InlineIfLambda>] invokable: 'a -> 'b -> 'c) = fun (value1, value2) -> invokable value1 value2

    let inline (||>) (value1: 'b, value2: 'c) invokable =
        (T $ invokable) (value1, value2)

[<AutoOpen; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
module Pipe3 =

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    type T = T with
        static member inline ($) (T, invokable: ^a) = fun (value1, value2, value3) -> (^a: (member Invoke: 'b * 'c * 'd -> 'e) invokable, value1, value2, value3)
        static member inline ($) (T, [<InlineIfLambda>] invokable: 'a -> 'b -> 'c -> 'd) = fun (value1, value2, value3) -> invokable value1 value2 value3

    let inline (|||>) (value1: 'b, value2: 'c, value3: 'd) invokable =
        (T $ invokable) (value1, value2, value3)

[<AutoOpen; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
module InvokeEx =

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    type T = T with
            static member inline ($) (_, invokable) = fun value -> (^a: (member Invoke: ^b -> ^c) invokable, value)
            static member inline ($) (_, [<InlineIfLambda>] invokable: 'a -> 'b) = fun value -> invokable value

    let inline (^) invokable value = (T $ invokable) value

[<AutoOpen; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
module IEquatableEqualityOperatorEx =

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    let inline callIEquatableEqualsOnValues<'a when 'a :> IEquatable<'a>> (left: 'a) (right: 'a) = left.Equals(right)

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    let inline callIEquatableEqualsOnArrays<'a when 'a :> IEquatable<'a>> (left: 'a[]) (right: 'a[]) =
        left.AsSpan().SequenceEqual(right)

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    let inline callIEquatableEqualsOnResizeArrays<'a when 'a :> IEquatable<'a>> (left: 'a ResizeArray) (right: 'a ResizeArray) =
        CollectionsMarshal.AsSpan(left).SequenceEqual(Span.op_Implicit(CollectionsMarshal.AsSpan(right))) // for some reason implicit conversion didn't work. This is a bug I think.

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    let inline callIEquatableEqualsOnILists<'a when 'a :> IEquatable<'a>> (left: 'a IList) (right: 'a IList) =
        left.Count = right.Count
        && (
            let count = left.Count
            let rec go index = // TODO: validate that compiler rewrites this into a while loop
                if uint index >= uint count then
                    true
                else
                    callIEquatableEqualsOnValues left[index] right[index]
                    && go (index + 1)
            go 0)

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    let inline callIEquatableEqualsOnSeq<'a when 'a :> IEquatable<'a>> (left: 'a seq) (right: 'a seq) =
        match left, right with
        | :? IList<'a> as left, (:? IList<'a> as right) ->
            callIEquatableEqualsOnILists left right
        | _ ->
            use leftEnumerator = left.GetEnumerator()
            use rightEnumerator = right.GetEnumerator()
            let rec go() =
                match leftEnumerator.MoveNext(), rightEnumerator.MoveNext() with
                | true, true ->
                    callIEquatableEqualsOnValues leftEnumerator.Current rightEnumerator.Current
                    && go()
                | false, false ->
                    true
                | _ ->
                    false
            go()

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    let inline callIEquatableEqualsOnLists<'a when 'a :> IEquatable<'a>> (left: 'a list) (right: 'a list) =
        let rec go left right = // TODO: validate that compiler rewrites this into a while loop
            match left, right with
            | leftHead :: leftTail, (rightHead :: rightTail) ->
                callIEquatableEqualsOnValues leftHead rightHead
                && go leftTail rightTail
            | [], [] ->
                true
            | [], _
            | _, [] ->
                false

        go left right

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    let inline callIEquatableEqualsOnHashSets<'a when 'a :> IEquatable<'a>> (left: 'a HashSet) (right: 'a HashSet) =
        left.Count = right.Count
        && Object.ReferenceEquals(left.Comparer, right.Comparer)
        && Object.ReferenceEquals(left.Comparer, EqualityComparer<'a>.Default)
        && (let mutable leftEnumerator = left.GetEnumerator()
            let rec go rightCount =
                if leftEnumerator.MoveNext() then
                    if rightCount = 0 then
                        false
                    else
                        let mutable rightValue = Unchecked.defaultof<_>
                        right.TryGetValue(leftEnumerator.Current, &rightValue)
                        && go (rightCount - 1)
                else
                    rightCount = 0
            go right.Count)

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    let inline callIEquatableEqualsOnDictionaries<'a, 'b when 'a :> IEquatable<'a> and 'b :> IEquatable<'b>> (left: Dictionary<'a, 'b>) (right: Dictionary<'a, 'b>) =
        left.Count = right.Count
        && Object.ReferenceEquals(left.Comparer, right.Comparer)
        && Object.ReferenceEquals(left.Comparer, EqualityComparer<'a>.Default)
        && (let mutable leftEnumerator = left.GetEnumerator()
            let rec go rightCount =
                if leftEnumerator.MoveNext() then
                    if rightCount = 0 then
                        false
                    else
                        let leftValue = leftEnumerator.Current
                        let mutable rightValue = Unchecked.defaultof<_>
                        right.TryGetValue(leftValue.Key, &rightValue)
                        && callIEquatableEqualsOnValues leftValue.Value rightValue
                        && go (rightCount - 1)
                else
                    rightCount = 0
            go right.Count)

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    type T = T with
        static member inline ($) (T, value) = fun otherValue -> callIEquatableEqualsOnValues value otherValue
        static member inline ($) (T, value) = fun otherValue -> callIEquatableEqualsOnArrays value otherValue
        static member inline ($) (T, value) = fun otherValue -> callIEquatableEqualsOnResizeArrays value otherValue
        static member inline ($) (T, value) = fun otherValue -> callIEquatableEqualsOnLists value otherValue
        static member inline ($) (T, value) = fun otherValue -> callIEquatableEqualsOnSeq value otherValue
        static member inline ($) (T, value) = fun otherValue -> callIEquatableEqualsOnHashSets value otherValue
        static member inline ($) (T, value) = fun otherValue -> callIEquatableEqualsOnDictionaries value otherValue

    let inline (==) a b = (T $ a) b

    let inline (!=) a b = not (a == b)

[<AutoOpen; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
module Core =

    [<AbstractClass; AutoOpen>]
    type Defer =
        static member inline defer ([<InlineIfLambda>] disposer) = UnitAsyncDisposable(disposer)
        static member inline defer ([<InlineIfLambda>] disposer) = new UnitDisposable(disposer)
        static member inline deferv ([<InlineIfLambda>] disposer) = fun value -> ValueAsyncDisposable<_>(value, disposer)
        static member inline deferv ([<InlineIfLambda>] disposer) = fun value -> new ValueDisposable<_>(value, disposer)

    let someObj = Some()

    /// cast via op_Implicit
    let inline icast< ^a, ^b when (^a or ^b): (static member op_Implicit: ^a -> ^b)> (value: ^a): ^b = ((^a or ^b): (static member op_Implicit: ^a -> ^b) value)

    /// cast via op_Explicit
    let inline ecast< ^a, ^b when (^a or ^b): (static member op_Explicit: ^a -> ^b)> (value: ^a): ^b = ((^a or ^b): (static member op_Explicit: ^a -> ^b) value)

    /// unsafe cast
    let inline ucast<'a, 'b> (a: 'a): 'b = (# "" a: 'b #)

    let ignore2 _ _ = ()
    let ignore3 _ _ _ = ()
    let inline referenceEquals< ^a when ^a : not struct> (obj1: ^a) (obj2: ^a) = Object.ReferenceEquals(obj1, obj2)

    let inline isNull< ^a when ^a : not struct> (obj: ^a) = Object.ReferenceEquals(obj, null)
    let inline isNotNull< ^a when ^a : not struct> (obj: ^a) = not ^ Object.ReferenceEquals(obj, null)
    let inline nullRef< ^a when ^a: not struct> = Unchecked.defaultof< ^a>
    let inline nullVal< ^a when ^a: struct> = Unchecked.defaultof< ^a>

    let inline toString (o: ^a) = o.ToString()

    [<Obsolete("This logic needs to be implemented")>]
    let inline TODO<'a> = raise ^ NotImplementedException()

    let inline (&==) (a: 'a when 'a: not struct) (b: 'a) = Object.ReferenceEquals(a, b)
    let inline (&!=) (a: 'a when 'a: not struct) (b: 'a) = not (Object.ReferenceEquals(a, b))

module Object =
    module Operators =
        let inline ( *==) a b =
          (^a: (static member op_Equality: 'a * 'a -> bool) (a, b))

        let inline ( *!=) a b = not (a *== b)

    open Operators
    let inline defaultValue def arg = if arg &== null then def else arg
    let inline defaultWith defThunk arg = if arg &== null then defThunk() else arg
    let inline nullCheck argName arg = if arg &== null then nullArg argName |> ignore
    let inline ensureNotNull argName arg = if arg &== null then nullArg argName else arg
    let toStringSafe str = if isNull str then "" else str.ToString()

module Functions =
    module Operators =
        let inline (&>>) f g = fun x -> f(); g x
        let inline (|>>) f g = fun x -> f x |> ignore; g x
    let inline ignoreAndReturnDefault _ = Unchecked.defaultof<'a>
    let inline ignoreAndReturnTrue _ = true
    let inline ignoreAndReturnFalse _ = false
    let inline ignore1AndReturnValue value = fun _ -> value
    let inline ignore2AndReturnValue value = fun _ _ -> value
    let inline ignore3AndReturnValue value = fun _ _ _ -> value
    let inline valueFun value = fun _ -> value
    let inline ignore2 _ _ = ()
    let inline ignore3 _ _ _ = ()
    let inline delay f x = fun _ -> f x
    let inline delay2 f x1 x2 = fun _ -> f x1 x2
    let inline delay3 f x1 x2 x3 = fun _ -> f x1 x2 x3
    let inline invokeWith x f = f x
    let inline revArgs2 (f: ^a -> ^b -> ^c) = fun (b: ^b) (a: ^a) -> f a b
    let inline revArgs3 (f: ^a -> ^b -> ^c -> ^d) = fun (c: ^c) (b: ^b) (a: ^a) -> f a b c

#if NET5_0_OR_GREATER
module Exception =
    let inline reraise ex = ExceptionDispatchInfo.Throw ex; Unchecked.defaultof<'a>
#endif

module Printf =
    type T = T with
        static member inline ($) (T, arg: unit) = ()
        static member inline ($) (T, arg: int) = 0 // mandatory second terminal case; is unused in runtime but is required for the code to compile
        static member inline ($) (T, func: ^a -> ^b): ^a -> ^b = fun (_: ^a) -> T $ Unchecked.defaultof< ^b>

    /// conditional ksprintf, ignores format on false and does not create any string
    let inline cksprintf runCondition stringFunc format: 'b =
        if runCondition then
           Printf.kprintf stringFunc format
        else
            T $ Unchecked.defaultof<'b>

module Option =
    open Object.Operators
    let iterAsync f opt =
        match opt with
        | Some value -> async.Bind(value, f)
        | None -> async.Zero()
    let mapAsync f opt = opt |> Option.map f |> async.Return
    let bindAsync f opt = opt |> Option.bind f |> async.Return
    let inline ofString str = if String.IsNullOrEmpty str then None else Some str
    let inline ofStringW str = if String.IsNullOrWhiteSpace str then None else Some str
    let inline ofBool bool = if bool then someObj else None
    let inline ofTryPattern (success, value) = if success then Some value else None
    let inline ofTryCast<'a> value =
        match box value with
        | :? 'a as value -> ValueSome value
        | _ -> ValueNone
    let inline ofCond condition (obj: ^a) = if condition obj then Some obj else None
    let inline ofAnyRef (obj: ^a) = if obj &== null then None else Some obj
    let inline toObjUnchecked opt = match opt with Some x -> x | None -> Unchecked.defaultof<_>
    let inline ofArray (array: 'a[]) = if array = null || array.Length = 0 then None else Some array
    let inline ofSeq (seq: 'a seq) = if seq = null || Seq.length seq = 0 then None else Some seq

    /// Similar to Option.iter but accepts an additional state value
    let inline iterv onSome value opt =
        match opt with
        | Some obj -> obj |> onSome value
        | None -> ()

module ValueOption =
    open Object.Operators
    let iterAsync f opt =
        match opt with
        | ValueSome value -> async.Bind(value, f)
        | ValueNone -> async.Zero()
    let mapAsync f opt = opt |> Option.map f |> async.Return
    let bindAsync f opt = opt |> Option.bind f |> async.Return
    let inline ofString str = if String.IsNullOrEmpty str then ValueNone else ValueSome str
    let inline ofStringW str = if String.IsNullOrWhiteSpace str then ValueNone else ValueSome str
    let inline ofBool bool = if bool then ValueSome() else ValueNone
    let inline ofTryPattern (success, value) = if success then ValueSome value else ValueNone
    let inline ofTryCast<'a> value =
        match box value with
        | :? 'a as value -> ValueSome value
        | _ -> ValueNone
    let inline ofCond condition (obj: ^a) = if condition obj then ValueSome obj else ValueNone
    let inline ofAnyRef (obj: ^a) = if obj &== null then ValueNone else ValueSome obj
    let inline toObjUnchecked opt = match opt with ValueSome x -> x | ValueNone -> Unchecked.defaultof<_>
    let inline ofArray (array: 'a[]) = if array = null || array.Length = 0 then ValueNone else ValueSome array
    let inline ofSeq (seq: 'a seq) = if seq = null || Seq.length seq = 0 then ValueNone else ValueSome seq

    /// Similar to Option.iter but accepts an additional state value
    let inline iterv onValueSome value opt =
        match opt with
        | ValueSome obj -> obj |> onValueSome value
        | ValueNone -> ()

module String =
    let inline nullOrWhiteSpaceCheck argName arg = if arg |> String.IsNullOrWhiteSpace then nullArg argName |> ignore
    let inline nullOrEmptyCheck argName arg = if arg |> String.IsNullOrEmpty then nullArg argName |> ignore
    let inline ensureNotNullOrWhiteSpace argName arg = if arg |> String.IsNullOrWhiteSpace then nullArg argName else arg
    let inline ensureNotNullOrEmpty argName arg = if arg |> String.IsNullOrEmpty then nullArg argName else arg
    let inline defaultValue def str = if String.IsNullOrEmpty str then def else str
    let inline defaultValueW def str = if String.IsNullOrWhiteSpace str then def else str
    let inline defaultWith defThunk str = if String.IsNullOrEmpty str then defThunk() else str
    let inline defaultWithW defThunk str = if String.IsNullOrWhiteSpace str then defThunk() else str
    let inline truncate maxLength (str: string) = if str.Length <= maxLength then str else str.Substring(0, maxLength)

    let [<return: Struct>] inline (|NullOrEmpty|_|) (str: string) = String.IsNullOrEmpty(str) |> ValueOption.ofBool
    let [<return: Struct>] inline (|NotNullOrEmpty|_|) (str: string) = String.IsNullOrEmpty(str) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|NullOrWhiteSpace|_|) (str: string) = String.IsNullOrWhiteSpace(str) |> ValueOption.ofBool
    let [<return: Struct>] inline (|NotNullOrWhiteSpace|_|) (str: string) = String.IsNullOrWhiteSpace(str) |> not |> ValueOption.ofBool
 
    let [<return: Struct>] inline (|Equals|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.Ordinal) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsIgnoreCase|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.OrdinalIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsCurrentCulture|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.CurrentCulture) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.CurrentCultureIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsInvariantCulture|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.InvariantCulture) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.InvariantCultureIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsChar|_|) (pattern: char) (str: string) = (str.Length = 1 && str.[0] = pattern) |> ValueOption.ofBool
 
    let [<return: Struct>] inline (|EqualsNot|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.Ordinal) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsNotIgnoreCase|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.OrdinalIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsNotCurrentCulture|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.CurrentCulture) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsNotCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.CurrentCultureIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsNotInvariantCulture|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.InvariantCulture) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsNotInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) = str.Equals(pattern, StringComparison.InvariantCultureIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsNotChar|_|) (pattern: char) (str: string) = (str.Length = 1 && str.[0] = pattern) |> not |> ValueOption.ofBool

    let private equalsAnyArray (patterns: string[]) (comparison: StringComparison) (str: string) =
        let mutable i = 0
        let mutable found = false
        while not found && i < patterns.Length do
            let pattern = patterns.[i]
            found <-  str.Equals(pattern, comparison)
        found

    let private equalsAnyList (patterns: string list) (comparison: StringComparison) (str: string) =
        let mutable list = patterns
        let mutable found = false
        while not found &&
            (match list with
             | pattern :: rest ->
                 found <- str.Equals(pattern, comparison)
                 list <- rest
                 true
             | [] ->
                 false) do ()
        found

    let private equalsAnySeq (patterns: string seq) (comparison: StringComparison) (str: string) =
        use enumerator = patterns.GetEnumerator()
        let mutable found = false
        while not found && enumerator.MoveNext() do
             let pattern = enumerator.Current
             found <- str.Equals(pattern, comparison)
        found

    let private equalsAnyCharArray (patterns: char[]) (str: string) =
        if str.Length > 1 then false else
        let mutable i = 0
        let mutable found = false
        while not found && i < patterns.Length do
            let pattern = patterns.[i]
            found <- str.[0] = pattern
        found

    let private equalsAnyCharList (patterns: char list) (str: string) =
        if str.Length > 1 then false else
        let mutable list = patterns
        let mutable found = false
        while not found &&
            (match list with
             | pattern :: rest ->
                 found <- str.[0] = pattern
                 list <- rest
                 true
             | [] ->
                 false) do ()
        found

    let private equalsAnyCharSeq (patterns: char seq) (str: string) =
        if str.Length > 1 then false else
        use enumerator = patterns.GetEnumerator()
        let mutable found = false
        while not found && enumerator.MoveNext() do
             let pattern = enumerator.Current
             found <- str.[0] = pattern
        found

    let equalsAny (patterns: string seq) (comparison: StringComparison) (str: string) =
        match patterns with
        | :? (string[]) as patterns -> equalsAnyArray patterns comparison str
        | :? (string list) as patterns -> equalsAnyList patterns comparison str
        | _ -> equalsAnySeq patterns comparison str

    let equalsAnyChar (patterns: char seq) (str: string) =
        match patterns with
        | :? (char[]) as patterns -> equalsAnyCharArray patterns str
        | :? (char list) as patterns -> equalsAnyCharList patterns str
        | _ -> equalsAnyCharSeq patterns str

    let [<return: Struct>] inline (|EqualsAny|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.Ordinal |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyIgnoreCase|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.OrdinalIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyCurrentCulture|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.CurrentCulture |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.CurrentCultureIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyInvariantCulture|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.InvariantCulture |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.InvariantCultureIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyChar|_|) (patterns: char[]) (str: string) = str |> equalsAnyChar patterns |> ValueOption.ofBool

    let [<return: Struct>] inline (|EqualsAnyNot|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.Ordinal |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyNotIgnoreCase|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.OrdinalIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyNotCurrentCulture|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.CurrentCulture |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyNotCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.CurrentCultureIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyNotInvariantCulture|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.InvariantCulture |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyNotInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> equalsAny patterns StringComparison.InvariantCultureIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EqualsAnyNotChar|_|) (patterns: char[]) (str: string) = str |> equalsAnyChar patterns |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|Contains|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.Ordinal) |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsIgnoreCase|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.OrdinalIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsCurrentCulture|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.CurrentCulture) |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.CurrentCultureIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsInvariantCulture|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.InvariantCulture) |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.InvariantCultureIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsChar|_|) (pattern: char) (str: string) = str.Contains(pattern) |> ValueOption.ofBool

    let [<return: Struct>] inline (|ContainsNot|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.Ordinal) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsNotIgnoreCase|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.OrdinalIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsNotCurrentCulture|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.CurrentCulture) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsNotCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.CurrentCultureIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsNotInvariantCulture|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.InvariantCulture) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsNotInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) = str.Contains(pattern, StringComparison.InvariantCultureIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsNotChar|_|) (pattern: char) (str: string) = str.Contains(pattern) |> not |> ValueOption.ofBool

    let private containsAnyArray (patterns: string[]) (comparison: StringComparison) (str: string) =
        let mutable i = 0
        let mutable found = false
        while not found && i < patterns.Length do
            let pattern = patterns.[i]
            found <-  str.Contains(pattern, comparison)
        found

    let private containsAnyList (patterns: string list) (comparison: StringComparison) (str: string) =
        let mutable list = patterns
        let mutable found = false
        while not found &&
            (match list with
             | pattern :: rest ->
                 found <- str.Contains(pattern, comparison)
                 list <- rest
                 true
             | [] ->
                 false) do ()
        found

    let private containsAnySeq (patterns: string seq) (comparison: StringComparison) (str: string) =
        use enumerator = patterns.GetEnumerator()
        let mutable found = false
        while not found && enumerator.MoveNext() do
             let pattern = enumerator.Current
             found <- str.Contains(pattern, comparison)
        found

    let private containsAnyCharArray (patterns: char[]) (str: string) =
        if str.Length <> 1 then false else
        let mutable i = 0
        let mutable found = false
        while not found && i < patterns.Length do
            let pattern = patterns.[i]
            found <- str.Contains(pattern)
        found

    let private containsAnyCharList (patterns: char list) (str: string) =
        if str.Length <> 1 then false else
        let mutable list = patterns
        let mutable found = false
        while not found &&
            (match list with
             | pattern :: rest ->
                 found <- str.Contains(pattern)
                 list <- rest
                 true
             | [] ->
                 false) do ()
        found

    let private containsAnyCharSeq (patterns: char seq) (str: string) =
        if str.Length <> 1 then false else
        use enumerator = patterns.GetEnumerator()
        let mutable found = false
        while not found && enumerator.MoveNext() do
             let pattern = enumerator.Current
             found <- str.Contains(pattern)
        found

    let containsAny (patterns: string seq) (comparison: StringComparison) (str: string) =
        match patterns with
        | :? (string[]) as patterns -> containsAnyArray patterns comparison str
        | :? (string list) as patterns -> containsAnyList patterns comparison str
        | _ -> containsAnySeq patterns comparison str

    let containsAnyChar (patterns: char seq) (str: string) =
        match patterns with
        | :? (char[]) as patterns -> containsAnyCharArray patterns str
        | :? (char list) as patterns -> containsAnyCharList patterns str
        | _ -> containsAnyCharSeq patterns str
    
    let [<return: Struct>] inline (|ContainsAny|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.Ordinal |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyIgnoreCase|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.OrdinalIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyCurrentCulture|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.CurrentCulture |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.CurrentCultureIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyInvariantCulture|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.InvariantCulture |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.InvariantCultureIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyChar|_|) (patterns: char[]) (str: string) = str |> containsAnyChar patterns |> ValueOption.ofBool

    let [<return: Struct>] inline (|ContainsAnyNot|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.Ordinal |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyNotIgnoreCase|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.OrdinalIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyNotCurrentCulture|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.CurrentCulture |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyNotCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.CurrentCultureIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyNotInvariantCulture|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.InvariantCulture |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyNotInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> containsAny patterns StringComparison.InvariantCultureIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|ContainsAnyNotChar|_|) (patterns: char[]) (str: string) = str |> containsAnyChar patterns |> not |> ValueOption.ofBool

    let [<return: Struct>] inline (|StartsWith|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.Ordinal) |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithIgnoreCase|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.OrdinalIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithCurrentCulture|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.CurrentCulture) |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithInvariantCulture|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.InvariantCulture) |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.InvariantCultureIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithChar|_|) (pattern: char) (str: string) = str.StartsWith(pattern) |> ValueOption.ofBool

    let [<return: Struct>] inline (|StartsWithNot|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.Ordinal) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithNotIgnoreCase|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.OrdinalIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithNotCurrentCulture|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.CurrentCulture) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithNotCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithNotInvariantCulture|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.InvariantCulture) |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithNotInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) = str.StartsWith(pattern, StringComparison.InvariantCultureIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithNotChar|_|) (pattern: char) (str: string) = str.StartsWith(pattern) |> not |> ValueOption.ofBool
    
    let private startsWithAnyArray (patterns: string[]) (comparison: StringComparison) (str: string) =
        let mutable i = 0
        let mutable found = false
        while not found && i < patterns.Length do
            let pattern = patterns.[i]
            found <-  str.StartsWith(pattern, comparison)
        found

    let private startsWithAnyList (patterns: string list) (comparison: StringComparison) (str: string) =
        let mutable list = patterns
        let mutable found = false
        while not found &&
            (match list with
             | pattern :: rest ->
                 found <- str.StartsWith(pattern, comparison)
                 list <- rest
                 true
             | [] ->
                 false) do ()
        found

    let private startsWithAnySeq (patterns: string seq) (comparison: StringComparison) (str: string) =
        use enumerator = patterns.GetEnumerator()
        let mutable found = false
        while not found && enumerator.MoveNext() do
             let pattern = enumerator.Current
             found <- str.StartsWith(pattern, comparison)
        found

    let private startsWithAnyCharArray (patterns: char[]) (str: string) =
        if str.Length <> 1 then false else
        let mutable i = 0
        let mutable found = false
        while not found && i < patterns.Length do
            let pattern = patterns.[i]
            found <- str.StartsWith(pattern)
        found

    let private startsWithAnyCharList (patterns: char list) (str: string) =
        if str.Length <> 1 then false else
        let mutable list = patterns
        let mutable found = false
        while not found &&
            (match list with
             | pattern :: rest ->
                 found <- str.StartsWith(pattern)
                 list <- rest
                 true
             | [] ->
                 false) do ()
        found

    let private startsWithAnyCharSeq (patterns: char seq) (str: string) =
        if str.Length <> 1 then false else
        use enumerator = patterns.GetEnumerator()
        let mutable found = false
        while not found && enumerator.MoveNext() do
             let pattern = enumerator.Current
             found <- str.StartsWith(pattern)
        found

    let startsWithAny (patterns: string seq) (comparison: StringComparison) (str: string) =
        match patterns with
        | :? (string[]) as patterns -> startsWithAnyArray patterns comparison str
        | :? (string list) as patterns -> startsWithAnyList patterns comparison str
        | _ -> startsWithAnySeq patterns comparison str

    let startsWithAnyChar (patterns: char seq) (str: string) =
        match patterns with
        | :? (char[]) as patterns -> startsWithAnyCharArray patterns str
        | :? (char list) as patterns -> startsWithAnyCharList patterns str
        | _ -> startsWithAnyCharSeq patterns str
    
    let [<return: Struct>] inline (|StartsWithAny|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.Ordinal |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyIgnoreCase|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.OrdinalIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyCurrentCulture|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.CurrentCulture |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.CurrentCultureIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyInvariantCulture|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.InvariantCulture |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.InvariantCultureIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyChar|_|) (patterns: char[]) (str: string) = str |> startsWithAnyChar patterns |> ValueOption.ofBool

    let [<return: Struct>] inline (|StartsWithAnyNot|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.Ordinal |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyNotIgnoreCase|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.OrdinalIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyNotCurrentCulture|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.CurrentCulture |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyNotCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.CurrentCultureIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyNotInvariantCulture|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.InvariantCulture |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyNotInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> startsWithAny patterns StringComparison.InvariantCultureIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|StartsWithAnyNotChar|_|) (patterns: char[]) (str: string) = str |> startsWithAnyChar patterns |> not |> ValueOption.ofBool
    
    let [<return: Struct>] inline (|EndsWith|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.Ordinal) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithIgnoreCase|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.OrdinalIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithCurrentCulture|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.CurrentCulture) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithInvariantCulture|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.InvariantCulture) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.InvariantCultureIgnoreCase) |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithChar|_|) (pattern: char) (str: string) = str.EndsWith(pattern) |> ValueOption.ofBool

    let [<return: Struct>] inline (|EndsWithNot|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.Ordinal) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithNotIgnoreCase|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.OrdinalIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithNotCurrentCulture|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.CurrentCulture) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithNotCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithNotInvariantCulture|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.InvariantCulture) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithNotInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) = str.EndsWith(pattern, StringComparison.InvariantCultureIgnoreCase) |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithNotChar|_|) (pattern: char) (str: string) = str.EndsWith(pattern) |> not |> ValueOption.ofBool
    
    let private endsWithAnyArray (patterns: string[]) (comparison: StringComparison) (str: string) =
        let mutable i = 0
        let mutable found = false
        while not found && i < patterns.Length do
            let pattern = patterns.[i]
            found <-  str.EndsWith(pattern, comparison)
        found

    let private endsWithAnyList (patterns: string list) (comparison: StringComparison) (str: string) =
        let mutable list = patterns
        let mutable found = false
        while not found &&
            (match list with
             | pattern :: rest ->
                 found <- str.EndsWith(pattern, comparison)
                 list <- rest
                 true
             | [] ->
                 false) do ()
        found

    let private endsWithAnySeq (patterns: string seq) (comparison: StringComparison) (str: string) =
        use enumerator = patterns.GetEnumerator()
        let mutable found = false
        while not found && enumerator.MoveNext() do
             let pattern = enumerator.Current
             found <- str.EndsWith(pattern, comparison)
        found

    let private endsWithAnyCharArray (patterns: char[]) (str: string) =
        if str.Length <> 1 then false else
        let mutable i = 0
        let mutable found = false
        while not found && i < patterns.Length do
            let pattern = patterns.[i]
            found <- str.EndsWith(pattern)
        found

    let private endsWithAnyCharList (patterns: char list) (str: string) =
        if str.Length <> 1 then false else
        let mutable list = patterns
        let mutable found = false
        while not found &&
            (match list with
             | pattern :: rest ->
                 found <- str.EndsWith(pattern)
                 list <- rest
                 true
             | [] ->
                 false) do ()
        found

    let private endsWithAnyCharSeq (patterns: char seq) (str: string) =
        if str.Length <> 1 then false else
        use enumerator = patterns.GetEnumerator()
        let mutable found = false
        while not found && enumerator.MoveNext() do
             let pattern = enumerator.Current
             found <- str.EndsWith(pattern)
        found

    let endsWithAny (patterns: string seq) (comparison: StringComparison) (str: string) =
        match patterns with
        | :? (string[]) as patterns -> endsWithAnyArray patterns comparison str
        | :? (string list) as patterns -> endsWithAnyList patterns comparison str
        | _ -> endsWithAnySeq patterns comparison str

    let endsWithAnyChar (patterns: char seq) (str: string) =
        match patterns with
        | :? (char[]) as patterns -> endsWithAnyCharArray patterns str
        | :? (char list) as patterns -> endsWithAnyCharList patterns str
        | _ -> endsWithAnyCharSeq patterns str
    
    let [<return: Struct>] inline (|EndsWithAny|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.Ordinal |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyIgnoreCase|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.OrdinalIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyCurrentCulture|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.CurrentCulture |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.CurrentCultureIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyInvariantCulture|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.InvariantCulture |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.InvariantCultureIgnoreCase |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyChar|_|) (patterns: char[]) (str: string) = str |> endsWithAnyChar patterns |> ValueOption.ofBool

    let [<return: Struct>] inline (|EndsWithAnyNot|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.Ordinal |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyNotIgnoreCase|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.OrdinalIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyNotCurrentCulture|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.CurrentCulture |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyNotCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.CurrentCultureIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyNotInvariantCulture|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.InvariantCulture |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyNotInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) = str |> endsWithAny patterns StringComparison.InvariantCultureIgnoreCase |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|EndsWithAnyNotChar|_|) (patterns: char[]) (str: string) = str |> endsWithAnyChar patterns |> not |> ValueOption.ofBool

type EnumShape<'enum when 'enum: struct
                      and 'enum :> Enum
                      and 'enum: (static member (|||): 'enum -> 'enum -> 'enum )
                      and 'enum: (static member (^^^): 'enum -> 'enum -> 'enum )
                      and 'enum: (static member (&&&): 'enum -> 'enum -> 'enum )> = 'enum
module Enum =
    let inline addFlag (flag: EnumShape<_>) (value: EnumShape<_>) = flag ||| value
    let inline addFlagWhen condition (flag: EnumShape<_>) (value: EnumShape<_>) = if condition then flag ||| value else value
    let inline removeFlag (flag: EnumShape<_>) (value: EnumShape<_>) = (flag ||| value) ^^^ flag
    let inline removeFlagWhen condition (flag: EnumShape<_>) (value: EnumShape<_>) = if condition then (flag ||| value) ^^^ flag else value
    let inline hasFlag (flag: EnumShape<_>) (value: EnumShape<_>) = value &&& flag = flag
    let inline isValid (value: EnumShape<'a>) = Enum.GetValues<'a>().AsSpan().Contains value
    let [<return: Struct>] inline (|HasFlag|_|) flag value = hasFlag flag value |> ValueOption.ofBool
    let [<return: Struct>] inline (|HasFlagNot|_|) flag value = hasFlag flag value |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|Valid|_|) value = isValid value |> ValueOption.ofBool
    let [<return: Struct>] inline (|ValidNot|_|) value = isValid value |> not |> ValueOption.ofBool

module Result =
    /// wraps a function with unit argument into try catch block returning a result
    let inline wrap f = // TODO: F# 6 Inline if lambda attribute
        try f() |> Ok
        with e -> e |> Error

    /// wraps a function with 1 additional argument into try catch block returning a result
    let inline wrapv f value =
        try f value |> Ok
        with e -> e |> Error

    /// Similar to Option.defaultWith but receives an mapError-like func
    let inline defaultWith f result =
        match result with
        | Ok v -> v
        | Error e -> f e

    /// Similar to Option.defaultValue but error is ignored
    let inline defaultValue value result =
        match result with
        | Ok v -> v
        | Error _ -> value

type EResult<'a, 'b when 'b :> exn> = Result<'a, 'b>
type AsyncEResult<'a, 'b when 'b :> exn> = Result<'a, 'b> ValueTask

type ExnResult<'a> = EResult<'a, exn>
type AsyncExnResult<'a> = EResult<'a, exn> ValueTask

module EResult =
    let inline unwrap (result: EResult<'a, 'b>) =
        match result with
        | Ok value -> value
        | Error err ->
            ExceptionDispatchInfo.Throw err
            Unchecked.defaultof<_>

    let inline unwrapWithStackTrace (result: EResult<'a, 'b>) =
        match result with
        | Ok value -> value
        | Error err ->
            ExceptionDispatchInfo.Throw ^
                if err.StackTrace = null then
                    err |> ExceptionDispatchInfo.SetCurrentStackTrace
                else
                    err :> exn
            Unchecked.defaultof<_>

    let inline defaultValue defaultValue (result: EResult<'a, 'b>) =
        match result with
        | Ok value -> value
        | _ -> defaultValue

    let inline defaultWith defThunk (result: EResult<'a, 'b>) =
        match result with
        | Ok value -> value
        | Error exn -> defThunk exn

    let inline trySetCurrentStackTrace (result: EResult<'a, 'b>) =
        match result with
        | Error exn when String.IsNullOrEmpty exn.StackTrace ->
            exn |> ExceptionDispatchInfo.SetCurrentStackTrace :?> 'b |> Error
        | _ -> result

module Async =
    let rec retryWhile condition retries work = async {
        let! result = work
        if condition result && retries > 0 then
            return! retryWhile condition (retries - 1) work
        else
            return result
    }

    let rec retryWhileWith condition delay retries work = async {
        let! result = work
        if condition result && retries > 0 then
            do! delay
            return! retryWhileWith condition delay (retries - 1) work
        else
            return result
    }

    let inline ofObj x = async.Return x

    module Array =
        let map2 mapping (array1: 'T[]) (array2: 'U[]) = async {
            Object.nullCheck "array1" array1
            Object.nullCheck "array2" array2
            let f = OptimizedClosures.FSharpFunc<_, _, _>.Adapt(mapping)
            if array1.Length <> array2.Length then "Arrays cannot have different length" |> invalidOp
            let res = Array.zeroCreate array1.Length
            for i = 0 to res.Length - 1 do
                let! result = f.Invoke(array1.[i], array2.[i])
                res.[i] <- result
            return res
        }

        let fold<'T, 'State> folder (state: 'State) (array: 'T[]) = async {
            Object.nullCheck "array" array
            let f = OptimizedClosures.FSharpFunc<_, _, _>.Adapt(folder)
            let mutable state = state
            for i = 0 to array.Length - 1 do
                let! result = f.Invoke(state, array.[i])
                state <- result
            return state
        }

    module Debug =
        let inline time logf work = async {
            let start = DateTime.Now
            let! result = work
            logf (DateTime.Now - start)
            return result
        }

module Byref =
    module Operators =
        let inline inc (a: 'a byref) = a <- a + LanguagePrimitives.GenericOne
        let inline dec (a: 'a byref) = a <- a - LanguagePrimitives.GenericOne
        let inline neg (a: 'a byref) = a <- (~-)a
        let inline (??<-) (a: 'a byref) v = if isNull a then a <- v
        let inline (???<-) (a: 'a byref) f = if isNull a then a <- f()
        let inline (+<-) (a: 'a byref) v = a <- a + v
        let inline (-<-) (a: 'a byref) v = a <- a - v
        let inline (/<-) (a: 'a byref) v = a <- a / v
        let inline (%<-) (a: 'a byref) v = a <- a % v
        let inline ( *<- ) (a: 'a byref) v = a <- a * v
        let inline (~~~) (a: 'a byref) = a <- ~~~a
        let inline (&&&<-) (a: 'a byref) v = a <- a &&& v
        let inline (|||<-) (a: 'a byref) v = a <- a ||| v
        let inline (^^^<-) (a: 'a byref) v = a <- a ^^^ v
        let inline (<<<<-) (a: 'a byref) v = a <- a <<< v
        let inline (>>><-) (a: 'a byref) v = a <- a >>> v
        let inline (&&<-) (a: bool byref) v = a <- a && v
        let inline (||<-) (a: bool byref) v = a <- a || v
        let inline ( **<- ) (a: 'a byref) v = a <- a ** v
        let inline (@<-) (a: 'a list byref) v = a <- a @ v
        let inline (|><-) (a: 'a byref) v = a <- a |> v

    /// all setters are reversed (byref is first parameter) because usually you want to write something into byref
    /// and pass (setv &ref or similar) into a function which then will write the value
    let inline setv (a: 'a byref) v = a <- v
    let inline setfn (a: 'a byref) f v = a <- f v
    let inline seti (a: 'a byref) v _ = a <- v
    let inline setTrue (a: bool byref) _ = a <- true
    let inline setFalse (a: bool byref) _ = a <- false
    let inline setZero (a: 'a byref) _ = a <- LanguagePrimitives.GenericZero
    let inline setOne (a: 'a byref) _ = a <- LanguagePrimitives.GenericOne
    let inline setMinusOne (a: 'a byref) _ = a <- -LanguagePrimitives.GenericOne
    let inline setParse (a: 'a byref) v = a <- (^a: (static member Parse: string -> 'a) v)
    let inline defaultValue (a: 'a byref) v = if isNull a then a <- v
    let inline defaultWith (a: 'a byref) defThunk = if isNull a then a <- defThunk()

module EqualityComparer =
    [<Sealed>]
    type private MapEqualityComparer<'a, 'b when 'b: equality>(map: 'a -> 'b) =
        inherit EqualityComparer<'a>()
        override this.Equals(x, y) = map x = map y
        override this.GetHashCode obj = map obj |> hash

    [<Sealed>]
    type private DelegateEqualityComparer<'a>(eq, ghc) =
        inherit EqualityComparer<'a>()
        override this.Equals(x, y) = eq x y
        override this.GetHashCode obj = ghc obj

    type EqualityComparer<'a> with
        static member Create(eq, ghc) = DelegateEqualityComparer(eq, ghc) :> EqualityComparer<'a>
        static member Create<'b when 'b: equality>(map: 'a -> 'b) = MapEqualityComparer map :> EqualityComparer<'a>

module Array =
    open Byref.Operators
    let inline isNullOrEmpty (arr: 'a[]) = Object.ReferenceEquals(arr, null) || arr.Length = 0
    let inline isNotNullOrEmpty (arr: 'a[]) = arr |> isNullOrEmpty |> not
    let inline defaultValue def arr = if isNullOrEmpty arr then def else arr
    let inline defaultWith defThunk arr = if isNullOrEmpty arr then defThunk() else arr
    let inline tryFindLast f arr =
        let mutable i = Array.length arr - 1
        while i >= 0 && not ^ f arr.[i] do dec &i
        if i >= 0 then Some arr.[i] else None

    let [<return: Struct>] inline (|NullOrEmpty|_|) (arr: 'a[]) =
        (Object.ReferenceEquals(arr, null) || arr.Length = 0) |> ValueOption.ofBool

    let [<return: Struct>] inline (|NotNullOrEmpty|_|) (arr: 'a[]) =
        (Object.ReferenceEquals(arr, null) || arr.Length = 0) |> not |> ValueOption.ofBool

module List =
    let inline replace original replacement list =
        list |> List.map (fun value -> if value = original then replacement else value)
    let inline replaceBy map replacement list =
        list |> List.map (fun value -> if map value = map replacement then replacement else value)
    let inline ofObj x = [ x ]
    let inline ofObj2 x1 x2 = [ x1; x2 ]
    let inline ofObj3 x1 x2 x3 = [ x1; x2; x3 ]

    let [<return: Struct>] inline (|NullOrEmpty|_|) (list: 'a list) =
        (Object.ReferenceEquals(list, null) || list.IsEmpty) |> ValueOption.ofBool

    let [<return: Struct>] inline (|NotNullOrEmpty|_|) (list: 'a list) =
        (Object.ReferenceEquals(list, null) || list.IsEmpty) |> not |> ValueOption.ofBool

// From https://github.com/fsharp/fsharp/blob/master/src/utils/ResizeArray.fs
module ResizeArray =
    let add value (arr: ResizeArray<'T>) = arr.Add value
    let remove value (arr: ResizeArray<'T>) = arr.Remove value
    let removeAll value (arr: ResizeArray<'T>) = arr.RemoveAll value
    let removeAt index (arr: ResizeArray<'T>) = arr.RemoveAt index
    let isNullOrEmpty (arr: ResizeArray<'T>) = arr |> isNull || arr.Count = 0
    let defaultValue def (arr: ResizeArray<'T>) = if arr |> isNullOrEmpty then def else arr
    let defaultWith defThunk (arr: ResizeArray<'T>) = if arr |> isNullOrEmpty then defThunk() else arr

    let length (arr: ResizeArray<'T>) =  arr.Count

    let get (arr: ResizeArray<'T>) (n: int) =  arr.[n]

    let set (arr: ResizeArray<'T>) (n: int) (x:'T) =  arr.[n] <- x

    let create (n: int) x = ResizeArray<_>(seq { for _ in 1 .. n -> x })

    let init (n: int) (f: int -> 'T) =  ResizeArray<_>(seq { for i in 0 .. n-1 -> f i })

    let blit (arr1: ResizeArray<'T>) start1 (arr2: ResizeArray<'T>) start2 len =
        if start1 < 0 then invalidArg "start1" "index must be positive"
        if start2 < 0 then invalidArg "start2" "index must be positive"
        if len < 0 then invalidArg "len" "length must be positive"
        if start1 + len > length arr1 then invalidArg "start1" "(start1+len) out of range"
        if start2 + len > length arr2 then invalidArg "start2" "(start2+len) out of range"
        for i = 0 to len - 1 do
            arr2.[start2 + i] <- arr1.[start1 + i]

    let concat (arrs: ResizeArray<'T> list) = ResizeArray<_>(seq { for arr in arrs do for x in arr do yield x })

    let append (arr1: ResizeArray<'T>) (arr2: ResizeArray<'T>) = concat [arr1; arr2]

    let sub (arr: ResizeArray<'T>) start len =
        if start < 0 then invalidArg "start" "index must be positive"
        if len < 0 then invalidArg "len" "length must be positive"
        if start + len > length arr then invalidArg "len" "length must be positive"
        ResizeArray<_>(seq { for i in start .. start+len-1 -> arr.[i] })

    let fill (arr: ResizeArray<'T>) (start: int) (len: int) (x:'T) =
        if start < 0 then invalidArg "start" "index must be positive"
        if len < 0 then invalidArg "len" "length must be positive"
        if start + len > length arr then invalidArg "len" "length must be positive"
        for i = start to start + len - 1 do
            arr.[i] <- x

    let copy (arr: ResizeArray<'T>) = ResizeArray<_>(arr)

    let toList (arr: ResizeArray<_>) =
        let mutable res = []
        for i = length arr - 1 downto 0 do
            res <- arr.[i] :: res
        res

    let ofList (l: _ list) =
        let len = l.Length
        let res = ResizeArray<_>(len)
        let rec add = function
          | [] -> ()
          | e::l -> res.Add(e); add l
        add l
        res

    let iter f (arr: ResizeArray<_>) =
        for i = 0 to arr.Count - 1 do
            f arr.[i]

    let map f (arr: ResizeArray<_>) =
        let len = length arr
        let res = ResizeArray<_>(len)
        for i = 0 to len - 1 do
            res.Add(f arr.[i])
        res

    let mapi f (arr: ResizeArray<_>) =
        let len = length arr
        let res = ResizeArray<_>(len)
        for i = 0 to len - 1 do
            res.Add(f i arr.[i])
        res

    let iteri f (arr: ResizeArray<_>) =
        for i = 0 to arr.Count - 1 do
            f i arr.[i]

    let exists (f: 'T -> bool) (arr: ResizeArray<'T>) =
        let len = length arr
        let rec loop i = i < len && (f arr.[i] || loop (i+1))
        loop 0

    let forall f (arr: ResizeArray<_>) =
        let len = length arr
        let rec loop i = i >= len || (f arr.[i] && loop (i+1))
        loop 0

    let indexNotFound() = raise (KeyNotFoundException("An index satisfying the predicate was not found in the collection"))

    let find f (arr: ResizeArray<_>) =
        let rec loop i =
            if i >= length arr then indexNotFound()
            elif f arr.[i] then arr.[i]
            else loop (i+1)
        loop 0

    let tryPick f (arr: ResizeArray<_>) =
        let rec loop i =
            if i >= length arr then None else
            match f arr.[i] with
            | None -> loop(i+1)
            | res -> res
        loop 0

    let tryFind f (arr: ResizeArray<_>) =
        let rec loop i =
            if i >= length arr then None
            elif f arr.[i] then Some arr.[i]
            else loop (i+1)
        loop 0

    let iter2 f (arr1: ResizeArray<'T>) (arr2: ResizeArray<'b>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        for i = 0 to len1 - 1 do
            f arr1.[i] arr2.[i]

    let map2 f (arr1: ResizeArray<'T>) (arr2: ResizeArray<'b>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        let res = ResizeArray<_>(len1)
        for i = 0 to len1 - 1 do
            res.Add(f arr1.[i] arr2.[i])
        res

    let choose f (arr: ResizeArray<_>) =
        let res = ResizeArray<_>()
        for i = 0 to length arr - 1 do
            match f arr.[i] with
            | None -> ()
            | Some b -> res.Add(b)
        res

    let filter f (arr: ResizeArray<_>) =
        let res = ResizeArray<_>()
        for i = 0 to length arr - 1 do
            let x = arr.[i]
            if f x then res.Add(x)
        res

    let partition f (arr: ResizeArray<_>) =
      let res1 = ResizeArray<_>()
      let res2 = ResizeArray<_>()
      for i = 0 to length arr - 1 do
          let x = arr.[i]
          if f x then res1.Add(x) else res2.Add(x)
      res1, res2

    let rev (arr: ResizeArray<_>) =
      let len = length arr
      let res = ResizeArray<_>(len)
      for i = len - 1 downto 0 do
          res.Add(arr.[i])
      res

    let foldBack (f : 'T -> 'State -> 'State) (arr: ResizeArray<'T>) (acc: 'State) =
        let mutable res = acc
        let len = length arr
        for i = len - 1 downto 0 do
            res <- f (get arr i) res
        res

    let fold (f : 'State -> 'T -> 'State) (acc: 'State) (arr: ResizeArray<'T>) =
        let mutable res = acc
        let len = length arr
        for i = 0 to len - 1 do
            res <- f res (get arr i)
        res

    let toArray (arr: ResizeArray<'T>) = arr.ToArray()

    let ofArray (arr: 'T[]) = ResizeArray<_>(arr)

    let toSeq (arr: ResizeArray<'T>) = Seq.readonly arr

    let sort f (arr: ResizeArray<'T>) = arr.Sort (System.Comparison(f))

    let sortBy f (arr: ResizeArray<'T>) = arr.Sort (System.Comparison(fun x y -> compare (f x) (f y)))

    let exists2 f (arr1: ResizeArray<_>) (arr2: ResizeArray<_>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        let rec loop i = i < len1 && (f arr1.[i] arr2.[i] || loop (i+1))
        loop 0

    let findIndex f (arr: ResizeArray<_>) =
        let rec go n = if n >= length arr then indexNotFound() elif f arr.[n] then n else go (n+1)
        go 0

    let findIndexi f (arr: ResizeArray<_>) =
        let rec go n = if n >= length arr then indexNotFound() elif f n arr.[n] then n else go (n+1)
        go 0

    let foldSub f acc (arr: ResizeArray<_>) start fin =
        let mutable res = acc
        for i = start to fin do
            res <- f res arr.[i]
        res

    let foldBackSub f (arr: ResizeArray<_>) start fin acc =
        let mutable res = acc
        for i = fin downto start do
            res <- f arr.[i] res
        res

    let reduce f (arr : ResizeArray<_>) =
        let arrn = length arr
        if arrn = 0 then invalidArg "arr" "the input array may not be empty"
        else foldSub f arr.[0] arr 1 (arrn - 1)

    let reduceBack f (arr: ResizeArray<_>) =
        let arrn = length arr
        if arrn = 0 then invalidArg "arr" "the input array may not be empty"
        else foldBackSub f arr 0 (arrn - 2) arr.[arrn - 1]

    let fold2 f (acc: 'T) (arr1: ResizeArray<'T1>) (arr2: ResizeArray<'T2>) =
        let mutable res = acc
        let len = length arr1
        if len <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        for i = 0 to len - 1 do
            res <- f res arr1.[i] arr2.[i]
        res

    let foldBack2 f (arr1: ResizeArray<'T1>) (arr2: ResizeArray<'T2>) (acc: 'b) =
        let mutable res = acc
        let len = length arr1
        if len <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        for i = len - 1 downto 0 do
            res <- f arr1.[i] arr2.[i] res
        res

    let forall2 f (arr1: ResizeArray<_>) (arr2: ResizeArray<_>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        let rec loop i = i >= len1 || (f arr1.[i] arr2.[i] && loop (i+1))
        loop 0

    let isEmpty (arr: ResizeArray<_>) = length (arr: ResizeArray<_>) = 0

    let iteri2 f (arr1: ResizeArray<'T>) (arr2: ResizeArray<'b>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        for i = 0 to len1 - 1 do
            f i arr1.[i] arr2.[i]

    let mapi2 (f: int -> 'a -> 'b -> 'c) (arr1: ResizeArray<'a>) (arr2: ResizeArray<'b>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        init len1 (fun i -> f i arr1.[i] arr2.[i])

    let scanBackSub f (arr: ResizeArray<'T>) start fin acc =
        let mutable state = acc
        let res = create (2 + fin - start) acc
        for i = fin downto start do
            state <- f arr.[i] state
            res.[i - start] <- state
        res

    let scanSub f  acc (arr : ResizeArray<'T>) start fin =
        let mutable state = acc
        let res = create (fin-start+2) acc
        for i = start to fin do
            state <- f state arr.[i]
            res.[i - start+1] <- state
        res

    let scan f acc (arr : ResizeArray<'T>) =
        let arrn = length arr
        scanSub f acc arr 0 (arrn - 1)

    let scanBack f (arr : ResizeArray<'T>) acc =
        let arrn = length arr
        scanBackSub f arr 0 (arrn - 1) acc

    let singleton x =
        let res = ResizeArray<_>(1)
        res.Add(x)
        res

    let tryFindIndex f (arr: ResizeArray<'T>) =
        let rec go n = if n >= length arr then None elif f arr.[n] then Some n else go (n+1)
        go 0

    let tryFindIndexi f (arr: ResizeArray<'T>) =
        let rec go n = if n >= length arr then None elif f n arr.[n] then Some n else go (n+1)
        go 0

    let zip (arr1: ResizeArray<_>) (arr2: ResizeArray<_>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        init len1 (fun i -> arr1.[i], arr2.[i])

    let unzip (arr: ResizeArray<_>) =
        let len = length arr
        let res1 = ResizeArray<_>(len)
        let res2 = ResizeArray<_>(len)
        for i = 0 to len - 1 do
            let x,y = arr.[i]
            res1.Add(x)
            res2.Add(y)
        res1,res2

#nowarn "9"
module Span =
    let inline isEmpty (span: Span<_>) = span.Length = 0
    let inline any (span: Span<_>) = span.Length <> 0
    let inline slice start count (span: Span<_>) = span.Slice(start, count)
    let inline sliceFrom start (span: Span<_>) = span.Slice(start)
    let inline sliceTo index (span: Span<_>) = span.Slice(0, index)
    let inline fromPtr count (ptr: nativeptr<'a>) = Span<'a>(ptr |> NativePtr.toVoidPtr, count)
    let inline fromVoidPtr<'a when 'a: unmanaged> count ptr = Span<'a>(ptr, count)
    let inline fromArray (array: 'a[]): Span<_> = Span.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): Span<_> = Span.op_Implicit array
    let inline fromMemory (memory: Memory<_>) = memory.Span

module ReadOnlySpan =
    let inline isEmpty (span: ReadOnlySpan<_>) = span.Length = 0
    let inline any (span: ReadOnlySpan<_>) = span.Length <> 0
    let inline slice start count (span: ReadOnlySpan<_>) = span.Slice(start, count)
    let inline sliceFrom start (span: ReadOnlySpan<_>) = span.Slice(start)
    let inline sliceTo index (span: ReadOnlySpan<_>) = span.Slice(0, index)
    let inline fromPtr count (ptr: nativeptr<'a>) = ReadOnlySpan<'a>(ptr |> NativePtr.toVoidPtr, count)
    let inline fromVoidPtr<'a when 'a: unmanaged> count ptr = ReadOnlySpan<'a>(ptr, count)
    let inline fromString (str: string): ReadOnlySpan<char> = String.op_Implicit str
    let inline fromSpan (span: Span<_>): ReadOnlySpan<_> = Span.op_Implicit span
    let inline fromArray (array: 'a[]): ReadOnlySpan<_> = ReadOnlySpan.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): ReadOnlySpan<_> = ReadOnlySpan.op_Implicit array
    let inline fromMemory (memory: ReadOnlyMemory<_>) = memory.Span

module Memory =
    let inline isEmpty (memory: Memory<_>) = memory.Length = 0
    let inline any (memory: Memory<_>) = memory.Length <> 0
    let inline slice start count (memory: Memory<_>) = memory.Slice(start, count)
    let inline sliceFrom start (memory: Memory<_>) = memory.Slice(start)
    let inline sliceTo index (memory: Memory<_>) = memory.Slice(0, index)
    let inline fromArray (array: 'a[]): Memory<_> = Memory.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): Memory<_> = Memory.op_Implicit array

module ReadOnlyMemory =
    let inline isEmpty (memory: ReadOnlyMemory<_>) = memory.Length = 0
    let inline any (memory: ReadOnlyMemory<_>) = memory.Length <> 0
    let inline slice start count (memory: ReadOnlyMemory<_>) = memory.Slice(start, count)
    let inline sliceFrom start (memory: ReadOnlyMemory<_>) = memory.Slice(start)
    let inline sliceTo index (memory: ReadOnlyMemory<_>) = memory.Slice(0, index)
    let inline fromMemory (span: Memory<_>): ReadOnlyMemory<_> = Memory.op_Implicit span
    let inline fromArray (array: 'a[]): ReadOnlyMemory<_> = ReadOnlyMemory.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): ReadOnlyMemory<_> = ReadOnlyMemory.op_Implicit array

module Seq =
    open EqualityComparer
    let inline intersect left right = Enumerable.Intersect(left, right)
    let inline intersectBy<'a, 'b when 'b: equality> (map: 'a -> 'b) left right = Enumerable.Intersect(left, right, EqualityComparer<'a>.Create map)
    let inline toResizeArray seq = Enumerable.ToList seq
    let inline ofType<'a> seq = Enumerable.OfType<'a> seq
    let inline isNotEmpty seq = Enumerable.Any seq
    let inline isNullOrEmpty seq = isNull seq || seq |> Enumerable.Any |> not
    let inline toLookup (keySelector: 'a -> 'b) (elementSelector: 'a -> 'c) seq = Enumerable.ToLookup(seq, keySelector, elementSelector)

    let inline ofGenericEnumerator enumerator =
        { new IEnumerable<'a> with
            member this.GetEnumerator(): IEnumerator<'a> = enumerator
            member this.GetEnumerator(): IEnumerator = enumerator :> IEnumerator }

    let inline ofEnumerator<'a> (enumerator: IEnumerator) =
        { new IEnumerator<'a> with
            member this.Current = enumerator.Current :?> 'a
            member this.Current: obj = enumerator.Current
            member this.MoveNext() = enumerator.MoveNext()
            member this.Reset() = enumerator.Reset()
            member this.Dispose() = () }
        |> ofGenericEnumerator

    let inline identical (seq2: 'a seq) (seq1: 'a seq) =
        use enum1 = seq1.GetEnumerator()
        use enum2 = seq2.GetEnumerator()

        let rec moveNext() =
            match enum1.MoveNext(), enum2.MoveNext() with
            | true, true ->
                enum1.Current = enum2.Current
                && moveNext()
            | false, false -> true
            | _ -> false

        moveNext()

    let identicalBy (seq2: 'a seq) comparer (seq1: 'a seq) =
        use enum1 = seq1.GetEnumerator()
        use enum2 = seq2.GetEnumerator()
        let optimized = OptimizedClosures.FSharpFunc<_,_,_>.Adapt comparer
        let rec moveNext() =
            match enum1.MoveNext(), enum2.MoveNext() with
            | true, true ->
                optimized.Invoke(enum1.Current, enum2.Current)
                && moveNext()
            | false, false -> true
            | _ -> false

        moveNext()

module Dictionary =
    let inline getValue key (d: #IDictionary<'key, 'value>) =
        d.[key]

    let inline tryGetValue key (d: #IDictionary<'key, 'value>) =
        d.TryGetValue key |> Option.ofTryPattern

    let inline tryAddValue key value (d: #IDictionary<'key, 'value>) =
        d.TryAdd(key, value)

    let inline addValue key value (d: #IDictionary<'key, 'value>) =
        d.Add(key, value)

    let inline setValue key value (d: #IDictionary<'key, 'value>) =
        d.[key] <- value

    let inline removeValue (key: 'key) (d: #IDictionary<'key, 'value>) =
        d.Remove key

module ConcurrentQueue =
    let inline tryDequeue (cq: 'a ConcurrentQueue) =
        cq.TryDequeue() |> Option.ofTryPattern

    let inline tryPeek (cq: 'a ConcurrentQueue) =
        cq.TryPeek() |> Option.ofTryPattern

module ConcurrentDictionary =
    let inline getValue key (cd: ConcurrentDictionary<'key, 'value>) =
        cd.[key]

    let inline setValue key value (cd: ConcurrentDictionary<'key, 'value>) =
        cd.[key] <- value

    let inline tryGetValue key (cd: ConcurrentDictionary<'key, 'value>) =
        cd.TryGetValue key |> Option.ofTryPattern

    let inline tryRemove (key: 'key) (cd: ConcurrentDictionary<'key, 'value>) =
        cd.TryRemove key |> Option.ofTryPattern

    let inline tryAdd key value (cd: ConcurrentDictionary<'key, 'value>) =
        cd.TryAdd(key, value)

module Func =
    let inline fromFun f = Func<'a, 'b> f
    let inline fromFun2 f = Func<'a, 'b, 'c> f
    let inline fromFun3 f = Func<'a, 'b, 'c, 'd> f

module Action =
    let inline fromFun f = Action<'a> f
    let inline fromFun2 f = Action<'a, 'b> f
    let inline fromFun3 f = Action<'a, 'b, 'c> f

module Tuple =
    let inline ofResVal f x = f x, x

/// Provides functions to get Union public fields which are not visible from F#
module Union =
    [<AbstractClass; Sealed>]
    type private TagGetter<'a>() =
       static member generateGetTag() =
            let parameterType = typeof<'a>
            let returnType = typeof<int>
            let tagPropertyInfo = parameterType.GetProperty("Tag", BindingFlags.Public ||| BindingFlags.Instance)
            // these check are delegate creation time only (static ctor)
            if parameterType |> FSharpType.IsUnion |> not
               // checks below are just to stay sure
               || isNull tagPropertyInfo
               || tagPropertyInfo.PropertyType <> returnType
               || isNull tagPropertyInfo.GetMethod then
                invalidOp <| sprintf "Invalid type specified: %s" parameterType.FullName
            else
                let dynamicMethod = DynamicMethod("GetTag", returnType, [| parameterType |])
                let generator = dynamicMethod.GetILGenerator()
                generator.Emit OpCodes.Ldarg_0
                generator.Emit(OpCodes.Call, tagPropertyInfo.GetMethod)
                generator.Emit OpCodes.Ret
                dynamicMethod.CreateDelegate typeof<Func<'a, int>> :?> Func<'a, int>

       [<DefaultValue>]
       static val mutable private _TagGetter: Func<'a, int>
       static do TagGetter<'a>._TagGetter <- TagGetter<'a>.generateGetTag()
       static member GetTag unionObj = TagGetter<'a>._TagGetter.Invoke unionObj

    [<AbstractClass; Sealed>]
    type private NameGetter<'a>() =

        [<DefaultValue>]
        static val mutable private _Names: string array
        static do NameGetter<'a>._Names <- [| for unionCase in typeof<'a> |> FSharpType.GetUnionCases -> unionCase.Name |]
        static member private GetNameInternal index = NameGetter<'a>._Names.[index]
        static member GetName<'a> (unionObj: 'a) = unionObj |> TagGetter.GetTag |> NameGetter<'a>.GetNameInternal

    /// Gets the value of "Tag" property of this union object
    let getTag unionObj = unionObj |> TagGetter.GetTag
    /// Gets the name of the union case of the underlying union object
    let getName unionObj = unionObj |> NameGetter<'a>.GetName

module Task =
    let inline map ([<InlineIfLambda>] mapper) (job: Task<'a>) =
        if job.IsCompleted then
            Task.FromResult(mapper job.Result)
        else task {
            let! result = job
            return mapper result
        }

    let inline bind ([<InlineIfLambda>] mapper) (job: Task<'a>) =
        if job.IsCompleted then
            mapper job.Result
        else task {
            let! result = job
            return! mapper result
        }

module ValueTask =
    let inline map ([<InlineIfLambda>] mapper) (job: ValueTask<'a>) =
        if job.IsCompleted then
            ValueTask.FromResult(mapper job.Result)
        else vtask {
            let! result = job
            return mapper result
        }

    let inline bind ([<InlineIfLambda>] mapper) (job: ValueTask<'a>) =
        if job.IsCompleted then
            mapper job.Result
        else vtask {
            let! result = job
            return! mapper result
        }