using System.Net.Http.Json;
using Football.Common.Application.Notification;
using Football.Modules.Leagues.Application.Matches.MatchAlignmentNotification;
using Football.Modules.Leagues.Domain.Matches;
using Microsoft.Extensions.Logging;

namespace Football.Modules.Leagues.Infrastructure.Matches.Jobs;

public class MatchAlignmentNotificationJob(
    HttpClient httpClient,
    IMatchRepository matchRepository,
    INotificationService matchNotificationService,
    ILogger<MatchAlignmentNotificationJob> logger) : IMatchAlignmentNotificationJob
{
    public async Task ExecuteAsync(int matchId)
    {
        var match = await matchRepository.FindByIdAsync(matchId);
        
        const string requestPath = "/api/IncorrectAlignment";

        var body = new List<int>
        {
            match.Id
        };

        HttpContent content = JsonContent.Create(body);

        var response = await httpClient.PostAsync(requestPath, content);
        
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            await matchNotificationService.NotifyMatchStartingSoonAsync(match.Id, "Match starting soon");
            logger.LogInformation("Alignment check for match {MatchId}: {Result}", matchId, result);
        }
        else
        {
            logger.LogWarning("Failed alignment check for match {MatchId}. Status: {StatusCode}", matchId, response.StatusCode);
        }
    }
}