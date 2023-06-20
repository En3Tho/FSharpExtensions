using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceScopeExtensions
{
    public static void Run<T1>(this IServiceScope scope, Action<T1> action)
        where T1 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2>(this IServiceScope scope, Action<T1, T2> action)
        where T1 : notnull
        where T2 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3>(this IServiceScope scope, Action<T1, T2, T3> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4>(this IServiceScope scope, Action<T1, T2, T3, T4> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5>(this IServiceScope scope, Action<T1, T2, T3, T4, T5> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5, T6>(this IServiceScope scope, Action<T1, T2, T3, T4, T5, T6> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7>(this IServiceScope scope, Action<T1, T2, T3, T4, T5, T6, T7> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8>(this IServiceScope scope, Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IServiceScope scope, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IServiceScope scope, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IServiceScope scope, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IServiceScope scope, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IServiceScope scope, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IServiceScope scope, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static void Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IServiceScope scope, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
        where T15 : notnull
    {
        scope.ServiceProvider.Run(action);
    }

    public static TOut Run<T1, TOut>(this IServiceScope scope, Func<T1, TOut> func)
        where T1 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, TOut>(this IServiceScope scope, Func<T1, T2, TOut> func)
        where T1 : notnull
        where T2 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, TOut>(this IServiceScope scope, Func<T1, T2, T3, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static TOut Run<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
        where T15 : notnull
    {
        return scope.ServiceProvider.Run(func);
    }

    public static Task RunAsync<T1>(this IServiceScope scope, Func<T1, Task> func)
        where T1 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2>(this IServiceScope scope, Func<T1, T2, Task> func)
        where T1 : notnull
        where T2 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3>(this IServiceScope scope, Func<T1, T2, T3, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4>(this IServiceScope scope, Func<T1, T2, T3, T4, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
        where T15 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, TOut>(this IServiceScope scope, Func<T1, Task<TOut>> func)
        where T1 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, TOut>(this IServiceScope scope, Func<T1, T2, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, TOut>(this IServiceScope scope, Func<T1, T2, T3, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static Task<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
        where T15 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1>(this IServiceScope scope, Func<T1, ValueTask> func)
        where T1 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2>(this IServiceScope scope, Func<T1, T2, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3>(this IServiceScope scope, Func<T1, T2, T3, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4>(this IServiceScope scope, Func<T1, T2, T3, T4, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
        where T15 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, TOut>(this IServiceScope scope, Func<T1, ValueTask<TOut>> func)
        where T1 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, TOut>(this IServiceScope scope, Func<T1, T2, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, TOut>(this IServiceScope scope, Func<T1, T2, T3, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

    public static ValueTask<TOut> RunAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>(this IServiceScope scope, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask<TOut>> func)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
        where T9 : notnull
        where T10 : notnull
        where T11 : notnull
        where T12 : notnull
        where T13 : notnull
        where T14 : notnull
        where T15 : notnull
    {
        return scope.ServiceProvider.RunAsync(func);
    }

}
