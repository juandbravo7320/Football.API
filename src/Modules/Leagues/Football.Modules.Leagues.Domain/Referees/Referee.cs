namespace Football.Modules.Leagues.Domain.Referees;

public sealed class Referee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MinutesPlayed { get; set; }

    public Referee()
    {
        
    }
    
    public Referee(string name)
    {
        Name = name;
        MinutesPlayed = 0;
    }

    public static Referee Create(string name)
    {
        return new Referee(name);
    }

    public void Update(
        string name,
        int minutesPlayed)
    {
        if (Name == name &&
            MinutesPlayed == minutesPlayed)
            return;

        Name = name;
        MinutesPlayed = minutesPlayed;
    }
}