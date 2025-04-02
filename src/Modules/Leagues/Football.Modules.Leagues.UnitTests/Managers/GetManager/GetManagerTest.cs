using Bogus;
using Football.Modules.Leagues.Application.Managers.GetManager;
using Football.Modules.Leagues.Application.Managers.GetManagers;
using Football.Modules.Leagues.Domain.Managers;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Football.Modules.Leagues.UnitTests.Managers.GetManager;

public class GetManagerTest
{
    private readonly Faker Faker = new();
    private readonly IManagerReadRepository managerReadRepository = Substitute.For<IManagerReadRepository>();
    private readonly GetManagerQueryHandler _handler;

    public GetManagerTest()
    {
        _handler = new GetManagerQueryHandler(managerReadRepository);
    }

    [Fact]
    public async Task Should_ReturnError_WhenManagerDoesNotExist()
    {
        // Arrange
        var request = new GetManagerQuery(Faker.Random.Int());
        managerReadRepository.QuerySingleOrDefaultAsync<ManagerResponse>(Arg.Any<string>(), Arg.Any<object>()).ReturnsNull();

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
        var managerId = Faker.Random.Int(0);
        var existentManager = new ManagerResponse(
            managerId,
            Faker.Name.FullName(),
            Faker.Random.Int(0),
            Faker.Random.Int(0));
        
        var request = new GetManagerQuery(managerId);
        managerReadRepository.QuerySingleOrDefaultAsync<ManagerResponse>(Arg.Any<string>(), Arg.Any<object>()).Returns(existentManager);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }
}