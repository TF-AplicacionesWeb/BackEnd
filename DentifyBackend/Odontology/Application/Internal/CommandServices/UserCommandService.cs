using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.User;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.User;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Application.Internal.CommandServices;

public class UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork) : IUserCommandService
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

    public async Task<User?> Handle(UpdateUserCommand command, int id)
    {
        var existingById = await userRepository.FindByIdAsync(id);
        if (existingById == null) throw new InvalidOperationException("A user with this ID does not exist.");


        existingById.SetAttributes(
            command.username,
            command.first_name,
            command.last_name,
            command.email,
            command.phone,
            command.company,
            command.password,
            command.trial
        );

        try
        {
            userRepository.Update(existingById);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while updating the user. Please try again.", dbEx);
        }

        return existingById;
    }

    public async Task Handle(DeleteUserCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.id);

        if (user == null) throw new KeyNotFoundException($"Dentist with ID {command.id} not found.");


        userRepository.Remove(user);


        await unitOfWork.CompleteAsync();
    }
}