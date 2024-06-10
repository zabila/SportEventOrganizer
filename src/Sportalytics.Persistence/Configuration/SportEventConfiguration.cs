using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sportalytics.Domain.Entities;

namespace Sportalytics.Persistence.Configuration;

public class SportEventConfiguration : IEntityTypeConfiguration<SportEvent>
{

    public void Configure(EntityTypeBuilder<SportEvent> builder)
    {
        builder.Property<Guid>(item => item.Id).ValueGeneratedOnAdd();
        builder.Property<string?>(item => item.Name).IsRequired();
    }
}