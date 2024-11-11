using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Interfaces.REST.Resources;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class ScheduleDentistResourceFromEntityAssembler
{
    public static ScheduleDentistResource ToResourceFromEntity(ScheduleDentist entity) =>
        new ScheduleDentistResource(entity.id, entity.dentist_id, entity. weekday, entity.start_time,
            entity.end_time, entity. date, entity.user_id);
}