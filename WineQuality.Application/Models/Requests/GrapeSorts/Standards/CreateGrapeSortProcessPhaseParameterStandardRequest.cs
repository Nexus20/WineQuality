using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.GrapeSorts.Standards;

public class CreateGrapeSortProcessPhaseParameterStandardRequest
{
    public double? LowerBound { get; set; }
    public double? UpperBound { get; set; }
    
    [Required] public string GrapeSortPhaseId { get; set; } = null!;
    [Required] public string PhaseParameterId { get; set; } = null!;
}