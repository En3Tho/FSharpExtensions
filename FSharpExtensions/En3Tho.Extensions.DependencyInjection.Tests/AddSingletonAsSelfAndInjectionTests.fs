module En3Tho.Extensions.DependencyInjection.Tests.AddSingletonAsSelfAndInjectionTests

open System
open En3Tho.Extensions.DependencyInjection
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.DependencyInjection.Extensions
open Xunit

type ITest = interface end

type Test() =
    interface ITest

// these tests are more for myself

[<Fact>]
let ``Test that singleton types registered via func are same objects`` () =
    let serviceProvider =
        ServiceCollection()
            .AddSingleton<Test>()
            .AddSingleton<ITest, Test>(fun sp -> sp.GetRequiredService<Test>())
            .BuildServiceProvider()

    let dep1 = serviceProvider.GetRequiredService<ITest>()
    let dep2 = serviceProvider.GetRequiredService<Test>()

    Assert.True(Object.ReferenceEquals(dep1, dep2))

[<Fact>]
let ``Test that scoped types registered via func are same objects`` () =
    let serviceProvider =
        ServiceCollection()
            .AddScoped<Test>()
            .AddScoped<ITest, Test>(fun sp -> sp.GetRequiredService<Test>())
            .BuildServiceProvider()

    let dep1 = serviceProvider.GetRequiredService<ITest>()
    let dep2 = serviceProvider.GetRequiredService<Test>()

    Assert.True(Object.ReferenceEquals(dep1, dep2))

    use scope = serviceProvider.CreateScope()
    let dep1 = serviceProvider.GetRequiredService<ITest>()
    let dep2 = serviceProvider.GetRequiredService<Test>()

    Assert.True(Object.ReferenceEquals(dep1, dep2))

[<Fact>]
let ``Test that singleton types registered via AddAsSelfAnd extension are same objects`` () =
    let serviceProvider =
        ServiceCollection()
            .AddSingletonAsSelfAnd<ITest, Test>()
            .BuildServiceProvider()

    let dep1 = serviceProvider.GetRequiredService<ITest>()
    let dep2 = serviceProvider.GetRequiredService<Test>()

    Assert.True(Object.ReferenceEquals(dep1, dep2))

[<Fact>]
let ``Test that scoped types registered via AddAsSelfAnd extension are same objects`` () =
    let serviceProvider =
        ServiceCollection()
            .AddScoped<Test>()
            .AddScoped<ITest, Test>(fun sp -> sp.GetRequiredService<Test>())
            .BuildServiceProvider()

    let dep1 = serviceProvider.GetRequiredService<ITest>()
    let dep2 = serviceProvider.GetRequiredService<Test>()

    Assert.True(Object.ReferenceEquals(dep1, dep2))

    use scope = serviceProvider.CreateScope()
    let dep1 = serviceProvider.GetRequiredService<ITest>()
    let dep2 = serviceProvider.GetRequiredService<Test>()

    Assert.True(Object.ReferenceEquals(dep1, dep2))

[<Fact>]
let ``Test that transient types registered via func are different objects`` () =
    let serviceProvider =
        ServiceCollection()
            .AddTransient<Test>()
            .AddTransient<ITest, Test>(fun sp -> sp.GetRequiredService<Test>())
            .BuildServiceProvider()

    let dep1 = serviceProvider.GetRequiredService<ITest>()
    let dep2 = serviceProvider.GetRequiredService<Test>()

    Assert.False(Object.ReferenceEquals(dep1, dep2))

[<Fact>]
let ``Test that singleton types registered via calls are different objects`` () =
    let serviceProvider =
        let serviceCollection =
            ServiceCollection()
                .AddSingleton<Test>()

        serviceCollection.TryAddSingleton<ITest, Test>()

        serviceCollection
            .BuildServiceProvider()

    let dep1 = serviceProvider.GetRequiredService<ITest>()
    let dep2 = serviceProvider.GetRequiredService<Test>()

    Assert.False(Object.ReferenceEquals(dep1, dep2))

[<Fact>]
let ``Test that singleton types registered via calls are different objects 2`` () =
    let serviceProvider =
        ServiceCollection()
            .AddSingleton<Test>()
            .AddSingleton<ITest, Test>()
            .BuildServiceProvider()

    let dep1 = serviceProvider.GetRequiredService<ITest>()
    let dep2 = serviceProvider.GetRequiredService<Test>()

    Assert.False(Object.ReferenceEquals(dep1, dep2))