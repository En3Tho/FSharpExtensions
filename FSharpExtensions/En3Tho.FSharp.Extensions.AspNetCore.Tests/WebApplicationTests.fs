module En3Tho.FSharp.Extensions.AspNetCore.WebApplicationTests

open System
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.Extensions.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Routing
open Microsoft.Extensions.Logging
open Xunit

let addSomeStuffToWebApplication(webApp: WebApplication) = webApp {
    Get("Get", fun () -> "Hello World!")
    Post("Post", fun () -> Console.WriteLine("Oops")).Configure(fun builder ->
        builder.RequireCors("Whatever")
    )
    Put("Put", fun (v: int) -> "v")
    Delete("Delete", fun () -> Console.WriteLine("Oops"))
    Patch("Patch", fun (x: string) -> Console.WriteLine("Oops"))
}

let addSomeStuffToWebApplicationViaExtensions(webApp: WebApplication) =
    webApp.MapGet("Get", fun () -> "Hello World!") |> ignore
    webApp.MapPost("Post", fun () -> Console.WriteLine("Oops")).RequireCors("Whatever") |> ignore
    webApp.MapPut("Put", fun (v: int) -> "v") |> ignore
    webApp.MapDelete("Delete", fun () -> Console.WriteLine("Oops")) |> ignore
    webApp.MapPatch("Patch", fun (x: string) -> Console.WriteLine("Oops")) |> ignore
    webApp

let addComplexObject(webApp: WebApplication) = webApp {
    Get("Get", fun () -> "Hello World!")
    // Get("Get", fun ([<FromServices>] logger: ILogger) -> logger.LogInformation("Hello world")) // requires new feature
    Post("Post", fun (logger: ILogger) -> logger.LogInformation("Hello world")) // inferred from body?
    Put("Put", fun (logger: ILogger) -> logger.LogInformation("Hello world")) // inferred from body?
    Delete("Delete", fun () -> "Hello World!")
    // Delete("Delete", fun ([<FromServices>] logger: ILogger) -> logger.LogInformation("Hello world")) // requires new feature
    Patch("Patch", fun (logger: ILogger) -> logger.LogInformation("Hello world")) // inferred from body?
}

let testWebAppContainsValidValues(webApp: WebApplication) =

    let builder =
        webApp
        :> IEndpointRouteBuilder

    let checkEndpoint(name, verb) =
        builder.DataSources
        |> Seq.exists (fun endpointDataSource ->
            endpointDataSource.Endpoints // calling this does the actual validation
            |> Seq.exists (fun endpoint ->
                endpoint.DisplayName.Equals($"HTTP: {verb} {name} => Invoke")))

    Assert.True(checkEndpoint("Get", "GET"))
    Assert.True(checkEndpoint("Post", "POST"))
    Assert.True(checkEndpoint("Put", "PUT"))
    Assert.True(checkEndpoint("Delete", "DELETE"))
    Assert.True(checkEndpoint("Patch", "PATCH"))

[<Fact>]
let ``let test that compexpr works``() =
    WebApplication.Create()
    |> addSomeStuffToWebApplication
    |> testWebAppContainsValidValues

[<Fact>]
let ``let test that extensions work``() =
    WebApplication.Create()
    |> addSomeStuffToWebApplicationViaExtensions
    |> testWebAppContainsValidValues

[<Fact>]
let ``let test logger works``() =
    WebApplication.Create()
    |> addComplexObject
    |> testWebAppContainsValidValues