using DentifyBackend.Odontology.Domain.Model.Queries.SupportMessage;

namespace DentifyBackend.Odontology.Domain.Services.SupportMessage;

public interface ISupportMessageQueryService
{
    Task<Model.Aggregates.SupportMessage?> Handle(GetSupportMessageByIdQuery query);
}