using WineQuality.Application.Specifications.Abstract;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Specifications.ProcessPhases;

public class ProcessPhaseIncludeParametersSpecification : IIncludeSpecification<ProcessPhase>
{
    public List<string> Includes { get; }

    public ProcessPhaseIncludeParametersSpecification()
    {
        Includes = new List<string> { $"{nameof(ProcessPhase.Parameters)}" };
    }
}