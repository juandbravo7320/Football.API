using Bogus;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Application.Players.UpdatePlayer;
using Football.Modules.Leagues.Domain.Players;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Football.Modules.Leagues.UnitTests.Players.UpdatePlayer;

public class UpdatePlayerTests
{
    private readonly Faker Faker = new();
    private readonly IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly IPlayerRepository playerRepositoryMock = Substitute.For<IPlayerRepository>();

    private readonly UpdatePlayerCommandHandler _handler;

    public UpdatePlayerTests()
    {
        _handler = new UpdatePlayerCommandHandler(
            unitOfWorkMock,
            playerRepositoryMock);
    }

    [Fact]
    public async Task Should_ReturnError_WhenPlayerNotExist()
    {
        // Arrange
        var request = new UpdatePlayerCommand(
            Faker.Random.Int(0),
            Faker.Name.FullName(),
            Faker.Random.Int(0),
            Faker.Random.Int(0),
            Faker.Random.Int(0));

        playerRepositoryMock.FindByIdAsync(request.Id).ReturnsNull();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(result.Error, PlayerErrors.NotFound(request.Id));
    }
    
    [Fact]
    public async Task Should_ReturnSuccess_WhenPlayerIsUpdated()
    {
        // Arrange
        var playerId = Faker.Random.Int(0);
        var request = new UpdatePlayerCommand(
            playerId,
            Faker.Name.FullName(),
            Faker.Random.Int(0),
            Faker.Random.Int(0),
            Faker.Random.Int(0));

        var player = new Player()
        {
            Id = playerId,
            Name = Faker.Name.FullName(),
            YellowCard = Faker.Random.Int(0),
            RedCard = Faker.Random.Int(0),
            MinutesPlayed = Faker.Random.Int(0)
        };
        
        playerRepositoryMock.FindByIdAsync(request.Id).Returns(player);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await unitOfWorkMock.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}