using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureByName<TOptions>(this IServiceCollection services) where TOptions : class
        => services.Configure<TOptions>(typeof(TOptions).Name, _ => {});

    public static IServiceCollection ConfigureByName<TOptions>(this IServiceCollection services, Action<TOptions> configure) where TOptions : class
        => services.Configure(typeof(TOptions).Name, configure);
}