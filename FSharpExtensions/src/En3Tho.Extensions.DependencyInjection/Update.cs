using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection Update<TService>(this IServiceCollection collection, Func<TService, TService> configureFactory)
        where TService : class
    {
        if (!collection.TryFindServiceDescriptor<TService>(out var possibleDescriptor))
            throw new InvalidOperationException(
                $"Service {typeof(TService).FullName} was not found among registered services");

        var (oldDescriptor, oldDescriptorIndex) = possibleDescriptor;

        ServiceDescriptor newDescriptor;

        switch (oldDescriptor)
        {
            // case of Add<TService, TImpl>(x => instance)
            case { ImplementationFactory: { } factory }:
                var factoryFromFactory = (IServiceProvider serviceProvider) =>
                {
                    var oldImplementationInstance = factory(serviceProvider);
                    return configureFactory((TService)oldImplementationInstance);
                };

                newDescriptor = new(typeof(TService), factoryFromFactory, oldDescriptor.Lifetime);
                break;

            // case of Add<TService, TImpl>(instance)
            case { ImplementationInstance: { } instance }:
                var factoryFromInstance = (IServiceProvider serviceProvider) => configureFactory((TService)instance);
                newDescriptor = new(typeof(TService), factoryFromInstance, oldDescriptor.Lifetime);
                break;

            // case of Add<TService, TImpl>()
            default:
                var implementationFactory = ActivatorUtilities.CreateFactory(typeof(TService), Array.Empty<Type>());
                var factoryFromServices = (IServiceProvider serviceProvider) =>
                {
                    var oldImplementationInstance = implementationFactory(serviceProvider, Array.Empty<object>());
                    return configureFactory((TService)oldImplementationInstance);
                };

                newDescriptor = new(typeof(TService), factoryFromServices, oldDescriptor.Lifetime);
                break;
        }

        collection[oldDescriptorIndex] = newDescriptor;

        return collection;
    }
}