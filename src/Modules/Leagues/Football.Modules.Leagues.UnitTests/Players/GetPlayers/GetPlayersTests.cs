using Bogus;
using Football.Modules.Leagues.Application.Players.GetPlayers;
using Football.Modules.Leagues.Domain.Players;
using NSubstitute;

namespace Football.Modules.Leagues.UnitTests.Players.GetPlayers;

public class GetPlayersTests
{
    private readonly Faker Faker = new(); 
    private readonly IPlayerReadRepository playerReadRepositoryMock = Substitute.For<IPlayerReadRepository>();
    private readonly GetPlayersQueryHandler _handler;

    public GetPlayersTests()
    {
        _handler = new GetPlayersQueryHandler(playerReadRepositoryMock);
    }
    
    [Fact]
    public async Task Should_ReturnEmptyList_WhenThereIsNoPlayers()
    {
        // Arrange
        var request = new GetPlayersQuery();

        var players = new List<PlayerResponse>();
        playerReadRepositoryMock.QueryAsync<PlayerResponse>(Arg.Any<string>(), Arg.Any<object>()).Returns(players);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }

    [Fact]
    public async Task Should_ReturnPlayersList_WhenExistPlayers()
    {
        // Arrange
        var request = new GetPlayersQuery();

        var players = new List<PlayerResponse>
        {
            new PlayerResponse(
                Faker.Random.Int(0),
                Faker.Name.FullName(),
                Faker.Random.Int(0),
                Faker.Random.Int(0),
                Faker.Random.Int(0))
        };
        playerReadRepositoryMock.QueryAsync<PlayerResponse>(Arg.Any<string>(), Arg.Any<object>()).Returns(players);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(players.Count, result.Value.Count);
    }
}