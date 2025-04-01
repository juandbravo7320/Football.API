namespace Football.Modules.Leagues.Application.Abstractions.Jobs;

public interface IJobScheduler
{
    void ScheduleMatchAlignmentNotification(int matchId, DateTime matchStartTime);
}