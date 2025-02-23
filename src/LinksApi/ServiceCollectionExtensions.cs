using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Dapper;

using LinksApi.Data;

namespace LinksApi;

/// <summary>
/// Extension methods for registering LinksApi services in dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Sets up the service collection for LinksApi.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns>Service collection configuration for LinksApi.</returns>
    public static IServiceCollection SetupLinksApiServices(this IServiceCollection services)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services
            .AddScoped<ILinksRepository, LinksRepository>()
            .AddScoped<ILinksService>(serviceProvider =>
                new LinksService(
                    serviceProvider.GetRequiredService<IConfiguration>(),
                    serviceProvider.GetRequiredService<ILinksRepository>()
                )
            );
    }
}