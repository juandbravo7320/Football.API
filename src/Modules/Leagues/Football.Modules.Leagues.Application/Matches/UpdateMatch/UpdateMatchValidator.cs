using FluentValidation;
using Football.Common.Application.Clock;

namespace Football.Modules.Leagues.Application.Matches.UpdateMatch;

public class UpdateMatchValidator : AbstractValidator<UpdateMatchCommand>
{
    public UpdateMatchValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(m => m.Id)
            .NotEmpty();
        
        RuleFor(m => m.HouseManager)
            .NotEmpty();
        
        RuleFor(m => m.AwayManager)
            .NotEmpty();
        
        RuleFor(m => m.Referee)
            .NotEmpty();
        
        RuleFor(m => m.StartsAtUtc)
            .NotEmpty()
            .GreaterThan(dateTimeProvider.UtcNow);
        
        RuleFor(m => m.HousePlayers)
            .NotEmpty();
        
        RuleFor(m => m.AwayPlayers)
            .NotEmpty();
    }
}