using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Domain.Players;

namespace Football.Modules.Leagues.Application.Players.UpdatePlayer;

public class UpdatePlayerCommandHandler(
    IUnitOfWork unitOfWork,
    IPlayerRepository playerRepository) : ICommandHandler<UpdatePlayerCommand, int>
{
    public async Task<Result<int>> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await playerRepository.FindByIdAsync(request.Id);

        if (player is null)
            return Result.Failure<int>(PlayerErrors.NotFound(request.Id));
        
        player.Update(
            request.Name, 
            request.YellowCard, 
            request.RedCard, 
            request.MinutesPlayed);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(player.Id);
    }
}