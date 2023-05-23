using WineQuality.Application.Models.Results.WineMaterialBatches;

namespace WineQuality.Application.Models.InternalCommunication;

public class ReadingsMessage
{
    public string DeviceId { get; set; }
    public WineMaterialBatchGrapeSortPhaseParameterValueResult Value { get; set; }
}