using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using static System.String;

namespace En3Tho.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddHttpClient<TClient>(this IServiceCollection collection, Uri uri)
        where TClient : class
    {
        collection.AddHttpClient<TClient>(client => client.BaseAddress = uri);
        return collection;
    }

    public static IServiceCollection AddSingletonAsOpenGeneric<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.AddSingleton(typeof(TService).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection AddSingletonAsOpenGeneric<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.AddSingleton(typeof(TService).GetGenericTypeDefinition(), typeof(TImpl).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection AddScopedAsOpenGeneric<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.AddScoped(typeof(TService).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection AddScopedAsOpenGeneric<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.AddScoped(typeof(TService).GetGenericTypeDefinition(), typeof(TImpl).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection AddTransientAsOpenGeneric<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.AddTransient(typeof(TService).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection AddTransientAsOpenGeneric<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.AddTransient(typeof(TService).GetGenericTypeDefinition(), typeof(TImpl).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsOpenGeneric<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.TryAddSingleton(typeof(TService).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsOpenGeneric<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.TryAddSingleton(typeof(TService).GetGenericTypeDefinition(), typeof(TImpl).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsOpenGeneric<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.TryAddScoped(typeof(TService).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsOpenGeneric<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.TryAddScoped(typeof(TService).GetGenericTypeDefinition(), typeof(TImpl).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection TryAddTransientAsOpenGeneric<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.TryAddTransient(typeof(TService).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection TryAddTransientAsOpenGeneric<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.TryAddTransient(typeof(TService).GetGenericTypeDefinition(), typeof(TImpl).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection TryAddSingletonOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        var descriptors = collection.Where(d => d.ServiceType == typeof(TService)).ToArray();
        if (descriptors is { Length: > 0 })
        {
            var message = $"Service type {typeof(TService).FullName} is already implemented by {Join(", ", descriptors.Select(d => (d.ImplementationType ?? typeof(TService)).FullName))}";
            throw new InvalidOperationException(message);
        }

        return collection.AddSingleton<TService>();
    }

    public static IServiceCollection TryAddSingletonOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        var descriptors = collection.Where(d => d.ServiceType == typeof(TService)).ToArray();
        if (descriptors is { Length: > 0 })
        {
            var message = $"Service type {typeof(TService).FullName} is already implemented by {Join(", ", descriptors.Select(d => (d.ImplementationType ?? typeof(TService)).FullName))}";
            throw new InvalidOperationException(message);
        }

        return collection.AddSingleton<TService, TImpl>();
    }

    public static IServiceCollection TryAddScopedOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        var descriptors = collection.Where(d => d.ServiceType == typeof(TService)).ToArray();
        if (descriptors is { Length: > 0 })
        {
            var message = $"Service type {typeof(TService).FullName} is already implemented by {Join(", ", descriptors.Select(d => (d.ImplementationType ?? typeof(TService)).FullName))}";
            throw new InvalidOperationException(message);
        }

        return collection.AddScoped<TService>();
    }

    public static IServiceCollection TryAddScopedOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        var descriptors = collection.Where(d => d.ServiceType == typeof(TService)).ToArray();
        if (descriptors is { Length: > 0 })
        {
            var message = $"Service type {typeof(TService).FullName} is already implemented by {Join(", ", descriptors.Select(d => (d.ImplementationType ?? typeof(TService)).FullName))}";
            throw new InvalidOperationException(message);
        }

        return collection.AddScoped<TService, TImpl>();
    }

    public static IServiceCollection TryAddTransientOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        var descriptors = collection.Where(d => d.ServiceType == typeof(TService)).ToArray();
        if (descriptors is { Length: > 0 })
        {
            var message = $"Service type {typeof(TService).FullName} is already implemented by {Join(", ", descriptors.Select(d => (d.ImplementationType ?? typeof(TService)).FullName))}";
            throw new InvalidOperationException(message);
        }

        return collection.AddTransient<TService>();
    }

    public static IServiceCollection TryAddTransientOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        var descriptors = collection.Where(d => d.ServiceType == typeof(TService)).ToArray();
        if (descriptors is { Length: > 0 })
        {
            var message = $"Service type {typeof(TService).FullName} is already implemented by {Join(", ", descriptors.Select(d => (d.ImplementationType ?? typeof(TService)).FullName))}";
            throw new InvalidOperationException(message);
        }

        return collection.AddTransient<TService, TImpl>();
    }


}