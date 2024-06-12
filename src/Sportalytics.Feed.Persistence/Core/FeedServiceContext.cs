using Microsoft.EntityFrameworkCore;
using Sportalytics.Feed.Domain.Entities;
using Sportalytics.Feed.Persistence.Configuration;

namespace Sportalytics.Feed.Persistence.Core;

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