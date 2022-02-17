using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
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

    // AddOrReplace

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

    // TryAddOrFail

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

    public static IServiceCollection TryAddTransientOrFail<TService, TImpl>(this IServiceCollection collection)
        where TService : class
        where TImpl : class, TService
    {
        collection.EnsureNotImplemented(typeof(TService));
        return collection.AddTransient<TService, TImpl>();
    }

    // TryAddOrReplaceOrFail

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

    // try add from configuration

    public static IServiceCollection TryAddSingletonFromConfiguration<TService>(this IServiceCollection collection, string name)
        where TService : class
    {
        collection.TryAddSingleton(services =>
            services.GetRequiredService<IConfiguration>()
                .GetSection(name)
                .Get<TService>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonFromConfiguration<TService>(this IServiceCollection collection)
        where TService : class
    {
        var name = typeof(TService).Name;
        return collection.TryAddSingletonFromConfiguration<TService>(name);
    }

    public static IServiceCollection TryAddSingletonFromConfigurationOrFail<TService>(this IServiceCollection collection, string name)
        where TService : class
    {
        return collection.TryAddSingletonOrFail(services =>
            services.GetRequiredService<IConfiguration>()
                .GetSection(name)
                .Get<TService>());
    }

    public static IServiceCollection TryAddSingletonFromConfigurationOrFail<TService>(this IServiceCollection collection)
        where TService : class
    {
        var name = typeof(TService).Name;
        return collection.TryAddSingletonFromConfigurationOrFail<TService>(name);
    }
}