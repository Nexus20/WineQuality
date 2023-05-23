using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;

public class AssignDevicesToWineMaterialBatchRequest
{
    [Required] public string[] SensorsIds { get; set; } = null!;
    [Required] public string WineMaterialBatchGrapeSortPhaseId { get; set; } = null!;
}