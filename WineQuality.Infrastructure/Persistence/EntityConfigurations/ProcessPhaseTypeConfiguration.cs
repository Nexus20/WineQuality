using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class ProcessPhaseTypeConfiguration : IEntityTypeConfiguration<ProcessPhaseType>
{
    public void Configure(EntityTypeBuilder<ProcessPhaseType> builder)
    {
        builder.HasMany(x => x.Parameters)
            .WithOne(x => x.PhaseType)
            .HasForeignKey(x => x.PhaseTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.GrapeSortPhaseForecastModels)
            .WithOne(x => x.PhaseType)
            .HasForeignKey(x => x.PhaseTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.NextPhase)
            .WithOne(x => x.PreviousPhase)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PreviousPhase)
            .WithOne(x => x.NextPhase)
            .OnDelete(DeleteBehavior.Restrict);
    }
}