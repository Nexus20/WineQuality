using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class WineMaterialBatchProcessPhaseParameter : BaseEntity
{
    public double? LowerBound { get; set; }
    public double? UpperBound { get; set; }
    public string PhaseParameterId { get; set; } = null!;
    public virtual ProcessPhaseParameter PhaseParameter { get; set; } = null!;
    public string WineMaterialBatchId { get; set; } = null!;
    public virtual WineMaterialBatchProcessPhase WineMaterialBatch { get; set; } = null!;
    public virtual List<WineMaterialBatchProcessParameterValue>? Values { get; set; }
}