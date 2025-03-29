using Football.Common.Domain;

namespace Football.Modules.Leagues.Domain.Players;

public static class PlayerErrors
{
    public static Error NotFound(int playerId) => 
        Error.NotFound("Player.NotFound", $"The player with the identifier {playerId} was not found");
    
    public static Error NotFound(List<int> playerIds) => 
        Error.NotFound("Players.NotFound", $"The players with the identifiers ({string.Join(", ", playerIds)}) were not found");
}