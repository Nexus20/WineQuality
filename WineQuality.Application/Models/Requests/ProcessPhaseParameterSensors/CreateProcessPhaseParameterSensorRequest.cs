using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;

public class CreateProcessPhaseParameterSensorRequest
{
    [Required] public string DeviceId { get; set; } = null!;
    [Required] public string PhaseParameterId { get; set; } = null!;
}