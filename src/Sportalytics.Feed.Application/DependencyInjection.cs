using Microsoft.Extensions.DependencyInjection;

namespace Sportalytics.Feed.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(config => {
            config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        });

        return services;
    }
}