using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class ProcessParameterConfiguration : IEntityTypeConfiguration<ProcessParameter>
{
    public void Configure(EntityTypeBuilder<ProcessParameter> builder)
    {
        builder.HasMany(x => x.Phases)
            .WithOne(x => x.Parameter)
            .HasForeignKey(x => x.ParameterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}