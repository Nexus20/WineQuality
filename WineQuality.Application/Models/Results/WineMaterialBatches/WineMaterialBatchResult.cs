using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.GrapeSorts;

namespace WineQuality.Application.Models.Results.WineMaterialBatches;

public class WineMaterialBatchResult : BaseResult
{
    public string Name { get; set; } = null!;
    public DateTime HarvestDate { get; set; }
    public string HarvestLocation { get; set; } = null!;
    public List<WineMaterialBatchProcessPhaseResult> Phases { get; set; } = null!;
    public GrapeSortResult GrapeSort { get; set; } = null!;
}