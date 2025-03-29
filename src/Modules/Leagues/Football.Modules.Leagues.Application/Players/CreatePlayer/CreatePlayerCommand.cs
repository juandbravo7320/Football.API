using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Players.CreatePlayer;

public record CreatePlayerCommand(string Name) : ICommand<int>;