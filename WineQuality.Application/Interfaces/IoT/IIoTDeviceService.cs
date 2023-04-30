using WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;

namespace WineQuality.Application.Interfaces.IoT;

public interface IIoTDeviceService
{
    Task<string> AddDeviceAsync(CreateProcessPhaseParameterSensorRequest request, CancellationToken cancellationToken = default);
}