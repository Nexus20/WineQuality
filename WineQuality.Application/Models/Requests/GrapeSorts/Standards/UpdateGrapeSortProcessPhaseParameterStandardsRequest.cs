using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.GrapeSorts.Standards;

public class UpdateGrapeSortProcessPhaseParameterStandardsRequest
{
    [Required] public List<UpdateGrapeSortProcessPhaseParameterStandardsRequestPart> Standards { get; set; }
    
    public class UpdateGrapeSortProcessPhaseParameterStandardsRequestPart
    {
        [Required] public string StandardId { get; set; }
        [Required] public double LowerBound { get; set; }
        [Required] public double UpperBound { get; set; }
    }
}