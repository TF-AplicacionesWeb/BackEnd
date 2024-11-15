namespace DentifyBackend.Odontology.Interfaces.REST.Resources.ClinicalRecord;

public record CreateClinicalRecordResource(
    int id,
    string medical_history,
    string record_date,
    string diagnosis,
    string treatment,
    int user_id);