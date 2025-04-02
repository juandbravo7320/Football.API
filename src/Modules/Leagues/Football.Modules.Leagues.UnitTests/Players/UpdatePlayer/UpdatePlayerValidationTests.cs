using Bogus;
using FluentValidation.TestHelper;
using Football.Modules.Leagues.Application.Players.UpdatePlayer;

namespace Football.Modules.Leagues.UnitTests.Players.UpdatePlayer;

public class UpdatePlayerValidationTests
{
    private readonly Faker Faker = new();
    private readonly UpdatePlayerValidator _validator = new();

    [Fact]
    public async Task Validate_ShouldHaveError_WhenIsIsEmpty()
    {
        // Arrange
        var request = new UpdatePlayerCommand(
            0,
            Faker.Name.FullName(),
            Faker.Random.Int(0),
            Faker.Random.Int(0),
            Faker.Random.Int(0));

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Fact]
    public async Task Validate_ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var request = new UpdatePlayerCommand(
            Faker.Random.Int(0),
            string.Empty,
            Faker.Random.Int(0),
            Faker.Random.Int(0),
            Faker.Random.Int(0));

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public async Task Validate_ShouldHaveError_WhenNameExceedsMaximumLength()
    {
        // Arrange
        var request = new UpdatePlayerCommand(
            Faker.Random.Int(0),
            Faker.Random.String(151),
            Faker.Random.Int(0),
            Faker.Random.Int(0),
            Faker.Random.Int(0));

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}