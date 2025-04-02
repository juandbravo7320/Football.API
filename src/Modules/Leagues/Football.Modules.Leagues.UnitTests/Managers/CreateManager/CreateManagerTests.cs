using Bogus;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Application.Managers.CreateManager;
using Football.Modules.Leagues.Domain.Managers;
using NSubstitute;

namespace Football.Modules.Leagues.UnitTests.Managers.CreateManager;

public class CreateManagerTests
{
    private readonly Faker Faker = new();
    private readonly IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly IManagerRepository managerRepositoryMock = Substitute.For<IManagerRepository>();
    
    private readonly CreateManagerCommandHandler _handler;

    public CreateManagerTests()
    {
        _handler = new CreateManagerCommandHandler(
            unitOfWorkMock,
            managerRepositoryMock);
    }
    
    [Fact]
    public async Task Should_ReturnSuccess_WhenUserIsCreatedSuccessfully()
    {
        // Arrange
        var request = new CreateManagerCommand(Faker.Name.FullName());
        
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await managerRepositoryMock.Received(1).Add(Arg.Is<Manager>(m =>
            m.Name == request.Name &&
                m.YellowCard == 0 &&
                m.RedCard == 0));
        await unitOfWorkMock.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}