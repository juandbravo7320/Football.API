using FluentValidation;

namespace Football.Modules.Leagues.Application.Managers.CreateManager;

public class CreateManagerValidator : AbstractValidator<CreateManagerCommand>
{
    public CreateManagerValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(150);
    }
}