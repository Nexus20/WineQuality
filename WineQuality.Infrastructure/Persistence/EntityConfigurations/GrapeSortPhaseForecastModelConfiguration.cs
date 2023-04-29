using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class GrapeSortPhaseForecastModelConfiguration : IEntityTypeConfiguration<GrapeSortPhaseForecastModel>
{
    public void Configure(EntityTypeBuilder<GrapeSortPhaseForecastModel> builder)
    {
        builder.HasMany(x => x.Datasets)
            .WithOne(x => x.GrapeSortPhaseForecastModel)
            .HasForeignKey(x => x.GrapeSortPhaseForecastModelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}