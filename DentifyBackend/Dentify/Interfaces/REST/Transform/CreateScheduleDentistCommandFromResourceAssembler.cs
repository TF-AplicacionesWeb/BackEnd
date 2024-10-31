using DentifyBackend.Dentify.Domain.Model.Commands;
using DentifyBackend.Dentify.Interfaces.REST.Resources;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class CreateScheduleDentistCommandFromResourceAssembler
{
    public static CreateScheduleDentistCommand ToCommandFromResource(CreateScheduleDentistResource resource) =>
        new CreateScheduleDentistCommand(resource.dentist_id, resource. weekday, resource.start_time,
            resource.end_time, resource. date, resource.user_id);
}