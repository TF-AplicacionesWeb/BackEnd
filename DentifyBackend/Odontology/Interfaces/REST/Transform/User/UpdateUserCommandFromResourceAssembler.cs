using DentifyBackend.Odontology.Domain.Model.Commands.User;
using DentifyBackend.Odontology.Interfaces.REST.Resources.User;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.User;

public class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(UpdateUserResource resource)
    {
        return new UpdateUserCommand(resource.username, resource.first_name, resource.last_name,
            resource.email, resource.phone, resource.company, resource.password,
            resource.trial);
    }
}