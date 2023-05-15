using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;

public class CreateProcessPhaseParameterSensorRequest
{
    [Required] public string PhaseId { get; set; } = null!;
    [Required] public string ParameterId { get; set; } = null!;
}