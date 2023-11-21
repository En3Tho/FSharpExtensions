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

    public static IServiceCollection TryAddSingletonOrFail<TService, TImplementation>(this IServiceCollection collection, Func<IServiceProvider, TImplementation> implementationFactory)
        where TService : class
        where TImplementation : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddSingleton<TService, TImplementation>(implementationFactory);
    }

    public static IServiceCollection TryAddSingletonOrFail<TService>(this IServiceCollection collection, TService implementationInstance)
        where TService : class
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddSingleton(implementationInstance);
    }

    public static IServiceCollection TryAddSingletonOrFail<TService, TImplementation>(this IServiceCollection collection)
        where TService : class
        where TImplementation : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddSingleton<TService, TImplementation>();
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

    public static IServiceCollection TryAddScopedOrFail<TService, TImplementation>(this IServiceCollection collection, Func<IServiceProvider, TImplementation> implementationFactory)
        where TService : class
        where TImplementation : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddScoped<TService, TImplementation>(implementationFactory);
    }

    public static IServiceCollection TryAddScopedOrFail<TService, TImplementation>(this IServiceCollection collection)
        where TService : class
        where TImplementation : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddScoped<TService, TImplementation>();
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

    public static IServiceCollection TryAddTransientOrFail<TService, TImplementation>(this IServiceCollection collection, Func<IServiceProvider, TImplementation> implementationFactory)
        where TService : class
        where TImplementation : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddTransient<TService, TImplementation>(implementationFactory);
    }

    public static IServiceCollection TryAddTransientOrFail<TService, TImplementation>(this IServiceCollection collection)
        where TService : class
        where TImplementation : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddTransient<TService, TImplementation>();
    }
}