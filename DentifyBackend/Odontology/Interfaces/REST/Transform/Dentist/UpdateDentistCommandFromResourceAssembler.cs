using DentifyBackend.Odontology.Domain.Model.Commands.Dentist;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Dentist;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Dentist;

public class UpdateDentistCommandFromResourceAssembler
{
    public static UpdateDentistCommand ToCommandFromResource(UpdateDentistResource resource)
    {
        return new UpdateDentistCommand(resource.first_name, resource.last_name,
            resource.specialty, resource.experience, resource.phone, resource.email,
            resource.total_appointments, resource.user_id);
    }
}