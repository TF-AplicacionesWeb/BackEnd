using DentifyBackend.Odontology.Domain.Model.Commands.User;

namespace DentifyBackend.Odontology.Domain.Services.User;

public interface IUserCommandService
{
    Task<Model.Aggregates.User?> Handle(CreateUserCommand request);

    Task<Model.Aggregates.User?> Handle(UpdateUserCommand command, int id);

    Task Handle(DeleteUserCommand command);
}