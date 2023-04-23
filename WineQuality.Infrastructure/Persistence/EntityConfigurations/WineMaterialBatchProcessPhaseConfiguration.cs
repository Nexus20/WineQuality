using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class WineMaterialBatchProcessPhaseConfiguration : IEntityTypeConfiguration<WineMaterialBatchProcessPhase>
{
    public void Configure(EntityTypeBuilder<WineMaterialBatchProcessPhase> builder)
    {
        builder.HasMany(x => x.Parameters)
            .WithOne(x => x.WineMaterialBatch)
            .HasForeignKey(x => x.WineMaterialBatchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PhaseType)
            .WithMany()
            .HasForeignKey(x => x.PhaseTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}