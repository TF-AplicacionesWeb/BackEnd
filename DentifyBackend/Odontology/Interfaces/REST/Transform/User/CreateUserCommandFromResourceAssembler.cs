using DentifyBackend.Odontology.Domain.Model.Commands.User;
using DentifyBackend.Odontology.Interfaces.REST.Resources.User;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.User;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        return new CreateUserCommand(resource.username, resource.first_name, resource.last_name, resource.email,
            resource.phone,
            resource.company, resource.password, resource.trial);
    }
}