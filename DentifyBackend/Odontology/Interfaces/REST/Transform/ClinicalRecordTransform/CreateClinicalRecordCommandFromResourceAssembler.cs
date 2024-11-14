using DentifyBackend.Odontology.Domain.Model.Commands.ClinicalRecord;
using DentifyBackend.Odontology.Interfaces.REST.Resources.ClinicalRecord;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.ClinicalRecordTransform;

public class CreateClinicalRecordCommandFromResourceAssembler
{
    public static CreateClinicalRecordCommand toCommandFromResource(CreateClinicalRecordResource resource)
    {
        return new CreateClinicalRecordCommand(resource.id, resource.medical_history, resource.record_date,
            resource.diagnosis, resource.treatment, resource.user_id);
    }
}