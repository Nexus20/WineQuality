using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Application.Models.Results.ProcessPhaseTypes;

namespace WineQuality.Application.Models.Results.WineMaterialBatches;

public class WineMaterialBatchResult : BaseResult
{
    public string Name { get; set; } = null!;
    public DateTime HarvestDate { get; set; }
    public string HarvestLocation { get; set; } = null!;
    //public List<WineMaterialBatchProcessPhase> Phases { get; set; } = null!;
    public GrapeSortResult GrapeSort { get; set; } = null!;
}