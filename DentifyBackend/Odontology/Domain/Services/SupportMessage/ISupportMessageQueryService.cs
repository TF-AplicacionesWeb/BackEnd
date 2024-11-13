using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries.SupportMessage;

namespace DentifyBackend.Dentify.Domain.Services;

public interface ISupportMessageQueryService
{
    Task<SupportMessage?> Handle(GetSupportMessageByIdQuery query);

}