using Football.Modules.Leagues.Application.Players.CreatePlayer;
using Football.Modules.Leagues.Application.Players.GetMinutesPlayed;
using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.IntergrationTests.Abstractions;

namespace Football.Modules.Leagues.IntergrationTests.Players;

public class GetMinutesPlayedTests : BaseIntegrationTest
{
    public GetMinutesPlayedTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnError_WhenPlayerDoesNotExist()
    {
        // Arrange
        var playerId = Faker.Random.Int(0);
        var request = new GetMinutesPlayedQuery(playerId);

        // Act
        var result = await Sender.Send(request);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(PlayerErrors.NotFound(playerId), result.Error);
    }
    
    [Fact]
    public async Task Should_ReturnPlayerMinutesPlayed_WhenPlayerExist()
    {
        // Arrange
        var result = await Sender.Send(
            new CreatePlayerCommand(
                Faker.Name.FullName()));
        var playerId = result.Value;

        // Act
        var playerResult = await Sender.Send(new GetMinutesPlayedQuery(playerId));

        // Assert
        Assert.True(playerResult.IsSuccess);
        Assert.NotNull(playerResult.Value);
    }
}