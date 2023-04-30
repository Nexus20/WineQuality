using System.Text;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using WineQuality.Application.Exceptions;
using WineQuality.Application.Interfaces.IoT;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Models.Requests.ProcessPhaseParameterSensors;
using WineQuality.Domain.Entities;
using DeviceStatus = WineQuality.Domain.Enums.DeviceStatus;

namespace WineQuality.Infrastructure.IoT;

public class AzureIoTDeviceService : IIoTDeviceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RegistryManager _registryManager;
    private readonly ServiceClient _serviceClient;

    public AzureIoTDeviceService(RegistryManager registryManager, IUnitOfWork unitOfWork, ServiceClient serviceClient)
    {
        _registryManager = registryManager;
        _unitOfWork = unitOfWork;
        _serviceClient = serviceClient;
    }

    public async Task<string> AddDeviceAsync(CreateProcessPhaseParameterSensorRequest request, CancellationToken cancellationToken = default)
    {
        var device = await _registryManager.AddDeviceAsync(new Device(request.DeviceId), cancellationToken);

        return device.Authentication.SymmetricKey.PrimaryKey;
    }

    public async Task RunSensorAsync(string sensorId, CancellationToken cancellationToken = default)
    {
        var sensor = await _unitOfWork.ProcessPhaseParameterSensors.GetByIdAsync(sensorId, cancellationToken);

        if (sensor == null)
            throw new NotFoundException(nameof(ProcessPhaseParameterSensor), nameof(sensorId));

        if (sensor.Status != DeviceStatus.BoundariesUpdated)
            throw new InvalidSensorStatusException("Can't run sensor when its parameter boundaries haven't been updated");
        
        var statusUpdateMessage = new StatusUpdateMessage(DeviceStatus.Working);
        var json = JsonConvert.SerializeObject(statusUpdateMessage);
        var responseMessage = new Message(Encoding.UTF8.GetBytes(json));
        responseMessage.Properties.Add(nameof(MessageType), MessageType.StatusUpdate.ToString());
        await _serviceClient.SendAsync(sensorId, responseMessage);
    }

    public async Task StopSensorAsync(string sensorId, CancellationToken cancellationToken = default)
    {
        var sensor = await _unitOfWork.ProcessPhaseParameterSensors.GetByIdAsync(sensorId, cancellationToken);

        if (sensor == null)
            throw new NotFoundException(nameof(ProcessPhaseParameterSensor), nameof(sensorId));
        
        if (sensor.Status != DeviceStatus.Working)
            throw new InvalidSensorStatusException("Can't stop sensor because it is not running");
        
        var statusUpdateMessage = new StatusUpdateMessage(DeviceStatus.Stopped);
        var json = JsonConvert.SerializeObject(statusUpdateMessage);
        var responseMessage = new Message(Encoding.UTF8.GetBytes(json));
        responseMessage.Properties.Add(nameof(MessageType), MessageType.StatusUpdate.ToString());
        await _serviceClient.SendAsync(sensorId, responseMessage);
    }
}