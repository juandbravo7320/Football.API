namespace Football.Common.Application.Notification;

public interface INotificationService
{
    Task NotifyMatchStartingSoonAsync(int matchId, string message);
}