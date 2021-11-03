module En3Tho.FSharp.Extensions.Tests.DependencyInjectionTests.ServiceCollectionExtensionsTests

open System
open En3Tho.Extensions.DependencyInjection
open Microsoft.Extensions.DependencyInjection
open Xunit

type IMyType = interface end
type MyType() =
    interface IMyType

[<Fact>]
let ``Test that try add methods properly work``() =
    let serviceCollection = ServiceCollection()
    serviceCollection.TryAddSingletonOrFail<IMyType, MyType>() |> ignore
    Assert.Throws<InvalidOperationException>(fun() ->
        serviceCollection.TryAddSingletonOrFail<IMyType, MyType>() |> ignore)
    |> ignore

    Assert.Throws<InvalidOperationException>(fun() ->
        serviceCollection.TryAddScopedOrFail<IMyType, MyType>() |> ignore)
    |> ignore

    Assert.Throws<InvalidOperationException>(fun() ->
        serviceCollection.TryAddTransientOrFail<IMyType, MyType>() |> ignore)
    |> ignore