using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseTypes;

public class EditPhaseParametersRequest
{
    [Required] public string ProcessPhaseId { get; set; } = null!;
    [Required] public string[] ProcessParametersIdsToAdd { get; set; } = null!;
    [Required] public string[] ProcessParametersIdsToRemove { get; set; } = null!;
}