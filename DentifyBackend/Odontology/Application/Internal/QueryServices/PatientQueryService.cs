using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Queries.Patient;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.Patient;

namespace DentifyBackend.Odontology.Application.Internal.QueryServices;

public class PatientQueryService(IPatientRepository patientRepository): IPatientQueryService
{
    public async Task<IEnumerable<Patient>> Handle(GetAllPatientsQuery query)
    {
        return await patientRepository.ListAsync();
    }

    public async Task<Patient?> Handle(GetPatientsByIdQuery query)
    {
        return await patientRepository.FindByIdAsync(query.id);
    }
}