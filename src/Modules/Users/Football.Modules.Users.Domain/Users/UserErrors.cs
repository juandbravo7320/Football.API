using Football.Common.Domain;

namespace Football.Modules.Users.Domain.Users;

public static class UserErrors
{
    public static Error ExistentEmail =>
        new Error("Users.ExistentEmail", "There's already exist an user with the same email");
    
    public static Error IncorrectCredentials =>
        new Error("Users.IncorrectCredentials", "Incorrect Credentials");
}