using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class CultureConfiguration : IEntityTypeConfiguration<Culture>
{
    public void Configure(EntityTypeBuilder<Culture> builder)
    {
        builder.HasMany(x => x.Localizations)
            .WithOne(x => x.Culture)
            .HasForeignKey(x => x.CultureId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}