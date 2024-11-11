using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries;

namespace DentifyBackend.Dentify.Domain.Services;

public interface IDentistQueryService
{
    Task<IEnumerable<Dentist>> Handle(GetAllDentistsQuery query);
    
    Task<Dentist?> Handle(GetDentistByIdQuery query);
}