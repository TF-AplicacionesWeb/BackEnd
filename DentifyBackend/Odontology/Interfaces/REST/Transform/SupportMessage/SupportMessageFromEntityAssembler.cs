using DentifyBackend.Odontology.Interfaces.REST.Resources.SupportMessage;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.SupportMessage;

public class SupportMessageFromEntityAssembler
{
    public static SupportMessageResource toResourceFromEntity(Domain.Model.Aggregates.SupportMessage entity)
    {
        return new SupportMessageResource(entity.id, entity.name, entity.email,
            entity.description, entity.user_id);
    }
}