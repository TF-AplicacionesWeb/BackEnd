using DentifyBackend.Odontology.Domain.Model.Commands.Dentist;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Dentist;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Dentist;

public class CreateDentistCommandFromResourceAssembler
{
    public static CreateDentistCommand ToCommandFromResource(CreateDentistResource resource)
    {
        return new CreateDentistCommand(resource.id, resource.first_name, resource.last_name,
            resource.specialty, resource.experience, resource.phone, resource.email,
            resource.total_appointments, resource.user_id);
    }
}