using System;
using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    private static bool TryChangeLifetimeInternal<TService>(this IServiceCollection collection, ServiceLifetime newLifetime)
    {
        var changed = false;

        for (var i = 0; i < collection.Count; i++)
        {
            var descriptor = collection[i];
            if (descriptor.ServiceType.Equals(typeof(TService))
                && descriptor.TryChangeLifetime(newLifetime, out var newDescriptor))
            {
                collection[i] = newDescriptor;
                changed = true;
            }
        }

        return changed;
    }

    public static IServiceCollection TryChangeLifetime<TService>(this IServiceCollection collection,
        ServiceLifetime newLifetime)
    {
        collection.TryChangeLifetimeInternal<TService>(newLifetime);
        return collection;
    }

    public static IServiceCollection TryChangeLifetimeOrFail<TService>(this IServiceCollection collection,
        ServiceLifetime newLifetime)
    {
        if (collection.TryChangeLifetimeInternal<TService>(newLifetime))
            return collection;

        throw new InvalidOperationException("Service type is either not found in collection or is registered as single object instance");
    }
}