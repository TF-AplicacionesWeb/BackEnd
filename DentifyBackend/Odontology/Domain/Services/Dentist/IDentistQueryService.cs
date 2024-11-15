using DentifyBackend.Odontology.Domain.Model.Queries.Dentist;

namespace DentifyBackend.Odontology.Domain.Services.Dentist;

public interface IDentistQueryService
{
    Task<IEnumerable<Model.Aggregates.Dentist>> Handle(GetAllDentistsQuery query);

    Task<Model.Aggregates.Dentist?> Handle(GetDentistByIdQuery query);
}