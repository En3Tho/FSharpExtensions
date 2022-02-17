using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    internal record struct ServiceDescriptorEnvelope(ServiceDescriptor Descriptor, int Index);

    internal static bool TryFindServiceDescriptor(this IServiceCollection collection, Type type,
        out ServiceDescriptorEnvelope serviceDescriptor)
    {
        for (var i = 0; i < collection.Count; i++)
        {
            var descriptor = collection[i];
            if (descriptor.ServiceType == type)
            {
                serviceDescriptor = new ServiceDescriptorEnvelope(descriptor, i);
                return true;
            }
        }

        serviceDescriptor = default;
        return false;
    }

    internal static bool TryFindServiceDescriptor<TService>(this IServiceCollection collection,
        out ServiceDescriptorEnvelope serviceDescriptor) =>
        collection.TryFindServiceDescriptor(typeof(TService), out serviceDescriptor);

    internal static void EnsureNotImplementedNoMoreThan(this IServiceCollection collection, Type serviceType, int times)
    {
        var descriptors = collection.Where(d => d.ServiceType == serviceType).ToArray();
        if (descriptors.Length > times)
        {
            var message =
                $"Service type {serviceType.FullName} is already implemented by {string.Join(", ", descriptors.Select(d => (d.ImplementationType ?? serviceType).FullName))}";
            throw new InvalidOperationException(message);
        }
    }

    internal static void EnsureNotImplemented(this IServiceCollection collection, Type serviceType) =>
        collection.EnsureNotImplementedNoMoreThan(serviceType, 0);


    internal static void EnsureImplementedOnceAtMaxAndRemoveIfImplemented(this IServiceCollection collection,
        Type serviceType)
    {
        var descriptors = collection.Where(d => d.ServiceType == serviceType).ToArray();
        if (descriptors is { Length: > 1 })
        {
            var message =
                $"Service type {serviceType.FullName} is already implemented by {string.Join(", ", descriptors.Select(d => (d.ImplementationType ?? serviceType).FullName))}";
            throw new InvalidOperationException(message);
        }

        if (descriptors.Length == 1)
            collection.Remove(descriptors[0]);
    }
}