using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Services;

namespace DentifyBackend.Dentify.Application.Internal.QueryServices;

public class DentistQueryService(IDentistRepository dentistRepository) 
    : IDentistQueryService
{
    public async Task<IEnumerable<Dentist>> Handle(GetAllDentistsQuery query)
    {
        return await dentistRepository.ListAsync();
    }

    public async Task<Dentist?> Handle(GetDentistByIdQuery query)
    {
        return await dentistRepository.FindByIdAsync(query.id);
    }
}