using FluentValidation;

namespace Football.Modules.Leagues.Application.Referees.CreateReferee;

public class CreateRefereeValidator : AbstractValidator<CreateRefereeCommand>
{
    public CreateRefereeValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(150);
    }
}