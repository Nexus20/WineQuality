using WineQuality.Application.Models.Results.WineMaterialBatches;

namespace WineQuality.Application.Models.InternalCommunication;

public class ReadingsMessage
{
    public string DeviceId { get; set; }
    public WineMaterialBatchGrapeSortPhaseParameterValueResult Value { get; set; }
    public string ParameterId { get; set; }
    public string ParameterName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string WineMaterialBatchId { get; set; }
}