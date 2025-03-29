using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Domain.Managers;

namespace Football.Modules.Leagues.Application.Managers.UpdateManager;

public class UpdateManagerCommandHandler(
    IUnitOfWork unitOfWork,
    IManagerRepository managerRepository) 
    : ICommandHandler<UpdateManagerCommand, int>
{
    public async Task<Result<int>> Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
    {
        var manager = await managerRepository.FindByIdAsync(request.Id);

        if (manager is null)
            return Result.Failure<int>(ManagerErrors.NotFound(request.Id));
        
        manager.Update(
            request.Name,
            request.YellowCard,
            request.RedCard);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(manager.Id);
    }
}