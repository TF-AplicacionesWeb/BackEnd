using DentifyBackend.Dentify.Domain.Model.Commands;
using DentifyBackend.Dentify.Interfaces.REST.Resources;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(UpdateUserResource resource) =>
        new UpdateUserCommand(resource.username, resource.first_name, resource.last_name,
            resource.email, resource.phone, resource.company, resource.password,
            resource.trial);
}