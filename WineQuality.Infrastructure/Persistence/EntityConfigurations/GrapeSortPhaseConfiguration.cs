using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class GrapeSortPhaseConfiguration : IEntityTypeConfiguration<GrapeSortPhase>
{
    public void Configure(EntityTypeBuilder<GrapeSortPhase> builder)
    {
        builder.HasMany(x => x.GrapeSortProcessPhaseParameterStandards)
            .WithOne(x => x.GrapeSortPhase)
            .HasForeignKey(x => x.GrapeSortPhaseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.GrapeSortPhaseForecastModels)
            .WithOne(x => x.GrapeSortPhase)
            .HasForeignKey(x => x.GrapeSortPhaseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Datasets)
            .WithOne(x => x.GrapeSortPhase)
            .HasForeignKey(x => x.GrapeSortPhaseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.NextPhase)
            .WithOne(x => x.PreviousPhase)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PreviousPhase)
            .WithOne(x => x.NextPhase)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(x => new {x.Order, x.PhaseId, x.GrapeSortId}).IsUnique();
    }
}