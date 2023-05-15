using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseTypes;

public class AddParametersToPhaseRequest
{
    [Required] public string ProcessPhaseId { get; set; } = null!;
    [Required] public string[] ProcessParameterIds { get; set; } = null!;
}