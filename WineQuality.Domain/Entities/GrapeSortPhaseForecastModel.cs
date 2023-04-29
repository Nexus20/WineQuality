using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class GrapeSortPhaseForecastModel : BaseEntity
{
    public decimal? Accuracy { get; set; }

    public string GrapeSortPhaseId { get; set; } = null!;
    public virtual GrapeSortPhase GrapeSortPhase { get; set; } = null!;
    public string? ForecastingModelFileReferenceId { get; set; }
    public virtual FileReference? ForecastingModelFileReference { get; set; }
}