using Microsoft.AspNetCore.SignalR;
using WineQuality.API.Constants;
using WineQuality.API.Hubs;
using WineQuality.Application.Interfaces.Infrastructure;
using WineQuality.Application.Models.InternalCommunication;

namespace WineQuality.API.Infrastructure;

/// <inheritdoc cref="IInternalCommunicator" />
public class SignalRInternalCommunicator : IInternalCommunicator
{
    private readonly IHubContext<WineQualityHub> _hubContext;

    /// <summary>
    /// Preferable DI constructor.
    /// </summary>
    /// <param name="hubContext">For sending messages via SignalR.</param>
    public SignalRInternalCommunicator(IHubContext<WineQualityHub> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <inheritdoc cref="IInternalCommunicator" />
    public Task SendDeviceStatusUpdatedMessageAsync(SensorStatusUpdatedMessage message,
        CancellationToken cancellationToken = default)
    {
        return _hubContext.Clients.All.SendAsync(SignalRMessageTypes.SensorStatusUpdated, message, cancellationToken);
    }

    /// <inheritdoc cref="IInternalCommunicator" />
    public Task SendReadingsMessageAsync(ReadingsMessage message, CancellationToken cancellationToken = default)
    {
        return _hubContext.Clients.All.SendAsync(SignalRMessageTypes.Readings, message, cancellationToken);
    }
}