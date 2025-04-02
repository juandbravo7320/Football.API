using Bogus;
using Football.Modules.Leagues.Application.Managers.GetManagers;
using Football.Modules.Leagues.Domain.Managers;
using NSubstitute;

namespace Football.Modules.Leagues.UnitTests.Managers.GetManagers;

public class GetManagersTests
{
    private readonly Faker Faker = new();
    private readonly IManagerReadRepository managerReadRepositoryMock = Substitute.For<IManagerReadRepository>();

    private readonly GetManagersQueryHandler _handler;

    public GetManagersTests()
    {
        _handler = new GetManagersQueryHandler(managerReadRepositoryMock);
    }

    [Fact]
    public async Task Should_ReturnEmptyList_WhenTheresNoManagers()
    {
        // Arrange
        var request = new GetManagersQuery();

        var emptyList = new List<ManagerResponse>();
        managerReadRepositoryMock.QueryAsync<ManagerResponse>(Arg.Any<string>(), Arg.Any<object>())
            .Returns(emptyList);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
    
    [Fact]
    public async Task Should_ReturnList_WhenExistManagers()
    {
        // Arrange
        var request = new GetManagersQuery();

        var list = new List<ManagerResponse>()
        {
            new ManagerResponse(
                Faker.Random.Int(0),
                Faker.Name.FullName(),
                Faker.Random.Int(0),
                Faker.Random.Int(0))
        };
        managerReadRepositoryMock.QueryAsync<ManagerResponse>(Arg.Any<string>(), Arg.Any<object>())
            .Returns(list);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value.Count == 1);
        Assert.Single(result.Value);
    }
}