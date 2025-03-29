using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Referees.CreateReferee;

public record CreateRefereeCommand(string Name) : ICommand<int>;