using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Domain.Managers;

namespace Football.Modules.Leagues.Application.Managers.CreateManager;

public class CreateManagerCommandHandler(
    IUnitOfWork unitOfWork,
    IManagerRepository managerRepository) 
    : ICommandHandler<CreateManagerCommand, int>
{
    public async Task<Result<int>> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
    {
        var manager = Manager.Create(request.Name);
        
        await managerRepository.Add(manager);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(manager.Id);
    }
}