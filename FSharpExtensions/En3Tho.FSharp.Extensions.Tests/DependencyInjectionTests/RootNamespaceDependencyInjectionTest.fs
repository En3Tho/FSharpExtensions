namespace En3Tho.FSharp.Extensions.Tests

open Microsoft.Extensions.DependencyInjection
open Xunit

type Dependency1 = {
    Value: string
}

type Dependency2 = {
    Value: Dependency1
}

type Startup() =
    member this.ConfigureServices(services: IServiceCollection) =
       services
           .AddSingleton({ Dependency1.Value = "abc" })
           .AddSingleton<Dependency2>()
       |> ignore

type Test(dep: Dependency1, dep2: Dependency2) =

    [<Fact>]
    let ``Test that DI works in tests``() =
        Assert.Equal(dep2.Value.Value, dep.Value)
        Assert.Equal(dep2.Value.Value, "abc")