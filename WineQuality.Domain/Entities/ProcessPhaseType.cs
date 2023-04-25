using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class ProcessPhaseType : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual ProcessPhaseType? NextPhase {get; set;}
    public virtual ProcessPhaseType? PreviousPhase {get; set;}
    public virtual List<ProcessPhaseParameter>? Parameters { get; set; }
    public virtual List<GrapeSortPhaseForecastModel> GrapeSortPhaseForecastModels { get; set; }
}