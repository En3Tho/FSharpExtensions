module En3Tho.FSharp.Extensions.Tests.IEquatableOperatorTests

open System.Collections.Generic
open Xunit
open En3Tho.FSharp.Extensions
open En3Tho.FSharp.ComputationExpressions.ICollectionBuilder

module Primitive =
    let value1 = 1
    let value2 = 1
    let value3 = 2

    let array1 = [| 1; 2; 3; 4; 5 |]
    let array2 = [| 1; 2; 3; 4; 5 |]
    let array3 = [| 1; 2; 3; 4; 6 |]

    let list1 = [ 1; 2; 3; 4; 5 ]
    let list2 = [ 1; 2; 3; 4; 5 ]
    let list3 = [ 1; 2; 3; 4; 6 ]

    let sequence1 = seq { 1; 2; 3; 4; 5 }
    let sequence2 = seq { 1; 2; 3; 4; 5 }
    let sequence3 = seq { 1; 2; 3; 4; 6 }

    let resizeArray1 = ResizeArray() { 1; 2; 3; 4; 5 }
    let resizeArray2 = ResizeArray() { 1; 2; 3; 4; 5 }
    let resizeArray3 = ResizeArray() { 1; 2; 3; 4; 6 }

    let hashSet1 = HashSet() { 1; 2; 3; 4; 5 }
    let hashSet2 = HashSet() { 1; 2; 3; 4; 5 }
    let hashSet3 = HashSet() { 1; 2; 3; 4; 6 }

    let dictionary1 = Dictionary() { KeyValuePair(1, value1); KeyValuePair(2, value1); KeyValuePair(3, value1); KeyValuePair(4, value1); KeyValuePair(5, value1); } :?> Dictionary<int, int>
    let dictionary2 = Dictionary() { KeyValuePair(1, value1); KeyValuePair(2, value1); KeyValuePair(3, value1); KeyValuePair(4, value1); KeyValuePair(5, value2); } :?> Dictionary<int, int>
    let dictionary3 = Dictionary() { KeyValuePair(1, value1); KeyValuePair(2, value1); KeyValuePair(3, value1); KeyValuePair(4, value1); KeyValuePair(6, value3); } :?> Dictionary<int, int>

    [<Fact>]
    let ``test that custom IEquatable operator works with all kinds of overloads on primitive type``() =
        Assert.Equal(true, value1 == value2)
        Assert.Equal(true, array1 == array2)
        Assert.Equal(true, list1 == list2)
        Assert.Equal(true, sequence1 == sequence2)
        Assert.Equal(true, resizeArray1 == resizeArray2)

        Assert.Equal(false, value1 == value3)
        Assert.Equal(false, array1 == array3)
        Assert.Equal(false, list1 == list3)
        Assert.Equal(false, sequence1 == sequence3)
        Assert.Equal(false, resizeArray1 == resizeArray3)

        Assert.Equal(true, (value1 = value2))
        Assert.Equal(true, (array1 = array2))
        Assert.Equal(true, (list1 = list2))

        Assert.Equal(false, (value1 = value3))
        Assert.Equal(false, (array1 = array3))
        Assert.Equal(false, (list1 = list3))

