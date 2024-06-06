module En3Tho.Extensions.DependencyInjection.Tests.IServiceCollectionFunkyCE

open System
open Microsoft.Extensions.DependencyInjection
open En3Tho.FSharp.ComputationExpressions
open En3Tho.FSharp.ComputationExpressions.SCollectionBuilder

// define more types like these for F#:
// Singleton, Scoped, Transient, changeLifetime, SingletonFunc, AddOrFail, Decorate, Update etc

// type IServiceCollectionYieldable =
//     abstract member Apply: serviceCollection: IServiceCollection -> unit
//
// type [<Struct>] Singleton<'TService when 'TService: not struct> =
//     interface IServiceCollectionYieldable with
//         member _.Apply(serviceCollection) = serviceCollection.AddSingleton<'TService>() |> ignore
//
// type [<Struct>] SingletonFactory<'TService>(factory: Func<'TService>) =
//     member _.Factory = factory
//
// type [<Struct>] SingletonValue<'TService>(value: 'TService) =
//     member _.Value = value
//
// // TODO: define in C#
// // type [<Struct>] Singleton<'TService, 'TImplementation when 'TImplementation :> 'TService> = struct end
// // type [<Struct>] SingletonFactory<'TService, 'TImplementation when 'TImplementation :> 'TService>(factory: Func<'TImplementation>) = struct end
//
// type [<AbstractClass; Sealed; AutoOpen>] IServiceCollectionShortcuts() =
//     static member inline singleton<'TService when 'TService: not struct>() =
//         Singleton<'TService>()
//     static member inline singleton<'TService>(factory: Func<'TService>) = SingletonFactory<'TService>(factory)
//     static member inline singleton<'TService>(value: 'TService) = SingletonValue<'TService>(value)
//
// type IServiceCollection with
//     member inline this.Yield(value: #IServiceCollectionYieldable) = fun() -> value.Apply this
//     member inline this.Zero() : CollectionCode = fun() -> ()
//     member inline this.Run ([<InlineIfLambda>] runExpr) =
//         runExpr()
//         this
//
// type Add =
//     | Add = 0
//     | OrIgnore = 1
//     | OrReplace = 2
//     | OrFail = 3
//
// let test() =
//     let col = ServiceCollection() :> IServiceCollection
//
//     // col.AddSingleton<IDisposable, ResizeArray<int>>()
//
//     col {
//         //scoped<>()
//         singleton<ServiceCollection>(Add.OrReplace)
//         singleton<ServiceCollection>(Add.OrFail)
//         decorate<IDisposable, ...>()
//
//         //transient<>()
//     }