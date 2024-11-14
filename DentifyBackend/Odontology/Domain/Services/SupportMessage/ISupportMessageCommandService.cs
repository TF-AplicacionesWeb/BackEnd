using DentifyBackend.Odontology.Domain.Model.Commands.SupportMessage;

namespace DentifyBackend.Odontology.Domain.Services.SupportMessage;

public interface ISupportMessageCommandService
{
    Task<Model.Aggregates.SupportMessage?> Handle(CreateSupportMessageCommand command);
}