using FluentValidation;

namespace Football.Modules.Leagues.Application.Managers.UpdateManager;

public class UpdateManagerValidator : AbstractValidator<UpdateManagerCommand>
{
    public UpdateManagerValidator()
    {
        RuleFor(m => m.Id)
            .NotEmpty();
        
        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(150);
    }
}