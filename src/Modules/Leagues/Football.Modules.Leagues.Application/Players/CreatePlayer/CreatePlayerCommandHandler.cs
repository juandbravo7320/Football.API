using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Leagues.Application.Abstractions.Data;
using Football.Modules.Leagues.Domain.Players;

namespace Football.Modules.Leagues.Application.Players.CreatePlayer;

public class CreatePlayerCommandHandler(
    IUnitOfWork unitOfWork,
    IPlayerRepository playerRepository) 
    : ICommandHandler<CreatePlayerCommand, int>
{
    public async Task<Result<int>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = Player.Create(request.Name);
        
        await playerRepository.Add(player);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(player.Id);
    }
}