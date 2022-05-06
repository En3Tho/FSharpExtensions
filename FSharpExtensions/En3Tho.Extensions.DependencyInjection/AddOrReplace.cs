using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddOrReplaceSingleton<TService>(this IServiceCollection collection)
        where TService : class
    {
        var descriptor = collection.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor is { })
            collection.Remove(descriptor);
        return collection.AddSingleton<TService>();
    }

    public static IServiceCollection AddOrReplaceSingleton<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        var descriptor = collection.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor is { })
            collection.Remove(descriptor);
        return collection.AddSingleton(implementationFactory);
    }

    public static IServiceCollection AddOrReplaceSingleton<TService>(this IServiceCollection collection, TService implementationValue)
        where TService : class
    {
        var descriptor = collection.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor is { })
            collection.Remove(descriptor);
        return collection.AddSingleton(implementationValue);
    }

    public static IServiceCollection AddOrReplaceSingleton<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        var descriptor = collection.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor is { })
            collection.Remove(descriptor);
        return collection.AddSingleton<TService, TImpl>();
    }

    public static IServiceCollection AddOrReplaceScoped<TService>(this IServiceCollection collection)
        where TService : class
    {
        var descriptor = collection.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor is { })
            collection.Remove(descriptor);
        return collection.AddScoped<TService>();
    }

    public static IServiceCollection AddOrReplaceScoped<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        var descriptor = collection.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor is { })
            collection.Remove(descriptor);
        return collection.AddScoped(implementationFactory);
    }

    public static IServiceCollection AddOrReplaceScoped<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        var descriptor = collection.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor is { })
            collection.Remove(descriptor);
        return collection.AddScoped<TService, TImpl>();
    }

    public static IServiceCollection AddOrReplaceTransient<TService>(this IServiceCollection collection)
        where TService : class
    {
        var descriptor = collection.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor is { })
            collection.Remove(descriptor);
        return collection.AddTransient<TService>();
    }

    public static IServiceCollection AddOrReplaceTransient<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        var descriptor = collection.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor is { })
            collection.Remove(descriptor);
        return collection.AddTransient(implementationFactory);
    }

    public static IServiceCollection AddOrReplaceTransient<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        var descriptor = collection.FirstOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor is { })
            collection.Remove(descriptor);
        return collection.AddTransient<TService, TImpl>();
    }
}