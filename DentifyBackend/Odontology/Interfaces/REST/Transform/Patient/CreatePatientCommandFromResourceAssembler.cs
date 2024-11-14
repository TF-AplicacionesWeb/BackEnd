using DentifyBackend.Odontology.Domain.Model.Commands.Patient;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Patient;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Patient;

public static class CreatePatientCommandFromResourceAssembler
{
    public static CreatePatientCommand ToCommandFromResource(CreatePatientResource resource)
    {
        return new CreatePatientCommand(resource.id, resource.clinical_record_id, resource.first_name, resource.last_name, resource.email, resource.age, resource.medical_history, resource.birth_date, resource.appointment_id, resource.user_id);
    }
}