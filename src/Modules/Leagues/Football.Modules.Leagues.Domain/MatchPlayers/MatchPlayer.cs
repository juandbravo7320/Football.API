using Football.Modules.Leagues.Domain.Matches;
using Football.Modules.Leagues.Domain.Players;

namespace Football.Modules.Leagues.Domain.MatchPlayers;

public sealed class MatchPlayer
{
    public int MatchId { get; private set; }
    public int PlayerId { get; private set; }
    public PlayerType PlayerType { get; private set; }
    
    public Match Match { get; }
    public Player Player { get; }

    public MatchPlayer()
    {
        
    }
    
    public MatchPlayer(
        int matchId,
        int playerId,
        PlayerType playerType)
    {
        MatchId = matchId;
        PlayerId = playerId;
        PlayerType = playerType;
    }

    public static MatchPlayer Create(
        int matchId,
        int playerId,
        PlayerType playerType)
    {
        return new MatchPlayer(
            matchId,
            playerId,
            playerType);
    }
}