using System.Net;
using Bogus;
using Football.Common.Application.Notification;
using Football.Modules.Leagues.Domain.Matches;
using Football.Modules.Leagues.Infrastructure.Matches.Jobs;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Football.Modules.Leagues.UnitTests.Matches.Jobs;

public class MatchAlignmentNotificationJobTests
{
    private readonly Faker Faker = new();
    
    private readonly IMatchRepository matchRepositoryMock = Substitute.For<IMatchRepository>();
    private readonly INotificationService matchNotificationServiceMock = Substitute.For<INotificationService>();
    private readonly ILogger<MatchAlignmentNotificationJob> loggerMock = Substitute.For<ILogger<MatchAlignmentNotificationJob>>();
    
    [Fact]
    public async Task Should_LogWarning_WhenStatusCodeIsNotSuccess()
    {
        // Arrange
        var matchId = Faker.Random.Int(0);
        var match = new Match()
        {
            Id = matchId,
            HouseManagerId = Faker.Random.Int(0),
            AwayManagerId = Faker.Random.Int(0),
            RefereeId = Faker.Random.Int(0),
            StartsAtUtc = DateTime.UtcNow.AddMinutes(1)
        };

        matchRepositoryMock.FindByIdAsync(matchId).Returns(match);
        
        var httpResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = new StringContent("")
        };
        var httpClientMock = new HttpClient(new FakeHttpMessageHandler(httpResponse))
        {
            BaseAddress = new Uri("http://interview-api.azurewebsites.net")
        };
        
        var job = new MatchAlignmentNotificationJob(
            httpClientMock,
            matchRepositoryMock,
            matchNotificationServiceMock,
            loggerMock);

        // Act
        await job.ExecuteAsync(matchId);
        
        httpResponse.Dispose();
        httpClientMock.Dispose();

        // Assert
        await matchNotificationServiceMock
            .DidNotReceiveWithAnyArgs()
            .NotifyMatchStartingSoonAsync(default, default!);
        
        loggerMock.Received().Log(
            LogLevel.Warning,
            Arg.Any<EventId>(),
            Arg.Is<object>(v => v.ToString()!.Contains("Failed alignment check for match")),
            null,
            Arg.Any<Func<object, Exception?, string>>());
    }
    
    [Fact]
    public async Task Should_LogInformation_WhenStatusCodeIsSuccess()
    {
        // Arrange
        var matchId = Faker.Random.Int(0);
        var match = new Match()
        {
            Id = matchId,
            HouseManagerId = Faker.Random.Int(0),
            AwayManagerId = Faker.Random.Int(0),
            RefereeId = Faker.Random.Int(0),
            StartsAtUtc = DateTime.UtcNow.AddMinutes(1)
        };

        matchRepositoryMock.FindByIdAsync(matchId).Returns(match);
        
        var httpResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("")
        };
        var httpClientMock = new HttpClient(new FakeHttpMessageHandler(httpResponse))
        {
            BaseAddress = new Uri("http://interview-api.azurewebsites.net")
        };
        
        var job = new MatchAlignmentNotificationJob(
            httpClientMock,
            matchRepositoryMock,
            matchNotificationServiceMock,
            loggerMock);

        // Act
        await job.ExecuteAsync(matchId);
        
        httpResponse.Dispose();
        httpClientMock.Dispose();

        // Assert
        await matchNotificationServiceMock
            .Received(1)
            .NotifyMatchStartingSoonAsync(match.Id, "Match starting soon");
        
        loggerMock.Received().Log(
            LogLevel.Information,
            Arg.Any<EventId>(),
            Arg.Is<object>(v => v.ToString()!.Contains("Alignment check for match")),
            null,
            Arg.Any<Func<object, Exception?, string>>());
    }
}