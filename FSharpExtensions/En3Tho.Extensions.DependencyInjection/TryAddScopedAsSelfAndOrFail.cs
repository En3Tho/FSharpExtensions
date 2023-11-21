// auto-generated
using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TImplementation : class, TService1
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TImplementation : class, TService1, TService2
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TImplementation : class, TService1, TService2, TService3
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TImplementation : class, TService1, TService2, TService3, TService4
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TService8 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TService8 : class
        where TService9 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TService8 : class
        where TService9 : class
        where TService10 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TService8 : class
        where TService9 : class
        where TService10 : class
        where TService11 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TService8 : class
        where TService9 : class
        where TService10 : class
        where TService11 : class
        where TService12 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TService8 : class
        where TService9 : class
        where TService10 : class
        where TService11 : class
        where TService12 : class
        where TService13 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TService8 : class
        where TService9 : class
        where TService10 : class
        where TService11 : class
        where TService12 : class
        where TService13 : class
        where TService14 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TService8 : class
        where TService9 : class
        where TService10 : class
        where TService11 : class
        where TService12 : class
        where TService13 : class
        where TService14 : class
        where TService15 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddScopedAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TService16, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TService8 : class
        where TService9 : class
        where TService10 : class
        where TService11 : class
        where TService12 : class
        where TService13 : class
        where TService14 : class
        where TService15 : class
        where TService16 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TService16
    {
        collection.TryAddScopedOrFail<TImplementation>();
        collection.TryAddScopedOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddScopedOrFail<TService16>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }
}