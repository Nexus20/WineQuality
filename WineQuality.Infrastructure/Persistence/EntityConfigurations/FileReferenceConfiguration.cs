using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

public class FileReferenceConfiguration : IEntityTypeConfiguration<FileReference>
{
    public void Configure(EntityTypeBuilder<FileReference> builder)
    {
        builder.HasMany(x => x.GrapeSortPhaseForecastModels)
            .WithOne(x => x.ForecastingModelFileReference)
            .HasForeignKey(x => x.ForecastingModelFileReferenceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}