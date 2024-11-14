using DentifyBackend.Dentify.Domain.Model.Commands;
using DentifyBackend.Dentify.Interfaces.REST.Resources;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource) =>
        new CreateUserCommand(resource.username, resource.first_name, resource.last_name, resource.email, resource.phone, resource.company, resource.password, resource.trial);
}