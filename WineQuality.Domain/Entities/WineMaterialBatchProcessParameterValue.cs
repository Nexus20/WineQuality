using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class WineMaterialBatchProcessParameterValue : BaseEntity
{
    public double Value { get; set; }
    
    public string PhaseParameterId { get; set; } = null!;
    public virtual WineMaterialBatchProcessPhaseParameter PhaseParameter { get; set; } = null!;
}