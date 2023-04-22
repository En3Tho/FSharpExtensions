module En3Tho.FSharp.ComputationExpressions.Tests.HttpBuilderTests

open System.IO
open System.Net.Http
open En3Tho.FSharp.ComputationExpressions.HttpBuilder
open Xunit

[<Fact>]
let ``test that http builder does the basic get request as string``() = task {
    let http = HttpClient()
    let! response = http.Get("https://google.com").AsString()
    Assert.NotEmpty(response)
}

[<Fact>]
let ``test that http builder does the basic get request as byte array``() = task {
    let http = HttpClient()
    let! response = http.Get("https://google.com").AsByteArray()
    Assert.NotEmpty(response)
}

[<Fact>]
let ``test that http builder does the basic get request as stream``() = task {
    let http = HttpClient()
    use! response = http.Get("https://google.com").AsStream()
    use reader = StreamReader(response)
    let! message = reader.ReadToEndAsync()
    Assert.NotEmpty(message)
}