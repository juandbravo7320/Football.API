using Football.Common.Application.Messaging;
using Football.Modules.Leagues.Application.Players.GetYellowCards;

namespace Football.Modules.Leagues.Application.Players.GetRedCards;

public record GetRedCardsQuery(int PlayerId) : IQuery<RedCardResponse>;