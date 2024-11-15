using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Queries.SupportMessage;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.SupportMessage;

namespace DentifyBackend.Odontology.Application.Internal.QueryServices;

public class SupportMessageQueryService(ISupportMessageRepository supportMessageRepository)
    : ISupportMessageQueryService
{
    public async Task<SupportMessage?> Handle(GetSupportMessageByIdQuery query)
    {
        return await supportMessageRepository.FindByIdAsync(query.id);
    }
}