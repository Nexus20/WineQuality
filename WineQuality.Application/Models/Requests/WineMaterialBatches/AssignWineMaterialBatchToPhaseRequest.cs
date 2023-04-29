using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.WineMaterialBatches;

public class AssignWineMaterialBatchToPhaseRequest
{
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    [Required] public string WineMaterialBatchId { get; set; } = null!;
    [Required] public string PhaseTypeId { get; set; } = null!;
}