using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.WineMaterialBatches;

public class ChangeWineProcessingRequestRunningState
{
    [Required] public string WineMaterialBatchId { get; set; } = null!;
    [Required] public string WineMaterialBatchGrapeSortPhaseId { get; set; } = null!;
}