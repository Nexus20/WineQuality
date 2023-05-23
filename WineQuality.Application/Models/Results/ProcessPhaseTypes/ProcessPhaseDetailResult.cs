using WineQuality.Application.Models.Results.ProcessParameters;

namespace WineQuality.Application.Models.Results.ProcessPhaseTypes;

public class ProcessPhaseDetailResult : ProcessPhaseResult
{
    public List<ProcessParameterResult>? Parameters { get; set; }
}