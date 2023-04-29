using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.ProcessParameters;

namespace WineQuality.Application.Models.Results.ProcessPhaseTypes;

public class ProcessPhaseResult : BaseResult
{
    public string Name { get; set; } = null!;
    // public virtual ProcessPhaseResult? NextPhase {get; set;}
    // public virtual ProcessPhaseResult? PreviousPhase {get; set;}
    public virtual List<ProcessParameterResult>? Parameters { get; set; }
    public int Order { get; set; }
}