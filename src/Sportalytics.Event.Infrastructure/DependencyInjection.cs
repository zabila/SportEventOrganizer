using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        services.AddHttpClient();
        services.AddScoped<IScopedApiSportService, ScopedApiSportService>();
        services.AddSingleton<IScopedBackgroundApiSportService, ScopedBackgroundApiSportService>();
        return services;
    }
}