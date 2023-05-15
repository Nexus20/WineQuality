using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.GrapeSorts.Standards;
using WineQuality.Application.Models.Results.ProcessParameters;

namespace WineQuality.Application.Models.Results.GrapeSorts;

public class GrapeSortPhaseResult : BaseResult
{
    public string PhaseId { get; set; } = null!;
    public string PhaseName { get; set; } = null!;

    public List<GrapeSortProcessPhaseParameterStandardResult>? GrapeSortProcessPhaseParameterStandards { get; set; }

    public int Duration { get; set; }
    public int Order { get; set; }
    public List<ProcessParameterResult> Parameters { get; set; }
}