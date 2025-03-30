namespace Football.Modules.Users.Domain.Users;

public sealed class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public User()
    {
        
    }
    
    public User(
        string firstName,
        string lastName,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email)
    {
        return new User(
            firstName,
            lastName,
            email);
    }

    public void Update(
        string firstName,
        string lastName,
        string email,
        string passwordHash)
    {
        if (FirstName == firstName &&
            LastName == lastName &&
            Email == email &&
            PasswordHash == passwordHash)
        {
            return;
        }
        
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }
}