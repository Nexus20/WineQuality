using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessPhaseParameters;

public class UpdateProcessParameterRequest
{
    [Required] public string Id { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;
}