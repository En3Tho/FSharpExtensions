using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddSingletonAsOpenGeneric<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.AddSingleton(typeof(TService).GetGenericTypeDefinition());
        return collection;
    }

    public static IServiceCollection AddSingletonAsOpenGeneric<TService>(this IServiceCollection collection, Func<IServiceProvider, object> implementationFactory)
        where TService : class
    {
        collection.AddSingleton(typeof(TService).GetGenericTypeDefinition(), implementationFactory);
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

    public static IServiceCollection AddScopedAsOpenGeneric<TService>(this IServiceCollection collection, Func<IServiceProvider, object> implementationFactory)
        where TService : class
    {
        collection.AddScoped(typeof(TService).GetGenericTypeDefinition(), implementationFactory);
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

    public static IServiceCollection AddTransientAsOpenGeneric<TService>(this IServiceCollection collection, Func<IServiceProvider, object> implementationFactory)
        where TService : class
    {
        collection.AddTransient(typeof(TService).GetGenericTypeDefinition(), implementationFactory);
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

    public static IServiceCollection TryAddSingletonAsOpenGenericOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        var serviceType = typeof(TService).GetGenericTypeDefinition();
        collection.EnsureNotImplemented(serviceType);
        return collection.AddSingleton(serviceType);
    }

    public static IServiceCollection TryAddSingletonAsOpenGenericOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        var serviceType = typeof(TService).GetGenericTypeDefinition();
        collection.EnsureNotImplemented(serviceType);
        return collection.AddSingleton(serviceType, typeof(TImpl).GetGenericTypeDefinition());
    }

    public static IServiceCollection TryAddScopedAsOpenGenericOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        var serviceType = typeof(TService).GetGenericTypeDefinition();
        collection.EnsureNotImplemented(serviceType);
        return collection.AddScoped(serviceType);
    }

    public static IServiceCollection TryAddScopedAsOpenGenericOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        var serviceType = typeof(TService).GetGenericTypeDefinition();
        collection.EnsureNotImplemented(serviceType);
        return collection.AddScoped(serviceType, typeof(TImpl).GetGenericTypeDefinition());
    }

    public static IServiceCollection TryAddTransientAsOpenGenericOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        var serviceType = typeof(TService).GetGenericTypeDefinition();
        collection.EnsureNotImplemented(serviceType);
        return collection.AddTransient(serviceType);
    }

    public static IServiceCollection TryAddTransientAsOpenGenericOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        var serviceType = typeof(TService).GetGenericTypeDefinition();
        collection.EnsureNotImplemented(serviceType);
        return collection.AddTransient(serviceType, typeof(TImpl).GetGenericTypeDefinition());
    }
}