using Microsoft.Azure.Devices;
using WineQuality.Application.Interfaces.IoT;
using WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;

namespace WineQuality.Infrastructure.IoT;

public class AzureIoTDeviceService : IIoTDeviceService
{
    private readonly RegistryManager _registryManager;

    public AzureIoTDeviceService(RegistryManager registryManager)
    {
        _registryManager = registryManager;
    }

    public async Task<string> AddDeviceAsync(CreateProcessPhaseParameterSensorRequest request, CancellationToken cancellationToken = default)
    {
        var device = await _registryManager.AddDeviceAsync(new Device(request.DeviceId), cancellationToken);

        return device.Authentication.SymmetricKey.PrimaryKey;
    }
}