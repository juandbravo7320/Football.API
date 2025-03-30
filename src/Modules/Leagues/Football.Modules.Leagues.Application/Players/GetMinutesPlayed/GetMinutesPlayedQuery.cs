using Football.Common.Application.Messaging;
using Football.Modules.Leagues.Application.Players.GetYellowCards;

namespace Football.Modules.Leagues.Application.Players.GetMinutesPlayed;

public record GetMinutesPlayedQuery(int PlayerId) : IQuery<MinutesPlayedResponse>;