using WineQuality.Domain.Enums;

namespace WineQuality.Application.Models.InternalCommunication;

public class SensorStatusUpdatedMessage
{
    public string DeviceId { get; set; } = null!;
    public DeviceStatus NewStatus { get; set; }
}