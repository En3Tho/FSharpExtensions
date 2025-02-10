module En3Tho.FSharp.ComputationExpressions.Tests.HttpBuilderTests

open System.IO
open System.Net
open System.Net.Http
open System.Text.Json
open System.Threading.Tasks
open En3Tho.FSharp.Extensions
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

module MockedTests =
    type FuncHandler(checkRequest) =
        inherit HttpMessageHandler()
        override this.SendAsync(request, _) =
            Task.FromResult(checkRequest request)

    let mkClient fn = HttpClient(FuncHandler(fn))

    [<Fact>]
    let ``test that http builder correctly passes all body headers``() = task {
        let http = mkClient ^ fun request ->
            Assert.True(request.Headers.Contains("TestRequestHeader1"))
            Assert.True(request.Headers.Contains("TestRequestHeader2"))
            Assert.True(request.Headers.Contains("TestRequestHeader3"))
            HttpResponseMessage(HttpStatusCode.OK)

        use! response = http.Get("https://google.com").AsResponse() {
            requestHeaders {
                "TestRequestHeader1" -- "Wow"
                "TestRequestHeader2" -- "Wow"
                "TestRequestHeader3" -- "Wow"
            }
        }
        Assert.Equal(response.StatusCode, HttpStatusCode.OK)
    }

    [<Fact>]
    let ``test that http builder correctly passes all content headers``() = task {
        let http = mkClient ^ fun request ->
            Assert.True(request.Headers.Contains("TestRequestHeader1"))
            Assert.True(request.Headers.Contains("TestRequestHeader2"))
            Assert.True(request.Headers.Contains("TestRequestHeader3"))
            Assert.True(request.Content.Headers.Contains("TestContentHeader1"))
            Assert.True(request.Content.Headers.Contains("TestContentHeader2"))
            Assert.True(request.Content.Headers.Contains("TestContentHeader3"))
            HttpResponseMessage(HttpStatusCode.OK)

        let! response = http.Post("https://google.com").Json({| Value = 10 |}).AsResponse() {
            requestHeaders {
                "TestRequestHeader1" -- "Wow"
                "TestRequestHeader2" -- "Wow"
                "TestRequestHeader3" -- "Wow"
            }
            contentHeaders {
                "TestContentHeader1" -- "Wow"
                "TestContentHeader2" -- "Wow"
                "TestContentHeader3" -- "Wow"
            }
        }
        Assert.Equal(response.StatusCode, HttpStatusCode.OK)
    }

    [<Fact>]
    let ``test that http builder correctly passes json body``() = task {
        let http = mkClient ^ fun request ->
            let body = JsonSerializer.Deserialize<{| Value: int |}>(request.Content.ReadAsStream())
            Assert.Equal(body.Value, 10)
            HttpResponseMessage(
                HttpStatusCode.OK,
                Content = StringContent(JsonSerializer.Serialize({| body with Value = 15 |}))
            )

        let! response = http.Post("https://google.com").Json({| Value = 10 |}).AsJson<{| Value: int |}>()
        Assert.Equal(response.Value, 15)
    }