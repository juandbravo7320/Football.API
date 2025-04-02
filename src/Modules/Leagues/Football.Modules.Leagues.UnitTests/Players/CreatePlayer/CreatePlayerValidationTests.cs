using Bogus;
using FluentValidation.TestHelper;
using Football.Modules.Leagues.Application.Players.CreatePlayer;

namespace Football.Modules.Leagues.UnitTests.Players.CreatePlayer;

public class CreatePlayerValidationTests
{
    private readonly Faker Faker = new();
    private readonly CreatePlayerValidator _validator = new();

    [Fact]
    public async Task Validate_ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var request = new CreatePlayerCommand(string.Empty);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public async Task Validate_ShouldHaveError_WhenNameExceedsMaximumLength()
    {
        // Arrange
        var request = new CreatePlayerCommand(Faker.Random.String(151));

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}