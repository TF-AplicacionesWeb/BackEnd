namespace DentifyBackend.Odontology.Interfaces.REST.Resources.Patient;

public record UpdatePatientResource(int clinical_record_id,
    string first_name,
    string last_name,
    string email,
    int age,
    string medical_history,
    string birth_date,
    int appointment_id,
    int user_id);