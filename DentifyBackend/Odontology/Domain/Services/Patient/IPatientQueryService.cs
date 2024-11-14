using DentifyBackend.Odontology.Domain.Model.Queries.Patient;

namespace DentifyBackend.Odontology.Domain.Services.Patient;

public interface IPatientQueryService
{
    Task<IEnumerable<Model.Aggregates.Patient>> Handle(GetAllPatientsQuery query);

    Task<Model.Aggregates.Patient?> Handle(GetPatientsByIdQuery query);
}