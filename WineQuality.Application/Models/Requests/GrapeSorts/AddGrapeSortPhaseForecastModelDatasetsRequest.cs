using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.GrapeSorts;

public class AddGrapeSortPhaseForecastModelDatasetsRequest
{
    [Required] public string GrapeSortPhaseId { get; set; } = null!;
}