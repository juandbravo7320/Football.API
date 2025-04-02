using Football.Modules.Leagues.Application.Players.CreatePlayer;
using Football.Modules.Leagues.Application.Players.GetRedCards;
using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.IntergrationTests.Abstractions;

namespace Football.Modules.Leagues.IntergrationTests.Players;

public class GetRedCardsTests : BaseIntegrationTest
{
    public GetRedCardsTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnError_WhenPlayerDoesNotExist()
    {
        // Arrange
        var playerId = Faker.Random.Int(0);
        var request = new GetRedCardsQuery(playerId);

        // Act
        var result = await Sender.Send(request);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(PlayerErrors.NotFound(request.PlayerId), result.Error);
    }
    
    [Fact]
    public async Task Should_ReturnRedCards_WhenPlayerExist()
    {
        // Arrange
        var createPlayerResult = await Sender.Send(
            new CreatePlayerCommand(
                Faker.Name.FullName()));
        
        var playerId = createPlayerResult.Value;

        // Act
        var result = await Sender.Send(new GetRedCardsQuery(playerId));

        // Assert
        Assert.True(result.IsSuccess);
    }
}