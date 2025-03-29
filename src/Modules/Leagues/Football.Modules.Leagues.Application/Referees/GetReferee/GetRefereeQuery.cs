using Football.Common.Application.Messaging;
using Football.Modules.Leagues.Application.Referees.GetReferees;

namespace Football.Modules.Leagues.Application.Referees.GetReferee;

public record GetRefereeQuery(int Id) : IQuery<RefereeResponse>;