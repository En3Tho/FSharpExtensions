namespace En3Tho.FSharp.Extensions

open System
open System.Collections
open System.Collections.Concurrent
open System.Collections.Generic
open System.Linq
open System.Reflection
open System.Reflection.Emit
open System.Runtime.ExceptionServices
open System.Threading.Tasks
open Microsoft.FSharp.Reflection

module Object =
    module Operators =
        let inline ( *==) a b =
          (^a: (static member op_Equality: 'a * 'a -> bool) (a, b))

        let inline ( *!=) a b = not (a *== b)

    open Operators
    let inline defaultValue def arg = if arg &== null then def else arg
    let inline defaultWith ([<InlineIfLambda>] defThunk) arg = if arg &== null then defThunk() else arg
    let inline nullCheck argName arg = if arg &== null then nullArg argName |> ignore
    let inline ensureNotNull argName arg = if arg &== null then nullArg argName else arg
    let toStringOrEmpty str = if isNull str then "" else str.ToString()

module Functions =
    module Operators =
        let inline (&>>) ([<InlineIfLambda>] f) ([<InlineIfLambda>] g) = fun x -> f(); g x
        let inline (|>>) ([<InlineIfLambda>] f) ([<InlineIfLambda>] g) = fun x -> f x |> ignore; g x
    let inline ignoreAndReturnDefault _ = Unchecked.defaultof<'a>
    let inline ignoreAndReturnTrue _ = true
    let inline ignoreAndReturnFalse _ = false
    let inline ignore1AndReturnValue value = fun _ -> value
    let inline ignore2AndReturnValue value = fun _ _ -> value
    let inline ignore3AndReturnValue value = fun _ _ _ -> value
    let inline valueFun value = fun _ -> value
    let inline ignore2 _ _ = ()
    let inline ignore3 _ _ _ = ()
    let inline delay ([<InlineIfLambda>] f) x = fun _ -> f x
    let inline delay2 ([<InlineIfLambda>] f) x1 x2 = fun _ -> f x1 x2
    let inline delay3 ([<InlineIfLambda>] f) x1 x2 x3 = fun _ -> f x1 x2 x3
    let inline invokeWith x ([<InlineIfLambda>] f) = f x
    let inline revArgs2 ([<InlineIfLambda>] f: ^a -> ^b -> ^c) = fun (b: ^b) (a: ^a) -> f a b
    let inline revArgs3 ([<InlineIfLambda>] f: ^a -> ^b -> ^c -> ^d) = fun (c: ^c) (b: ^b) (a: ^a) -> f a b c

module Exception =
    let inline reraise ex = ExceptionDispatchInfo.Throw ex; Unchecked.defaultof<'a>

module Printf =
    type T = T with
        static member inline ($) (T, _: unit) = ()
        static member inline ($) (T, _: int) = 0 // mandatory second terminal case; is unused in runtime but is required for the code to compile
        static member inline ($) (T, _: ^a -> ^b): ^a -> ^b = fun (_: ^a) -> T $ Unchecked.defaultof< ^b>

    /// conditional ksprintf, ignores format on false and does not create any string
    let inline cksprintf runCondition ([<InlineIfLambda>] stringFunc) format: 'b =
        if runCondition then
           Printf.kprintf stringFunc format
        else
            T $ Unchecked.defaultof<'b>

