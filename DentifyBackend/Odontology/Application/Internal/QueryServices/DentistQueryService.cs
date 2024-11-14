using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Queries.Dentist;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.Dentist;

namespace DentifyBackend.Odontology.Application.Internal.QueryServices;

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