using Microsoft.EntityFrameworkCore;
using Sportalytics.Domain.Contracts.Repositories;
using Sportalytics.Infrastructure.Repository;
using Sportalytics.Infrastructure.Repository.Core;
using Sportalytics.Infrastructure.Repository.Repositories;

namespace Sportalytics.API.Feed.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDataBase(configuration);
    }

    private static void ConfigureDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}