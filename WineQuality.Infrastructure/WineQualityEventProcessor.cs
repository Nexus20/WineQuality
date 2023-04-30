using System.Text;
using Azure.Messaging.EventHubs.Processor;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Infrastructure.IoT;
using DeviceStatus = WineQuality.Domain.Enums.DeviceStatus;

namespace WineQuality.Infrastructure;

public class WineQualityEventProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ServiceClient _serviceClient;
    private readonly ILogger<WineQualityEventProcessor> _logger;

    public WineQualityEventProcessor(IServiceScopeFactory serviceScopeFactory, ServiceClient serviceClient,
        ILogger<WineQualityEventProcessor> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _serviceClient = serviceClient;
        _logger = logger;
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

                    // TODO: Handle wrong device id

                    if (device.WineMaterialBatchGrapeSortPhaseParameter != null)
                    {
                        var grapeSortPhaseId = device.WineMaterialBatchGrapeSortPhaseParameter
                            .WineMaterialBatchGrapeSortPhase.GrapeSortPhaseId;

                        var standard = await unitOfWork.GrapeSortProcessPhaseParameterStandards.FirstOrDefaultAsync(x =>
                            x.PhaseParameterId == device.PhaseParameterId && x.GrapeSortPhaseId == grapeSortPhaseId);

                        var standardsUpdateMessage = new StandardsUpdateMessage
                        {
                            UpperBound = standard.UpperBound,
                            LowerBound = standard.LowerBound
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
            }

            if (messageType == MessageType.Readings)
            {
            }
        }


        Console.WriteLine($"Message received. Partition: {args.Partition.PartitionId} Data: '{message}'");

        // Обработка сообщений в зависимости от их типов
        // ...

        await args.UpdateCheckpointAsync(args.CancellationToken);
    }

    public async Task OnProcessingErrorAsync(ProcessErrorEventArgs args)
    {
        _logger.LogError("Error in operation \'{ArgsOperation}\': {ExceptionMessage}", args.Operation,
            args.Exception.Message);
        Console.WriteLine($"Error in operation '{args.Operation}': {args.Exception.Message}");
        await Task.CompletedTask;
    }
}