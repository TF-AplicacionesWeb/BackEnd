using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries.ClinicalRecord;

namespace DentifyBackend.Dentify.Domain.Services.ClinicalRecordService;

public interface IClinicalRecordQueryService
{
    Task<IEnumerable<ClinicalRecord>> Handle(GetAllClinicalRecordsQuery query);
    
    Task<ClinicalRecord?> Handle(GetClinicalRecordByIdQuery query);
}