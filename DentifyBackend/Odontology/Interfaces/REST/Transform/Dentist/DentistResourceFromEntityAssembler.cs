using DentifyBackend.Odontology.Interfaces.REST.Resources.Dentist;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Dentist;

public class DentistResourceFromEntityAssembler
{
    public static DentistResource ToResourceFromEntity(Domain.Model.Aggregates.Dentist entity)
    {
        return new DentistResource(entity.id, entity.first_name, entity.last_name,
            entity.specialty, entity.experience, entity.phone, entity.email,
            entity.total_appointments, entity.user_id);
    }
}