using Microsoft.AspNetCore.SignalR;

namespace Football.Common.Presentation.Hubs;

public sealed class MatchHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
    }
    
    public async Task SendMatchStartsSoonNotificationToUser(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
}