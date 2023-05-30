using WineQuality.Application.Models.Results.Abstract;

namespace WineQuality.Application.Models.Results.ProcessPhases;

public class ProcessPhaseResult : BaseResult
{
    public string Name { get; set; } = null!;
    // public virtual ProcessPhaseResult? NextPhase {get; set;}
    // public virtual ProcessPhaseResult? PreviousPhase {get; set;}
}