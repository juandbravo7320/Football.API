using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Players.GetYellowCards;

public record GetYellowCardsQuery(int PlayerId) : IQuery<YellowCardResponse>;