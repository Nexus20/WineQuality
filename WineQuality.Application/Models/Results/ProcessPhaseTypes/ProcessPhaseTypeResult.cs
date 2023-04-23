using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.ProcessParameters;

namespace WineQuality.Application.Models.Results.ProcessPhaseTypes;

public class ProcessPhaseTypeResult : BaseResult
{
    public string Name { get; set; } = null!;
    public virtual ProcessPhaseTypeResult? NextPhase {get; set;}
    public virtual ProcessPhaseTypeResult? PreviousPhase {get; set;}
    public virtual List<ProcessParameterResult>? Parameters { get; set; }
}