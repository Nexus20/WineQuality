using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Storage.Blobs;
using Microsoft.Azure.Devices;
using WineQuality.Application.Interfaces.Infrastructure;
using WineQuality.Application.Interfaces.Persistence;
using WineQuality.Infrastructure;

namespace WineQuality.API.Extensions;

public static class HostExtensions
{
    public static IHost SetupIdentity(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IIdentityInitializer>();
        initializer.InitializeIdentityData();

        return host;
    }
    
    public static async Task StartEventProcessorAsync(this IHost host)
    {
        var configuration = host.Services.GetRequiredService<IConfiguration>();

        var storageConnectionString = configuration.GetValue<string>("IoTSettings:Storage");

        const string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
        var blobContainerClient = new BlobContainerClient(storageConnectionString, configuration.GetValue<string>("IoTSettings:LeaseContainerName"));
        var eventProcessorClient = new EventProcessorClient(blobContainerClient, consumerGroup, configuration.GetValue<string>("IoTSettings:EventHubsCompatiblePath"));

        var serviceScopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        var logger = host.Services.GetRequiredService<ILogger<WineQualityEventProcessor>>();
        var serviceClient = ServiceClient.CreateFromConnectionString(configuration.GetValue<string>("IoTSettings:HubConnectionString"));
        
        var processor = new WineQualityEventProcessor(serviceScopeFactory, serviceClient, logger);
            
        eventProcessorClient.ProcessEventAsync += processor.OnProcessingEventBatchAsync;
        eventProcessorClient.ProcessErrorAsync += processor.OnProcessingErrorAsync;

        await eventProcessorClient.StartProcessingAsync();
    }
}