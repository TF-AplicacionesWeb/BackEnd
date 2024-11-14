using DentifyBackend.Odontology.Domain.Model.Commands.ScheduleDentist;
using DentifyBackend.Odontology.Interfaces.REST.Resources.ScheduleDentist;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.ScheduleDentist;

public class CreateScheduleDentistCommandFromResourceAssembler
{
    public static CreateScheduleDentistCommand ToCommandFromResource(CreateScheduleDentistResource resource)
    {
        return new CreateScheduleDentistCommand(resource.dentist_id, resource.weekday, resource.start_time,
            resource.end_time, resource.date, resource.user_id);
    }
}