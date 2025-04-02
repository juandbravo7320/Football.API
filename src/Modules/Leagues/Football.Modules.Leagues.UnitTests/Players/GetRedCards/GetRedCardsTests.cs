using Bogus;
using Football.Modules.Leagues.Application.Players.GetRedCards;
using Football.Modules.Leagues.Application.Players.GetYellowCards;
using Football.Modules.Leagues.Domain.Players;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Football.Modules.Leagues.UnitTests.Players.GetRedCards;

public class GetRedCardsTests
{
    private readonly Faker Faker = new();
    private readonly IPlayerReadRepository playerReadRepositoryMock = Substitute.For<IPlayerReadRepository>();
    private readonly GetRedCardsQueryHandler _handler;

    public GetRedCardsTests()
    {
        _handler = new GetRedCardsQueryHandler(playerReadRepositoryMock);
    }
    
    [Fact]
    public async Task Should_ReturnError_WhenPlayerDoesNotExist()
    {
        // Arrange
        var request = new GetRedCardsQuery(Faker.Random.Int(0));
        playerReadRepositoryMock.QuerySingleOrDefaultAsync<RedCardResponse>(Arg.Any<string>(), Arg.Any<object>()).ReturnsNull();

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
        var request = new GetRedCardsQuery(playerId);

        var redCardResponse = new RedCardResponse(Faker.Random.Int(0));
        
        playerReadRepositoryMock.QuerySingleOrDefaultAsync<RedCardResponse>(Arg.Any<string>(), Arg.Any<object>()).Returns(redCardResponse);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(redCardResponse, result.Value);
    }
}