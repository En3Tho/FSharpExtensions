module En3Tho.Extensions.DependencyInjection.Tests.ConfigureTests

open En3Tho.Extensions.DependencyInjection
open Microsoft.Extensions.DependencyInjection
open Xunit

type SomeDependency() = class end

type SomeMutableType(dep: SomeDependency) =
    member _.Dep = dep
    member val X = 0 with get, set

[<Fact>]
let ``Test that updating once works`` () =
    let serviceProvider =
        ServiceCollection()
            .AddSingleton<SomeDependency>()
            .AddSingleton<SomeMutableType>()
            .Update<SomeMutableType>(fun mutableType -> mutableType.X <- 10; mutableType)
            .BuildServiceProvider()
    let service = serviceProvider.GetRequiredService<SomeMutableType>()
    Assert.Equal(10, service.X)

[<Fact>]
let ``Test that updating multiple times works`` () =
    let serviceProvider =
        ServiceCollection()
            .AddSingleton<SomeDependency>()
            .AddSingleton<SomeMutableType>()
            .Update<SomeMutableType>(fun mutableType -> mutableType.X <- mutableType.X + 1; mutableType)
            .Update<SomeMutableType>(fun mutableType -> mutableType.X <- mutableType.X + 2; mutableType)
            .Update<SomeMutableType>(fun mutableType -> mutableType.X <- mutableType.X + 3; mutableType)
            .BuildServiceProvider()
    let service = serviceProvider.GetRequiredService<SomeMutableType>()
    Assert.Equal(6, service.X)