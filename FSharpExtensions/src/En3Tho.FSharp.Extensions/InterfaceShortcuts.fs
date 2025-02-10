namespace En3Tho.FSharp.Extensions.InterfaceShortcuts

open System

module IEquatable =
    let inline equals<'a when 'a :> IEquatable<'a>> (left: 'a) (right: 'a) = left.Equals(right)

module IComparable =
    let inline compareTo<'a when 'a :> IComparable<'a>> (left: 'a) (right: 'a) = left.CompareTo(right)
    let inline gt<'a when 'a :> IComparable<'a>> (left: 'a) (right: 'a) = left.CompareTo(right) > 0
    let inline gte<'a when 'a :> IComparable<'a>> (left: 'a) (right: 'a) = left.CompareTo(right) >= 0
    let inline lt<'a when 'a :> IComparable<'a>> (left: 'a) (right: 'a) = left.CompareTo(right) < 0
    let inline lte<'a when 'a :> IComparable<'a>> (left: 'a) (right: 'a) = left.CompareTo(right) <= 0
    let inline eq<'a when 'a :> IComparable<'a>> (left: 'a) (right: 'a) = left.CompareTo(right) = 0
    let inline min<'a when 'a :> IComparable<'a>> (left: 'a) (right: 'a) = if left.CompareTo(right) < 0 then left else right
    let inline max<'a when 'a :> IComparable<'a>> (left: 'a) (right: 'a) = if left.CompareTo(right) > 0 then left else right