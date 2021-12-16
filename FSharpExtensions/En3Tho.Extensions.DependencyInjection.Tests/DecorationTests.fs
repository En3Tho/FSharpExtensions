module En3Tho.Extensions.DependencyInjection.Tests.DecorationTests

open System
open Microsoft.Extensions.DependencyInjection
open En3Tho.Extensions.DependencyInjection
open Xunit

type IService =
    inherit IDisposable
    abstract GetValue: unit -> int

exception CustomObjectDisposedException

[<AbstractClass>]
type Service() =
    abstract member GetValue: unit -> int
    interface IService with
        member this.GetValue() = this.GetValue()
        member this.Dispose() = raise CustomObjectDisposedException

type Service1() =
    inherit Service()
    override this.GetValue() = 0

type ServiceBaseDecorator1(service: Service) =
    inherit Service()
    override this.GetValue() = service.GetValue() + 1

type ServiceBaseDecorator2(service: Service) =
    inherit Service()
    override this.GetValue() = service.GetValue() + 2

type ServiceInterfaceDecorator1(service: IService) =
    interface IService with
        member this.GetValue() = service.GetValue() + 11
        member this.Dispose() = service.Dispose()

type ServiceInterfaceDecorator2(service: IService) =
    interface IService with
        member this.GetValue() = service.GetValue() + 12
        member this.Dispose() = service.Dispose()

type ServiceWithValue(value: int) =
    member _.Value = value
    interface IService with
        member this.GetValue() = this.Value
        member this.Dispose() = raise (ObjectDisposedException("This is intentional"))

module Basics =

    [<Fact>]
    let ``Check how services work`` () =
        let serviceProvider =
            ServiceCollection()
                .AddSingleton<IService, Service1>()
                .BuildServiceProvider()
        let service = serviceProvider.GetService<Service1>()
        Assert.True(Object.ReferenceEquals(null, service))

    [<Fact>]
    let ``Check how placement in di works`` () =
        let serviceProvider =
            ServiceCollection()
                .AddSingleton<IService, ServiceWithValue>()
                .AddSingleton<IService, ServiceWithValue>(fun _ -> new ServiceWithValue(1))
                .AddSingleton<IService>(new ServiceWithValue(2))
                .BuildServiceProvider()
        let service = serviceProvider.GetRequiredService<IService>()
        Assert.Equal(2, service.GetValue())

module HappyCases =

    [<Fact>]
    let ``Test that basic interface decoration works via types`` () =
        let serviceProvider =
            ServiceCollection()
                .AddSingleton<IService, Service1>()
                .Decorate<IService, ServiceInterfaceDecorator1>()
                .BuildServiceProvider()

        let service = serviceProvider.GetRequiredService<IService>()
        Assert.Equal(typeof<ServiceInterfaceDecorator1>, service.GetType())
        Assert.Equal(11, service.GetValue())

    [<Fact>]
    let ``Test that basic interface decoration works via factory`` () =
        let serviceProvider =
            ServiceCollection()
                .AddSingleton<IService, Service1>(fun _ -> new Service1())
                .Decorate<IService, ServiceInterfaceDecorator1>()
                .BuildServiceProvider()

        let service = serviceProvider.GetRequiredService<IService>()
        Assert.Equal(typeof<ServiceInterfaceDecorator1>, service.GetType())
        Assert.Equal(11, service.GetValue())

    [<Fact>]
    let ``Test that basic interface decoration works via singleton`` () =
        let serviceProvider =
            ServiceCollection()
                .AddSingleton<IService>(new Service1())
                .Decorate<IService, ServiceInterfaceDecorator1>()
                .BuildServiceProvider()

        let service = serviceProvider.GetRequiredService<IService>()
        Assert.Equal(typeof<ServiceInterfaceDecorator1>, service.GetType())
        Assert.Equal(11, service.GetValue())

    [<Fact>]
    let ``Test that multiple interface decoration works`` () =
        let serviceProvider =
            ServiceCollection()
                .AddSingleton<IService, Service1>()
                .Decorate<IService, ServiceInterfaceDecorator1>()
                .Decorate<IService, ServiceInterfaceDecorator2>()
                .BuildServiceProvider()

        let service = serviceProvider.GetRequiredService<IService>()
        Assert.Equal(typeof<ServiceInterfaceDecorator2>, service.GetType())
        Assert.Equal(23, service.GetValue())

    [<Fact>]
    let ``Test that basic inheritance based decoration works`` () =
        let serviceProvider =
            ServiceCollection()
                .AddSingleton<Service, Service1>()
                .Decorate<Service, ServiceBaseDecorator1>()
                .BuildServiceProvider()
        let service = serviceProvider.GetRequiredService<Service>()
        Assert.Equal(typeof<ServiceBaseDecorator1>, service.GetType())
        Assert.Equal(1, service.GetValue())

    [<Fact>]
    let ``Test that multiple inheritance based decoration works`` () =
        let serviceProvider =
            ServiceCollection()
                .AddSingleton<Service, Service1>()
                .Decorate<Service, ServiceBaseDecorator1>()
                .Decorate<Service, ServiceBaseDecorator2>()
                .BuildServiceProvider()
        let service = serviceProvider.GetRequiredService<Service>()
        Assert.Equal(typeof<ServiceBaseDecorator2>, service.GetType())
        Assert.Equal(3, service.GetValue())

