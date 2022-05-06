using System;
using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection TryAddSingletonOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddSingleton<TService>();
    }

    public static IServiceCollection TryAddSingletonOrFail<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddSingleton(implementationFactory);
    }

    public static IServiceCollection TryAddSingletonOrFail<TService, TImpl>(this IServiceCollection collection, Func<IServiceProvider, TImpl> implementationFactory)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddSingleton<TService, TImpl>(implementationFactory);
    }

    public static IServiceCollection TryAddSingletonOrFail<TService>(this IServiceCollection collection, TService implementationInstance)
        where TService : class
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddSingleton(implementationInstance);
    }

    public static IServiceCollection TryAddSingletonOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddSingleton<TService, TImpl>();
    }

    public static IServiceCollection TryAddScopedOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddScoped<TService>();
    }

    public static IServiceCollection TryAddScopedOrFail<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddScoped(implementationFactory);
    }

    public static IServiceCollection TryAddScopedOrFail<TService, TImpl>(this IServiceCollection collection, Func<IServiceProvider, TImpl> implementationFactory)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddScoped<TService, TImpl>(implementationFactory);
    }

    public static IServiceCollection TryAddScopedOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddScoped<TService, TImpl>();
    }

    public static IServiceCollection TryAddTransientOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddTransient<TService>();
    }

    public static IServiceCollection TryAddTransientOrFail<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddTransient(implementationFactory);
    }

    public static IServiceCollection TryAddTransientOrFail<TService, TImpl>(this IServiceCollection collection, Func<IServiceProvider, TImpl> implementationFactory)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddTransient<TService, TImpl>(implementationFactory);
    }

    public static IServiceCollection TryAddTransientOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddTransient<TService, TImpl>();
    }
}