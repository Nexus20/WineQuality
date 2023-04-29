using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class ProcessPhaseParameterConfiguration : IEntityTypeConfiguration<ProcessPhaseParameter>
{
    public void Configure(EntityTypeBuilder<ProcessPhaseParameter> builder)
    {
        builder.HasIndex(x => new { x.ParameterId, x.PhaseId }).IsUnique();
        
        builder.HasMany(x => x.GrapeSortProcessPhaseParameterStandards)
            .WithOne(x => x.PhaseParameter)
            .HasForeignKey(x => x.PhaseParameterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}