using Microsoft.Extensions.DependencyInjection;

namespace LinksApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SetupLinksApiServices(this IServiceCollection services)
    {
        return services.AddScoped<ILinksService>(serviceProvider => new LinksService());
    }
}