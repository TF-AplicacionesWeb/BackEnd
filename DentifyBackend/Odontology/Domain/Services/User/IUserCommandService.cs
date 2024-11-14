using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Commands;

namespace DentifyBackend.Dentify.Domain.Services;

public interface IUserCommandService
{
    Task<User?> Handle(CreateUserCommand request);
    
    Task<User?> Handle(UpdateUserCommand command, int id);

    Task Handle(DeleteUserCommand command);
}