using Bogus;
using Football.Modules.Leagues.Application.Managers.GetManager;
using Football.Modules.Leagues.Domain.Managers;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Football.Modules.Leagues.UnitTests.Managers.GetManager;

public class GetManagerTest
{
    private readonly Faker Faker = new();
    private readonly IManagerRepository managerRepositoryMock = Substitute.For<IManagerRepository>();
    private readonly GetManagerQueryHandler _handler;

    public GetManagerTest()
    {
        _handler = new GetManagerQueryHandler(managerRepositoryMock);
    }

    [Fact]
    public async Task Should_ReturnError_WhenManagerDoesNotExist()
    {
        // Arrange
        var request = new GetManagerQuery(Faker.Random.Int());
        managerRepositoryMock.FindByIdAsync(request.Id).ReturnsNull();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(ManagerErrors.NotFound(request.Id), result.Error);
    }
    
    [Fact]
    public async Task Should_ReturnSuccess_WhenManagerExist()
    {
        // Arrange
        var existentManager = new Manager()
        {
            Id = Faker.Random.Int(),
            Name = Faker.Name.FullName(),
            YellowCard = Faker.Random.Int(),
            RedCard = Faker.Random.Int() 
        };
        
        var request = new GetManagerQuery(Faker.Random.Int());
        managerRepositoryMock.FindByIdAsync(request.Id).Returns(existentManager);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }
}