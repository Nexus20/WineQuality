using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseTypes;

public class AddParameterToPhaseRequest
{
    [Required] public string ProcessPhaseTypeId { get; set; } = null!;
    [Required] public string ProcessParameterId { get; set; } = null!;
}