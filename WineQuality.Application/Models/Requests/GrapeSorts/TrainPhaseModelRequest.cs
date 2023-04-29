using System.ComponentModel.DataAnnotations;

namespace WineQuality.Application.Models.Requests.GrapeSorts;

public class TrainPhaseModelRequest
{
    [Required]
    public string DatasetId { get; set; } = null!;
}