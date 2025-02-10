using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection TryAddOrReplaceSingletonOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddSingleton<TService>();
    }

    public static IServiceCollection TryAddOrReplaceSingletonOrFail<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddSingleton(implementationFactory);
    }

    public static IServiceCollection TryAddOrReplaceSingletonOrFail<TService, TImpl>(this IServiceCollection collection, Func<IServiceProvider, TImpl> implementationFactory)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddSingleton<TService, TImpl>(implementationFactory);
    }

    public static IServiceCollection TryAddOrReplaceSingletonOrFail<TService>(this IServiceCollection collection, TService implementationValue)
        where TService : class
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddSingleton(implementationValue);
    }

    public static IServiceCollection TryAddOrReplaceSingletonOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddSingleton<TService, TImpl>();
    }

    public static IServiceCollection TryAddOrReplaceScopedOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddScoped<TService>();
    }

    public static IServiceCollection TryAddOrReplaceScopedOrFail<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddScoped(implementationFactory);
    }

    public static IServiceCollection TryAddOrReplaceScopedOrFail<TService, TImpl>(this IServiceCollection collection, Func<IServiceProvider, TImpl> implementationFactory)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddScoped<TService, TImpl>(implementationFactory);
    }

    public static IServiceCollection TryAddOrReplaceScopedOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddScoped<TService, TImpl>();
    }

    public static IServiceCollection TryAddOrReplaceTransientOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddTransient<TService>();
    }

    public static IServiceCollection TryAddOrReplaceTransientOrFail<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddTransient(implementationFactory);
    }

    public static IServiceCollection TryAddOrReplaceTransientOrFail<TService, TImpl>(this IServiceCollection collection, Func<IServiceProvider, TImpl> implementationFactory)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddTransient<TService, TImpl>(implementationFactory);
    }

    public static IServiceCollection TryAddOrReplaceTransientOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureImplementedOnceAtMaxAndRemoveIfImplemented(typeof(TService));
        return collection.AddTransient<TService, TImpl>();
    }
}