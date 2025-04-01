using Football.Common.Application.Notification;
using Football.Common.Presentation.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Football.Common.Infrastructure.Notification;

public class SignalRNotificationService(IHubContext<MatchHub> matchHub) : INotificationService
{
    public async Task NotifyMatchStartingSoonAsync(int matchId, string message)
    {
        await matchHub.Clients.All.SendAsync("MatchStartingSoon", new
        {
            MatchId = matchId,
            Message = message
        });
    }
}