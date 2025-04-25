using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace En3Tho.Extensions.DependencyInjection.XUnit;

public interface IXunitStartup
{
    IHostBuilder? CreateHostBuilder() => null;
    void ConfigureHost(IHostBuilder hostBuilder) { }
    void ConfigureServices(IServiceCollection serviceCollection, HostBuilderContext hostBuilderContext) { }
}

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMockSingleton<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.AddSingleton<Moq.Mock<TService>>()
                  .AddSingleton(services => services.GetRequiredService<Moq.Mock<TService>>().Object);
        return collection;
    }

    public static IServiceCollection AddMockScoped<TService>(this IServiceCollection collection)
        where TService : class
    {
        collection.AddScoped<Moq.Mock<TService>>()
                  .AddScoped(services => services.GetRequiredService<Moq.Mock<TService>>().Object);
        return collection;
    }
}