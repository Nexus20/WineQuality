using WineQuality.Application.Models.InternalCommunication;

namespace WineQuality.Application.Interfaces.Infrastructure;

public interface IInternalCommunicator
{
    Task SendDeviceStatusUpdatedMessageAsync(SensorStatusUpdatedMessage message, CancellationToken cancellationToken = default);
    Task SendReadingsMessageAsync(ReadingsMessage readingsMessage, CancellationToken cancellationToken = default);
}