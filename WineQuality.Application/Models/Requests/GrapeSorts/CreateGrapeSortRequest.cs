using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.GrapeSorts;

public class CreateGrapeSortRequest
{
    [Required] public string Name { get; set; } = null!;
}