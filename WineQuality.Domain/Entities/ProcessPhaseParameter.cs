using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class ProcessPhaseParameter : BaseEntity
{
    public string PhaseId { get; set; } = null!;
    public virtual ProcessPhase Phase { get; set; } = null!;

    public string ParameterId { get; set; } = null!;
    public virtual ProcessParameter Parameter { get; set; } = null!;
    
    public virtual List<GrapeSortProcessPhaseParameterStandard>? GrapeSortProcessPhaseParameterStandards { get; set; }
    
    public virtual List<ProcessPhaseParameterSensor>? Sensors { get; set; }
}