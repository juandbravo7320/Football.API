using Football.Modules.Leagues.Application.Players.CreatePlayer;
using Football.Modules.Leagues.Application.Players.UpdatePlayer;
using Football.Modules.Leagues.Domain.Players;
using Football.Modules.Leagues.IntergrationTests.Abstractions;

namespace Football.Modules.Leagues.IntergrationTests.Players;

public class UpdatePlayerTests : BaseIntegrationTest
{
    public UpdatePlayerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnError_WhenPlayerDoesNotExist()
    {
        // Arrange
        var playerId = Faker.Random.Int(0);

        // Act
        var updateResult = await Sender.Send(
            new UpdatePlayerCommand(
                playerId, 
                Faker.Name.FirstName(), 
                Faker.Random.Int(0),
                Faker.Random.Int(0),
                Faker.Random.Int(0)));

        // Assert
        Assert.True(updateResult.IsFailure);
        Assert.Equal(PlayerErrors.NotFound(playerId), updateResult.Error);
    }
    
    [Fact]
    public async Task Should_ReturnSuccess_WhenPlayerExist()
    {
        // Arrange
        var createPlayerResult = await Sender.Send(
            new CreatePlayerCommand(
                Faker.Name.FullName()));
            
        var playerId = createPlayerResult.Value;

        // Act
        var updateResult = await Sender.Send(
            new UpdatePlayerCommand(
                playerId, 
                Faker.Name.FirstName(), 
                Faker.Random.Int(0),
                Faker.Random.Int(0),
                Faker.Random.Int(0)));

        // Assert
        Assert.True(updateResult.IsSuccess);
        Assert.Equal(playerId, updateResult.Value);
    }
}