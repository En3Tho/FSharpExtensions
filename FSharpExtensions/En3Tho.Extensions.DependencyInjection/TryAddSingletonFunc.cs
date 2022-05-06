using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1>(this IServiceCollection collection,
        Func<TDependency1, TService> factory)
        where TService : class
        where TDependency1 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2>(
        this IServiceCollection collection,
        Func<TDependency1, TDependency2, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3>(
        this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4>(this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5>(this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5, TDependency6>(this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
        where TDependency6 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>(),
                serviceProvider.GetRequiredService<TDependency6>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5, TDependency6, TDependency7>(this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7, TService>
            factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
        where TDependency6 : notnull
        where TDependency7 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>(),
                serviceProvider.GetRequiredService<TDependency6>(),
                serviceProvider.GetRequiredService<TDependency7>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5, TDependency6, TDependency7, TDependency8>(this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7,
            TDependency8, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
        where TDependency6 : notnull
        where TDependency7 : notnull
        where TDependency8 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>(),
                serviceProvider.GetRequiredService<TDependency6>(),
                serviceProvider.GetRequiredService<TDependency7>(),
                serviceProvider.GetRequiredService<TDependency8>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9>(
        this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7,
            TDependency8, TDependency9, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
        where TDependency6 : notnull
        where TDependency7 : notnull
        where TDependency8 : notnull
        where TDependency9 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>(),
                serviceProvider.GetRequiredService<TDependency6>(),
                serviceProvider.GetRequiredService<TDependency7>(),
                serviceProvider.GetRequiredService<TDependency8>(),
                serviceProvider.GetRequiredService<TDependency9>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10>(
        this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7,
            TDependency8, TDependency9, TDependency10, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
        where TDependency6 : notnull
        where TDependency7 : notnull
        where TDependency8 : notnull
        where TDependency9 : notnull
        where TDependency10 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>(),
                serviceProvider.GetRequiredService<TDependency6>(),
                serviceProvider.GetRequiredService<TDependency7>(),
                serviceProvider.GetRequiredService<TDependency8>(),
                serviceProvider.GetRequiredService<TDependency9>(),
                serviceProvider.GetRequiredService<TDependency10>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10,
        TDependency11>(this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7,
            TDependency8, TDependency9, TDependency10, TDependency11, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
        where TDependency6 : notnull
        where TDependency7 : notnull
        where TDependency8 : notnull
        where TDependency9 : notnull
        where TDependency10 : notnull
        where TDependency11 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>(),
                serviceProvider.GetRequiredService<TDependency6>(),
                serviceProvider.GetRequiredService<TDependency7>(),
                serviceProvider.GetRequiredService<TDependency8>(),
                serviceProvider.GetRequiredService<TDependency9>(),
                serviceProvider.GetRequiredService<TDependency10>(),
                serviceProvider.GetRequiredService<TDependency11>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10,
        TDependency11, TDependency12>(this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7,
            TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
        where TDependency6 : notnull
        where TDependency7 : notnull
        where TDependency8 : notnull
        where TDependency9 : notnull
        where TDependency10 : notnull
        where TDependency11 : notnull
        where TDependency12 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>(),
                serviceProvider.GetRequiredService<TDependency6>(),
                serviceProvider.GetRequiredService<TDependency7>(),
                serviceProvider.GetRequiredService<TDependency8>(),
                serviceProvider.GetRequiredService<TDependency9>(),
                serviceProvider.GetRequiredService<TDependency10>(),
                serviceProvider.GetRequiredService<TDependency11>(),
                serviceProvider.GetRequiredService<TDependency12>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10,
        TDependency11, TDependency12, TDependency13>(this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7,
            TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, TDependency13, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
        where TDependency6 : notnull
        where TDependency7 : notnull
        where TDependency8 : notnull
        where TDependency9 : notnull
        where TDependency10 : notnull
        where TDependency11 : notnull
        where TDependency12 : notnull
        where TDependency13 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>(),
                serviceProvider.GetRequiredService<TDependency6>(),
                serviceProvider.GetRequiredService<TDependency7>(),
                serviceProvider.GetRequiredService<TDependency8>(),
                serviceProvider.GetRequiredService<TDependency9>(),
                serviceProvider.GetRequiredService<TDependency10>(),
                serviceProvider.GetRequiredService<TDependency11>(),
                serviceProvider.GetRequiredService<TDependency12>(),
                serviceProvider.GetRequiredService<TDependency13>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10,
        TDependency11, TDependency12, TDependency13, TDependency14>(this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7,
            TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, TDependency13, TDependency14,
            TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
        where TDependency6 : notnull
        where TDependency7 : notnull
        where TDependency8 : notnull
        where TDependency9 : notnull
        where TDependency10 : notnull
        where TDependency11 : notnull
        where TDependency12 : notnull
        where TDependency13 : notnull
        where TDependency14 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>(),
                serviceProvider.GetRequiredService<TDependency6>(),
                serviceProvider.GetRequiredService<TDependency7>(),
                serviceProvider.GetRequiredService<TDependency8>(),
                serviceProvider.GetRequiredService<TDependency9>(),
                serviceProvider.GetRequiredService<TDependency10>(),
                serviceProvider.GetRequiredService<TDependency11>(),
                serviceProvider.GetRequiredService<TDependency12>(),
                serviceProvider.GetRequiredService<TDependency13>(),
                serviceProvider.GetRequiredService<TDependency14>()));
        return collection;
    }

    public static IServiceCollection TryAddSingletonFunc<TService, TDependency1, TDependency2, TDependency3,
        TDependency4, TDependency5, TDependency6, TDependency7, TDependency8, TDependency9, TDependency10,
        TDependency11, TDependency12, TDependency13, TDependency14, TDependency15>(this IServiceCollection collection,
        Func<TDependency1, TDependency2, TDependency3, TDependency4, TDependency5, TDependency6, TDependency7,
            TDependency8, TDependency9, TDependency10, TDependency11, TDependency12, TDependency13, TDependency14,
            TDependency15, TService> factory)
        where TService : class
        where TDependency1 : notnull
        where TDependency2 : notnull
        where TDependency3 : notnull
        where TDependency4 : notnull
        where TDependency5 : notnull
        where TDependency6 : notnull
        where TDependency7 : notnull
        where TDependency8 : notnull
        where TDependency9 : notnull
        where TDependency10 : notnull
        where TDependency11 : notnull
        where TDependency12 : notnull
        where TDependency13 : notnull
        where TDependency14 : notnull
        where TDependency15 : notnull
    {
        collection.TryAddSingleton(
            serviceProvider => factory(
                serviceProvider.GetRequiredService<TDependency1>(),
                serviceProvider.GetRequiredService<TDependency2>(),
                serviceProvider.GetRequiredService<TDependency3>(),
                serviceProvider.GetRequiredService<TDependency4>(),
                serviceProvider.GetRequiredService<TDependency5>(),
                serviceProvider.GetRequiredService<TDependency6>(),
                serviceProvider.GetRequiredService<TDependency7>(),
                serviceProvider.GetRequiredService<TDependency8>(),
                serviceProvider.GetRequiredService<TDependency9>(),
                serviceProvider.GetRequiredService<TDependency10>(),
                serviceProvider.GetRequiredService<TDependency11>(),
                serviceProvider.GetRequiredService<TDependency12>(),
                serviceProvider.GetRequiredService<TDependency13>(),
                serviceProvider.GetRequiredService<TDependency14>(),
                serviceProvider.GetRequiredService<TDependency15>()));
        return collection;
    }
}