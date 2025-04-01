namespace Football.Modules.Leagues.Application.Matches.MatchAlignmentNotification;

public interface IMatchAlignmentNotificationJob
{
    Task ExecuteAsync(int matchId);
}