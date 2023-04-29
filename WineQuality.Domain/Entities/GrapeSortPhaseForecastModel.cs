using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class GrapeSortPhaseForecastModel : BaseEntity
{
    public string PhaseTypeId { get; set; } = null!;
    public virtual ProcessPhaseType PhaseType { get; set; } = null!;
    public string GrapeSortId { get; set; } = null!;
    public virtual GrapeSort GrapeSort { get; set; } = null!;
    public string? ForecastingModelFileReferenceId { get; set; }
    public virtual FileReference? ForecastingModelFileReference { get; set; }
    public virtual List<GrapeSortPhaseDataset>? Datasets { get; set; }
}