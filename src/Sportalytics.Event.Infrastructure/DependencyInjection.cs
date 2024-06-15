using Flurl.Http.Configuration;
using Flurl.Http;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportalytics.Event.Domain.Extensions;
using Sportalytics.Event.Infrastructure.Interfaces;
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

        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(c =>
                c.UseNpgsqlConnection(configuration.GetConnectionString("DefaultConnection"))));

        services.AddHangfireServer();

        var apiSportsServiceSettings = configuration.GetSection(nameof(ApiSportsServiceSettings)).Get<ApiSportsServiceSettings>();
        var baseUrl = apiSportsServiceSettings.EnsureExists().BaseUrl.EnsureExists();
        services.AddSingleton<IFlurlClientCache>(sp => new FlurlClientCache().Add(nameof(ApiSportService), baseUrl));

        services.AddScoped<IApiSportService, ApiSportService>();
        services.AddSingleton<IBackgroundApiSportService, BackgroundApiSportService>();
        return services;
    }
}