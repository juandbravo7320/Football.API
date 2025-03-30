using Football.Common.Application.Messaging;

namespace Football.Modules.Users.Application.Users.Login;

public record LoginCommand(
    string Email,
    string Password) : ICommand<LoginResponse>;