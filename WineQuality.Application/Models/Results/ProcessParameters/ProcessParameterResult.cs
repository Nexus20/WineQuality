using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.ProcessPhaseTypes;

namespace WineQuality.Application.Models.Results.ProcessParameters;

public class ProcessParameterResult : BaseResult
{
    public string Name { get; set; } = null!;
    public virtual List<ProcessPhaseTypeResult>? Phases { get; set; }
}