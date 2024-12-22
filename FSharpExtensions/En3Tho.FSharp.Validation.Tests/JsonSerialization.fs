module En3Tho.FSharp.Validation.Tests.JsonSerializationTests

open System
open System.Text.Json
open En3Tho.FSharp.Validation
open En3Tho.FSharp.Validation.CommonValidatedTypes
open En3Tho.FSharp.Validation.Json
open Xunit

let makeJsonSerializerOptions() =
    JsonSerializerOptions()
        .AddValidatedConverters()

let options = makeJsonSerializerOptions()

type InstancePlainValue<'a> = InstanceValidatorValidated<'a, PlainValue.Validator<'a>>

[<Fact>]
let ``test that new ctor validator validated values get serialized and deserialized``() =
    let plain10 = PlainValue.Make(10)
    let str = JsonSerializer.Serialize(plain10, options)
    let result = JsonSerializer.Deserialize<PlainValue<int>>(str, options)
    Assert.Equal(plain10, result)

[<Fact>]
let ``test that NonNegativeValue get serialized and deserialized``() =
    let plain10 = NonNegativeValue.Make(10)
    let str = JsonSerializer.Serialize(plain10, options)
    let result = JsonSerializer.Deserialize<NonNegativeValue<int>>(str, options) // should throw on negative case
    Assert.Equal(plain10, result)

[<Fact>]
let ``test that NonNegativeValue should throw on deserialization on bad case``() =
    let str = "-15"
    let result = JsonSerializer.Deserialize<NonNegativeValue<int>>(str, options)
    Assert.Throws<NonNegativeValue.ValueIsNegativeException>(fun () ->
        JsonSerializer.Deserialize<NonNegativeValue<int>>(str, options) |> ignore)

[<Fact>]
let ``test that new ctor validator validated values get serialized as their underlying values``() =
    let plain10 = PlainValue.Make(10)
    let serializedPlain10 = JsonSerializer.Serialize(plain10, options)
    let serialized10 = JsonSerializer.Serialize(plain10.Value, options)
    Assert.Equal(serialized10, serializedPlain10)

[<Fact>]
let ``test that instance validator validated values get serialized as their underlying values``() =
    let plain10 = InstancePlainValue.Make(10, PlainValue.Validator())
    let serializedPlain10 = JsonSerializer.Serialize(plain10, options)
    let serialized10 = JsonSerializer.Serialize(plain10.Value, options)
    Assert.Equal(serialized10, serializedPlain10)

[<Fact>]
let ``test that instance validator validated values get serialized but not deserialized``() =
    let plain10 = InstancePlainValue.Make(10, PlainValue.Validator())
    let serializedPlain10 = JsonSerializer.Serialize(plain10, options)
    Assert.Throws<InvalidOperationException>(fun () ->
        JsonSerializer.Deserialize<InstancePlainValue<int>>(serializedPlain10, options) |> ignore)