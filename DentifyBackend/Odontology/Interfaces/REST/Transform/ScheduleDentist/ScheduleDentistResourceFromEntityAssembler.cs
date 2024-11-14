using DentifyBackend.Odontology.Interfaces.REST.Resources.ScheduleDentist;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.ScheduleDentist;

public class ScheduleDentistResourceFromEntityAssembler
{
    public static ScheduleDentistResource ToResourceFromEntity(Domain.Model.Aggregates.ScheduleDentist entity)
    {
        return new ScheduleDentistResource(entity.id, entity.dentist_id, entity.weekday, entity.start_time,
            entity.end_time, entity.date, entity.user_id);
    }
}