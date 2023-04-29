using WineQuality.Domain.Entities.Abstract;

namespace WineQuality.Domain.Entities;

public class WineMaterialBatchGrapeSortPhase : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public string WineMaterialBatchId { get; set; } = null!;
    public virtual WineMaterialBatch WineMaterialBatch { get; set; } = null!;
    
    public string GrapeSortPhaseId { get; set; } = null!;
    public virtual GrapeSortPhase GrapeSortPhase { get; set; } = null!;
    public virtual List<WineMaterialBatchGrapeSortPhaseParameter>? Parameters { get; set; }
}