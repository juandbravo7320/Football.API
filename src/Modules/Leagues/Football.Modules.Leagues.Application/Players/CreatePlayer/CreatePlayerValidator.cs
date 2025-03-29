using FluentValidation;

namespace Football.Modules.Leagues.Application.Players.CreatePlayer;

public class CreatePlayerValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(150);
    }
}