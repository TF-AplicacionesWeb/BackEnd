using DentifyBackend.Odontology.Domain.Model.Commands.User;
using DentifyBackend.Odontology.Interfaces.REST.Resources.User;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.User;

public class DeleteUserCommandFromResourceAssembler
{
    public static DeleteUserCommand ToCommandFromResource(DeleteUserResource resource)
    {
        return new DeleteUserCommand(resource.id);
    }
}