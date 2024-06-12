using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportalytics.Feed.Application.Interfaces;
using Sportalytics.Feed.Persistence.Core;
using Sportalytics.Feed.Persistence.Interfaces;

namespace Sportalytics.Feed.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FeedServiceContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        return services;
    }
}