using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Commands.ClinicalRecord;

namespace DentifyBackend.Dentify.Domain.Services.ClinicalRecordService;

public interface IClinicalRecordCommandService
{
    Task<ClinicalRecord?> Handle(CreateClinicalRecordCommand command);
}