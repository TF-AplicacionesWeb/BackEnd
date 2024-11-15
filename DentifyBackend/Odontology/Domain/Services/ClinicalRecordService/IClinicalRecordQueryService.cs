using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Queries.ClinicalRecord;

namespace DentifyBackend.Odontology.Domain.Services.ClinicalRecordService;

public interface IClinicalRecordQueryService
{
    Task<IEnumerable<ClinicalRecord>> Handle(GetAllClinicalRecordsQuery query);

    Task<ClinicalRecord?> Handle(GetClinicalRecordByIdQuery query);
}