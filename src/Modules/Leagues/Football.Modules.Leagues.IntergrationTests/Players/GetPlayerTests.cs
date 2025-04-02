using Football.Modules.Leagues.Application.Players.CreatePlayer;
using Football.Modules.Leagues.Application.Players.GetPlayer;
using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.IntergrationTests.Abstractions;

namespace Football.Modules.Leagues.IntergrationTests.Players;

public class GetPlayerTests : BaseIntegrationTest
{
    public GetPlayerTests(IntegrationTestWebAppFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnError_WhenPlayerDoesNotExist()
    {
        // Arrange
        var playerId = Faker.Random.Int(0);
        var request = new GetPlayerQuery(playerId);

        // Act
        var result = await Sender.Send(request);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(PlayerErrors.NotFound(playerId), result.Error);
    }
    
    [Fact]
    public async Task Should_ReturnSuccess_WhenPlayerExist()
    {
        // Arrange
        var result = await Sender.Send(
            new CreatePlayerCommand(
                Faker.Name.FullName()));
        var playerId = result.Value;

        // Act
        var playerResult = await Sender.Send(new GetPlayerQuery(playerId));

        // Assert
        Assert.True(playerResult.IsSuccess);
        Assert.NotNull(playerResult.Value);
    }
}