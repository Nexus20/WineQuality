using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class FileReference : BaseEntity
{
    public string Uri { get; set; } = null!;
    public virtual List<GrapeSortPhaseForecastModel> GrapeSortPhaseForecastModels { get; set; }
    public virtual List<GrapeSortPhaseDataset> GrapeSortPhaseDatasets { get; set; }
    public virtual List<QualityPrediction>? QualityPredictions { get; set; }
}