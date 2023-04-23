using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseParameters;

public class CreateProcessParameterRequest
{
    [Required] public string Name { get; set; } = null!;
}