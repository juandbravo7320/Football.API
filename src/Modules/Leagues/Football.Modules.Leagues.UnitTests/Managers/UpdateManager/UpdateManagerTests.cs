using Bogus;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Application.Managers.UpdateManager;
using Football.Modules.Leagues.Domain.Managers;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Football.Modules.Leagues.UnitTests.Managers.UpdateManager;

public class UpdateManagerTests
{
    private readonly Faker Faker = new(); 
    private readonly IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly IManagerRepository managerRepositoryMock = Substitute.For<IManagerRepository>();
    private readonly UpdateManagerCommandHandler _handler;

    public UpdateManagerTests()
    {
        _handler = new UpdateManagerCommandHandler(
            unitOfWorkMock,
            managerRepositoryMock);
    }

    [Fact]
    public async Task Should_ReturnError_WhenManagerDoesNotExist()
    {
        // Arrange
        var request = new UpdateManagerCommand(
            Faker.Random.Int(),
            Faker.Name.FullName(),
            Faker.Random.Int(),
            Faker.Random.Int());

        managerRepositoryMock.FindByIdAsync(request.Id).ReturnsNull();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(ManagerErrors.NotFound(request.Id), result.Error);
    }
    
    [Fact]
    public async Task Should_ReturnSuccess_WhenManagerIsUpdateSuccessfully()
    {
        // Arrange
        var existentManager = new Manager()
        {
            Id = Faker.Random.Int(),
            Name = Faker.Name.FullName(),
            YellowCard = Faker.Random.Int(),
            RedCard = Faker.Random.Int()
        };
        
        var request = new UpdateManagerCommand(
            Faker.Random.Int(),
            Faker.Name.FullName(),
            Faker.Random.Int(),
            Faker.Random.Int());

        managerRepositoryMock.FindByIdAsync(request.Id).Returns(existentManager);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await unitOfWorkMock.Received(1).SaveChangesAsync(CancellationToken.None);
        Assert.Equal(existentManager.Id, result.Value);
    }
}