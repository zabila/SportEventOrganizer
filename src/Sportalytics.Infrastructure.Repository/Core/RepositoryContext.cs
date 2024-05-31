using Microsoft.EntityFrameworkCore;
using Sportalytics.Domain.Entities;
using Sportalytics.Infrastructure.Repository.Configuration;

namespace Sportalytics.Infrastructure.Repository.Core;

public class RepositoryContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Sportalytics");

        modelBuilder.ApplyConfiguration(new SportEventConfiguration());
    }

    public DbSet<SportEvent>? SportEvents { get; set; }
}