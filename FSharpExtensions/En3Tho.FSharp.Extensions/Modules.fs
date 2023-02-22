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
open FSharp.NativeInterop
open Microsoft.FSharp.Reflection

#nowarn "0077"
#nowarn "0042"

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
    let inline ofString str = if String.IsNullOrEmpty str then None else Some str
    let inline ofStringW str = if String.IsNullOrWhiteSpace str then None else Some str
    let inline ofBool bool = if bool then someObj else None
    let inline ofTryPattern (success, value) = if success then Some value else None
    let inline ofTryCast<'a> value =
        match box value with
        | :? 'a as value -> ValueSome value
        | _ -> ValueNone
    let inline ofCond ([<InlineIfLambda>] condition) (obj: ^a) = if condition obj then Some obj else None
    let inline ofAnyRef (obj: ^a) = if obj &== null then None else Some obj
    let inline toObjUnchecked opt = match opt with Some x -> x | None -> Unchecked.defaultof<_>
    let inline ofArray (array: 'a[]) = if array = null || array.Length = 0 then None else Some array
    let inline ofSeq (seq: 'a seq) = if seq = null || Seq.length seq = 0 then None else Some seq

    /// Similar to Option.iter but accepts an additional state value
    let inline iterv ([<InlineIfLambda>] onSome) value opt =
        match opt with
        | Some obj -> obj |> onSome value
        | None -> ()

module ValueOption =
    let inline ofString str = if String.IsNullOrEmpty str then ValueNone else ValueSome str
    let inline ofStringW str = if String.IsNullOrWhiteSpace str then ValueNone else ValueSome str
    let inline ofBool bool = if bool then ValueSome() else ValueNone
    let inline ofTryPattern (success, value) = if success then ValueSome value else ValueNone
    let inline ofTryCast<'a> value =
        match box value with
        | :? 'a as value -> ValueSome value
        | _ -> ValueNone
    let inline ofCond ([<InlineIfLambda>] condition) (obj: ^a) = if condition obj then ValueSome obj else ValueNone
    let inline ofAnyRef (obj: ^a) = if obj &== null then ValueNone else ValueSome obj
    let inline toObjUnchecked opt = match opt with ValueSome x -> x | ValueNone -> Unchecked.defaultof<_>
    let inline ofArray (array: 'a[]) = if array = null || array.Length = 0 then ValueNone else ValueSome array
    let inline ofSeq (seq: 'a seq) = if seq = null || Seq.isEmpty seq then ValueNone else ValueSome seq

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

    let [<return: Struct>] inline (|HasFlag|_|) flag value = hasFlag flag value |> ValueOption.ofBool
    let [<return: Struct>] inline (|HasFlagNot|_|) flag value = hasFlag flag value |> not |> ValueOption.ofBool
    let [<return: Struct>] inline (|Valid|_|) value = isValid value |> ValueOption.ofBool
    let [<return: Struct>] inline (|ValidNot|_|) value = isValid value |> not |> ValueOption.ofBool

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
        | Error exn when String.IsNullOrEmpty exn.StackTrace ->
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
    let inline setZero (a: 'a byref) _ = a <- LanguagePrimitives.GenericZero
    let inline setOne (a: 'a byref) _ = a <- LanguagePrimitives.GenericOne
    let inline setMinusOne (a: 'a byref) _ = a <- -LanguagePrimitives.GenericOne
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
        static member Create<'b when 'b: equality>(map: 'a -> 'b) = MapEqualityComparer map :> EqualityComparer<'a>

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

    let [<return: Struct>] inline (|NullOrEmpty|_|) (arr: 'a[]) =
        (Object.ReferenceEquals(arr, null) || arr.Length = 0) |> ValueOption.ofBool

    let [<return: Struct>] inline (|NotNullOrEmpty|_|) (arr: 'a[]) =
        (Object.ReferenceEquals(arr, null) || arr.Length = 0) |> not |> ValueOption.ofBool

module List =
    let inline replace original replacement list =
        list |> List.map (fun value -> if value = original then replacement else value)
    let inline replaceBy ([<InlineIfLambda>] map) replacement list =
        list |> List.map (fun value -> if map value = map replacement then replacement else value)
    let inline ofObj x = [ x ]
    let inline ofObj2 x1 x2 = [ x1; x2 ]
    let inline ofObj3 x1 x2 x3 = [ x1; x2; x3 ]

    let [<return: Struct>] inline (|NullOrEmpty|_|) (list: 'a list) =
        (Object.ReferenceEquals(list, null) || list.IsEmpty) |> ValueOption.ofBool

    let [<return: Struct>] inline (|NotNullOrEmpty|_|) (list: 'a list) =
        (Object.ReferenceEquals(list, null) || list.IsEmpty) |> not |> ValueOption.ofBool

// From https://github.com/fsharp/fsharp/blob/master/src/utils/ResizeArray.fs
// TODO: rewrite these with cool stuff from CollectionsMarshal
module ResizeArray =
    let add value (arr: ResizeArray<'T>) = arr.Add value
    let remove value (arr: ResizeArray<'T>) = arr.Remove value
    let removeAll value (arr: ResizeArray<'T>) = arr.RemoveAll value
    let removeAt index (arr: ResizeArray<'T>) = arr.RemoveAt index
    let isNullOrEmpty (arr: ResizeArray<'T>) = arr |> isNull || arr.Count = 0
    let defaultValue def (arr: ResizeArray<'T>) = if arr |> isNullOrEmpty then def else arr
    let defaultWith defThunk (arr: ResizeArray<'T>) = if arr |> isNullOrEmpty then defThunk() else arr

    let length (arr: ResizeArray<'T>) = arr.Count

    let get (arr: ResizeArray<'T>) (n: int) = arr[n]

    let set (arr: ResizeArray<'T>) (n: int) (x:'T) = arr[n] <- x

    let create (n: int) x = ResizeArray<_>(seq { for _ in 1 .. n -> x })

    let init (n: int) (f: int -> 'T) =  ResizeArray<_>(seq { for i in 0 .. n-1 -> f i })

    let blit (arr1: ResizeArray<'T>) start1 (arr2: ResizeArray<'T>) start2 len =
        if start1 < 0 then invalidArg "start1" "index must be positive"
        if start2 < 0 then invalidArg "start2" "index must be positive"
        if len < 0 then invalidArg "len" "length must be positive"
        if start1 + len > length arr1 then invalidArg "start1" "(start1+len) out of range"
        if start2 + len > length arr2 then invalidArg "start2" "(start2+len) out of range"
        for i = 0 to len - 1 do
            arr2[start2 + i] <- arr1[start1 + i]

    let concat (arrs: ResizeArray<'T> list) = ResizeArray<_>(seq { for arr in arrs do for x in arr do yield x })

    let append (arr1: ResizeArray<'T>) (arr2: ResizeArray<'T>) = concat [arr1; arr2]

    let sub (arr: ResizeArray<'T>) start len =
        if start < 0 then invalidArg "start" "index must be positive"
        if len < 0 then invalidArg "len" "length must be positive"
        if start + len > length arr then invalidArg "len" "length must be positive"
        ResizeArray<_>(seq { for i in start .. start+len-1 -> arr[i] })

    let fill (arr: ResizeArray<'T>) (start: int) (len: int) (x:'T) =
        if start < 0 then invalidArg "start" "index must be positive"
        if len < 0 then invalidArg "len" "length must be positive"
        if start + len > length arr then invalidArg "len" "length must be positive"
        for i = start to start + len - 1 do
            arr[i] <- x

    let copy (arr: ResizeArray<'T>) = ResizeArray<_>(arr)

    let toList (arr: ResizeArray<_>) =
        let mutable res = []
        for i = length arr - 1 downto 0 do
            res <- arr[i] :: res
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
            f arr[i]

    let map f (arr: ResizeArray<_>) =
        let len = length arr
        let res = ResizeArray<_>(len)
        for i = 0 to len - 1 do
            res.Add(f arr[i])
        res

    let mapi f (arr: ResizeArray<_>) =
        let len = length arr
        let res = ResizeArray<_>(len)
        for i = 0 to len - 1 do
            res.Add(f i arr[i])
        res

    let iteri f (arr: ResizeArray<_>) =
        for i = 0 to arr.Count - 1 do
            f i arr[i]

    let exists (f: 'T -> bool) (arr: ResizeArray<'T>) =
        let len = length arr
        let rec loop i = i < len && (f arr[i] || loop (i+1))
        loop 0

    let forall f (arr: ResizeArray<_>) =
        let len = length arr
        let rec loop i = i >= len || (f arr[i] && loop (i+1))
        loop 0

    let indexNotFound() = raise (KeyNotFoundException("An index satisfying the predicate was not found in the collection"))

    let find f (arr: ResizeArray<_>) =
        let rec loop i =
            if i >= length arr then indexNotFound()
            elif f arr[i] then arr[i]
            else loop (i+1)
        loop 0

    let tryPick f (arr: ResizeArray<_>) =
        let rec loop i =
            if i >= length arr then None else
            match f arr[i] with
            | None -> loop(i+1)
            | res -> res
        loop 0

    let tryFind f (arr: ResizeArray<_>) =
        let rec loop i =
            if i >= length arr then None
            elif f arr[i] then Some arr[i]
            else loop (i+1)
        loop 0

    let iter2 f (arr1: ResizeArray<'T>) (arr2: ResizeArray<'b>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        for i = 0 to len1 - 1 do
            f arr1[i] arr2[i]

    let map2 f (arr1: ResizeArray<'T>) (arr2: ResizeArray<'b>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        let res = ResizeArray<_>(len1)
        for i = 0 to len1 - 1 do
            res.Add(f arr1[i] arr2[i])
        res

    let choose f (arr: ResizeArray<_>) =
        let res = ResizeArray<_>()
        for i = 0 to length arr - 1 do
            match f arr[i] with
            | None -> ()
            | Some b -> res.Add(b)
        res

    let filter f (arr: ResizeArray<_>) =
        let res = ResizeArray<_>()
        for i = 0 to length arr - 1 do
            let x = arr[i]
            if f x then res.Add(x)
        res

    let partition f (arr: ResizeArray<_>) =
      let res1 = ResizeArray<_>()
      let res2 = ResizeArray<_>()
      for i = 0 to length arr - 1 do
          let x = arr[i]
          if f x then res1.Add(x) else res2.Add(x)
      res1, res2

    let rev (arr: ResizeArray<_>) =
      let len = length arr
      let res = ResizeArray<_>(len)
      for i = len - 1 downto 0 do
          res.Add(arr[i])
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

    let sort f (arr: ResizeArray<'T>) = arr.Sort (Comparison(f))

    let sortBy f (arr: ResizeArray<'T>) = arr.Sort (Comparison(fun x y -> compare (f x) (f y)))

    let exists2 f (arr1: ResizeArray<_>) (arr2: ResizeArray<_>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        let rec loop i = i < len1 && (f arr1[i] arr2[i] || loop (i+1))
        loop 0

    let findIndex f (arr: ResizeArray<_>) =
        let rec go n = if n >= length arr then indexNotFound() elif f arr[n] then n else go (n+1)
        go 0

    let findIndexi f (arr: ResizeArray<_>) =
        let rec go n = if n >= length arr then indexNotFound() elif f n arr[n] then n else go (n+1)
        go 0

    let foldSub f acc (arr: ResizeArray<_>) start fin =
        let mutable res = acc
        for i = start to fin do
            res <- f res arr[i]
        res

    let foldBackSub f (arr: ResizeArray<_>) start fin acc =
        let mutable res = acc
        for i = fin downto start do
            res <- f arr[i] res
        res

    let reduce f (arr : ResizeArray<_>) =
        let arrn = length arr
        if arrn = 0 then invalidArg "arr" "the input array may not be empty"
        else foldSub f arr[0] arr 1 (arrn - 1)

    let reduceBack f (arr: ResizeArray<_>) =
        let arrn = length arr
        if arrn = 0 then invalidArg "arr" "the input array may not be empty"
        else foldBackSub f arr 0 (arrn - 2) arr[arrn - 1]

    let fold2 f (acc: 'T) (arr1: ResizeArray<'T1>) (arr2: ResizeArray<'T2>) =
        let mutable res = acc
        let len = length arr1
        if len <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        for i = 0 to len - 1 do
            res <- f res arr1[i] arr2[i]
        res

    let foldBack2 f (arr1: ResizeArray<'T1>) (arr2: ResizeArray<'T2>) (acc: 'b) =
        let mutable res = acc
        let len = length arr1
        if len <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        for i = len - 1 downto 0 do
            res <- f arr1[i] arr2[i] res
        res

    let forall2 f (arr1: ResizeArray<_>) (arr2: ResizeArray<_>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        let rec loop i = i >= len1 || (f arr1[i] arr2[i] && loop (i+1))
        loop 0

    let isEmpty (arr: ResizeArray<_>) = length (arr: ResizeArray<_>) = 0

    let iteri2 f (arr1: ResizeArray<'T>) (arr2: ResizeArray<'b>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        for i = 0 to len1 - 1 do
            f i arr1[i] arr2[i]

    let mapi2 (f: int -> 'a -> 'b -> 'c) (arr1: ResizeArray<'a>) (arr2: ResizeArray<'b>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        init len1 (fun i -> f i arr1[i] arr2[i])

    let scanBackSub f (arr: ResizeArray<'T>) start fin acc =
        let mutable state = acc
        let res = create (2 + fin - start) acc
        for i = fin downto start do
            state <- f arr[i] state
            res[i - start] <- state
        res

    let scanSub f  acc (arr : ResizeArray<'T>) start fin =
        let mutable state = acc
        let res = create (fin-start+2) acc
        for i = start to fin do
            state <- f state arr[i]
            res[i - start+1] <- state
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
        let rec go n = if n >= length arr then None elif f arr[n] then Some n else go (n+1)
        go 0

    let tryFindIndexi f (arr: ResizeArray<'T>) =
        let rec go n = if n >= length arr then None elif f n arr[n] then Some n else go (n+1)
        go 0

    let zip (arr1: ResizeArray<_>) (arr2: ResizeArray<_>) =
        let len1 = length arr1
        if len1 <> length arr2 then invalidArg "arr2" "the arrays have different lengths"
        init len1 (fun i -> arr1[i], arr2[i])

    let unzip (arr: ResizeArray<_>) =
        let len = length arr
        let res1 = ResizeArray<_>(len)
        let res2 = ResizeArray<_>(len)
        for i = 0 to len - 1 do
            let x,y = arr[i]
            res1.Add(x)
            res2.Add(y)
        res1,res2

// TODO: active patterns
module Memory =
    let inline isEmpty (memory: Memory<_>) = memory.Length = 0
    let inline any (memory: Memory<_>) = memory.Length <> 0
    let inline slice start count (memory: Memory<_>) = memory.Slice(start, count)
    let inline sliceFrom start (memory: Memory<_>) = memory.Slice(start)
    let inline sliceTo index (memory: Memory<_>) = memory.Slice(0, index)
    let inline fromArray (array: 'a[]): Memory<_> = Memory.op_Implicit array
    let inline fromArraySegment (array: 'a ArraySegment): Memory<_> = Memory.op_Implicit array

// TODO: active patterns
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
    let inline intersectBy<'a, 'b when 'b: equality> ([<InlineIfLambda>] map: 'a -> 'b) left right =
        Enumerable.Intersect(left, right, EqualityComparer<'a>.Create map)
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
        seq1 == seq2

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
       static member GetTag unionObj = TagGetter<'a>._TagGetter.Invoke unionObj

    [<AbstractClass; Sealed>]
    type private NameGetter<'a>() =

        [<DefaultValue>]
        static val mutable private _Names: string array
        static do NameGetter<'a>._Names <- [| for unionCase in typeof<'a> |> FSharpType.GetUnionCases -> unionCase.Name |]
        static member private GetNameInternal index = NameGetter<'a>._Names[index]
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