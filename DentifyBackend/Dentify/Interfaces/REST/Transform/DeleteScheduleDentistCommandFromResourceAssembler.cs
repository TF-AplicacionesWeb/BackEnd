using DentifyBackend.Dentify.Domain.Model.Commands;
using DentifyBackend.Dentify.Interfaces.REST.Resources;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class DeleteScheduleDentistCommandFromResourceAssembler
{
    public static DeleteScheduleDentistCommand ToCommandFromResource(DeleteScheduleDentistResource resource) =>
        new DeleteScheduleDentistCommand(resource.id);
}