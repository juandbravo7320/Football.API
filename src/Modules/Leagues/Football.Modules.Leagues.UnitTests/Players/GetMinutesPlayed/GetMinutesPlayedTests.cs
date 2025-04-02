using Bogus;
using Football.Modules.Leagues.Application.Players.GetMinutesPlayed;
using Football.Modules.Leagues.Application.Players.GetPlayers;
using Football.Modules.Leagues.Application.Players.GetYellowCards;
using Football.Modules.Leagues.Domain.Players;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Football.Modules.Leagues.UnitTests.Players.GetMinutesPlayed;

public class GetMinutesPlayedTests
{
    private readonly Faker Faker = new();
    private readonly IPlayerReadRepository playerReadRepositoryMock = Substitute.For<IPlayerReadRepository>();

    private readonly GetMinutesPlayedQueryHandler _handler;

    public GetMinutesPlayedTests()
    {
        _handler = new GetMinutesPlayedQueryHandler(playerReadRepositoryMock);
    }

    [Fact]
    public async Task Should_ReturnError_WhenPlayerDoesNotExist()
    {
        // Arrange
        var request = new GetMinutesPlayedQuery(Faker.Random.Int(0));
        playerReadRepositoryMock.QuerySingleOrDefaultAsync<MinutesPlayedResponse>(Arg.Any<string>(), Arg.Any<object>()).ReturnsNull();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(PlayerErrors.NotFound(request.PlayerId), result.Error);
    }
    
    [Fact]
    public async Task Should_ReturnSuccess_WhenPlayerIsCreatedSuccessfully()
    {
        // Arrange
        var playerId = Faker.Random.Int(0);
        var request = new GetMinutesPlayedQuery(playerId);

        var existentPlayer = new PlayerResponse(
            playerId,
            Faker.Name.FullName(),
            Faker.Random.Int(0),
            Faker.Random.Int(0),
            Faker.Random.Int(0)
        );

        var minutesPlayedResponse = new MinutesPlayedResponse(existentPlayer.MinutesPlayed);
        
        playerReadRepositoryMock.QuerySingleOrDefaultAsync<MinutesPlayedResponse>(Arg.Any<string>(), Arg.Any<object>()).Returns(minutesPlayedResponse);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(existentPlayer.MinutesPlayed, result.Value.Quantity);
    }
}