using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class GrapeSortPhaseForecastModelConfiguration : IEntityTypeConfiguration<GrapeSortPhaseForecastModel>
{
    public void Configure(EntityTypeBuilder<GrapeSortPhaseForecastModel> builder)
    {
        builder.HasIndex(x => new { x.GrapeSortPhaseId, x.ForecastingModelFileReferenceId }).IsUnique();

        builder.HasOne(x => x.Dataset)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);
    }
}