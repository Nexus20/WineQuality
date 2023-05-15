using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseTypes;

public class RemoveParametersFromPhaseRequest
{
    [Required] public string ProcessPhaseId { get; set; } = null!;
    [Required] public string[] ProcessParameterIds { get; set; } = null!;
}