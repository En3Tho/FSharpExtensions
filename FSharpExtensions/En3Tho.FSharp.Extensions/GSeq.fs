namespace En3Tho.FSharp.Extensions

open En3Tho.FSharp.Extensions.GSeqEnumerators
open System.Collections.Generic
open System.Runtime.CompilerServices
open En3Tho.FSharp.Extensions

module GSeq =

    let inline getEnumerator<'i, 'e, ^seq when 'e: struct
                                           and 'e :> IEnumerator<'i>
                                           and ^seq: (member GetEnumerator: unit -> SStructEnumerator<'i, 'e>)> (seq: ^seq) =
        (^seq: (member GetEnumerator: unit ->  SStructEnumerator<'i, 'e>) seq)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofIEnumerator enumerator = StructIEnumeratorWrapper(enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofSeq (seq: 'a seq) = StructIEnumeratorWrapper(seq.GetEnumerator())

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofArray array = StructArrayEnumerator(array)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofArrayRev array = StructArrayRevEnumerator(array)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofResizeArray array = StructResizeArrayEnumerator(array)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofResizeArrayRev array = StructResizeArrayRevEnumerator(array)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofIList array = StructIListEnumerator(array)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofIListRev array = StructIListRevEnumerator(array)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofBlock array = StructImmutableArrayEnumerator(array)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofList list = StructFSharpListEnumerator(list)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let ofMap (map: Map<_,_>) = map |> ofSeq

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let filter filter enumerator = StructFilterEnumerator(filter, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let choose chooser enumerator = StructChooseEnumerator(chooser, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let choose2 enumerator2 chooser enumerator = StructChoose2Enumerator(chooser, enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let choose3 enumerator2 enumerator3 chooser enumerator = StructChoose3Enumerator(chooser, enumerator, enumerator2, enumerator3)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let valueChoose chooser enumerator = StructValueChooseEnumerator(chooser, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let valueChoose2 enumerator2 chooser enumerator = StructValueChoose2Enumerator(chooser, enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let valueChoose3 enumerator2 enumerator3 chooser enumerator = StructValueChoose3Enumerator(chooser, enumerator, enumerator2, enumerator3)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let append enumerator2 enumerator = StructAppendEnumerator(enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let zip enumerator2 enumerator = StructZipEnumerator(enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let valueZip enumerator2 enumerator = StructValueZipEnumerator(enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let map map enumerator = StructMapEnumerator(map, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let mapi map enumerator = StructMapiEnumerator(map, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let map2 map enumerator2 enumerator = StructMap2Enumerator(map, enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let mapi2 map enumerator2 enumerator = StructMapi2Enumerator(map, enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let skip count enumerator = StructSkipEnumerator(count, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let take count enumerator = StructTakeEnumerator(count, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let skipWhile filter enumerator = StructSkipWhileEnumerator(filter, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let takeWhile filter enumerator = StructTakeWhileEnumerator(filter, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryFind (filter: 'i -> bool) (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        let mutable result = None
        while result.IsNone && enumerator.MoveNext() do
            let value = enumerator.Current
            if value |> filter then result <- Some value
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValueFind (filter: 'i -> bool) (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        let mutable result = ValueNone
        while result.IsNone && enumerator.MoveNext() do
            let value = enumerator.Current
            if value |> filter then result <- ValueSome value
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryPick filter (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        let mutable result = None
        while result.IsNone && enumerator.MoveNext() do
            let value = enumerator.Current
            result <- filter value
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValuePick filter (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        let mutable result = ValueNone
        while result.IsNone && enumerator.MoveNext() do
            let value = enumerator.Current
            result <- filter value
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let exists (filter: 'i -> bool) (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        let mutable found = false
        while not found && enumerator.MoveNext() do
            let value = enumerator.Current
            found <- value |> filter
        found

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let forall (filter: 'i -> bool) (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        let mutable result = true
        while result && enumerator.MoveNext() do
            let value = enumerator.Current
            result <- value |> filter
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let forall2 (enumerator2: SStructEnumerator<'i2,'e2>) filter (enumerator: SStructEnumerator<'i,'e>) =
        let filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt filter
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable result = true
        while result && enumerator.MoveNext() && enumerator2.MoveNext() do
            result <- filter.Invoke(enumerator2.Current, enumerator.Current)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let forall3 (enumerator2: SStructEnumerator<'i2,'e2>) (enumerator3: SStructEnumerator<'i2,'e2>) filter (enumerator: SStructEnumerator<'i,'e>) =
        let filter = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt filter
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable enumerator3 = enumerator3
        let mutable result = true
        while result && enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() do
            result <- filter.Invoke(enumerator2.Current, enumerator3.Current, enumerator.Current)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let contains value (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        let mutable found = false
        while not found && enumerator.MoveNext() do
            let value2 = enumerator.Current
            found <- value = value2
        found

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValueItem index (enumerator: SStructEnumerator<'i,'e>) =
        if index < 0 then ValueNone else
        let mutable enumerator = enumerator
        let mutable counter = 0
        while counter < index && enumerator.MoveNext() do ()
        if enumerator.MoveNext() then ValueSome enumerator.Current
        else ValueNone

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryItem index (enumerator: SStructEnumerator<'i,'e>) =
        if index < 0 then None else
        let mutable enumerator = enumerator
        let mutable counter = 0
        while counter < index && enumerator.MoveNext() do ()
        if enumerator.MoveNext() then Some enumerator.Current
        else None

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let fold initial folder (enumerator: SStructEnumerator<'i,'e>) =
        let folder = OptimizedClosures.FSharpFunc<_,_,_>.Adapt folder
        let mutable enumerator = enumerator
        let mutable result = initial
        while enumerator.MoveNext() do
            result <- folder.Invoke(result, enumerator.Current)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let fold2 (enumerator2: SStructEnumerator<'i2,'e2>) initial folder (enumerator: SStructEnumerator<'i,'e>) =
        let folder = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt folder
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable result = initial
        while enumerator.MoveNext() && enumerator2.MoveNext() do
            result <- folder.Invoke(result, enumerator2.Current, enumerator.Current)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let foldi initial folder (enumerator: SStructEnumerator<'i,'e>) =
        let folder = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt folder
        let mutable enumerator = enumerator
        let mutable count = 0
        let mutable result = initial
        while enumerator.MoveNext() do
            result <- folder.Invoke(count, result, enumerator.Current)
            &count +<- 1
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let foldi2 (enumerator2: SStructEnumerator<'i2,'e2>) initial folder (enumerator: SStructEnumerator<'i,'e>) =
        let folder = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt folder
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable count = 0
        let mutable result = initial
        while enumerator.MoveNext() && enumerator2.MoveNext() do
            result <- folder.Invoke(count, result, enumerator2.Current, enumerator.Current)
            &count +<- 1
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iter action (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        while enumerator.MoveNext() do
            action enumerator.Current

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iter2 (enumerator2: SStructEnumerator<'i2,'e2>) action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_>.Adapt action
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        while enumerator.MoveNext() && enumerator2.MoveNext() do
            action.Invoke(enumerator2.Current, enumerator.Current)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iter3 (enumerator2: SStructEnumerator<'i2,'e2>) (enumerator3: SStructEnumerator<'i3,'e3>) action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt action
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable enumerator3 = enumerator3
        while enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() do
            action.Invoke(enumerator2.Current, enumerator3.Current, enumerator.Current)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iteri action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_>.Adapt action
        let mutable enumerator = enumerator
        let mutable i = 0
        while enumerator.MoveNext() do
            action.Invoke(i, enumerator.Current)
            &i +<- 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iteri2 (enumerator2: SStructEnumerator<'i2,'e2>) action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt action
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable i = 0
        while enumerator.MoveNext() && enumerator2.MoveNext() do
            action.Invoke(i, enumerator2.Current, enumerator.Current)
            &i +<- 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iteri3 (enumerator2: SStructEnumerator<'i2,'e2>) (enumerator3: SStructEnumerator<'i3,'e3>) action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt action
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable enumerator3 = enumerator3
        let mutable i = 0
        while enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() do
            action.Invoke(i, enumerator2.Current, enumerator3.Current, enumerator.Current)
            &i +<- 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let identical (enumerator2: SStructEnumerator<'i,'e>) (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable state = 0
        while state = 0 do
            state <-
                match enumerator.MoveNext(), enumerator2.MoveNext() with
                | false, false -> 1
                | true, true when enumerator.Current = enumerator2.Current -> 0
                | _ -> -1
        state = 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let identicalBy (enumerator2: SStructEnumerator<'i,'e>) comparer (enumerator: SStructEnumerator<'i,'e>) =
        let comparer = OptimizedClosures.FSharpFunc<_,_,_>.Adapt comparer
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable flag = 0
        while flag = 0 do
            flag <-
                match enumerator.MoveNext(), enumerator2.MoveNext() with
                | false, false -> 1
                | true, true when comparer.Invoke(enumerator2.Current, enumerator.Current) -> 0
                | _ -> -1
        flag = 1

    let inline private minImpl (enumerator: SStructEnumerator<'i,'e> byref) =
        let mutable current = enumerator.Current
        while enumerator.MoveNext() do
            current <- Operators.min current enumerator.Current
        current

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let min (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            invalidOp "Seq is empty"
        else
            minImpl &enumerator

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryMin (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            None
        else
            minImpl &enumerator |> Some

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValueMin (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            ValueNone
        else
            minImpl &enumerator |> ValueSome

    let inline private maxImpl (enumerator: SStructEnumerator<'i,'e> byref) =
        let mutable current = enumerator.Current
        while enumerator.MoveNext() do
            current <- Operators.max current enumerator.Current
        current

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let max (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            invalidOp "Seq is empty"
        else
            maxImpl &enumerator

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryMax (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            None
        else
            maxImpl &enumerator |> Some

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValueMax (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            ValueNone
        else
            maxImpl &enumerator |> ValueSome

    let inline private minByImpl map (enumerator: SStructEnumerator<'i,'e> byref) =
        let mutable result = enumerator.Current
        let mutable mapping = map result
        while enumerator.MoveNext() do
            let next = enumerator.Current
            let nextMapping = map next
            if mapping > nextMapping then
                mapping <- nextMapping
                result <- next
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let minBy map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            invalidOp "Seq is empty"
        else
            minByImpl map &enumerator

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryMinBy map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            None
        else
            minByImpl map &enumerator |> Some

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValueMinBy map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            ValueNone
        else
            minByImpl map &enumerator |> ValueSome

    let inline private maxByImpl map (enumerator: SStructEnumerator<'i,'e> byref) =
        let mutable result = enumerator.Current
        let mutable mapping = map result
        while enumerator.MoveNext() do
            let next = enumerator.Current
            let nextMapping = map next
            if mapping < nextMapping then
                mapping <- nextMapping
                result <- next
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let maxBy map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            invalidOp "Seq is empty"
        else
            maxByImpl map &enumerator

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryMaxBy map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            None
        else
            maxByImpl map &enumerator |> Some

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValueMaxBy map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            ValueNone
        else
            maxByImpl map &enumerator |> ValueSome

    let inline sum (enumerator: SStructEnumerator<'i,'e>) = TODO

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let length (enumerator: SStructEnumerator<'i,'e>) =
        let mutable result = 0
        let mutable enumerator = enumerator
        while enumerator.MoveNext() do
            &result +<- 1
        result

    // [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    // let toArray (enumerator: SStructEnumerator<'i,'e>) = arr {
    //     let mutable enumerator = enumerator
    //     while enumerator.MoveNext() do
    //         enumerator.Current
    // }
    //
    // [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    // let toResizeArray (enumerator: SStructEnumerator<'i,'e>) = rsz {
    //     let mutable enumerator = enumerator
    //     while enumerator.MoveNext() do
    //         enumerator.Current
    // }
    //
    // [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    // let toBlock (enumerator: SStructEnumerator<'i,'e>) = block {
    //     let mutable enumerator = enumerator
    //     while enumerator.MoveNext() do
    //         enumerator.Current
    // }

    let toSeq (enumerator: SStructEnumerator<'i,'e>) =
        let boxed = enumerator :> IEnumerator<'i>
        { new IEnumerable<'i> with
            member _.GetEnumerator() = boxed :> System.Collections.IEnumerator
            member _.GetEnumerator() = boxed }

    // TODO: Match Seq's module functions
    // TODO: ActivePatterns

module GSeqv =

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let filter state filter enumerator = StructFilterVEnumerator(state, filter, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let choose state chooser enumerator = StructChooseVEnumerator(state, chooser, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let choose2 state enumerator2 chooser enumerator = StructChoose2VEnumerator(state, chooser, enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let choose3 state enumerator2 enumerator3 chooser enumerator = StructChoose3VEnumerator(state, chooser, enumerator, enumerator2, enumerator3)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let valueChoose state chooser enumerator = StructValueChooseVEnumerator(state, chooser, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let valueChoose2 state enumerator2 chooser enumerator = StructValueChoose2VEnumerator(state, chooser, enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let valueChoose3 state enumerator2 enumerator3 chooser enumerator = StructValueChoose3VEnumerator(state, chooser, enumerator, enumerator2, enumerator3)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let map state map enumerator = StructMapVEnumerator(state, map, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let mapi state map enumerator = StructMapiVEnumerator(state, map, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let map2 state map enumerator2 enumerator = StructMap2VEnumerator(state, map, enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let mapi2 state map enumerator2 enumerator = StructMapi2VEnumerator(state, map, enumerator, enumerator2)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let skipWhile state filter enumerator = StructSkipWhileVEnumerator(state, filter, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let takeWhile state filter enumerator = StructTakeWhileVEnumerator(state, filter, enumerator)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryFind state filter (enumerator: SStructEnumerator<'i,'e>) =
        let filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt filter
        let mutable enumerator = enumerator
        let mutable result = None
        while result.IsNone && enumerator.MoveNext() do
            let value = enumerator.Current
            if filter.Invoke(state, value) then result <- Some value
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValueFind state filter (enumerator: SStructEnumerator<'i,'e>) =
        let filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt filter
        let mutable enumerator = enumerator
        let mutable result = ValueNone
        while result.IsNone && enumerator.MoveNext() do
            let value = enumerator.Current
            if filter.Invoke(state, value) then result <- ValueSome value
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryPick state filter (enumerator: SStructEnumerator<'i,'e>) =
        let filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt filter
        let mutable enumerator = enumerator
        let mutable result = None
        while result.IsNone && enumerator.MoveNext() do
            let value = enumerator.Current
            result <- filter.Invoke(state, value)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValuePick state filter (enumerator: SStructEnumerator<'i,'e>) =
        let filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt filter
        let mutable enumerator = enumerator
        let mutable result = ValueNone
        while result.IsNone && enumerator.MoveNext() do
            let value = enumerator.Current
            result <- filter.Invoke(state, value)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let exists state filter (enumerator: SStructEnumerator<'i,'e>) =
        let filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt filter
        let mutable enumerator = enumerator
        let mutable found = false
        while not found && enumerator.MoveNext() do
            let value = enumerator.Current
            found <- filter.Invoke(state, value)
        found

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let forall state filter (enumerator: SStructEnumerator<'i,'e>) =
        let filter = OptimizedClosures.FSharpFunc<_,_,_>.Adapt filter
        let mutable enumerator = enumerator
        let mutable result = true
        while result && enumerator.MoveNext() do
            let value = enumerator.Current
            result <- filter.Invoke(state, value)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let forall2 state (enumerator2: SStructEnumerator<'i2,'e2>) filter (enumerator: SStructEnumerator<'i,'e>) =
        let filter = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt filter
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable result = true
        while result && enumerator.MoveNext() && enumerator2.MoveNext() do
            result <- filter.Invoke(state, enumerator2.Current, enumerator.Current)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let forall3 state (enumerator2: SStructEnumerator<'i2,'e2>) (enumerator3: SStructEnumerator<'i2,'e2>) filter (enumerator: SStructEnumerator<'i,'e>) =
        let filter = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt filter
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable enumerator3 = enumerator3
        let mutable result = true
        while result && enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() do
            result <- filter.Invoke(state, enumerator2.Current, enumerator3.Current, enumerator.Current)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let fold state initial folder (enumerator: SStructEnumerator<'i,'e>) =
        let folder = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt folder
        let mutable enumerator = enumerator
        let mutable result = initial
        while enumerator.MoveNext() do
            result <- folder.Invoke(state, result, enumerator.Current)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let fold2 state initial (enumerator2: SStructEnumerator<'i2,'e2>) folder (enumerator: SStructEnumerator<'i,'e>) =
        let folder = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt folder
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable result = initial
        while enumerator.MoveNext() && enumerator2.MoveNext() do
            result <- folder.Invoke(state, result, enumerator2.Current, enumerator.Current)
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let foldi state initial folder (enumerator: SStructEnumerator<'i,'e>) =
        let folder = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt folder
        let mutable enumerator = enumerator
        let mutable count = 0
        let mutable result = initial
        while enumerator.MoveNext() do
            result <- folder.Invoke(count, state, result, enumerator.Current)
            &count +<- 1
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let foldi2 state initial (enumerator2: SStructEnumerator<'i2,'e2>) folder (enumerator: SStructEnumerator<'i,'e>) =
        let folder = OptimizedClosures.FSharpFunc<_,_,_,_,_,_>.Adapt folder
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable count = 0
        let mutable result = initial
        while enumerator.MoveNext() && enumerator2.MoveNext() do
            result <- folder.Invoke(count, state, result, enumerator2.Current, enumerator.Current)
            &count +<- 1
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iter state action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_>.Adapt action
        let mutable enumerator = enumerator
        while enumerator.MoveNext() do
            action.Invoke(state, enumerator.Current)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iter2 state (enumerator2: SStructEnumerator<'i2,'e2>) action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt action
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        while enumerator.MoveNext() && enumerator2.MoveNext() do
            action.Invoke(state, enumerator2.Current, enumerator.Current)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iter3 state (enumerator2: SStructEnumerator<'i2,'e2>) (enumerator3: SStructEnumerator<'i3,'e3>) action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt action
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable enumerator3 = enumerator3
        while enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() do
            action.Invoke(state, enumerator2.Current, enumerator3.Current, enumerator.Current)

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iteri state action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt action
        let mutable enumerator = enumerator
        let mutable i = 0
        while enumerator.MoveNext() do
            action.Invoke(i, state, enumerator.Current)
            &i +<- 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iteri2 state (enumerator2: SStructEnumerator<'i2,'e2>) action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_,_,_>.Adapt action
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable i = 0
        while enumerator.MoveNext() && enumerator2.MoveNext() do
            action.Invoke(i, state, enumerator2.Current, enumerator.Current)
            &i +<- 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let iteri3 state (enumerator2: SStructEnumerator<'i2,'e2>) (enumerator3: SStructEnumerator<'i3,'e3>) action (enumerator: SStructEnumerator<'i,'e>) =
        let action = OptimizedClosures.FSharpFunc<_,_,_,_,_,_>.Adapt action
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable enumerator3 = enumerator3
        let mutable i = 0
        while enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext() do
            action.Invoke(i, state, enumerator2.Current, enumerator3.Current, enumerator.Current)
            &i +<- 1

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let identicalBy state (enumerator2: SStructEnumerator<'i,'e>) comparer (enumerator: SStructEnumerator<'i,'e>) =
        let comparer = OptimizedClosures.FSharpFunc<_,_,_,_>.Adapt comparer
        let mutable enumerator = enumerator
        let mutable enumerator2 = enumerator2
        let mutable flag = 0
        while flag = 0 do
            flag <-
                match enumerator.MoveNext(), enumerator2.MoveNext() with
                | false, false -> 1
                | true, true when comparer.Invoke(state, enumerator2.Current, enumerator.Current) -> 0
                | _ -> -1
        flag = 1

    let inline private minByImpl state map (enumerator: SStructEnumerator<'i,'e> byref) =
        let map = OptimizedClosures.FSharpFunc<_,_,_>.Adapt map
        let mutable result = enumerator.Current
        let mutable mapping = map.Invoke(state, result)
        while enumerator.MoveNext() do
            let next = enumerator.Current
            let nextMapping = map.Invoke(state, next)
            if mapping > nextMapping then
                mapping <- nextMapping
                result <- next
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let minBy state map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            invalidOp "Seq is empty"
        else
            minByImpl state map &enumerator

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryMinBy state map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            None
        else
            minByImpl state map &enumerator |> Some

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValueMinBy state map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            ValueNone
        else
            minByImpl state map &enumerator |> ValueSome

    let inline private maxByImpl state map (enumerator: SStructEnumerator<'i,'e> byref) =
        let map = OptimizedClosures.FSharpFunc<_,_,_>.Adapt map
        let mutable result = enumerator.Current
        let mutable mapping = map.Invoke(state,  result)
        while enumerator.MoveNext() do
            let next = enumerator.Current
            let nextMapping = map.Invoke(state, next)
            if mapping < nextMapping then
                mapping <- nextMapping
                result <- next
        result

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let maxBy state map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            invalidOp "Seq is empty"
        else
            maxByImpl state map &enumerator

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryMaxBy state map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            None
        else
            maxByImpl state map &enumerator |> Some

    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let tryValueMaxBy state map (enumerator: SStructEnumerator<'i,'e>) =
        let mutable enumerator = enumerator
        if not ^ enumerator.MoveNext() then
            ValueNone
        else
            maxByImpl state map &enumerator |> ValueSome