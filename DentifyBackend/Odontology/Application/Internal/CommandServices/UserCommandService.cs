using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Commands;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Services;
using DentifyBackend.Shared.Domain.Repositories;

namespace DentifyBackend.Dentify.Application.Internal.CommandServices;

public class UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork): IUserCommandService
{
    public async Task<User?> Handle(CreateUserCommand command)
    {
        var user =
            await userRepository.FindByUsernameAndPasswordAsync(command.username, command.password);
        if (user != null)
            throw new Exception("User with this username already exists for the given password");
        
        user = new User(command);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            return null;
        }

        return user;
    }
}