using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class WineMaterialBatchGrapeSortPhaseConfiguration : IEntityTypeConfiguration<WineMaterialBatchGrapeSortPhase>
{
    public void Configure(EntityTypeBuilder<WineMaterialBatchGrapeSortPhase> builder)
    {
        builder.HasIndex(x => new { x.WineMaterialBatchId, x.GrapeSortPhaseId }).IsUnique();
        
        builder.HasMany(x => x.Parameters)
            .WithOne(x => x.WineMaterialBatchGrapeSortPhase)
            .HasForeignKey(x => x.WineMaterialBatchGrapeSortPhaseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.QualityPredictions)
            .WithOne(x => x.WineMaterialBatchGrapeSortPhase)
            .HasForeignKey(x => x.WineMaterialBatchGrapeSortPhaseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}