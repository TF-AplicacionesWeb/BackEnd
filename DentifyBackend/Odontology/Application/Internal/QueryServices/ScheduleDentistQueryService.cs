using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Queries.ScheduleDentist;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.ScheduleDentist;

namespace DentifyBackend.Odontology.Application.Internal.QueryServices;

public class ScheduleDentistQueryService(IScheduleDentistRepository scheduleDentistRepository)
    : IScheduleDentistQueryService
{
    public async Task<IEnumerable<ScheduleDentist>> Handle(GetAllSchedulesDentistsQuery query)
    {
        return await scheduleDentistRepository.ListAsync();
    }

    public async Task<ScheduleDentist?> Handle(GetScheduleDentistByIdQuery query)
    {
        return await scheduleDentistRepository.FindByIdAsync(query.id);
    }
}