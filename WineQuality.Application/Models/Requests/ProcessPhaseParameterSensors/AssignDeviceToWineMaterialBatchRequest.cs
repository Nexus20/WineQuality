using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;

public class AssignDeviceToWineMaterialBatchRequest
{
    [Required] public string DeviceId { get; set; } = null!;
    [Required] public string WineMaterialBatchGrapeSortPhaseParameterId { get; set; } = null!;
}