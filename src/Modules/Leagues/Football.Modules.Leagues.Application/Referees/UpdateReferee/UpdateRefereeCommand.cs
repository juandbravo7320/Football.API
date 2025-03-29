using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Referees.UpdateReferee;

public record UpdateRefereeCommand(
    int Id,
    string Name,
    int MinutesPlayed) : ICommand<int>;