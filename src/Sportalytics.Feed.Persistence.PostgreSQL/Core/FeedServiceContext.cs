using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.PostgreSQL.Configuration;

namespace Sportalytics.Feed.Persistence.PostgreSQL.Core;

public class FeedServiceContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Sportalytics");

        modelBuilder.ApplyConfiguration(new SportEventConfiguration());
    }

    public DbSet<SportEvent> SportEvents { get; set; }
}