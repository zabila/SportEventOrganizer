using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sportalytics.Event.Infrastructure.Services.ApiSportsService;

namespace Sportalytics.Event.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApiSportsService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => {
            config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });
        services.Configure<ApiSportsServiceSettings>(configuration.GetSection(nameof(ApiSportsServiceSettings)));

        services.AddHttpClient();
        services.AddHostedService<BackgroundApiSportsService>();
        return services;
    }
}