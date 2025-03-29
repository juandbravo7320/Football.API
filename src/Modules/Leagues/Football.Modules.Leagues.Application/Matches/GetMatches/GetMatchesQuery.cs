using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Matches.GetMatches;

public record GetMatchesQuery() : IQuery<IReadOnlyCollection<MatchesResponse>>;