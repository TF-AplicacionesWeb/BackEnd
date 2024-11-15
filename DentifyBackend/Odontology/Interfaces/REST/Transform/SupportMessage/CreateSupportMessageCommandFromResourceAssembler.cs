using DentifyBackend.Odontology.Domain.Model.Commands.SupportMessage;
using DentifyBackend.Odontology.Interfaces.REST.Resources.SupportMessage;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.SupportMessage;

public class CreateSupportMessageCommandFromResourceAssembler
{
    public static CreateSupportMessageCommand toCommandFromResource(CreateSupportMessageResource resource)
    {
        return new CreateSupportMessageCommand(resource.name, resource.email, resource.description,
            resource.user_id);
    }
}