using Microsoft.Extensions.DependencyInjection;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection Configure<TOptions>(this IServiceCollection services) where TOptions : class
        => services.Configure<TOptions>(typeof(TOptions).Name, _ => {});
}