using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class GrapeSortProcessPhaseParameterStandard : BaseEntity
{
    public double? LowerBound { get; set; }
    public double? UpperBound { get; set; }
    
    public string GrapeSortPhaseId { get; set; } = null!;
    public virtual GrapeSortPhase GrapeSortPhase { get; set; } = null!;
    
    public string PhaseParameterId { get; set; } = null!;
    public virtual ProcessPhaseParameter PhaseParameter { get; set; } = null!;
}