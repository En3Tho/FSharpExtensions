module En3Tho.FSharp.Extensions.Tests.Module

open Microsoft.Extensions.DependencyInjection
open Xunit

type Startup() =
    member this.ConfigureServices(services: IServiceCollection) =
       services
           .AddSingleton({ Dependency1.Value = "Module" })
           .AddSingleton<Dependency2>()
       |> ignore

type Test(dep: Dependency1, dep2: Dependency2) =

    [<Fact>]
    let ``Test that DI works in tests``() =
        Assert.Equal(dep2.Value.Value, dep.Value)
        Assert.Equal(dep2.Value.Value, "Module")