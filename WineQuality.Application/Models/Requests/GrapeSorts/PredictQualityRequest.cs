using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.GrapeSorts;

public class PredictQualityRequest
{
    [Required]
    public string ForecastModelId { get; set; } = null!;
    
    [Required]
    public string WineMaterialBatchGrapeSortPhaseId { get; set; } = null!;
    
    public Dictionary<string, double>? ParametersValues { get; set; }
}