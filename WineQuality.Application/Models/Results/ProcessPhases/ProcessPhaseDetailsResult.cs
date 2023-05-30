using WineQuality.Application.Models.Results.ProcessParameters;

namespace WineQuality.Application.Models.Results.ProcessPhases;

public class ProcessPhaseDetailsResult : ProcessPhaseResult
{
    public List<ProcessParameterResult>? Parameters { get; set; }
}