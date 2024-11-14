using DentifyBackend.Dentify.Domain.Model.Commands.ClinicalRecord;
using DentifyBackend.Dentify.Interfaces.REST.Resources.ClinicalRecord;

namespace DentifyBackend.Dentify.Interfaces.REST.Transform.ClinicalRecordTransform;

public class CreateClinicalRecordCommandFromResourceAssembler
{
    public static CreateClinicalRecordCommand toCommandFromResource(CreateClinicalRecordResource resource)
    {
        return new CreateClinicalRecordCommand(resource.id, resource.medical_history, resource.record_date,
            resource.diagnosis, resource.treatment, resource.user_id);
    }
}