using FluentValidation;
using Football.Common.Application.Clock;

namespace Football.Modules.Leagues.Application.Matches.CreateMatch;

public class CreateMatchValidator : AbstractValidator<CreateMatchCommand>
{
    public CreateMatchValidator(IDateTimeProvider dateTimeProvider)
    {
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