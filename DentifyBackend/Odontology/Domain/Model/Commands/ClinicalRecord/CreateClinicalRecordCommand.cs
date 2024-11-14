namespace DentifyBackend.Odontology.Domain.Model.Commands.ClinicalRecord;

public record CreateClinicalRecordCommand(
    int id,
    string medical_history,
    string record_date,
    string diagnosis,
    string treatment,
    int user_id);