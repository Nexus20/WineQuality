using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class WineMaterialBatchProcessPhaseParameterConfiguration : IEntityTypeConfiguration<WineMaterialBatchProcessPhaseParameter>
{
    public void Configure(EntityTypeBuilder<WineMaterialBatchProcessPhaseParameter> builder)
    {
        builder.HasMany(x => x.Values)
            .WithOne(x => x.PhaseParameter)
            .HasForeignKey(x => x.PhaseParameterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}