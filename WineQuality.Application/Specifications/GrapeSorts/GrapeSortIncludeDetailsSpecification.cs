using WineQuality.Application.Specifications.Abstract;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Specifications.GrapeSorts;

public class GrapeSortIncludeDetailsSpecification : IIncludeSpecification<GrapeSort>
{
    public List<string> Includes { get; }

    public GrapeSortIncludeDetailsSpecification()
    {
        Includes = new List<string>
        {
            $"{nameof(GrapeSort.Phases)}.{nameof(GrapeSortPhase.GrapeSortProcessPhaseParameterStandards)}",
            $"{nameof(GrapeSort.Phases)}.{nameof(GrapeSortPhase.Phase)}.{nameof(ProcessPhase.Parameters)}",
            nameof(GrapeSort.WineMaterialBatches)
        };
    }
}