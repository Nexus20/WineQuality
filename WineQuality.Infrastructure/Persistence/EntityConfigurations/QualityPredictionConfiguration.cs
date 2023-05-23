using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using WineQuality.Domain.Entities;

namespace WineQuality.Infrastructure.Persistence.EntityConfigurations;

internal class QualityPredictionConfiguration : IEntityTypeConfiguration<QualityPrediction>
{
    public void Configure(EntityTypeBuilder<QualityPrediction> builder)
    {
        builder.Property(x => x.PredictionExplanation)
            .HasConversion(
                x => x != null ? JsonConvert.SerializeObject(x) : null,
                x => x != null ? JsonConvert.DeserializeObject<Dictionary<string, double>>(x) : null
            );
        
        builder.Property(x => x.ParametersValues)
            .HasConversion(
                x => x != null ? JsonConvert.SerializeObject(x) : null,
                x => x != null ? JsonConvert.DeserializeObject<Dictionary<string, double>>(x) : null
            );
    }
}