module En3Tho.Extensions.DependencyInjection.Tests.ServiceCollectionExtensionsTests

open System
open En3Tho.Extensions.DependencyInjection
open Microsoft.Extensions.DependencyInjection
open Xunit

type IMyType = interface end

type MyType() =
    override _.Equals(value: obj) =
        match value with
        | :? MyType -> true
        | _ -> false
    override _.GetHashCode() = 0
    interface IMyType

type MyType2() =
    override _.Equals(value: obj) =
        match value with
        | :? MyType2 -> true
        | _ -> false
    override _.GetHashCode() = 0
    interface IMyType

type MyType3() =
    override _.Equals(value: obj) =
        match value with
        | :? MyType3 -> true
        | _ -> false
    override _.GetHashCode() = 0
    interface IMyType

type MyType4() =
    override _.Equals(value: obj) =
        match value with
        | :? MyType4 -> true
        | _ -> false
    override _.GetHashCode() = 0
    interface IMyType

[<Fact>]
let ``Test that try add methods throw when type is already registered``() =
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

[<Fact>]
let ``Test that try add or update methods throw when there are more than 1 implementation``() =
    let serviceCollection = ServiceCollection()
    serviceCollection.TryAddOrUpdateSingletonOrFail<IMyType, MyType>() |> ignore
    serviceCollection.AddSingleton<IMyType, MyType>() |> ignore
    Assert.Throws<InvalidOperationException>(fun() ->
        serviceCollection.TryAddOrUpdateSingletonOrFail<IMyType, MyType>() |> ignore)
    |> ignore

    Assert.Throws<InvalidOperationException>(fun() ->
        serviceCollection.TryAddOrUpdateScopedOrFail<IMyType, MyType>() |> ignore)
    |> ignore

    Assert.Throws<InvalidOperationException>(fun() ->
        serviceCollection.TryAddOrUpdateTransientOrFail<IMyType, MyType>() |> ignore)
    |> ignore

[<Fact>]
let ``Test that try add or update methods properly update implementation type``() =
    let serviceCollection = ServiceCollection()
    serviceCollection.TryAddOrUpdateSingletonOrFail<IMyType, MyType>() |> ignore
    Assert.Equal(serviceCollection.BuildServiceProvider().GetRequiredService<IMyType>(), MyType())

    serviceCollection.TryAddOrUpdateSingletonOrFail<IMyType, MyType2>() |> ignore
    Assert.Equal(serviceCollection.BuildServiceProvider().GetRequiredService<IMyType>(), MyType2())


    serviceCollection.TryAddOrUpdateScopedOrFail<IMyType, MyType3>() |> ignore
    Assert.Equal(serviceCollection.BuildServiceProvider().GetRequiredService<IMyType>(), MyType3())

    serviceCollection.TryAddOrUpdateTransientOrFail<IMyType, MyType4>() |> ignore
    Assert.Equal(serviceCollection.BuildServiceProvider().GetRequiredService<IMyType>(), MyType4())
