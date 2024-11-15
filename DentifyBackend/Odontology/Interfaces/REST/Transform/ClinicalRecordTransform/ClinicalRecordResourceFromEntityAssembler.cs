using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Interfaces.REST.Resources.ClinicalRecord;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.ClinicalRecordTransform;

public class ClinicalRecordResourceFromEntityAssembler
{
    public static ClinicalRecordResource toResourceFromEntity(ClinicalRecord entity)
    {
        return new ClinicalRecordResource(entity.id, entity.medical_history, entity.record_date,
            entity.diagnosis, entity.treatment, entity.user_id);
    }
}