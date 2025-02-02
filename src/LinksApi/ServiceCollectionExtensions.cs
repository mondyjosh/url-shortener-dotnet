using Microsoft.Extensions.DependencyInjection;

using Dapper;

using LinksApi.Data;

namespace LinksApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupLinksApiServices(this IServiceCollection services)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services
            .AddScoped<ILinksRepository, LinksRepository>()
            .AddScoped<ILinksService>(serviceProvider => new LinksService(serviceProvider.GetRequiredService<ILinksRepository>()));
    }
}