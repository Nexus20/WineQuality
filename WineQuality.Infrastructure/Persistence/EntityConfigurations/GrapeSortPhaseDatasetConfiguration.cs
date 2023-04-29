using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class GrapeSortPhaseDatasetConfiguration : IEntityTypeConfiguration<GrapeSortPhaseDataset>
{
    public void Configure(EntityTypeBuilder<GrapeSortPhaseDataset> builder)
    {
        builder.HasIndex(x => new { x.GrapeSortPhaseId, x.DatasetFileReferenceId }).IsUnique();
    }
}