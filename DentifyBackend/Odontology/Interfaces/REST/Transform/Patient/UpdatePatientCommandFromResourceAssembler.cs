using DentifyBackend.Odontology.Domain.Model.Commands.Patient;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Patient;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Patient;

public static class UpdatePatientCommandFromResourceAssembler
{
    public static UpdatePatientCommand ToCommandFromResource(UpdatePatientResource resource)
    {
        return new UpdatePatientCommand(resource.clinical_record_id, resource.first_name, resource.last_name, resource.email, resource.age, resource.medical_history, resource.birth_date, resource.appointment_id, resource.user_id);
    }
}