using Football.Common.Application.Messaging;
using Football.Modules.Leagues.Application.Players.GetPlayers;

namespace Football.Modules.Leagues.Application.Players.GetPlayer;

public record GetPlayerQuery(int Id) : IQuery<PlayerResponse>;