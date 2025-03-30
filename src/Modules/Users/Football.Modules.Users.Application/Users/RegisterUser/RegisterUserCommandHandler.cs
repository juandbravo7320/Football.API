using Football.Common.Application.Messaging;
using Football.Common.Domain;
using Football.Modules.Users.Application.Abstractions.Data;
using Football.Modules.Users.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Football.Modules.Users.Application.Users.RegisterUser;

public class RegisterUserCommandHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher) : ICommandHandler<RegisterUserCommand, int>
{
    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existentUser = await userRepository.FindByEmailAsync(request.Email);

        if (existentUser is not null)
            return Result.Failure<int>(UserErrors.ExistentEmail);

        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email);
        
        var hashedPassword = passwordHasher.HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;

        await userRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(user.Id);
    }
}