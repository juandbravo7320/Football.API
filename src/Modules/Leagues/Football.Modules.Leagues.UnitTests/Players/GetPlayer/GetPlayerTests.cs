using Bogus;
using Football.Modules.Leagues.Application.Players.GetPlayer;
using Football.Modules.Leagues.Application.Players.GetPlayers;
using Football.Modules.Leagues.Domain.Players;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Football.Modules.Leagues.UnitTests.Players.GetPlayer;

public class GetPlayerTests
{
    private readonly Faker Faker = new();
    private readonly IPlayerReadRepository playerReadRepositoryMock = Substitute.For<IPlayerReadRepository>();

    private readonly GetPlayerQueryHandler _handler;

    public GetPlayerTests()
    {
        _handler = new GetPlayerQueryHandler(playerReadRepositoryMock);
    }
    
    [Fact]
    public async Task Should_ReturnError_WhenPlayerDoesNotExist()
    {
        // Arrange
        var request = new GetPlayerQuery(Faker.Random.Int(0));
        playerReadRepositoryMock.QuerySingleOrDefaultAsync<PlayerResponse>(Arg.Any<string>(), Arg.Any<object>()).ReturnsNull();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(PlayerErrors.NotFound(request.Id), result.Error);
    }
    
    [Fact]
    public async Task Should_ReturnPlayer_WhenPlayerExist()
    {
        // Arrange
        var playerId = Faker.Random.Int(0);
        var request = new GetPlayerQuery(playerId);

        var player = new PlayerResponse(
            playerId,
            Faker.Name.FullName(),
            Faker.Random.Int(0),
            Faker.Random.Int(0),
            Faker.Random.Int(0));
        
        playerReadRepositoryMock.QuerySingleOrDefaultAsync<PlayerResponse>(Arg.Any<string>(), Arg.Any<object>()).Returns(player);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(player, result.Value);
    }
}