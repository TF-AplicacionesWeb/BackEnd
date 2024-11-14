using DentifyBackend.Dentify.Interfaces.REST.Resources.SupportMessage;
using DentifyBackend.Dentify.Domain.Model.Aggregates;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class SupportMessageFromEntityAssembler
{
    public static SupportMessageResource toResourceFromEntity(SupportMessage entity)
    {
        return new SupportMessageResource(entity.id, entity.name, entity.email,
            entity.description, entity.user_id);
    }
}