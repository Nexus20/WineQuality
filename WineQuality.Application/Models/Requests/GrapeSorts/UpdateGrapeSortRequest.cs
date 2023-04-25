using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.GrapeSorts;

public class UpdateGrapeSortRequest
{
    [Required] public string Id { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;
}