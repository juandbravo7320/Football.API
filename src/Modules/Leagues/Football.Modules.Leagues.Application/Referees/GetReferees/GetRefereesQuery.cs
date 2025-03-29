using Football.Common.Application.Messaging;

namespace Football.Modules.Leagues.Application.Referees.GetReferees;

public record GetRefereesQuery() : IQuery<IReadOnlyCollection<RefereeResponse>>;