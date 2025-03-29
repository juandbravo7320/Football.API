using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Matches.GetMatch;

public record GetMatchQuery(int Id) : IQuery<MatchResponse>;