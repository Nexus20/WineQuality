using WineQuality.Application.Specifications.Abstract;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Specifications.WineMaterialBatches;

public class WineMaterialBatchIncludeDetailsSpecification : IIncludeSpecification<WineMaterialBatch>
{
    public List<string> Includes { get; }
    
    public WineMaterialBatchIncludeDetailsSpecification()
    {
        Includes = new List<string>
        {
            $"{nameof(WineMaterialBatch.Phases)}.{nameof(WineMaterialBatchGrapeSortPhase.GrapeSortPhase)}",
            $"{nameof(WineMaterialBatch.Phases)}.{nameof(WineMaterialBatchGrapeSortPhase.Parameters)}",
            $"{nameof(WineMaterialBatch.GrapeSort)}.{nameof(GrapeSort.Phases)}.{nameof(GrapeSortPhase.GrapeSortProcessPhaseParameterStandards)}",
            $"{nameof(WineMaterialBatch.GrapeSort)}.{nameof(GrapeSort.Phases)}.{nameof(GrapeSortPhase.Phase)}.{nameof(ProcessPhase.Parameters)}",
        };
    }
}