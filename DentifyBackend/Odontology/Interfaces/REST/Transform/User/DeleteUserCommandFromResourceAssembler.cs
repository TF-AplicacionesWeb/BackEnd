using DentifyBackend.Dentify.Domain.Model.Commands;
using DentifyBackend.Dentify.Interfaces.REST.Resources;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class DeleteUserCommandFromResourceAssembler
{
    public static DeleteUserCommand ToCommandFromResource(DeleteUserResource resource) =>
        new DeleteUserCommand(resource.id);
}