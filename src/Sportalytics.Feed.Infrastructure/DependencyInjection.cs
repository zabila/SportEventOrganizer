using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportalytics.Feed.Infrastructure.Interfaces;
using Sportalytics.Feed.Infrastructure.Kafka.Consumer;
using Sportalytics.Feed.Infrastructure.Kafka.Interfaces;
using Sportalytics.Feed.Infrastructure.Services.ApiSportsService;

namespace Sportalytics.Feed.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApiSportsService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => {
            config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });

        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(c =>
                c.UseNpgsqlConnection(configuration.GetConnectionString("DefaultHangfireConnection"))));

        services.AddHangfireServer();
        services.AddSingleton<IBackgroundApiSportService, BackgroundApiSportService>();
        return services;
    }
}