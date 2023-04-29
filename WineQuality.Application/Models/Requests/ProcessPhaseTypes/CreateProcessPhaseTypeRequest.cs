using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseTypes;

public class CreateProcessPhaseRequest
{
    [Required] public string Name { get; set; } = null!;
}