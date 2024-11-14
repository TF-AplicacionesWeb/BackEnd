using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries.ClinicalRecord;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Services.ClinicalRecordService;

namespace DentifyBackend.Dentify.Application.Internal.QueryServices;

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