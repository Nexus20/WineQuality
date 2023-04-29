using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class GrapeSortPhase : BaseEntity
{
    public string PhaseId { get; set; } = null!;
    public virtual ProcessPhase Phase { get; set; } = null!;

    public string GrapeSortId { get; set; } = null!;
    public virtual GrapeSort GrapeSort { get; set; } = null!;
    
    public virtual List<GrapeSortPhaseForecastModel>? GrapeSortPhaseForecastModels { get; set; }
    public virtual List<GrapeSortProcessPhaseParameterStandard>? GrapeSortProcessPhaseParameterStandards { get; set; }
    public virtual List<GrapeSortPhaseDataset>? Datasets { get; set; }
    
    public int Duration { get; set; }
    public int Order { get; set; }
    
    public virtual GrapeSortPhase? NextPhase {get; set;}
    public virtual GrapeSortPhase? PreviousPhase {get; set;}
}