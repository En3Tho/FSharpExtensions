module En3Tho.Extensions.DependencyInjection.Tests.AddFuncTests

open En3Tho.Extensions.DependencyInjection.Tests
open Microsoft.Extensions.DependencyInjection
open En3Tho.Extensions.DependencyInjection
open Xunit

type Dependency1 = {
    Value: string
}

type Dependency2 = {
    Value: Dependency1
}

type Dependency3 = {
    Value1: string
    Value2: string
}

let getDep2 (dep1: Dependency1) : Dependency2 = {
    Value = { dep1 with Value = "Dep2"}
}

let getDep3 (dep1: Dependency1) (dep2: Dependency2) : Dependency3 = {
    Value1 = dep1.Value
    Value2 = dep2.Value.Value
}

[<Fact>]
let ``Test that registered func work properly`` () =
    let serviceProvider =
        ServiceCollection()
            .AddSingleton<Dependency1>(implementationInstance = { Value = "Dep1"})
            .AddSingletonFunc(getDep2)
            .AddSingletonFunc(getDep3)
            .BuildServiceProvider()

    let dep3 = serviceProvider.GetRequiredService<Dependency3>()
    Assert.Equal(dep3, { Value1 = "Dep1"; Value2 = "Dep2" }
)