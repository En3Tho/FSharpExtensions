namespace En3Tho.FSharp.Extensions

open System
open System.Collections.Generic
open System.Collections.Immutable
open System.Runtime.InteropServices
open En3Tho.FSharp.Extensions.Disposables

type block<'a> = ImmutableArray<'a>

[<AutoOpen; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
module Core =

    [<AbstractClass; AutoOpen>]
    type Defer =
        static member inline defer ([<InlineIfLambda>] disposer) = UnitAsyncDisposable(disposer)
        static member inline defer ([<InlineIfLambda>] disposer) = new UnitDisposable(disposer)
        static member inline deferv ([<InlineIfLambda>] disposer) = fun value -> ValueAsyncDisposable<_>(value, disposer)
        static member inline deferv ([<InlineIfLambda>] disposer) = fun value -> new ValueDisposable<_>(value, disposer)

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
    let inline isNotNull< ^a when ^a : not struct> (obj: ^a) = not (Object.ReferenceEquals(obj, null))
    let inline nullRef< ^a when ^a: not struct> = Unchecked.defaultof< ^a>
    let inline nullVal< ^a when ^a: struct> = Unchecked.defaultof< ^a>

    let inline toString (o: ^a) = o.ToString()
    let inline getHashCode (o: ^a) = o.GetHashCode()

    [<Obsolete("This logic needs to be implemented")>]
    let inline TODO<'a> = raise<'a> (NotImplementedException())

    let inline (&==) (a: 'a when 'a: not struct) (b: 'a) = Object.ReferenceEquals(a, b)
    let inline (&!=) (a: 'a when 'a: not struct) (b: 'a) = not (Object.ReferenceEquals(a, b))

    let inline (--) key value = KeyValuePair(key, value)
    let inline (~%) value = (^a: (member Value: ^b) value)

    [<AbstractClass; AutoOpen>]
    type ActionFuncConverter =
        static member inline func<'a, 'b> ([<InlineIfLambda>] f: 'a -> 'b) = Func<'a, 'b>(f)
        static member inline func<'a, 'b, 'c> ([<InlineIfLambda>] f: 'a -> 'b -> 'c) = Func<'a, 'b, 'c>(f)
        static member inline func<'a, 'b, 'c, 'd> ([<InlineIfLambda>] f: 'a -> 'b -> 'c -> 'd) = Func<'a, 'b, 'c, 'd>(f)

        static member inline action<'a, 'b> ([<InlineIfLambda>] f: 'a -> unit) = Action<'a>(f)
        static member inline action<'a, 'b> ([<InlineIfLambda>] f: 'a -> 'b -> unit) = Action<'a, 'b>(f)
        static member inline action<'a, 'b, 'c, 'd> ([<InlineIfLambda>] f: 'a -> 'b -> 'c -> unit) = Action<'a, 'b, 'c>(f)

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
            let rec go index =
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
    let inline callIEquatableEqualsOnLinkedLists<'a when 'a :> IEquatable<'a>> (left: 'a LinkedList) (right: 'a LinkedList) =
        left.Count = right.Count
        && (
            let rec go (left: 'a LinkedListNode) (right: 'a LinkedListNode) =
                isNull left && isNull right
                || (isNotNull left
                    && isNotNull right
                    && callIEquatableEqualsOnValues left.Value right.Value
                    && go left.Next right.Next)

            go left.First right.First
        )

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    let inline callIEquatableEqualsOnLists<'a when 'a :> IEquatable<'a>> (left: 'a list) (right: 'a list) =
        let rec go left right =
            match left, right with
            | leftHead :: leftTail, rightHead :: rightTail ->
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
        && (let mutable leftEnumerator = left.GetEnumerator()
            let rec go rightCount =
                if leftEnumerator.MoveNext() then
                    if rightCount = 0 then
                        false
                    else
                        right.Contains(leftEnumerator.Current)
                        && go (rightCount - 1)
                else
                    rightCount = 0
            go right.Count)

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    let inline callIEquatableEqualsOnDictionaries<'a, 'b when 'a :> IEquatable<'a> and 'b :> IEquatable<'b>> (left: Dictionary<'a, 'b>) (right: Dictionary<'a, 'b>) =
        left.Count = right.Count
        && Object.ReferenceEquals(left.Comparer, right.Comparer)
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
    type ValueEquality = ValueEquality with
        static member inline ($) (ValueEquality, value) = fun otherValue -> callIEquatableEqualsOnValues value otherValue

    let inline (==) a b = (ValueEquality $ a) b

    let inline (!=) a b = not (a == b)

    [<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
    type CollectionEquality = CollectionEquality with
        static member inline ($) (CollectionEquality, value) = fun otherValue -> callIEquatableEqualsOnArrays value otherValue
        static member inline ($) (CollectionEquality, value) = fun otherValue -> callIEquatableEqualsOnResizeArrays value otherValue
        static member inline ($) (CollectionEquality, value) = fun otherValue -> callIEquatableEqualsOnLists value otherValue
        static member inline ($) (CollectionEquality, value) = fun otherValue -> callIEquatableEqualsOnLinkedLists value otherValue
        static member inline ($) (CollectionEquality, value) = fun otherValue -> callIEquatableEqualsOnSeq value otherValue
        static member inline ($) (CollectionEquality, value) = fun otherValue -> callIEquatableEqualsOnHashSets value otherValue
        static member inline ($) (CollectionEquality, value) = fun otherValue -> callIEquatableEqualsOnDictionaries value otherValue

    let inline (===) a b = (CollectionEquality $ a) b

    let inline (!==) a b = not (a == b)

[<AutoOpen; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
module ByRefOperators =

    let inline inc (a: 'a byref) = a <- a + LanguagePrimitives.GenericOne
    let inline dec (a: 'a byref) = a <- a - LanguagePrimitives.GenericOne
    let inline neg (a: 'a byref) = a <- (~-)a
    /// like a default value ( or ?? value in C#)
    let inline (??<-) (a: 'a byref) v = if isNull a then a <- v
    /// like a default with (or ?? expr in C#)
    let inline (???<-) (a: 'a byref) ([<InlineIfLambda>] f) = if isNull a then a <- f()
    /// like +=
    let inline (+<-) (a: 'a byref) v = a <- a + v
    /// like -=
    let inline (-<-) (a: 'a byref) v = a <- a - v
    /// like /=
    let inline (/<-) (a: 'a byref) v = a <- a / v
    /// like %=
    let inline (%<-) (a: 'a byref) v = a <- a % v
    /// like *=
    let inline ( *<- ) (a: 'a byref) v = a <- a * v
    let inline (~~~) (a: 'a byref) = a <- ~~~a
    /// like &&=
    let inline (&&&<-) (a: 'a byref) v = a <- a &&& v
    /// like ||=
    let inline (|||<-) (a: 'a byref) v = a <- a ||| v
    let inline (^^^<-) (a: 'a byref) v = a <- a ^^^ v
    /// like <<=
    let inline (<<<<-) (a: 'a byref) v = a <- a <<< v
    /// like >>=
    let inline (>>><-) (a: 'a byref) v = a <- a >>> v
    /// like &&=
    let inline (&&<-) (a: bool byref) v = a <- a && v
    /// like ||=
    let inline (||<-) (a: bool byref) v = a <- a || v
    /// like *= self
    let inline ( **<- ) (a: 'a byref) v = a <- a ** v
    let inline (@<-) (a: 'a list byref) v = a <- a @ v
    let inline (|><-) (a: 'a byref) v = a <- a |> v