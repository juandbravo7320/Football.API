using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Managers.GetManagers;
using Football.Modules.Leagues.Domain.Managers;

namespace Football.Modules.Leagues.Application.Managers.GetManager;

public class GetManagerQueryHandler(IManagerReadRepository managerReadRepository) 
    : IQueryHandler<GetManagerQuery, ManagerResponse>
{
    public async Task<Result<ManagerResponse>> Handle(GetManagerQuery request, CancellationToken cancellationToken)
    {
        const string sql =
            $"""
             SELECT
                 "Id" AS {nameof(ManagerResponse.Id)},
                 "Name" AS {nameof(ManagerResponse.Name)},
                 "YellowCard" AS {nameof(ManagerResponse.YellowCard)},
                 "RedCard" AS {nameof(ManagerResponse.RedCard)}
             FROM leagues."Manager"
             WHERE "Id" = @ManagerId
             """;
        
        var manager = await managerReadRepository.QuerySingleOrDefaultAsync<ManagerResponse>(sql, new { ManagerId = request.Id });
        
        if (manager is null)
            return Result.Failure<ManagerResponse>(ManagerErrors.NotFound(request.Id));

        return Result.Success(manager);
    }
}