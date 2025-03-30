using Football.Common.Application.Messaging;

namespace Football.Modules.Users.Application.Users.RegisterUser;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<int>;