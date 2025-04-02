using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Managers.GetManagers;
using Football.Modules.Leagues.Domain.Managers;

namespace Football.Modules.Leagues.Application.Managers.GetManager;

public class GetManagerQueryHandler(IManagerRepository managerRepository) 
    : IQueryHandler<GetManagerQuery, ManagerResponse>
{
    public async Task<Result<ManagerResponse>> Handle(GetManagerQuery request, CancellationToken cancellationToken)
    {
        var manager = await managerRepository.FindByIdAsync(request.Id);
        
        if (manager is null)
            return Result.Failure<ManagerResponse>(ManagerErrors.NotFound(request.Id));

        return Result.Success(
            new ManagerResponse(
                manager.Id,
                manager.Name,
                manager.YellowCard,
                manager.RedCard));
    }
}