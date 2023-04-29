using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class ProcessPhaseConfiguration : IEntityTypeConfiguration<ProcessPhase>
{
    public void Configure(EntityTypeBuilder<ProcessPhase> builder)
    {
        builder.HasMany(x => x.Parameters)
            .WithOne(x => x.Phase)
            .HasForeignKey(x => x.PhaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Localizations)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}