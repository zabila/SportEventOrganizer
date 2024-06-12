using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sportalytics.Feed.Persistence.PostgreSQL.Core;
using Sportalytics.Feed.Persistence.PostgreSQL.Interfaces;

namespace Sportalytics.Feed.Persistence.PostgreSQL;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgreSQL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FeedServiceContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        return services;
    }
}