using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class WineMaterialBatchGrapeSortPhaseParameterValue : BaseEntity
{
    public double Value { get; set; }
    
    public string PhaseParameterId { get; set; } = null!;
    public virtual WineMaterialBatchGrapeSortPhaseParameter PhaseParameter { get; set; } = null!;
}