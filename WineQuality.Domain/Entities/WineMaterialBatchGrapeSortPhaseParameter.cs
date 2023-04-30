using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class WineMaterialBatchGrapeSortPhaseParameter : BaseEntity
{
    public string PhaseParameterId { get; set; } = null!;
    public virtual ProcessPhaseParameter PhaseParameter { get; set; } = null!;
    
    public string WineMaterialBatchGrapeSortPhaseId { get; set; } = null!;
    public virtual WineMaterialBatchGrapeSortPhase WineMaterialBatchGrapeSortPhase { get; set; } = null!;
    public virtual List<WineMaterialBatchGrapeSortPhaseParameterValue>? Values { get; set; }
    
    public virtual List<ProcessPhaseParameterSensor>? Sensors { get; set; }
}