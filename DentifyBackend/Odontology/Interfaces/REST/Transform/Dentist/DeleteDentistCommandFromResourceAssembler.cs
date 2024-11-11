using DentifyBackend.Dentify.Domain.Model.Commands;
using DentifyBackend.Dentify.Interfaces.REST.Resources;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class DeleteDentistCommandFromResourceAssembler
{
    public static DeleteDentistCommand ToCommandFromResource(DeleteDentistResource resource) =>
        new DeleteDentistCommand(resource.id);
}