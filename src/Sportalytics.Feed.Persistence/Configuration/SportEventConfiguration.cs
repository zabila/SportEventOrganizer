using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportalytics.Feed.Domain.Entities;

namespace Sportalytics.Feed.Persistence.Configuration;

public class SportEventConfiguration : IEntityTypeConfiguration<SportEvent>
{

    public void Configure(EntityTypeBuilder<SportEvent> builder)
    {
        builder.HasKey(item => item.Id);
        builder.Property<Guid>(item => item.Id).ValueGeneratedOnAdd();
        builder.Property<string?>(item => item.Name).IsRequired();
    }
}