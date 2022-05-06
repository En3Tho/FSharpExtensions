using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace En3Tho.Extensions.DependencyInjection;

public static partial class IServiceCollectionExtensions
{
    public static IServiceCollection AddHttpClient<TClient>(this IServiceCollection collection, Uri uri)
        where TClient : class
    {
        collection.AddHttpClient<TClient>(client => client.BaseAddress = uri);
        return collection;
    }
}