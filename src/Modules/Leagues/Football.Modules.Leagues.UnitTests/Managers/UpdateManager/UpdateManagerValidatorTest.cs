using Bogus;
using FluentValidation.TestHelper;
using Football.Modules.Leagues.Application.Managers.UpdateManager;

namespace Football.Modules.Leagues.UnitTests.Managers.UpdateManager;

public class UpdateManagerValidatorTest
{
    private readonly Faker Faker = new();
    private readonly UpdateManagerValidator _validator = new();

    [Fact]
    public async Task Validate_ShouldHaveError_WhenIdIsEmpty()
    {
        // Arrange
        var request = new UpdateManagerCommand(
            0, 
            Faker.Name.FullName(),
            Faker.Random.Int(),
            Faker.Random.Int());

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
    
    [Fact]
    public async Task Validate_ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var request = new UpdateManagerCommand(
            Faker.Random.Int(),
            "",
            Faker.Random.Int(),
            Faker.Random.Int());

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public async Task Validate_ShouldHaveError_WhenNameExceedsMaximumLength()
    {
        // Arrange
        var request = new UpdateManagerCommand(
            Faker.Random.Int(),
            Faker.Random.String(151),
            Faker.Random.Int(),
            Faker.Random.Int());

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}