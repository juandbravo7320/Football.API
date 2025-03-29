using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Domain.Referees;

namespace Football.Modules.Leagues.Application.Referees.UpdateReferee;

public class UpdateRefereeCommandHandler(
    IUnitOfWork unitOfWork,
    IRefereeRepository refereeRepository) : ICommandHandler<UpdateRefereeCommand, int>
{
    public async Task<Result<int>> Handle(UpdateRefereeCommand request, CancellationToken cancellationToken)
    {
        var referee = await refereeRepository.FindByIdAsync(request.Id);

        if (referee is null)
            return Result.Failure<int>(RefereeErrors.NotFound(request.Id));
        
        referee.Update(
            request.Name,
            request.MinutesPlayed);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(referee.Id);
    }
}