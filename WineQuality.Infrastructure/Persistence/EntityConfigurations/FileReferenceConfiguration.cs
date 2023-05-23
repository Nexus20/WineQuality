using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class FileReferenceConfiguration : IEntityTypeConfiguration<FileReference>
{
    public void Configure(EntityTypeBuilder<FileReference> builder)
    {
        builder.HasMany(x => x.GrapeSortPhaseForecastModels)
            .WithOne(x => x.ForecastingModelFileReference)
            .HasForeignKey(x => x.ForecastingModelFileReferenceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.GrapeSortPhaseDatasets)
            .WithOne(x => x.DatasetFileReference)
            .HasForeignKey(x => x.DatasetFileReferenceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.QualityPredictions)
            .WithOne(x => x.FileReference)
            .HasForeignKey(x => x.FileReferenceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}