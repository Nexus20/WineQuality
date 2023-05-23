using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.WineMaterialBatches;

public class AssignWineMaterialBatchToPhasesRequest
{
    [Required] public string WineMaterialBatchId { get; set; } = null!;

    [Required] public List<AssignWineMaterialBatchToPhasesRequestPart> Phases { get; set; } = null!;
    
    public class AssignWineMaterialBatchToPhasesRequestPart
    {
        [Required] public DateTime StartDate { get; set; }
        [Required] public DateTime EndDate { get; set; }
        [Required] public string GrapeSortPhaseId { get; set; } = null!;
    }
}

public class UpdateWineMaterialBatchPhasesTermsRequest
{
    [Required] public List<UpdateWineMaterialBatchPhasesTerm> Terms { get; set; } = null!;
}

public class UpdateWineMaterialBatchPhasesTerm
{
    [Required] public string Id { get; set; } = null!;
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
}

public class GetWineMaterialBatchPhaseParameterChartDataRequest
{
    [Required] public string WineMaterialBatchGrapeSortPhaseParameterId { get; set; } = null!;
    [Required] public ParameterChartType ChartType { get; set; }
}

public enum ParameterChartType
{
    Hour = 1000,
    Day = 2000,
    Week = 3000,
    Month = 4000,
    Year = 5000,
    AllTime = 6000
}