using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class WineMaterialBatchProcessPhase : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public string WineMaterialBatchId { get; set; } = null!;
    public virtual WineMaterialBatch WineMaterialBatch { get; set; } = null!;
    public string PhaseTypeId { get; set; } = null!;
    public virtual ProcessPhaseType PhaseType { get; set; } = null!;
    public virtual List<WineMaterialBatchProcessPhaseParameter>? Parameters { get; set; }
}