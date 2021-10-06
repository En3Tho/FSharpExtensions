module En3Tho.FSharp.Extensions.Tests.ScanfTest

open Xunit

open En3Tho.FSharp.Extensions.Scanf

[<Fact>]
let ``Test good cases of scanf`` () =
    let value1 = ref ""
    let value2 = ref 0
    Assert.True (scanf "/authorize myText 123 qwe" $"/authorize {value1} {value2} qwe")
    Assert.True (scanfl "/authorize myText 123 qwe" $"/authorize {value1} {value2} qwe")
    Assert.True (scanfl "/authorize myText 123" $"/authorize {value1} {value2} qwe")
    Assert.True (scanfl "/authorize myText 123 qwe" $"/authorize {value1} {value2}")

[<Fact>]
let ``Test scanf supports basic types`` () =
    let value1 = ref "0"
    let value2 = ref 0
    let value3 = ref 0.
    let value4 = ref 0.f
    let value5 = ref '0'
    let value6 = ref false
    let value7 = ref 0u

    let text = $"{value1.Value} {value2.Value} {value3.Value} {value4.Value} {value5.Value} {value6.Value} {value7.Value}"
    Assert.True(scanf text $"{value1} {value2} {value3} {value4} {value5} {value6} {value7}")

[<Fact>]
let ``Test bad cases of scanf`` () =
    let value1 = ref ""
    let value2 = ref 0
    Assert.False (scanf "/authorize myText 123 qwe1" $"/authorize {value1} {value2} qwe")
    Assert.False (scanf "/authorize myText 123 qwe" $"/authorize {value1} {value2} qwe1")
    Assert.False (scanf "/authorize myText 123 qwe" $"/authorize {value1}1 {value2} qwe")
    Assert.False (scanf "/authorize myText 123 qwe" $"/authorize {value1}1 {value2}")
    Assert.False (scanfl "/authorize 123 qwe" $"/authorize {value1} {value2} qwe")
    Assert.False (scanfl "/authorize1 myText 123" $"/authorize {value1} {value2} qwe")
    Assert.False (scanfl "/authorize myText 123 qwe" $"/authorize {value1} 123 {value2}")