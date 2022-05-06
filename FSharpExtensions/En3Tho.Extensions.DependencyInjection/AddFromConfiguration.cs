using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
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