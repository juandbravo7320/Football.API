using Football.Modules.Leagues.Application.Players.CreatePlayer;
using Football.Modules.Leagues.Application.Players.GetYellowCards;
using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.IntergrationTests.Abstractions;

namespace Football.Modules.Leagues.IntergrationTests.Players;

public class GetYellowCardsTests : BaseIntegrationTest
{
    public GetYellowCardsTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Should_ReturnError_WhenPlayerDoesNotExist()
    {
        // Arrange
        var playerId = Faker.Random.Int(0);
        var request = new GetYellowCardsQuery(playerId);

        // Act
        var result = await Sender.Send(request);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(PlayerErrors.NotFound(request.PlayerId), result.Error);
    }
    
    [Fact]
    public async Task Should_ReturnYellowCards_WhenPlayerExist()
    {
        // Arrange
        var createPlayerResult = await Sender.Send(
            new CreatePlayerCommand(
                Faker.Name.FullName()));
        
        var playerId = createPlayerResult.Value;

        // Act
        var result = await Sender.Send(new GetYellowCardsQuery(playerId));

        // Assert
        Assert.True(result.IsSuccess);
    }
}