using Football.Modules.Leagues.Application.Abstractions.Jobs;
using Football.Modules.Leagues.Application.Matches.MatchAlignmentNotification;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Football.Modules.Leagues.Infrastructure.Abstractions;

public class JobScheduler(ILogger<JobScheduler> logger) : IJobScheduler
{
    public void ScheduleMatchAlignmentNotification(int matchId, DateTime matchStartTime)
    {
        var executionTime = matchStartTime.AddMinutes(-1);

        logger.LogInformation("Scheduling Job MatchAlignmentNotificationJob");
        
        BackgroundJob.Schedule<IMatchAlignmentNotificationJob>(
            job => job.ExecuteAsync(matchId),
            executionTime
        );
    }
}