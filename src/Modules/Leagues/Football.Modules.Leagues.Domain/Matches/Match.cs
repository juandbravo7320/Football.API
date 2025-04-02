using Football.Modules.Leagues.Domain.Managers;
using Football.Modules.Leagues.Domain.MatchPlayers;
using Football.Modules.Leagues.Domain.Referees;

namespace Football.Modules.Leagues.Domain.Matches;

public sealed class Match
{
    public int Id { get; set; }

    public int HouseManagerId { get; set; }
    public int AwayManagerId { get; set; }
    public int RefereeId { get; set; }
    public DateTime StartsAtUtc { get; set; }

    public Manager HouseManager { get; }
    public Manager AwayManager { get; }
    public Referee Referee { get; }
    
    public ICollection<MatchPlayer> MatchPlayers { get; set; }

    public Match()
    {
        
    }
    
    public Match(
        int houseManagerId,
        int awayManagerId,
        int refereeId,
        DateTime startsAtUtc)
    {
        HouseManagerId = houseManagerId;
        AwayManagerId = awayManagerId;
        RefereeId = refereeId;
        StartsAtUtc = startsAtUtc;
    }
    
    public static Match Create(
        int houseManagerId,
        int awayManagerId,
        int refereeId,
        DateTime startsAtUtc)
    {   
        return new Match(
            houseManagerId,
            awayManagerId,
            refereeId,
            startsAtUtc);
    }
    
    public void Update(
        int houseManagerId,
        int awayManagerId,
        int refereeId,
        DateTime startsAtUtc)
    {
        if (HouseManagerId == houseManagerId &&
            AwayManagerId == awayManagerId &&
            RefereeId == refereeId &&
            StartsAtUtc == startsAtUtc)
            return;
        
        HouseManagerId = houseManagerId;
        AwayManagerId = awayManagerId;
        RefereeId = refereeId;
        StartsAtUtc = startsAtUtc;
    }
}