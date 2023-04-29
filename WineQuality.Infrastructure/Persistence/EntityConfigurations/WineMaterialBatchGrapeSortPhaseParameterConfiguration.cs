using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class WineMaterialBatchGrapeSortPhaseParameterConfiguration : IEntityTypeConfiguration<WineMaterialBatchGrapeSortPhaseParameter>
{
    public void Configure(EntityTypeBuilder<WineMaterialBatchGrapeSortPhaseParameter> builder)
    {
        builder.HasMany(x => x.Values)
            .WithOne(x => x.PhaseParameter)
            .HasForeignKey(x => x.PhaseParameterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}