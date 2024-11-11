using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Interfaces.REST.Resources;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class DentistResourceFromEntityAssembler
{
    public static DentistResource ToResourceFromEntity(Dentist entity) =>
        new DentistResource(entity.id, entity.first_name, entity.last_name,
            entity.specialty, entity.experience, entity.phone, entity.email,
            entity.total_appointments, entity.user_id);
}