using System.Reflection.Metadata;
using WineQuality.Application.Models.Results.Abstract;
using WineQuality.Application.Models.Results.GrapeSorts;
using WineQuality.Application.Models.Results.ProcessParameters;
using WineQuality.Application.Models.Results.ProcessPhaseParameterSensors;

namespace WineQuality.Application.Models.Results.WineMaterialBatches;

public class ActiveWineMaterialBatchGrapeSortPhaseResult : WineMaterialBatchGrapeSortPhaseResult
{
    public List<WineMaterialBatchProcessPhaseReadingsResult> Readings { get; set; }
}

public class WineMaterialBatchGrapeSortPhaseResult : BaseResult
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public GrapeSortPhaseResult Phase { get; set; } = null!;
    public List<WineMaterialBatchGrapeSortPhaseParameterResult>? Parameters { get; set; }
}

public class WineMaterialBatchGrapeSortPhaseDetailsResult : WineMaterialBatchGrapeSortPhaseResult
{
    public List<WineMaterialBatchGrapeSortPhaseParameterDetailsResult>? ParametersDetails { get; set; } 
}

public class WineMaterialBatchGrapeSortPhaseParameterDetailsResult : BaseResult
{
    public ProcessParameterResult Parameter { get; set; }
    public List<ProcessPhaseParameterSensorResult>? Sensors { get; set; }
}

public class WineMaterialBatchGrapeSortPhaseParameterResult : BaseResult
{
    // public ProcessParameterResult Parameter { get; set; }
}

public class WineMaterialBatchGrapeSortPhaseParameterValueResult : BaseResult
{
    public double Value { get; set; }
    public virtual WineMaterialBatchGrapeSortPhaseParameterResult PhaseParameter { get; set; } = null!;
}