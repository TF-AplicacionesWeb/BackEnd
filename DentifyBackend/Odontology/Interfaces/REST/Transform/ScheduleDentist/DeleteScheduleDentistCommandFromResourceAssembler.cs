using DentifyBackend.Odontology.Domain.Model.Commands.ScheduleDentist;
using DentifyBackend.Odontology.Interfaces.REST.Resources.ScheduleDentist;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.ScheduleDentist;

public class DeleteScheduleDentistCommandFromResourceAssembler
{
    public static DeleteScheduleDentistCommand ToCommandFromResource(DeleteScheduleDentistResource resource)
    {
        return new DeleteScheduleDentistCommand(resource.id);
    }
}