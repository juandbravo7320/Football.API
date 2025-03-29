using FluentValidation;

namespace Football.Modules.Leagues.Application.Referees.UpdateReferee;

public class UpdateRefereeValidator : AbstractValidator<UpdateRefereeCommand>
{
    public UpdateRefereeValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();
        
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(150);
    }
}