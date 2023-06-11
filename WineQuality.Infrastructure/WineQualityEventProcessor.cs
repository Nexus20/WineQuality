using System.Text;
using AutoMapper;
using Azure.Messaging.EventHubs.Processor;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WineQuality.Application.Interfaces.Infrastructure;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Application.Models.InternalCommunication;
using WineQuality.Application.Models.Results.WineMaterialBatches;
using WineQuality.Domain.Entities;
using WineQuality.Infrastructure.IoT;
using DeviceStatus = WineQuality.Domain.Enums.DeviceStatus;

namespace WineQuality.Infrastructure;

public class WineQualityEventProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ServiceClient _serviceClient;
    private readonly ILogger<WineQualityEventProcessor> _logger;
    private readonly IInternalCommunicator _internalCommunicator;
    private readonly IMapper _mapper;

    public WineQualityEventProcessor(IServiceScopeFactory serviceScopeFactory, ServiceClient serviceClient,
        ILogger<WineQualityEventProcessor> logger, IInternalCommunicator internalCommunicator, IMapper mapper)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _serviceClient = serviceClient;
        _logger = logger;
        _internalCommunicator = internalCommunicator;
        _mapper = mapper;
    }

    public async Task OnProcessingEventBatchAsync(ProcessEventArgs args)
    {
        var message = Encoding.UTF8.GetString(args.Data.EventBody.ToArray());

        if (!args.Data.Properties.ContainsKey("DeviceId"))
            return;

        var deviceId = args.Data.Properties["DeviceId"].ToString();

        if (args.Data.Properties.ContainsKey(nameof(MessageType)))
        {
            var messageType = Enum.Parse<MessageType>(args.Data.Properties[nameof(MessageType)].ToString()!);

            if (messageType == MessageType.StatusUpdate)
            {
                var statusUpdateMessage = JsonConvert.DeserializeObject<StatusUpdateMessage>(message);
                
                using var scope = _serviceScopeFactory.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var device = await unitOfWork.ProcessPhaseParameterSensors.GetByIdAsync(deviceId);

                if (statusUpdateMessage.Status == DeviceStatus.Ready)
                {

                    if (device.WineMaterialBatchGrapeSortPhaseParameter != null)
                    {
                        var grapeSortPhaseId = device.WineMaterialBatchGrapeSortPhaseParameter
                            .WineMaterialBatchGrapeSortPhase.GrapeSortPhaseId;

                        var standard = await unitOfWork.GrapeSortProcessPhaseParameterStandards.FirstOrDefaultAsync(x =>
                            x.PhaseParameterId == device.PhaseParameterId && x.GrapeSortPhaseId == grapeSortPhaseId);

                        var standardsUpdateMessage = new StandardsUpdateMessage
                        {
                            UpperBound = standard.UpperBound,
                            LowerBound = standard.LowerBound,
                            PhaseName = standard.PhaseParameter.Phase.Name,
                            ParameterName = standard.PhaseParameter.Parameter.Name,
                            GrapeSortName = standard.GrapeSortPhase.GrapeSort.Name,
                            WineMaterialBatchId = device.WineMaterialBatchGrapeSortPhaseParameter
                                .WineMaterialBatchGrapeSortPhase.WineMaterialBatchId,
                            WineMaterialBatchName = device.WineMaterialBatchGrapeSortPhaseParameter
                                .WineMaterialBatchGrapeSortPhase.WineMaterialBatch.Name
                        };

                        var json = JsonConvert.SerializeObject(standardsUpdateMessage);
                        var responseMessage = new Message(Encoding.UTF8.GetBytes(json));
                        responseMessage.Properties.Add(nameof(MessageType), MessageType.Standards.ToString());
                        await _serviceClient.SendAsync(deviceId, responseMessage);
                    }
                }
                
                device.Status = statusUpdateMessage.Status;
                unitOfWork.ProcessPhaseParameterSensors.Update(device);
                await unitOfWork.SaveChangesAsync();

                await _internalCommunicator.SendDeviceStatusUpdatedMessageAsync(new SensorStatusUpdatedMessage()
                {
                    DeviceId = deviceId,
                    NewStatus = statusUpdateMessage.Status
                });
            }

            if (messageType == MessageType.Readings)
            {
                var readingsMessage = JsonConvert.DeserializeObject<SensorReadingsMessage>(message);
                
                using var scope = _serviceScopeFactory.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var device = await unitOfWork.ProcessPhaseParameterSensors.GetByIdAsync(deviceId);
                var parameter = device.WineMaterialBatchGrapeSortPhaseParameter;

                var valueToAdd = new WineMaterialBatchGrapeSortPhaseParameterValue()
                {
                    PhaseParameter = parameter,
                    PhaseParameterId = device.WineMaterialBatchGrapeSortPhaseParameterId,
                    Value = readingsMessage.Value
                };

                await unitOfWork.WineMaterialBatchGrapeSortPhaseParameterValues.AddAsync(valueToAdd);
                await unitOfWork.SaveChangesAsync();
                
                var valueToSend = _mapper.Map<WineMaterialBatchGrapeSortPhaseParameterValue, WineMaterialBatchGrapeSortPhaseParameterValueResult>(valueToAdd);
                
                await _internalCommunicator.SendReadingsMessageAsync(new ReadingsMessage()
                {
                    WineMaterialBatchId = parameter.WineMaterialBatchGrapeSortPhase.WineMaterialBatchId,
                    DeviceId = deviceId,
                    Value = valueToSend,
                    ParameterId = valueToSend.PhaseParameter.Id,
                    ParameterName = parameter?.PhaseParameter.Parameter.Name,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        Console.WriteLine($"Message received. Partition: {args.Partition.PartitionId} Data: '{message}'");

        await args.UpdateCheckpointAsync(args.CancellationToken);
    }

    public Task OnProcessingErrorAsync(ProcessErrorEventArgs args)
    {
        _logger.LogError("Error in operation \'{ArgsOperation}\': {ExceptionMessage}", args.Operation,
            args.Exception.Message);
        Console.WriteLine($"Error in operation '{args.Operation}': {args.Exception.Message}");
        return Task.CompletedTask;
    }
}