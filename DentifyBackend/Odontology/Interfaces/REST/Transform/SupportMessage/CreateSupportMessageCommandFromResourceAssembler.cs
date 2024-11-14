using DentifyBackend.Dentify.Domain.Model.Commands.SupportMessage;
using DentifyBackend.Dentify.Interfaces.REST.Resources.SupportMessage;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform;

public class CreateSupportMessageCommandFromResourceAssembler
{

    public static CreateSupportMessageCommand toCommandFromResource(CreateSupportMessageResource resource) =>
        new CreateSupportMessageCommand(resource.name, resource.email, resource.description,
            resource.user_id);
}