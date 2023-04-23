using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseTypes;

public class UpdateProcessPhaseTypeRequest
{
    [Required] public string Id { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;
}