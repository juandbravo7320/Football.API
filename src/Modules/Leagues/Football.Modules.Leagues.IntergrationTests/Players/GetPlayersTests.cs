using Football.Modules.Leagues.Application.Players.CreatePlayer;
using Football.Modules.Leagues.Application.Players.GetPlayers;
using Football.Modules.Leagues.IntergrationTests.Abstractions;

namespace Football.Modules.Leagues.IntergrationTests.Players;

public class GetPlayersTests : BaseIntegrationTest
{
    public GetPlayersTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Should_ReturnPlayersList_WhenExistPlayers()
    {
        // Arrange
        var createPlayerResult1 = await Sender.Send(
            new CreatePlayerCommand(
                Faker.Name.FullName()));
        var playerId1 = createPlayerResult1.Value;

        // Act
        var result = await Sender.Send(new GetPlayersQuery());

        // Assert
        Assert.True(result.IsSuccess);
    }
}