using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseTypes;

public class CreateProcessPhaseTypeRequest
{
    [Required] public string Name { get; set; } = null!;
    public string? PreviousPhaseId { get; set; }
}