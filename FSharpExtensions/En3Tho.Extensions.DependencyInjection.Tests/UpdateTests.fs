module En3Tho.Extensions.DependencyInjection.Tests.ConfigureTests

open System
open En3Tho.FSharp.Extensions
open En3Tho.Extensions.DependencyInjection
open Microsoft.Extensions.DependencyInjection
open Xunit

type SomeDependency() = class end

type SomeMutableType(dep: SomeDependency) =
    member _.Dep = dep
    member val X = 0 with get, set

[<Fact>]
let ``Test that updating actually works`` () =
    let serviceProvider =
        ServiceCollection()
            .AddSingleton<SomeDependency>()
            .AddSingleton<SomeMutableType>()
            .Update<SomeMutableType>(fun mutableType -> mutableType.X <- 10; mutableType)
            .BuildServiceProvider()
    let service = serviceProvider.GetRequiredService<SomeMutableType>()
    Assert.Equal(10, service.X)