using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Users.Application.Authentication;
using Football.Modules.Users.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Football.Modules.Users.Application.Users.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher,
    IAuthenticationService authenticationService) : ICommandHandler<LoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Failure<LoginResponse>(UserErrors.IncorrectCredentials);

        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return Result.Failure<LoginResponse>(UserErrors.IncorrectCredentials);

        var accessToken = authenticationService.GenerateAccessToken(user);

        return Result.Success(
            new LoginResponse(accessToken));
    }
}