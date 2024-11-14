using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Interfaces.REST.Resources.ClinicalRecord;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform.ClinicalRecordTransform;

public class ClinicalRecordResourceFromEntityAssembler
{
    public static ClinicalRecordResource toResourceFromEntity(ClinicalRecord entity)
    {
        return new ClinicalRecordResource(entity.id, entity.medical_history, entity.record_date,
            entity.diagnosis, entity.treatment, entity.user_id);
    }
}