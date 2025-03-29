using Football.Modules.Leagues.Domain.MatchPlayers;

namespace Football.Modules.Leagues.Domain.Players;

public sealed class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int YellowCard { get; set; }
    public int RedCard { get; set; }
    public int MinutesPlayed { get; set; }
    
    public ICollection<MatchPlayer> MatchPlayers { get; }

    public Player()
    {
        
    }
    
    public Player(string name)
    {
        Name = name;
        YellowCard = 0;
        RedCard = 0;
        MinutesPlayed = 0;
    }
    
    public static Player Create(string name)
    {
        return new Player(name);
    }
    
    public void Update(
        string name,
        int yellowCard,
        int redCard,
        int minutesPlayed)
    {
        if (Name == name &&
            YellowCard == yellowCard &&
            RedCard == redCard &&
            MinutesPlayed == minutesPlayed)
        {
            return;
        }
        
        Name = name;
        YellowCard = yellowCard;
        RedCard = redCard;
        MinutesPlayed = minutesPlayed;
    }
}