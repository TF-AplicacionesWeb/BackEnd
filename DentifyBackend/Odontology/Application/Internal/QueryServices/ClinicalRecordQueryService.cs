using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Queries.ClinicalRecord;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.ClinicalRecordService;

namespace DentifyBackend.Odontology.Application.Internal.QueryServices;

public class ClinicalRecordQueryService(IClinicalRecordRepository clinicalRecordRepository)
    : IClinicalRecordQueryService
{
    public async Task<IEnumerable<ClinicalRecord>> Handle(GetAllClinicalRecordsQuery query)
    {
        return await clinicalRecordRepository.ListAsync();
    }

    public async Task<ClinicalRecord?> Handle(GetClinicalRecordByIdQuery query)
    {
        return await clinicalRecordRepository.FindByIdAsync(query.id);
    }
}