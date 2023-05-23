using Microsoft.AspNetCore.SignalR;

namespace WineQuality.API.Hubs;

/// <summary>
/// 
/// </summary>
public class WineQualityHub : Hub
{
    // /// <summary>
    // /// 
    // /// </summary>
    // /// <param name="deviceId"></param>
    // /// <param name="message"></param>
    // public async Task SendMessage(string deviceId, string message)
    // {
    //     await Clients.All.SendAsync("ReceiveMessage", deviceId, message);
    // }
}