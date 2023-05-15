using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.GrapeSorts;

namespace WineQuality.Application.Models.Results.WineMaterialBatches;

public class WineMaterialBatchDetailsResult : BaseResult
{
    public string Name { get; set; } = null!;
    public DateTime HarvestDate { get; set; }
    public string HarvestLocation { get; set; } = null!;
    public GrapeSortResult GrapeSort { get; set; } = null!;
    public List<WineMaterialBatchGrapeSortPhaseResult> Phases { get; set; } = null!;
    public ActiveWineMaterialBatchGrapeSortPhaseResult? ActivePhase { get; set; }
}