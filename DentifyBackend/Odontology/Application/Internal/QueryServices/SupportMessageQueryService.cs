using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Services;
using DentifyBackend.Dentify.Domain.Model.Queries.SupportMessage;

namespace DentifyBackend.Dentify.Application.Internal.QueryServices;

public class SupportMessageQueryService(ISupportMessageRepository supportMessageRepository) 
    : ISupportMessageQueryService
{
    public async Task<SupportMessage?> Handle(GetSupportMessageByIdQuery query)
    {
        return await supportMessageRepository.FindByIdAsync(query.id);
    }
}