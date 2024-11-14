using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Commands.SupportMessage;

namespace DentifyBackend.Dentify.Domain.Services;

public interface ISupportMessageCommandService
{
    Task<SupportMessage?> Handle(CreateSupportMessageCommand command);
}