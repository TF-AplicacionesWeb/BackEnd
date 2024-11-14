using DentifyBackend.Odontology.Domain.Model.Commands.Patient;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Patient;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Patient;

public static class DeletePatientCommandFromResourceAssembler
{
    public static DeletePatientCommand ToCommandFromResource(DeletePatientResource resource)
    {
        return new DeletePatientCommand(resource.id);
    }
}