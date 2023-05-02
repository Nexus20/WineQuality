using WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;
using WineQuality.Domain.Entities;

namespace WineQuality.Application.Interfaces.IoT;

public interface IIoTDeviceService
{
    Task<string> AddDeviceAsync(CreateProcessPhaseParameterSensorRequest request, CancellationToken cancellationToken = default);
    Task RunSensorAsync(ProcessPhaseParameterSensor sensor, CancellationToken cancellationToken = default);
    Task StopSensorAsync(ProcessPhaseParameterSensor sensorId, CancellationToken cancellationToken = default);
    Task SendStandardsUpdateMessageAsync(ProcessPhaseParameterSensor sensor, GrapeSortProcessPhaseParameterStandard standard, CancellationToken cancellationToken = default);
}