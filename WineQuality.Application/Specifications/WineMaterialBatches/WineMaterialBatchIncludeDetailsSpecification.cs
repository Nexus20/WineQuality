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
            $"{nameof(WineMaterialBatch.Phases)}.{nameof(WineMaterialBatchGrapeSortPhase.GrapeSortPhase)}.{nameof(GrapeSortPhase.Phase)}.{nameof(ProcessPhase.Parameters)}.{nameof(ProcessPhaseParameter.Parameter)}",
            $"{nameof(WineMaterialBatch.Phases)}.{nameof(WineMaterialBatchGrapeSortPhase.GrapeSortPhase)}.{nameof(GrapeSortPhase.GrapeSortProcessPhaseParameterStandards)}.{nameof(GrapeSortProcessPhaseParameterStandard.PhaseParameter)}.{nameof(ProcessPhaseParameter.Parameter)}",
            $"{nameof(WineMaterialBatch.Phases)}.{nameof(WineMaterialBatchGrapeSortPhase.GrapeSortPhase)}.{nameof(GrapeSortPhase.GrapeSortProcessPhaseParameterStandards)}.{nameof(GrapeSortProcessPhaseParameterStandard.PhaseParameter)}.{nameof(ProcessPhaseParameter.Phase)}",
            $"{nameof(WineMaterialBatch.Phases)}.{nameof(WineMaterialBatchGrapeSortPhase.GrapeSortPhase)}.{nameof(GrapeSortPhase.GrapeSort)}",
            $"{nameof(WineMaterialBatch.Phases)}.{nameof(WineMaterialBatchGrapeSortPhase.Parameters)}",
            $"{nameof(WineMaterialBatch.GrapeSort)}",
        };
    }
}