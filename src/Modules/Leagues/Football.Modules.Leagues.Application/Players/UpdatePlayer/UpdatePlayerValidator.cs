using FluentValidation;

namespace Football.Modules.Leagues.Application.Players.UpdatePlayer;

public class UpdatePlayerValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();
        
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(150);
    }
}