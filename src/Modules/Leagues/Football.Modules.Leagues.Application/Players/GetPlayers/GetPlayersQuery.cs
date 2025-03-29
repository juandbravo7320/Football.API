using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Players.GetPlayers;

public record GetPlayersQuery() : IQuery<IReadOnlyCollection<PlayerResponse>>;