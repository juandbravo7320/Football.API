using FluentValidation.TestHelper;
using Football.Modules.Leagues.Application.Managers.CreateManager;

namespace Football.Modules.Leagues.UnitTests.Managers.CreateManager;

public class CreateManagerValidatorTest
{ 
    private readonly CreateManagerValidator _validator = new();

    [Fact]
    public void Validate_ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var request = new CreateManagerCommand("");

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public void Validate_ShouldHaveError_WhenNameIsNull()
    {
        // Arrange
        var request = new CreateManagerCommand(null);

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}