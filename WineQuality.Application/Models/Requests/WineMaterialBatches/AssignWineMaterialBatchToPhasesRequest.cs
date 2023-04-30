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