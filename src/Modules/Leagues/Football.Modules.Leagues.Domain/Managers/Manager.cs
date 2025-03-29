namespace Football.Modules.Leagues.Domain.Managers;

public sealed class Manager
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int YellowCard { get; set; }
    public int RedCard { get; set; }

    public Manager()
    {
        
    }
    
    public Manager(string name)
    {
        Name = name;
        YellowCard = 0;
        RedCard = 0;
    }

    public static Manager Create(string name)
    {
        return new Manager(name);
    }
    
    public void Update(
        string name,
        int yellowCard,
        int redCard)
    {
        if (Name == name &&
            YellowCard == yellowCard &&
            RedCard == redCard)
        {
            return;
        }
        
        Name = name;
        YellowCard = yellowCard;
        RedCard = redCard;
    }
}