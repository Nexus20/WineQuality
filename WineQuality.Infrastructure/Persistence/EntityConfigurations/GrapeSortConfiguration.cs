using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class GrapeSortConfiguration : IEntityTypeConfiguration<GrapeSort>
{
    public void Configure(EntityTypeBuilder<GrapeSort> builder)
    {
        builder.HasMany(x => x.WineMaterialBatches)
            .WithOne(x => x.GrapeSort)
            .HasForeignKey(x => x.GrapeSortId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Phases)
            .WithOne(x => x.GrapeSort)
            .HasForeignKey(x => x.GrapeSortId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}