module En3Tho.Extensions.DependencyInjection.Tests.AddSingletonAsSelfAndInjectionTests

open System
open Microsoft.Extensions.DependencyInjection
open Xunit

type ITest = interface end

type Test() =
    interface ITest

[<Fact>]
let ``Test that registered func work properly`` () =
    let serviceProvider =
        ServiceCollection()
            .AddSingleton<Test>()
            .AddSingleton<ITest, Test>(fun sp -> sp.GetRequiredService<Test>())
            .BuildServiceProvider()

    let dep1 = serviceProvider.GetRequiredService<ITest>()
    let dep2 = serviceProvider.GetRequiredService<Test>()

    Assert.True(Object.ReferenceEquals(dep1, dep2))