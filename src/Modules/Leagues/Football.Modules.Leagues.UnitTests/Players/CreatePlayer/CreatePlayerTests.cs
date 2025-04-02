using Bogus;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Application.Players.CreatePlayer;
using Football.Modules.Leagues.Domain.Players;
using NSubstitute;

namespace Football.Modules.Leagues.UnitTests.Players.CreatePlayer;

public class CreatePlayerTests
{
    private readonly Faker Faker = new();
    private readonly IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly IPlayerRepository playerRepositoryMock = Substitute.For<IPlayerRepository>();

    private readonly CreatePlayerCommandHandler _handler;

    public CreatePlayerTests()
    {
        _handler = new CreatePlayerCommandHandler(
            unitOfWorkMock,
            playerRepositoryMock);
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenUserIsCreated()
    {
        // Arrange
        var request = new CreatePlayerCommand(Faker.Name.FullName());

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await playerRepositoryMock.Received(1).Add(Arg.Is<Player>(p => p.Name == request.Name));
        await unitOfWorkMock.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}