module Disposing =

    [<Fact>]
    let ``Test that registered as types objects are properly disposed`` () =
        let serviceProvider =
            ServiceCollection()
                .AddScoped<IService, Service1>()
                .Decorate<IService, ServiceInterfaceDecorator1>()
                .Decorate<IService, ServiceInterfaceDecorator2>()
                .BuildServiceProvider()

        Assert.Throws<CustomObjectDisposedException>(fun _ ->
            use scope = serviceProvider.CreateScope()
            let service = scope.ServiceProvider.GetRequiredService<IService>()
            ignore service
        ) |> ignore

    [<Fact>]
    let ``Test that registered as factory objects are properly disposed`` () =
        let serviceProvider =
            ServiceCollection()
                .AddScoped<IService, Service1>(fun _ -> new Service1())
                .Decorate<IService, ServiceInterfaceDecorator1>()
                .Decorate<IService, ServiceInterfaceDecorator2>()
                .BuildServiceProvider()

        Assert.Throws<CustomObjectDisposedException>(fun _ ->
            use scope = serviceProvider.CreateScope()
            let service = scope.ServiceProvider.GetRequiredService<IService>()
            ignore service
        ) |> ignore

module UnhappyCases =

    [<Fact>]
    let ``Test that decoration wont substitute existing implementation registartion`` () =
        let serviceProvider =
            ServiceCollection()
                .AddSingleton<ServiceWithValue>(new ServiceWithValue(1))
                .AddSingleton<IService, ServiceWithValue>() // this one won't get substituted
                .Decorate<IService, ServiceInterfaceDecorator1>()
                .BuildServiceProvider()
        let service = serviceProvider.GetRequiredService<IService>()
        Assert.Equal(typeof<ServiceInterfaceDecorator1>, service.GetType())
        // It's 12, not 11 as we can't substitute a value
        // It's not a problem as this kind of registration is uber fishy, Factory is better suited for this kinds of things
        Assert.Equal(12, service.GetValue())

    [<Fact>]
    let ``Test that decoration won't work if decorated service is not registered`` () =
        Assert.Throws<InvalidOperationException>(fun _ ->
            ServiceCollection()
                .AddSingleton<ServiceWithValue>(new ServiceWithValue(1))
                .Decorate<IService, ServiceInterfaceDecorator1>()
                .BuildServiceProvider()
            |> ignore) |> ignore