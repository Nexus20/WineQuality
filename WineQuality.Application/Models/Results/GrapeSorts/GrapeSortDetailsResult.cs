using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.WineMaterialBatches;

namespace WineQuality.Application.Models.Results.GrapeSorts;

public class GrapeSortDetailsResult : BaseResult
{
    public string Name { get; set; } = null!;
    public List<GrapeSortPhaseResult> Phases { get; set; }
    public List<WineMaterialBatchResult> WineMaterialBatches { get; set; }
}