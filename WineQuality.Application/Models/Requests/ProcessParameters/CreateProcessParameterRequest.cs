using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.ProcessParameters;

public class CreateProcessParameterRequest
{
    [Required] public string Name { get; set; } = null!;
}