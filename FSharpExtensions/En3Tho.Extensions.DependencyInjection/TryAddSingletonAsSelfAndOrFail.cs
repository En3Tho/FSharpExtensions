// auto-generated
using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TImplementation : class, TService1
    {
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TImplementation : class, TService1
    {
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TImplementation : class, TService1
    {
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TImplementation : class, TService1, TService2
    {
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TImplementation : class, TService1, TService2
    {
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TImplementation : class, TService1, TService2
    {
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TImplementation : class, TService1, TService2, TService3
    {
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TImplementation : class, TService1, TService2, TService3
    {
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TImplementation : class, TService1, TService2, TService3
    {
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TImplementation : class, TService1, TService2, TService3, TService4
    {
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TImplementation : class, TService1, TService2, TService3, TService4
    {
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TImplementation : class, TService1, TService2, TService3, TService4
    {
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5
    {
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5
    {
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5
    {
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6
    {
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6
    {
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6
    {
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7
    {
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7
    {
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7
    {
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TImplementation>(this IServiceCollection collection)
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
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TImplementation>(this IServiceCollection collection)
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
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TImplementation>(this IServiceCollection collection)
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
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TImplementation>(this IServiceCollection collection)
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
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TImplementation>(this IServiceCollection collection)
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
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TImplementation>(this IServiceCollection collection)
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
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TImplementation>(this IServiceCollection collection)
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
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TImplementation>(this IServiceCollection collection)
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
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TService16, TImplementation>(this IServiceCollection collection)
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
        collection.TryAddSingletonOrFail<TImplementation>();
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService16>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TService16, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.TryAddSingletonOrFail(implementationInstance);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService16>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection TryAddSingletonAsSelfAndOrFail<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TService16, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.TryAddSingletonOrFail(implementationFactory);
        collection.TryAddSingletonOrFail<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.TryAddSingletonOrFail<TService16>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }
}