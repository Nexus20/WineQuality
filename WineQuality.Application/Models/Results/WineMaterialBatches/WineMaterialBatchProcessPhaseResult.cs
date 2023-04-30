using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Application.Models.Results.ProcessPhaseTypes;

namespace WineQuality.Application.Models.Results.WineMaterialBatches;

public class WineMaterialBatchProcessPhaseResult : BaseResult
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public GrapeSortPhaseResult Phase { get; set; } = null!;
    // public virtual List<WineMaterialBatchProcessPhaseParameter>? Parameters { get; set; }
}