module CustomStructRecord =

    type [<Struct>] CustomRecordType = {
        V1: string
        V2: int
        V3: int64
    }

    let value1 = {
        V1 = "test"
        V2 = 10
        V3 = 10
    }

    let value2 = value1

    let value3 = {
        V1 = "test"
        V2 = 10
        V3 = 11
    }

    let array1 = [| value1; value1; value1; value1; value1 |]
    let array2 = [| value1; value1; value1; value1; value2 |]
    let array3 = [| value1; value1; value1; value1; value3 |]

    let list1 = [ value1; value1; value1; value1; value1 ]
    let list2 = [ value1; value1; value1; value1; value2 ]
    let list3 = [ value1; value1; value1; value1; value3 ]

    let sequence1 = seq { value1; value1; value1; value1; value1 }
    let sequence2 = seq { value1; value1; value1; value1; value2 }
    let sequence3 = seq { value1; value1; value1; value1; value3 }

    let resizeArray1 = ResizeArray() { value1; value1; value1; value1; value1 }
    let resizeArray2 = ResizeArray() { value1; value1; value1; value1; value2 }
    let resizeArray3 = ResizeArray() { value1; value1; value1; value1; value3 }

    let hashSet1 = HashSet() { value1; value1; value1; value1; value1 }
    let hashSet2 = HashSet() { value1; value1; value1; value1; value2 }
    let hashSet3 = HashSet() { value1; value1; value1; value1; value3 }

    let dictionary1 = Dictionary(seq { KeyValuePair(value1, value1); KeyValuePair(value3, value2) })
    let dictionary2 = Dictionary(seq { KeyValuePair(value1, value1); KeyValuePair(value3, value2) })
    let dictionary3 = Dictionary(seq { KeyValuePair(value1, value1); KeyValuePair(value3, value3) })

    [<Fact>]
    let ``test that custom IEquatable operator works with all kinds of overloads on custom struct record``() =

        Assert.Equal(true, value1 == value2)
        Assert.Equal(true, array1 == array2)
        Assert.Equal(true, list1 == list2)
        Assert.Equal(true, sequence1 == sequence2)
        Assert.Equal(true, resizeArray1 == resizeArray2)
        Assert.Equal(true, hashSet1 == hashSet2)
        Assert.Equal(true, dictionary1 == dictionary2)

        Assert.Equal(false, value1 == value3)
        Assert.Equal(false, array1 == array3)
        Assert.Equal(false, list1 == list3)
        Assert.Equal(false, sequence1 == sequence3)
        Assert.Equal(false, resizeArray1 == resizeArray3)
        Assert.Equal(false, hashSet1 == hashSet3)
        Assert.Equal(false, dictionary1 == dictionary3)

        Assert.Equal(true, (value1 = value2))
        Assert.Equal(true, (array1 = array2))
        Assert.Equal(true, (list1 = list2))

        Assert.Equal(false, (value1 = value3))
        Assert.Equal(false, (array1 = array3))
        Assert.Equal(false, (list1 = list3))

module CustomRecord =

    type CustomRecordType = {
        V1: string
        V2: int
        V3: int64
    }

    let value1 = {
        V1 = "test"
        V2 = 10
        V3 = 10
    }

    let value2 = value1

    let value3 = {
        V1 = "test"
        V2 = 10
        V3 = 11
    }

    let array1 = [| value1; value1; value1; value1; value1 |]
    let array2 = [| value1; value1; value1; value1; value2 |]
    let array3 = [| value1; value1; value1; value1; value3 |]

    let list1 = [ value1; value1; value1; value1; value1 ]
    let list2 = [ value1; value1; value1; value1; value2 ]
    let list3 = [ value1; value1; value1; value1; value3 ]

    let sequence1 = seq { value1; value1; value1; value1; value1 }
    let sequence2 = seq { value1; value1; value1; value1; value2 }
    let sequence3 = seq { value1; value1; value1; value1; value3 }

    let resizeArray1 = ResizeArray() { value1; value1; value1; value1; value1 }
    let resizeArray2 = ResizeArray() { value1; value1; value1; value1; value2 }
    let resizeArray3 = ResizeArray() { value1; value1; value1; value1; value3 }

    let hashSet1 = HashSet() { value1; value1; value1; value1; value1 }
    let hashSet2 = HashSet() { value1; value1; value1; value1; value2 }
    let hashSet3 = HashSet() { value1; value1; value1; value1; value3 }

    let dictionary1 = Dictionary(seq { KeyValuePair(value1, value1); KeyValuePair(value3, value2) })
    let dictionary2 = Dictionary(seq { KeyValuePair(value1, value1); KeyValuePair(value3, value2) })
    let dictionary3 = Dictionary(seq { KeyValuePair(value1, value1); KeyValuePair(value3, value3) })

    [<Fact>]
    let ``test that custom IEquatable operator works with all kinds of overloads on custom record``() =

        Assert.Equal(true, value1 == value2)
        Assert.Equal(true, array1 == array2)
        Assert.Equal(true, list1 == list2)
        Assert.Equal(true, sequence1 == sequence2)
        Assert.Equal(true, resizeArray1 == resizeArray2)
        Assert.Equal(true, hashSet1 == hashSet2)
        Assert.Equal(true, dictionary1 == dictionary2)

        Assert.Equal(false, value1 == value3)
        Assert.Equal(false, array1 == array3)
        Assert.Equal(false, list1 == list3)
        Assert.Equal(false, sequence1 == sequence3)
        Assert.Equal(false, resizeArray1 == resizeArray3)
        Assert.Equal(false, hashSet1 == hashSet3)
        Assert.Equal(false, dictionary1 == dictionary3)

        Assert.Equal(true, (value1 = value2))
        Assert.Equal(true, (array1 = array2))
        Assert.Equal(true, (list1 = list2))

        Assert.Equal(false, (value1 = value3))
        Assert.Equal(false, (array1 = array3))
        Assert.Equal(false, (list1 = list3))