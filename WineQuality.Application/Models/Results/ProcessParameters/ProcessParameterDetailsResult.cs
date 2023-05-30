using WineQuality.Application.Models.Results.ProcessPhases;

namespace WineQuality.Application.Models.Results.ProcessParameters;

public class ProcessParameterDetailsResult : ProcessParameterResult
{
    public virtual List<ProcessPhaseResult>? Phases { get; set; }
}