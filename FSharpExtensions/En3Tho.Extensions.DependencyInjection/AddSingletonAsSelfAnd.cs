// auto-generated
using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TImplementation : class, TService1
    {
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TImplementation : class, TService1
    {
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TImplementation : class, TService1
    {
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TImplementation : class, TService1, TService2
    {
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TImplementation : class, TService1, TService2
    {
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TImplementation : class, TService1, TService2
    {
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TImplementation : class, TService1, TService2, TService3
    {
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TImplementation : class, TService1, TService2, TService3
    {
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TImplementation : class, TService1, TService2, TService3
    {
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TImplementation : class, TService1, TService2, TService3, TService4
    {
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TImplementation : class, TService1, TService2, TService3, TService4
    {
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TImplementation : class, TService1, TService2, TService3, TService4
    {
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5
    {
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5
    {
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5
    {
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6
    {
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6
    {
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6
    {
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TImplementation>(this IServiceCollection collection)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7
    {
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7
    {
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
        where TService1 : class
        where TService2 : class
        where TService3 : class
        where TService4 : class
        where TService5 : class
        where TService6 : class
        where TService7 : class
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6, TService7
    {
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TImplementation>(this IServiceCollection collection)
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
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TImplementation>(this IServiceCollection collection)
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
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TImplementation>(this IServiceCollection collection)
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
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TImplementation>(this IServiceCollection collection)
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
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TImplementation>(this IServiceCollection collection)
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
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TImplementation>(this IServiceCollection collection)
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
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TImplementation>(this IServiceCollection collection)
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
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TImplementation>(this IServiceCollection collection)
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
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TService16, TImplementation>(this IServiceCollection collection)
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
        collection.AddSingleton<TImplementation>();
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService16>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TService16, TImplementation>(this IServiceCollection collection, TImplementation implementationInstance)
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
        collection.AddSingleton(implementationInstance);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService16>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }

    public static IServiceCollection AddSingletonAsSelfAnd<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8, TService9, TService10, TService11, TService12, TService13, TService14, TService15, TService16, TImplementation>(this IServiceCollection collection, Func<TImplementation> implementationFactory)
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
        collection.AddSingleton(implementationFactory);
        collection.AddSingleton<TService1>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService2>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService3>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService4>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService5>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService6>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService7>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService8>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService9>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService10>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService11>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService12>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService13>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService14>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService15>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        collection.AddSingleton<TService16>(serviceProvider => serviceProvider.GetRequiredService<TImplementation>());
        return collection;
    }
}