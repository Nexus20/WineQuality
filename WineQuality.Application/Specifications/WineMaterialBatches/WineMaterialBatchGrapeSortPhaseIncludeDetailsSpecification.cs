using WineQuality.Application.Specifications.Abstract;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Specifications.WineMaterialBatches;

public class WineMaterialBatchGrapeSortPhaseIncludeDetailsSpecification : IIncludeSpecification<WineMaterialBatchGrapeSortPhase>
{
    public List<string> Includes { get; }

    public WineMaterialBatchGrapeSortPhaseIncludeDetailsSpecification()
    {
        Includes = new List<string>()
        {
            $"{nameof(WineMaterialBatchGrapeSortPhase.GrapeSortPhase)}.{nameof(GrapeSortPhase.GrapeSortProcessPhaseParameterStandards)}",
            $"{nameof(WineMaterialBatchGrapeSortPhase.Parameters)}.{nameof(WineMaterialBatchGrapeSortPhaseParameter.PhaseParameter)}.{nameof(ProcessPhaseParameter.Parameter)}",
            $"{nameof(WineMaterialBatchGrapeSortPhase.Parameters)}.{nameof(WineMaterialBatchGrapeSortPhaseParameter.Sensors)}.{nameof(ProcessPhaseParameterSensor.PhaseParameter)}.{nameof(ProcessPhaseParameter.Parameter)}",
            $"{nameof(WineMaterialBatchGrapeSortPhase.Parameters)}.{nameof(WineMaterialBatchGrapeSortPhaseParameter.Sensors)}.{nameof(ProcessPhaseParameterSensor.PhaseParameter)}.{nameof(ProcessPhaseParameter.Phase)}",
            $"{nameof(WineMaterialBatchGrapeSortPhase.Parameters)}.{nameof(WineMaterialBatchGrapeSortPhaseParameter.Sensors)}.{nameof(ProcessPhaseParameterSensor.WineMaterialBatchGrapeSortPhaseParameter)}.{nameof(WineMaterialBatchGrapeSortPhaseParameter.WineMaterialBatchGrapeSortPhase)}.{nameof(WineMaterialBatchGrapeSortPhase.WineMaterialBatch)}"
        };
    }
}