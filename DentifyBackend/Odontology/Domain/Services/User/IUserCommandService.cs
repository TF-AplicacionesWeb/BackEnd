using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Commands;

namespace DentifyBackend.Dentify.Domain.Services;

public interface IUserCommandService
{
    Task<User?> Handle(CreateUserCommand request);
}