using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Services;

namespace DentifyBackend.Dentify.Application.Internal.QueryServices;

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