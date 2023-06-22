using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static class IServiceProviderExtensions
{
    public static void Run<T1>(this IServiceProvider provider, Action<T1> action)
        where T1 : class
    {
        action(
            provider.GetRequiredService<T1>()
        );
    }

    public static void Run<T1, T2>(this IServiceProvider provider, Action<T1, T2> action)
        where T1 : class
        where T2 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>()
        );
    }

    public static void Run<T1, T2, T3>(this IServiceProvider provider, Action<T1, T2, T3> action)
        where T1 : class
        where T2 : class
        where T3 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>()
        );
    }

    public static void Run<T1, T2, T3, T4>(this IServiceProvider provider, Action<T1, T2, T3, T4> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6, T7> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>()
        );
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IServiceProvider provider, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
        where T16 : class
    {
        action(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>(),
            provider.GetRequiredService<T16>()
        );
    }

    public static TOut Run<T1, TOut>(this IServiceProvider provider, Func<T1, TOut> func)
        where T1 : class
    {
        return func(
            provider.GetRequiredService<T1>()
        );
    }

    public static TOut Run<T1, T2, TOut>(this IServiceProvider provider, Func<T1, T2, TOut> func)
        where T1 : class
        where T2 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>()
        );
    }

    public static TOut Run<T1, T2, T3, TOut>(this IServiceProvider provider, Func<T1, T2, T3, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>()
        );
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
        where T16 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>(),
            provider.GetRequiredService<T16>()
        );
    }

    public static Task RunAsync<T1>(this IServiceProvider provider, Func<T1, Task> func)
        where T1 : class
    {
        return func(
            provider.GetRequiredService<T1>()
        );
    }

    public static Task RunAsync<T1, T2>(this IServiceProvider provider, Func<T1, T2, Task> func)
        where T1 : class
        where T2 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>()
        );
    }

    public static Task RunAsync<T1, T2, T3>(this IServiceProvider provider, Func<T1, T2, T3, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4>(this IServiceProvider provider, Func<T1, T2, T3, T4, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>()
        );
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
        where T16 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>(),
            provider.GetRequiredService<T16>()
        );
    }

    public static Task<TOut> RunAsync<T1, TOut>(this IServiceProvider provider, Func<T1, Task<TOut>> func)
        where T1 : class
    {
        return func(
            provider.GetRequiredService<T1>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, TOut>(this IServiceProvider provider, Func<T1, T2, Task<TOut>> func)
        where T1 : class
        where T2 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, TOut>(this IServiceProvider provider, Func<T1, T2, T3, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>()
        );
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
        where T16 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>(),
            provider.GetRequiredService<T16>()
        );
    }

    public static ValueTask RunAsync<T1>(this IServiceProvider provider, Func<T1, ValueTask> func)
        where T1 : class
    {
        return func(
            provider.GetRequiredService<T1>()
        );
    }

    public static ValueTask RunAsync<T1, T2>(this IServiceProvider provider, Func<T1, T2, ValueTask> func)
        where T1 : class
        where T2 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3>(this IServiceProvider provider, Func<T1, T2, T3, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4>(this IServiceProvider provider, Func<T1, T2, T3, T4, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>()
        );
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
        where T16 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>(),
            provider.GetRequiredService<T16>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, TOut>(this IServiceProvider provider, Func<T1, ValueTask<TOut>> func)
        where T1 : class
    {
        return func(
            provider.GetRequiredService<T1>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, TOut>(this IServiceProvider provider, Func<T1, T2, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, TOut>(this IServiceProvider provider, Func<T1, T2, T3, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>()
        );
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut>(this IServiceProvider provider, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask<TOut>> func)
        where T1 : class
        where T2 : class
        where T3 : class
        where T4 : class
        where T5 : class
        where T6 : class
        where T7 : class
        where T8 : class
        where T9 : class
        where T10 : class
        where T11 : class
        where T12 : class
        where T13 : class
        where T14 : class
        where T15 : class
        where T16 : class
    {
        return func(
            provider.GetRequiredService<T1>(),
            provider.GetRequiredService<T2>(),
            provider.GetRequiredService<T3>(),
            provider.GetRequiredService<T4>(),
            provider.GetRequiredService<T5>(),
            provider.GetRequiredService<T6>(),
            provider.GetRequiredService<T7>(),
            provider.GetRequiredService<T8>(),
            provider.GetRequiredService<T9>(),
            provider.GetRequiredService<T10>(),
            provider.GetRequiredService<T11>(),
            provider.GetRequiredService<T12>(),
            provider.GetRequiredService<T13>(),
            provider.GetRequiredService<T14>(),
            provider.GetRequiredService<T15>(),
            provider.GetRequiredService<T16>()
        );
    }
}