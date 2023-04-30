using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.GrapeSorts.Standards;

namespace WineQuality.Application.Models.Results.GrapeSorts;

public class GrapeSortPhaseResult : BaseResult
{
    public string PhaseId { get; set; } = null!;
    public string PhaseName { get; set; } = null!;

    public List<GrapeSortProcessPhaseParameterStandardResult>? GrapeSortProcessPhaseParameterStandards { get; set; }

    public int Duration { get; set; }
    public int Order { get; set; }
}