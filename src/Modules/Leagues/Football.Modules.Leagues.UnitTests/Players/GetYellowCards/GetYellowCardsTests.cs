using Bogus;
using Football.Modules.Leagues.Application.Players.GetYellowCards;
using Football.Modules.Leagues.Domain.Players;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Football.Modules.Leagues.UnitTests.Players.GetYellowCards;

public class GetYellowCardsTests
{
    private readonly Faker Faker = new();
    private readonly IPlayerReadRepository playerReadRepositoryMock = Substitute.For<IPlayerReadRepository>();
    private readonly GetYellowCardsQueryHandler _handler;

    public GetYellowCardsTests()
    {
        _handler = new GetYellowCardsQueryHandler(playerReadRepositoryMock);
    }
    
    [Fact]
    public async Task Should_ReturnError_WhenPlayerDoesNotExist()
    {
        // Arrange
        var request = new GetYellowCardsQuery(Faker.Random.Int(0));
        playerReadRepositoryMock.QuerySingleOrDefaultAsync<YellowCardResponse>(Arg.Any<string>(), Arg.Any<object>()).ReturnsNull();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(PlayerErrors.NotFound(request.PlayerId), result.Error);
    }
    
    [Fact]
    public async Task Should_ReturnRedCards_WhenPlayerExist()
    {
        // Arrange
        var playerId = Faker.Random.Int(0);
        var request = new GetYellowCardsQuery(playerId);

        var yellowCardResponse = new YellowCardResponse(Faker.Random.Int(0));
        
        playerReadRepositoryMock.QuerySingleOrDefaultAsync<YellowCardResponse>(Arg.Any<string>(), Arg.Any<object>()).Returns(yellowCardResponse);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(yellowCardResponse, result.Value);
    }
}