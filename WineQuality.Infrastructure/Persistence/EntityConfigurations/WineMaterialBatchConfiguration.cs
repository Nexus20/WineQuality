using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class WineMaterialBatchConfiguration : IEntityTypeConfiguration<WineMaterialBatch>
{
    public void Configure(EntityTypeBuilder<WineMaterialBatch> builder)
    {
        builder.HasMany(x => x.Phases)
            .WithOne(x => x.WineMaterialBatch)
            .HasForeignKey(x => x.WineMaterialBatchId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}