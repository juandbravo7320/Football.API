using Football.Modules.Leagues.Application.Players.CreatePlayer;
using Football.Modules.Leagues.IntergrationTests.Abstractions;

namespace Football.Modules.Leagues.IntergrationTests.Players;

public class CreatePlayerTests : BaseIntegrationTest
{
    public CreatePlayerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenPlayerIsCreated()
    {
        // Arrange
        var request = new CreatePlayerCommand(
            Faker.Name.FullName());
        
        // Act
        var createPlayerResult = await Sender.Send(request);

        // Assert
        Assert.True(createPlayerResult.IsSuccess);
    }
}