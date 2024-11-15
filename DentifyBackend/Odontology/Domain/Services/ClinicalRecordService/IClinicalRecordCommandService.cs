using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.ClinicalRecord;

namespace DentifyBackend.Odontology.Domain.Services.ClinicalRecordService;

public interface IClinicalRecordCommandService
{
    Task<ClinicalRecord?> Handle(CreateClinicalRecordCommand command);
}