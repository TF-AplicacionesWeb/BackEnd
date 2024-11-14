using DentifyBackend.Odontology.Domain.Model.Commands.Dentist;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Dentist;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Dentist;

public class DeleteDentistCommandFromResourceAssembler
{
    public static DeleteDentistCommand ToCommandFromResource(DeleteDentistResource resource)
    {
        return new DeleteDentistCommand(resource.id);
    }
}