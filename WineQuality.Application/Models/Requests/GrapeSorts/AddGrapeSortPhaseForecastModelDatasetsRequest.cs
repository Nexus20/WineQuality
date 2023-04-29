using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.GrapeSorts;

public class AddGrapeSortPhaseForecastModelDatasetsRequest
{
    [Required] public string GrapeSortId { get; set; } = null!;
    [Required] public string ProcessPhaseTypeId { get; set; } = null!;
}