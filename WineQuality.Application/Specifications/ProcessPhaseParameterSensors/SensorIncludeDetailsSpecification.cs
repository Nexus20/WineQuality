using WineQuality.Application.Specifications.Abstract;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Specifications.ProcessPhaseParameterSensors;

public class SensorIncludeDetailsSpecification : IIncludeSpecification<ProcessPhaseParameterSensor>
{
    public List<string> Includes { get; }
    
    public SensorIncludeDetailsSpecification()
    {
        Includes = new List<string>()
        {
            $"{nameof(ProcessPhaseParameterSensor.PhaseParameter)}.{nameof(ProcessPhaseParameter.Parameter)}",
            $"{nameof(ProcessPhaseParameterSensor.PhaseParameter)}.{nameof(ProcessPhaseParameter.Phase)}",
            $"{nameof(ProcessPhaseParameterSensor.WineMaterialBatchGrapeSortPhaseParameter)}.{nameof(WineMaterialBatchGrapeSortPhaseParameter.WineMaterialBatchGrapeSortPhase)}.{nameof(WineMaterialBatchGrapeSortPhase.WineMaterialBatch)}"
        };
    }
}