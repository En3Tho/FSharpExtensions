module En3Tho.Extensions.DependencyInjection.Tests.ServiceCollectionExtensionsTests

open System
open En3Tho.Extensions.DependencyInjection
open En3Tho.Extensions.DependencyInjection.Tests.DecorationTests
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

type DoubleCtorType(myType: MyType, myType2: MyType2, myType3: MyType3) =
    new (myType: MyType) = DoubleCtorType(myType, MyType2(), MyType3()) // DI will just use this one because it can be resolved by it
    member _.MyType = myType
    member _.MyType2 = myType2
    member val X = 0 with get, set

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
let ``Test that try add or replace methods throw when there are more than 1 implementation``() =
    let serviceCollection = ServiceCollection()
    serviceCollection.TryAddOrReplaceSingletonOrFail<IMyType, MyType>() |> ignore
    serviceCollection.AddSingleton<IMyType, MyType>() |> ignore
    Assert.Throws<InvalidOperationException>(fun() ->
        serviceCollection.TryAddOrReplaceSingletonOrFail<IMyType, MyType>() |> ignore)
    |> ignore

    Assert.Throws<InvalidOperationException>(fun() ->
        serviceCollection.TryAddOrReplaceScopedOrFail<IMyType, MyType>() |> ignore)
    |> ignore

    Assert.Throws<InvalidOperationException>(fun() ->
        serviceCollection.TryAddOrReplaceTransientOrFail<IMyType, MyType>() |> ignore)
    |> ignore

[<Fact>]
let ``Test that try add or replace methods properly update implementation type``() =
    let serviceCollection = ServiceCollection()
    serviceCollection.TryAddOrReplaceSingletonOrFail<IMyType, MyType>() |> ignore
    Assert.Equal(serviceCollection.BuildServiceProvider().GetRequiredService<IMyType>(), MyType())

    serviceCollection.TryAddOrReplaceSingletonOrFail<IMyType, MyType2>() |> ignore
    Assert.Equal(serviceCollection.BuildServiceProvider().GetRequiredService<IMyType>(), MyType2())


    serviceCollection.TryAddOrReplaceScopedOrFail<IMyType, MyType3>() |> ignore
    Assert.Equal(serviceCollection.BuildServiceProvider().GetRequiredService<IMyType>(), MyType3())

    serviceCollection.TryAddOrReplaceTransientOrFail<IMyType, MyType4>() |> ignore
    Assert.Equal(serviceCollection.BuildServiceProvider().GetRequiredService<IMyType>(), MyType4())

[<Fact>]
let ``Test that replace will substitute existing implementation if it is registered as base type`` () =
    let serviceProvider =
        ServiceCollection()
            .AddSingleton<ServiceWithValue>(new ServiceWithValue(1)) // this will get substituted
            .AddSingleton<IService, ServiceWithValue>(fun provider -> provider.GetRequiredService<ServiceWithValue>())
            .TryAddOrReplaceSingletonOrFail<ServiceWithValue, ServiceWithValueChild>(fun _ -> new ServiceWithValueChild(2))
            .BuildServiceProvider()
    let service = serviceProvider.GetRequiredService<ServiceWithValue>()
    Assert.Equal(2, service.Value)

    let service = serviceProvider.GetRequiredService<IService>()
    Assert.Equal(typeof<ServiceWithValueChild>, service.GetType())
    Assert.Equal(2, service.GetValue())

[<Fact>]
let ``Test that DI works when there is a double ctor``() =
    let serviceProvider =
        ServiceCollection()
            .AddSingleton<MyType>()
            .AddSingleton<MyType2>()
            .AddSingleton<DoubleCtorType>()
            .BuildServiceProvider()
    let service = serviceProvider.GetRequiredService<DoubleCtorType>()

    ignore service