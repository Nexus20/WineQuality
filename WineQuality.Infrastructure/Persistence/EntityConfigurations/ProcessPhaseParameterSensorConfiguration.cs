using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class ProcessPhaseParameterSensorConfiguration : IEntityTypeConfiguration<ProcessPhaseParameterSensor>
{
    public void Configure(EntityTypeBuilder<ProcessPhaseParameterSensor> builder)
    {
        builder.HasIndex(x => x.DeviceKey).IsUnique();
        builder.HasIndex(x => x.DeviceName).IsUnique();
    }
}