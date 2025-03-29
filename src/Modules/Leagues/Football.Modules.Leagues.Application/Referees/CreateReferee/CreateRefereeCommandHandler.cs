using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Domain.Referees;

namespace Football.Modules.Leagues.Application.Referees.CreateReferee;

public class CreateRefereeCommandHandler(
    IUnitOfWork unitOfWork,
    IRefereeRepository refereeRepository) 
    : ICommandHandler<CreateRefereeCommand, int>
{
    public async Task<Result<int>> Handle(CreateRefereeCommand request, CancellationToken cancellationToken)
    {
        var referee = Referee.Create(request.Name);
        
        await refereeRepository.Add(referee);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(referee.Id);
    }
}