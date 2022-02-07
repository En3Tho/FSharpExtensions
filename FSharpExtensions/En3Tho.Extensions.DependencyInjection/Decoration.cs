using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    /// <summary>
    /// Used to decorate existing TService.
    /// </summary>
    /// <param name="collection"></param>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TImpl"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IServiceCollection Decorate<TService, TImpl>(this IServiceCollection collection) where TImpl : TService
    {
        // first, find the most latest service registered
        ServiceDescriptorEnvelope? possibleDescriptor = default;

        for (var i = 0; i < collection.Count; i++)
        {
            var descriptor = collection[i];
            if (descriptor.ServiceType == typeof(TService))
                possibleDescriptor = new ServiceDescriptorEnvelope(descriptor, i);
        }

        if (!possibleDescriptor.HasValue)
            throw new InvalidOperationException(
                $"Service {typeof(TService).FullName} was not found among registered services");

        var (oldDescriptor, oldDescriptorIndex) = possibleDescriptor.Value;

        var oldImplementationType = oldDescriptor.ImplementationType ?? oldDescriptor.ServiceType;

        // Sanity check
        if (oldImplementationType == typeof(TImpl))
            throw new InvalidOperationException($"Cannot decorate {typeof(TImpl)}  with itself");

        var implementationFactory = ActivatorUtilities.CreateFactory(typeof(TImpl), new[] { oldImplementationType });

        ServiceDescriptor newDescriptor;
        var newDescriptorIndex = oldDescriptorIndex;

        switch (oldDescriptor)
        {
            case { ImplementationFactory: { } factory }:
                var factoryFromFactory = (IServiceProvider serviceProvider) =>
                {
                    var oldImplementationInstance = factory(serviceProvider);
                    return implementationFactory(serviceProvider, new[] { oldImplementationInstance });
                };

                newDescriptor = new(typeof(TService), factoryFromFactory, oldDescriptor.Lifetime);
                break;

            case { ImplementationInstance: { } instance }:
                // old type was registered as instance so just capture it
                var factoryFromInstance = (IServiceProvider serviceProvider) =>
                {
                    return implementationFactory(serviceProvider, new[] { instance });
                };
                newDescriptor = new(typeof(TService), factoryFromInstance, oldDescriptor.Lifetime);
                break;

            default:
                // we don't need to change it if service was already added, so check again
                var found = false;
                for (var j = 0; j < collection.Count && !found; j++)
                {
                    found = collection[j].ServiceType == oldImplementationType;
                }

                // if old implementation was not registered directly, then insert it before where it was.
                if (!found)
                {
                    var oldDescriptorSubstitute = new ServiceDescriptor(oldImplementationType, oldImplementationType, oldDescriptor.Lifetime);
                    var indexBeforeOldOne = Math.Max(0, oldDescriptorIndex - 1);
                    collection.Insert(indexBeforeOldOne, oldDescriptorSubstitute);
                    // increment new descriptor index because old one was just inserted
                    newDescriptorIndex++;
                }

                var factoryFromServices = (IServiceProvider serviceProvider) =>
                {
                    var oldImplementationInstance = serviceProvider.GetRequiredService(oldImplementationType);
                    return implementationFactory(serviceProvider, new[] { oldImplementationInstance });
                };

                newDescriptor = new(typeof(TService), factoryFromServices, oldDescriptor.Lifetime);
                break;
        }

        collection[newDescriptorIndex] = newDescriptor;

        return collection;
    }
}