module Option =
    let someObj = Some()
    let inline ofString str = if String.IsNullOrEmpty(str) then None else Some str
    let inline ofStringW str = if String.IsNullOrWhiteSpace(str) then None else Some str
    let inline ofBool bool = if bool then someObj else None
    let inline ofTryPattern (success, value) = if success then Some value else None
    let inline ofTryCast<'a when 'a: not struct> value =
        match box value with
        | :? 'a as value -> ValueSome value
        | _ -> ValueNone
    let inline ofCond ([<InlineIfLambda>] condition) (obj: ^a) = if condition obj then Some obj else None
    let inline ofAnyRef (obj: ^a) = if obj &== null then None else Some obj
    let inline toObjUnchecked opt = match opt with Some x -> x | None -> Unchecked.defaultof<_>
    let inline ofArray (array: 'a[]) = if array = null || array.Length = 0 then None else Some array
    let inline ofSeq (seq: 'a seq) = if seq = null || Seq.length seq = 0 then None else Some seq
    let inline ofValueOption (opt: ValueOption<'a>) = match opt with ValueSome x -> Some x | ValueNone -> None
    let inline toValueOption (opt: 'a option) = match opt with Some x -> ValueSome x | None -> ValueNone

    /// Similar to Option.iter but accepts an additional state value
    let inline iterv ([<InlineIfLambda>] onSome) value opt =
        match opt with
        | Some obj -> obj |> onSome value
        | None -> ()

module ValueOption =
    let inline ofString str = if String.IsNullOrEmpty(str) then ValueNone else ValueSome str
    let inline ofStringW str = if String.IsNullOrWhiteSpace(str) then ValueNone else ValueSome str
    let inline ofBool bool = if bool then ValueSome() else ValueNone
    let inline ofTryPattern (success, value) = if success then ValueSome value else ValueNone
    let inline ofTryCast<'a when 'a: not struct> value =
        match box value with
        | :? 'a as value -> ValueSome value
        | _ -> ValueNone
    let inline ofCond ([<InlineIfLambda>] condition) (obj: ^a) = if condition obj then ValueSome obj else ValueNone
    let inline ofAnyRef (obj: ^a) = if obj &== null then ValueNone else ValueSome obj
    let inline toObjUnchecked opt = match opt with ValueSome x -> x | ValueNone -> Unchecked.defaultof<_>
    let inline ofArray (array: 'a[]) = if array = null || array.Length = 0 then ValueNone else ValueSome array
    let inline ofSeq (seq: 'a seq) = if seq = null || Seq.isEmpty seq then ValueNone else ValueSome seq
    let inline ofOption (opt: 'a option) = match opt with Some x -> ValueSome x | None -> ValueNone
    let inline toOption (opt: ValueOption<'a>) = match opt with ValueSome x -> Some x | ValueNone -> None

    /// Similar to Option.iter but accepts an additional state value
    let inline iterv ([<InlineIfLambda>] onValueSome) value opt =
        match opt with
        | ValueSome obj -> obj |> onValueSome value
        | ValueNone -> ()

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
    let inline isValid (value: EnumShape<'a>) =
        Enum.GetValues<'a>().AsSpan().Contains value

    let inline (|HasFlag|_|) flag value = hasFlag flag value
    let inline (|NotHasFlag|_|) flag value = hasFlag flag value |> not
    let inline (|Valid|_|) value = isValid value
    let inline (|NotValid|_|) value = isValid value |> not

module Result =
    /// wraps a function with unit argument into try catch block returning a result
    let inline wrap ([<InlineIfLambda>] f) =
        try f() |> Ok
        with e -> e |> Error

    /// wraps a function with 1 additional argument into try catch block returning a result
    let inline wrapv ([<InlineIfLambda>] f) value =
        try f value |> Ok
        with e -> e |> Error

    /// Similar to Option.defaultWith but receives an mapError-like func
    let inline defaultWith ([<InlineIfLambda>] f) result =
        match result with
        | Ok v -> v
        | Error e -> f e

    /// Similar to Option.defaultValue but error is ignored
    let inline defaultValue value result =
        match result with
        | Ok v -> v
        | Error _ -> value

    let inline iter ([<InlineIfLambda>] f) value =
        match value with
        | Ok v -> f v
        | Error _ -> ()

    let inline iterError ([<InlineIfLambda>] f) value =
        match value with
        | Ok _ -> ()
        | Error e -> f e

type EResult<'a, 'b when 'b :> exn> = Result<'a, 'b>
type AsyncEResult<'a, 'b when 'b :> exn> = Result<'a, 'b> ValueTask

type ExnResult<'a> = EResult<'a, exn>
type AsyncExnResult<'a> = EResult<'a, exn> ValueTask

module EResult =

    let inline defaultValue defaultValue (result: EResult<'a, 'b>) =
        match result with
        | Ok value ->
            value
        | _ ->
            defaultValue

    let inline defaultWith ([<InlineIfLambda>] defThunk) (result: EResult<'a, 'b>) =
        match result with
        | Ok value ->
            value
        | Error exn ->
            defThunk exn

    let inline get (result: EResult<'a, 'b>) =
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

    let inline trySetCurrentStackTrace (result: EResult<'a, 'b>) =
        match result with
        | Error exn when String.IsNullOrEmpty(exn.StackTrace) ->
            exn |> ExceptionDispatchInfo.SetCurrentStackTrace :?> 'b |> Error
        | _ ->
            result

module Byref =
    /// all setters are reversed (byref is first parameter) because usually you want to write something into byref
    /// and pass (setv &ref or similar) into a function which then will write the value
    let inline setv (a: 'a byref) v = a <- v
    let inline setfn (a: 'a byref) f v = a <- f v
    let inline seti (a: 'a byref) v _ = a <- v
    let inline setTrue (a: bool byref) _ = a <- true
    let inline setFalse (a: bool byref) _ = a <- false
    let inline setZero (a: 'a byref) _ = a <- LanguagePrimitives.GenericZero // TODO: .Net 7
    let inline setOne (a: 'a byref) _ = a <- LanguagePrimitives.GenericOne // TODO: .Net 7
    let inline setMinusOne (a: 'a byref) _ = a <- -LanguagePrimitives.GenericOne // TODO: .Net 7
    let inline setParse (a: 'a byref) v = a <- (^a: (static member Parse: string -> 'a) v)
    let inline defaultValue (a: 'a byref) v = if isNull a then a <- v
    let inline defaultWith (a: 'a byref) ([<InlineIfLambda>] defThunk) = if isNull a then a <- defThunk()

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
        static member Create<'b when 'b: equality>(map: 'a -> 'b) = MapEqualityComparer(map) :> EqualityComparer<'a>

module Array =
    let inline isNullOrEmpty (arr: 'a[]) =
        Object.ReferenceEquals(arr, null) || arr.Length = 0

    let inline isNotNullOrEmpty (arr: 'a[]) =
        arr |> isNullOrEmpty |> not

    let inline defaultValue def arr =
        if isNullOrEmpty arr then def else arr

    let inline defaultWith ([<InlineIfLambda>] defThunk) arr =
        if isNullOrEmpty arr then defThunk() else arr

    let inline tryFindLast ([<InlineIfLambda>] f) arr =
        let mutable i = Array.length arr - 1
        while i >= 0 && not ^ f arr[i] do i <- i - 1
        if i >= 0 then Some arr[i] else None

    let inline ofObj x = [| x |]
    let inline ofObj2 x1 x2 = [| x1; x2 |]
    let inline ofObj3 x1 x2 x3 = [| x1; x2; x3 |]

    let inline (|NullOrEmpty|_|) (arr: 'a[]) =
        (Object.ReferenceEquals(arr, null) || arr.Length = 0)

    let inline (|NotNullOrEmpty|_|) (arr: 'a[]) =
        (Object.ReferenceEquals(arr, null) || arr.Length = 0) |> not

module List =
    let inline replace original replacement list =
        list |> List.map (fun value -> if value = original then replacement else value)
    let inline replaceBy ([<InlineIfLambda>] map) replacement list =
        list |> List.map (fun value -> if map value = map replacement then replacement else value)

    let inline ofObj x = [ x ]
    let inline ofObj2 x1 x2 = [ x1; x2 ]
    let inline ofObj3 x1 x2 x3 = [ x1; x2; x3 ]

    let inline (|NullOrEmpty|_|) (list: 'a list) =
        (Object.ReferenceEquals(list, null) || list.IsEmpty)

    let inline (|NotNullOrEmpty|_|) (list: 'a list) =
        (Object.ReferenceEquals(list, null) || list.IsEmpty) |> not

// TODO: active patterns
module Memory =
    let inline isEmpty (memory: Memory<_>) = memory.Length = 0
    let inline any (memory: Memory<_>) = memory.Length <> 0
    let inline slice start count (memory: Memory<_>) = memory.Slice(start, count)
    let inline sliceFrom start (memory: Memory<_>) = memory.Slice(start)
    let inline sliceTo index (memory: Memory<_>) = memory.Slice(0, index)
    let inline fromArray (array: 'a[]): Memory<_> = Memory.op_Implicit(array)
    let inline fromArraySegment (array: 'a ArraySegment): Memory<_> = Memory.op_Implicit(array)

// TODO: active patterns
module ReadOnlyMemory =
    let inline isEmpty (memory: ReadOnlyMemory<_>) = memory.Length = 0
    let inline any (memory: ReadOnlyMemory<_>) = memory.Length <> 0
    let inline slice start count (memory: ReadOnlyMemory<_>) = memory.Slice(start, count)
    let inline sliceFrom start (memory: ReadOnlyMemory<_>) = memory.Slice(start)
    let inline sliceTo index (memory: ReadOnlyMemory<_>) = memory.Slice(0, index)
    let inline fromMemory (span: Memory<_>): ReadOnlyMemory<_> = Memory.op_Implicit(span)
    let inline fromArray (array: 'a[]): ReadOnlyMemory<_> = ReadOnlyMemory.op_Implicit(array)
    let inline fromArraySegment (array: 'a ArraySegment): ReadOnlyMemory<_> = ReadOnlyMemory.op_Implicit(array)

module Seq =
    open EqualityComparer
    let inline intersect left right = Enumerable.Intersect(left, right)
    let inline intersectBy<'a, 'b when 'b: equality> ([<InlineIfLambda>] map: 'a -> 'b) left right =
        Enumerable.Intersect(left, right, EqualityComparer.Create(map))
    let inline toResizeArray seq = Enumerable.ToList seq
    let inline ofType<'a> seq = Enumerable.OfType<'a> seq
    let inline isNotEmpty seq = Enumerable.Any seq
    let inline isNullOrEmpty seq = isNull seq || seq |> Enumerable.Any |> not
    let inline toLookup ([<InlineIfLambda>] keySelector: 'a -> 'b) ([<InlineIfLambda>] elementSelector: 'a -> 'c) seq =
        Enumerable.ToLookup(seq, keySelector, elementSelector)

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
        seq1 === seq2

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
        d[key]

    let inline tryGetValue key (d: #IDictionary<'key, 'value>) =
        d.TryGetValue key |> ValueOption.ofTryPattern

    let inline tryAddValue key value (d: #IDictionary<'key, 'value>) =
        d.TryAdd(key, value)

    let inline addValue key value (d: #IDictionary<'key, 'value>) =
        d.Add(key, value)

    let inline setValue key value (d: #IDictionary<'key, 'value>) =
        d[key] <- value

    let inline removeValue (key: 'key) (d: #IDictionary<'key, 'value>) =
        d.Remove key

module ConcurrentQueue =
    let inline tryDequeue (cq: 'a ConcurrentQueue) =
        cq.TryDequeue() |> ValueOption.ofTryPattern

    let inline tryPeek (cq: 'a ConcurrentQueue) =
        cq.TryPeek() |> ValueOption.ofTryPattern

module ConcurrentDictionary =
    let inline getValue key (cd: ConcurrentDictionary<'key, 'value>) =
        cd[key]

    let inline setValue key value (cd: ConcurrentDictionary<'key, 'value>) =
        cd[key] <- value

    let inline tryGetValue key (cd: ConcurrentDictionary<'key, 'value>) =
        cd.TryGetValue key |> ValueOption.ofTryPattern

    let inline tryRemove (key: 'key) (cd: ConcurrentDictionary<'key, 'value>) =
        cd.TryRemove key |> ValueOption.ofTryPattern

    let inline tryAdd key value (cd: ConcurrentDictionary<'key, 'value>) =
        cd.TryAdd(key, value)

module Tuple =
    let inline ofResVal ([<InlineIfLambda>] f) x = f x, x

module ValueTuple =
    let inline ofResVal ([<InlineIfLambda>] f) x = struct (f x, x)

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
        static member GetTag(unionObj) = TagGetter<'a>._TagGetter.Invoke unionObj

    [<AbstractClass; Sealed>]
    type private NameGetter<'a>() =

        [<DefaultValue>]
        static val mutable private _Names: string array
        static do NameGetter<'a>._Names <- [| for unionCase in typeof<'a> |> FSharpType.GetUnionCases -> unionCase.Name |]
        static member private GetNameInternal index = NameGetter<'a>._Names[index]
        static member GetName<'a>(unionObj: 'a) = unionObj |> TagGetter.GetTag |> NameGetter<'a>.GetNameInternal

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