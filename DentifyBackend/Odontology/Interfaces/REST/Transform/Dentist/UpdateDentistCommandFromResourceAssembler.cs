using DentifyBackend.Dentify.Domain.Model.Commands;
using DentifyBackend.Dentify.Interfaces.REST.Resources;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class UpdateDentistCommandFromResourceAssembler
{
    public static UpdateDentistCommand ToCommandFromResource(UpdateDentistResource resource) =>
        new UpdateDentistCommand(resource.first_name, resource.last_name,
            resource.specialty, resource.experience, resource.phone, resource.email,
            resource.total_appointments, resource.user_id);
}